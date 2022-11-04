namespace Movie_Store.Models.ViewModells
{
    public class CartVM
    {
        public List<int> MovieIds { get; set; }
        public CartVM()
        {
            MovieIds = new List<int>();
        }

    }
}
