namespace Movie_Store.Models.ViewModells
{
    public class FrontPageVM
    {
        public List<FiveMostPopularMoviesVM> MostPopular { get; set; }
        public List<FiveNewestVM> FiveNewest { get; set; }
        public List<FiveOldestVM> FiveOldest { get; set; }
        public List<FiveCheapestVM> FiveCheapest { get; set; }
        public List<CustomerExpVM> CustomerExp { get; set; }


        public FrontPageVM()
        {
            MostPopular = new List<FiveMostPopularMoviesVM>();
            FiveNewest = new List<FiveNewestVM>();
            FiveOldest = new List<FiveOldestVM>();
            FiveCheapest = new List<FiveCheapestVM>();
            CustomerExp = new List<CustomerExpVM>();
        }
    }
}
