using ChromeRigs.MVC.Models.PCs;

namespace ChromeRigs.MVC.Models.Homes
{
    public class HomePageViewModel
    {
        public List<string> OurCustomers { get; set; }
        public List<PCViewModel> BestSoldPCs { get; set; } = [];
    }
}
