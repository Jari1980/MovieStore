

using Microsoft.EntityFrameworkCore;
using Movie_Store.Data;

namespace Movie_Store.Models
{
    public class SeedOrders
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Orders.Any())
                {
                    return;
                }

                context.Orders.AddRange(
                new Orders
                {
                OrderDate = DateTime.Parse("2002-1-16"),
                CustomerId = 1
                },

                new Orders
                {
                    OrderDate = DateTime.Parse("1959-4-15"),
                    CustomerId = 3
                },
                new Orders
                {
                    OrderDate = DateTime.Parse("1997-10-24"),
                    CustomerId = 2
                },
                new Orders
                {
                    OrderDate = DateTime.Parse("2022-11-02"),
                    CustomerId =  5
                },
                new Orders
                {
                    OrderDate = DateTime.Parse("2003-6-04"),
                    CustomerId = 1
                }

                );
                context.SaveChanges();
            }
         }
    }
}
