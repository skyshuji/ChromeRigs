using AutoMapper;
using ChromeRigs.MVC.Data;
using ChromeRigs.MVC.Models;
using ChromeRigs.MVC.Models.Homes;
using ChromeRigs.MVC.Models.PCs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ChromeRigs.MVC.Controllers
{
    public class HomeController : Controller
    {
        #region Data & Const

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions
        public async Task<IActionResult> Index()
        {
            var homePageVM = new HomePageViewModel();

            homePageVM.BestSoldPCs = await GetBestSoldPCs();
            homePageVM.OurCustomers = await GetOurCustomers();

            return View(homePageVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region Private Methods

        private async Task<List<PCViewModel>> GetBestSoldPCs()
        {
            var bestSoldPCs = await _context
                                   .PCs
                                   .OrderByDescending(pcs => pcs.Price)
                                   .Take(6)
                                   .ToListAsync();

            var bestSoldPCVMs = _mapper.Map<List<PCViewModel>>(bestSoldPCs);

            return bestSoldPCVMs;
        }

        private async Task<List<string>> GetOurCustomers()
        {
            var customers = await _context
                                   .Customers
                                   .Select(customers => customers.FullName)
                                   .ToListAsync();

            return customers;
        }

        #endregion
    }
}