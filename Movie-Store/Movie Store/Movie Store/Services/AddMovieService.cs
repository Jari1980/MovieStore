using Movie_Store.Models;
using Movie_Store.Data;


namespace Movie_Store.Services
{
    public class AddMovieService : IAddMovieService
    {
        private readonly ApplicationDbContext _movie;

        public AddMovieService(ApplicationDbContext movie)
        {
            _movie = movie;
        }
        public Movies AddMovie(Movies addMovie)
        {
            _movie.Movies.Add(addMovie);
            _movie.SaveChanges();
            return (addMovie);


        }

        public void DeleteMovie()
        {

        }

        public IEnumerable<Movies> GetAllMovies()
        {
            var movie = _movie.Movies;
            return movie;
        }

    }
}
