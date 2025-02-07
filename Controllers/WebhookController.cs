using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swiggy_App.Data;
using Swiggy_App.Models;

namespace Swiggy_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WebhookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveOrder([FromBody] Order order)
        {
            if(order == null)
            {
                return BadRequest("Invalid Recived Data");
            }

            //Save the Order in Database
            await SaveOrderDatabase(order);

            // Log received order (Replace this with actual processing logic)
            Console.WriteLine($"Received Order: {order.FoodName} - {order.Quantity}");

            return Ok(new { message = "Webhook received successfully!" });
        }

        private async Task SaveOrderDatabase(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }


    }
}
