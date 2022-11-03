using Movie_Store.Models;
using Movie_Store.Data;


namespace Movie_Store.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _movie;

        public MovieService(ApplicationDbContext movie)
        {
            _movie = movie;
        }
        public Movies AddMovie(Movies addMovie)
        {
            _movie.Movies.Add(addMovie);
            _movie.SaveChanges();
            return (addMovie);


        }

        public Movies GetMovies(int? Id)
        {
            var movieId = _movie.Movies.Find(Id);

            return (movieId);
        }

        public Movies DeleteMovie(Movies deleteMovie)
        {
            _movie.Movies.Remove(deleteMovie);
            _movie.SaveChanges();
            return (deleteMovie);

        }

        public IEnumerable<Movies> GetAllMovies()
        {
            var movie = _movie.Movies;
            return movie;
        }

    }
}
