using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie_Store.Models;

namespace Movie_Store.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie_Store.Models.Customers> Customers { get; set; }
        public DbSet<Movie_Store.Models.Movies> Movies { get; set; }
        public DbSet<Movie_Store.Models.OrderRows> OrderRows { get; set; }
        public DbSet<Movie_Store.Models.Orders> Orders { get; set; }
    }
}