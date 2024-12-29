using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping_Cart_Book.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly ILogger<CartController> _logger;


        public CartController(ICartRepository cartRepo, ILogger<CartController> logger)
        {
            _cartRepo = cartRepo;
            _logger = logger;

        }
        public async Task<IActionResult> AddItem(int bookId, int qty = 1, int redirect = 0)
        {
            var cartCount = await  _cartRepo.AddItem(bookId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await _cartRepo.RemoveItem(bookId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }
        [HttpGet]
        public async Task< IActionResult> GetTotalItemCart()
        {
            try
            {
                // Get cart item count from the repository
                int cartItem = await _cartRepo.GetCartItemCount();

                // Return the count as JSON response
                return Ok(cartItem); // Send cartItem as a response (HTTP 200 OK)
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine($"Error in GetTotalItemCart: {ex.Message}");

                // Return HTTP 500 Internal Server Error with a message
                return StatusCode(500, "An error occurred while retrieving cart item count.");
            }
        }

        public async Task<IActionResult> Checkout()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool isCheckout = await _cartRepo.DoCheckout(model);
            if (!isCheckout)
                return RedirectToAction(nameof(OrderFailure));
            return RedirectToAction(nameof(OrderSuccess));
        }
        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }

    }
}
