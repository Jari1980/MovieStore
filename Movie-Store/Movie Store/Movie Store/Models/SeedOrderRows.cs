using Microsoft.EntityFrameworkCore;
using Movie_Store.Data;

namespace Movie_Store.Models
{
    public class SeedOrderRows
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.OrderRows.Any())
                {
                    return;
                }
                context.OrderRows.AddRange(
                    new OrderRows
                    {
                        OrderId = 1,
                        MovieId = 4,
                        Price = 999
                    },
                    new OrderRows
                    {
                        OrderId = 1,
                        MovieId = 2,
                        Price = 92.3
                    },
                    new OrderRows
                    {
                        OrderId = 3,
                        MovieId = 6,
                        Price = 199
                    },
                    new OrderRows
                    {
                       OrderId = 2,
                       MovieId = 7,
                       Price = 149
                    
                    },
                    new OrderRows
                    {
                        OrderId = 4,
                        MovieId = 10,
                        Price = 169
                    }

                    );
                context.SaveChanges();
                
            }






        }







    }
}
