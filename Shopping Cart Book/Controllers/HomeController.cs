using Microsoft.AspNetCore.Mvc;
using Shopping_Cart_Book.Models;
using Shopping_Cart_Book.Models.DTOs;
using System.Diagnostics;

namespace Shopping_Cart_Book.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomerepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomerepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }

        public async Task<IActionResult> Index(string sTerm = "", int genreId = 0)
        {
            var books = await _homeRepository.GetBooks(sTerm, genreId);
            var genres = await _homeRepository.Genres();

            var model = new BookDisplayModel
            {
                Books = books,
                Genres = genres,
                GenreId = genreId, 
                STerm = sTerm
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
