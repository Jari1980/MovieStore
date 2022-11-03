using Movie_Store.Models;

namespace Movie_Store.Services
{
    public interface IMovieService
    {
        Movies AddMovie(Movies addMovie);
        Movies GetMovies(int? Id);
        Movies DeleteMovie(Movies deleteMovies);
        IEnumerable<Movies> GetAllMovies();
    }
}