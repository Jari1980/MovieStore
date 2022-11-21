using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movie_Store.Data;
using System;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace Movie_Store.Models
{
    public class SeedMovieData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            
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
                        Price = 199.0,
                        MovieImg = "https://assets.benchmarkrings.com/media/6337520_ammarastone_minibanner_original.jpg?1638217968"

                    },
                    new Movies
                    {
                        Title = "Jaws",
                        Director = "Ebba Grön",
                        ReleaseYear = 1970,
                        Price = 92.3,
                        MovieImg = "https://warm1069.com/wp-content/uploads/2021/02/bigstock-Baby-Shark-Png-Baby-Shark-Boy-332245105-scaled.jpg"

                    },
                    new Movies
                    {
                        Title = "Kaffe på Morgon",
                        Director = "Vidomar",
                        ReleaseYear = 2000,
                        Price = 10,
                        MovieImg = "https://i0.wp.com/techwek.com/wp-content/uploads/2022/07/Good-Morning-With-Coffee-Images-9.jpg?fit=600%2C800&ssl=1"
                    },
                    new Movies
                    {
                        Title = "Broccoli for life",
                        Director = "ForEver",
                        ReleaseYear = 2022,
                        Price = 999,
                        MovieImg = "https://pbs.twimg.com/media/EO5cKYQX0AAN2PB.jpg:large"
                    },
                    new Movies
                    {
                        Title = "Det våras i Rymden",
                        Director = "Lexi Con",
                        ReleaseYear = 2020,
                        Price = 129,
                        MovieImg = "https://image.tmdb.org/t/p/w500/kGO9s56ffMflNMh18v2DRYlCCpi.jpg"
                    },
                    new Movies
                    {
                        Title = "Star Wars",
                        Director = "George Lucas",
                        ReleaseYear = 1980,
                        Price = 199,
                        MovieImg = "https://cdn.vox-cdn.com/thumbor/LxoKTqMgKkmM6BehGrTU3F33dMY=/1400x1400/filters:format(jpeg)/cdn.vox-cdn.com/uploads/chorus_asset/file/24155867/SWG01_1920x1080_nologo.jpg"

                    },
                    new Movies
                    {
                        Title = "Princes Monoke",
                        Director = "Roger Pontare",
                        ReleaseYear = 2002,
                        Price = 149,
                        MovieImg = "https://upload.wikimedia.org/wikipedia/en/8/8c/Princess_Mononoke_Japanese_poster.png"
                    },
                    new Movies
                    {
                        Title = "Den långa färden",
                        Director = "Tarantino",
                        ReleaseYear = 1975,
                        Price = 129.6,
                        MovieImg = "https://m.media-amazon.com/images/M/MV5BMTkzYjliODctMjhlMi00YTBkLWFmN2QtMTE4ODU2NGU0OTFiXkEyXkFqcGdeQXVyNTE1NjY5Mg@@._V1_.jpg"
                    },
                    new Movies
                    {
                        Title = "RocknRollHighSchool",
                        Director = "Jack Black",
                        ReleaseYear = 2005,
                        Price = 149,
                        MovieImg = "https://upload.wikimedia.org/wikipedia/en/c/c8/Rock_%27n%27_Roll_High_SchoolPoster.jpg"
                    },
                    new Movies
                    {
                        Title = "Ninja Turtles",
                        Director = "Dr Alban",
                        ReleaseYear = 1990,
                        Price = 169,
                        MovieImg = "https://storage.googleapis.com/pod_public/750/120836.jpg"
                    },
                    new Movies
                    {
                        Title = "Life Of Brian",
                        Director = "Monty Pluto",
                        ReleaseYear = 1970,
                        Price = 129.9,
                        MovieImg = "https://upload.wikimedia.org/wikipedia/en/1/18/Lifeofbrianfilmposter.jpg"
                    },
                    new Movies
                    {
                        Title = "Dum o dummare",
                        Director = "Ace Ventura",
                        ReleaseYear = 1995,
                        Price = 159,
                        MovieImg = "https://dez1v4fbcawql.cloudfront.net/product/1650338/8316894/5c4a1d927166e.jpg"
                    },
                    new Movies
                    {
                        Title = "Wheel of time",
                        Director = "Robert Jordan",
                        ReleaseYear = 2021,
                        Price = 499,
                        MovieImg = "https://m.media-amazon.com/images/M/MV5BYzA2Nzk5M2EtNWY4Yi00ZDY4LThkZTgtYjhhNWEyMGY0MjFjXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_.jpg"
                    },
                    new Movies
                    {
                        Title = "Kung fu legenden",
                        Director = "Kwai chang coon",
                        ReleaseYear = 1975,
                        Price = 599,
                        MovieImg = "https://m.media-amazon.com/images/M/MV5BN2NhNTU3NzYtOGM5ZC00NDQwLWJmMGMtYjhjM2UzOWY5ODJmXkEyXkFqcGdeQXVyMzU0NzkwMDg@._V1_.jpg"
                    },
                    new Movies
                    {
                        Title = "Vänner",
                        Director = "Chandler Bing",
                        ReleaseYear = 2000,
                        Price = 299,
                        MovieImg = "https://hbomax-images.warnermediacdn.com/images/GXdbR_gOXWJuAuwEAACVH/tileburnedin?size=1280x720&partner=hbomaxcom&v=6a409f09891f75549fdb8d07dc969b63&host=art-gallery.api.hbo.com&language=sv-se&w=1280"
                    },
                    new Movies
                    {
                        Title = "The Office",
                        Director = "Pam",
                        ReleaseYear = 2010,
                        Price = 299,
                        MovieImg = "https://roost.nbcuni.com/bin/viewasset.html/content/dam/Peacock/Landing-Pages/library/theoffice/mainpage/office-vertical-art.jpg/_jcr_content/renditions/original.JPEG"
                    },
                    new Movies
                    {
                        Title = "Seinfeld",
                        Director = "Jerry Seinfeld",
                        ReleaseYear = 1985,
                        Price = 249,
                        MovieImg = "https://m.media-amazon.com/images/M/MV5BZjZjMzQ2ZmUtZWEyZC00NWJiLWFjM2UtMzhmYzZmZDcxMzllXkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_FMjpg_UX1000_.jpg"
                    },
                    new Movies
                    {
                        Title = "Coding C#",
                        Director = "Bill Microsoft",
                        ReleaseYear = 1999,
                        Price = 599,
                        MovieImg = "https://i.stack.imgur.com/3E6sq.jpg"
                    },
                    new Movies
                    {
                        Title = "Game of Thrones",
                        Director = "Jon Snow",
                        ReleaseYear = 2012,
                        Price = 599,
                        MovieImg = "https://noguiltfangirl.com/wp-content/uploads/2019/05/game-of-thrones-e1556752739961.jpeg"
                    },
                    new Movies
                    {
                        Title = "Elite",
                        Director = "WoW Jocke",
                        ReleaseYear = 1337,
                        Price = 13.37,
                        MovieImg = "https://pbs.twimg.com/media/CFyt7YmWYAEM_gn.jpg"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}