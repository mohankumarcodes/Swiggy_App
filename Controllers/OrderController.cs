using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiggy_App.Data;
using Swiggy_App.Models;
using Swiggy_App.Services;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Swiggy_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //Depedency injection
        private readonly ApplicationDbContext _context;
        //Add Configuration becasue need to use appSettings.json file elements
        private readonly IConfiguration _configuration;
        private readonly EmailServices _emailService;
        private readonly HttpClient _httpClient;

        //one controller hove only one contructor only.
        public OrderController(ApplicationDbContext context,IConfiguration configuration, EmailServices emailServices, HttpClient httpClient)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailServices;
            _httpClient = httpClient;
            
        }
  

        //Get All Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        //Place the Order
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            if (order == null)
                return BadRequest("Invalid order data.");

            bool isWebhookSuccess = await NotifyWebHook(order);

            if (!isWebhookSuccess)
            {
                // If webhook fails, send email notification
                await _emailService.SendEmailNotification("Webhook Failed - Order Created",
                    $"Order {order.Id} could not be sent to webhook. Notifying admin.");
            }

            return CreatedAtAction(nameof(GetOrders), new { orderId = order.Id }, order);

        }

        //Notify WebHook External Services
        private async Task<bool> NotifyWebHook(Order order)
        {
            var webhookUrl = _configuration["WebhookSettings:OrderWebhookUrl"]; // WebHookURL plces in appsettings.json

            try
            {
                if (string.IsNullOrWhiteSpace(webhookUrl))
                    throw new Exception("Webhook URL is empty, skipping webhook request.");

                var jsonContent = JsonSerializer.Serialize(order);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(webhookUrl, content);

                return response.IsSuccessStatusCode; // Return true if webhook succeeds
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Webhook failed: {ex.Message}");
                return false; // Return false if webhook fails
            }

        }

        // DELETE: api/Order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content (successful deletion)
        }

    }
}
