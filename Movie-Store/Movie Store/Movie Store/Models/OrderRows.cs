using System.ComponentModel.DataAnnotations;

namespace Movie_Store.Models
{
    public class OrderRows
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public double Price { get; set; }
    }
}
