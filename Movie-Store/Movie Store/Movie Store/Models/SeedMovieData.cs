using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movie_Store.Data;
using System;
using System.Linq;

namespace Movie_Store.Models
{
    public class SeedMovieData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            //DefaultConnection
            {
                if (context.Movies.Any())
                {
                    return;
                }
                context.Movies.AddRange(
                    new Movies
                    {
                        Title = "Lord Of the Great rings",
                        Director = "Peter Jackson",
                        ReleaseYear = 2001,
                        Price = 199.0
                    },
                    new Movies
                    {
                        Title = "Jaws",
                        Director = "Ebba Grön",
                        ReleaseYear = 1970,
                        Price = 92.3
                    },
                    new Movies
                    {
                        Title = "Kaffe på Morgon",
                        Director = "Vidomar",
                        ReleaseYear = 2000,
                        Price = 10
                    },
                    new Movies
                    {
                        Title = "Broccoli for life",
                        Director = "ForEver",
                        ReleaseYear = 2022,
                        Price = 999
                    },
                    new Movies
                    {
                        Title = "Det våras i Rymden",
                        Director = "Lexi Con",
                        ReleaseYear = 2020,
                        Price = 129
                    },
                    new Movies
                    {
                        Title = "Star Wars",
                        Director = "George Lucas",
                        ReleaseYear = 1980,
                        Price = 199
                    },
                    new Movies
                    {
                        Title = "Princes Monoke",
                        Director = "Roger Pontare",
                        ReleaseYear = 2002,
                        Price = 149
                    },
                    new Movies
                    {
                        Title = "Den långa färden",
                        Director = "Tarantino",
                        ReleaseYear = 1975,
                        Price = 129.6
                    },
                    new Movies
                    {
                        Title = "RocknRollHighSchool",
                        Director = "Jack Black",
                        ReleaseYear = 2005,
                        Price = 149
                    },
                    new Movies
                    {
                        Title = "Ninja Turtles",
                        Director = "Dr Alban",
                        ReleaseYear = 1990,
                        Price = 169
                    },
                    new Movies
                    {
                        Title = "Life Of Brian",
                        Director = "Monty Pluto",
                        ReleaseYear = 1970,
                        Price = 129.9
                    },
                    new Movies
                    {
                        Title = "Dum o dummare",
                        Director = "Ace Ventura",
                        ReleaseYear = 1995,
                        Price = 159
                    },
                    new Movies
                    {
                        Title = "Wheel of time",
                        Director = "Robert Jordan",
                        ReleaseYear = 2021,
                        Price = 499
                    },
                    new Movies
                    {
                        Title = "Kung fu legenden",
                        Director = "Kwai chang coon",
                        ReleaseYear = 1975,
                        Price = 599
                    },
                    new Movies
                    {
                        Title = "Vänner",
                        Director = "Chandler Bing",
                        ReleaseYear = 2000,
                        Price = 299
                    },
                    new Movies
                    {
                        Title = "The Office",
                        Director = "Pam",
                        ReleaseYear = 2010,
                        Price = 299
                    },
                    new Movies
                    {
                        Title = "Seinfeld",
                        Director = "Jerry Seinfeld",
                        ReleaseYear = 1985,
                        Price = 249
                    },
                    new Movies
                    {
                        Title = "Coding C#",
                        Director = "Bill Microsoft",
                        ReleaseYear = 1999,
                        Price = 599
                    },
                    new Movies
                    {
                        Title = "Game of Thrones",
                        Director = "Jon Snow",
                        ReleaseYear = 2012,
                        Price = 599
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}