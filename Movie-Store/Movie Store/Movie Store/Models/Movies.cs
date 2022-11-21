using System.ComponentModel.DataAnnotations;

namespace Movie_Store.Models
{
    public class Movies
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }
        public double Price { get; set; }
        public string MovieImg { get; set; }

    }
}
