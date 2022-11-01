using System.ComponentModel.DataAnnotations;

namespace Movie_Store.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int CustomerId { get; set; }
        //public virtual Customer Customer { get; set; }
    }
}
