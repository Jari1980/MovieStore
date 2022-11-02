using Microsoft.EntityFrameworkCore;
using Movie_Store.Data;
using Movie_Store.Data.Migrations;
using System.Net.NetworkInformation;

namespace Movie_Store.Models
{
    public class CustomersSeed
    {


        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any customers
                if (context.Customers.Any())
                {
                    return;   // DB has been seeded
                }

                context.Customers.AddRange(
                    new Customers
                    {
                        FirstName = "Peter",
                        LastName = "Svensson",
                        BillingAdress = "Sveavägen 15",
                        BillingZip = "614 32 Linköping",
                        DeliveryAddress = "Sveavägen 15",
                        DeliveryZip = "614 32 Linköping",
                        EmailAdress = "Psvensson@gmail.com",
                        PhoneNo = "+46702456336"
                    },

                    new Customers
                    {
                        FirstName = "Jan",
                        LastName = "Banan",
                        BillingAdress = "Stpersgatan 24",
                        BillingZip = "614 55 Linköping",
                        DeliveryAddress = "Landsgatan 24",
                        DeliveryZip = "614 55 Linköping",
                        EmailAdress = "Jbanan@gmail.com",
                        PhoneNo = "0702656446"

                    },

                    new Customers
                    {
                        FirstName = "Lotta",
                        LastName = "Andersson",
                        BillingAdress = "Generalsgatan 34",
                        BillingZip = "620 45 Norrköping",
                        DeliveryAddress = "Generalsgatan 34",
                        DeliveryZip = "620 45 Norrköping",
                        EmailAdress = "Lotta@gmail.com",
                        PhoneNo = "0702667498",

                    },

                    new Customers
                    {
                        FirstName = "Lisa",
                        LastName = "Gran",
                        BillingAdress = "Almväggen 54 ",
                        BillingZip = "620 76 Norrköping",
                        DeliveryAddress = "Almväggen 54",
                        DeliveryZip = "620 76 Norrköping",
                        EmailAdress = "LisaGran@gmail.com",
                        PhoneNo = "0706785667",

                    },


                    new Customers
                    {
                        FirstName = "Anders",
                        LastName = "Karlsson",
                        BillingAdress = "Askvägen 4",
                        BillingZip = "614 11 Söderköping",
                        DeliveryAddress = "Askvägen 4",
                        DeliveryZip = "614 11 Söderköping",
                        EmailAdress = "AKarlsson@gmail.com",
                        PhoneNo = "0706754623",

                    }

                    );
                context.SaveChanges();

            }

        }

    }
}