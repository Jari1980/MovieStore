using MessagePack;

namespace Movie_Store.Models.ViewModells
{
    public class ReceiptVM
    {

        public int Id { get; set; }
        public double Price { get; set; }

        
        public string Title { get; set; }

    }
}
