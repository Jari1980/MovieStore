using Movie_Store.Models;

namespace Movie_Store.Services
{
    public interface IAddMovieService
    {
        Movies AddMovie(Movies addMovie);
        void DeleteMovie();
        IEnumerable<Movies> GetAllMovies();
    }
}