
using Microsoft.EntityFrameworkCore;
using Swiggy_App.Models;

namespace Swiggy_App.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}
