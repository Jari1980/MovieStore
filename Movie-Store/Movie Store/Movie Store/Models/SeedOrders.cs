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
                OrderDate = DateTime.Parse("2002-1-16 12:45"),
                CustomerId = 1
                },

                new Orders
                {
                    OrderDate = DateTime.Parse("1959-4-15 13:00"),
                    CustomerId = 3
                },
                new Orders
                {
                    OrderDate = DateTime.Parse("1997-10-24 18:00"),
                    CustomerId = 2
                },
                new Orders
                {
                    OrderDate = DateTime.Parse("2022-11-02 23:30"),
                    CustomerId =  5
                },
                new Orders
                {
                    OrderDate = DateTime.Parse("2003-6-04 07:07"),
                    CustomerId = 1
                }

                );
                context.SaveChanges();
            }
         }
    }
}
