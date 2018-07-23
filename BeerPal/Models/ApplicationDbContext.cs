using System.Data.Entity;
using BeerPal.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeerPal.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Entities.Subscription> Subscriptions { get; set; }
        public DbSet<WebhookEvent> WebhookEvents { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
