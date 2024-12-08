using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Shopping_Cart_Book.Repositories
{
    public class Cartrepository: ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Cartrepository(ApplicationDbContext db,UserManager<IdentityUser>userManager, IHttpContextAccessor httpContextAccessor)
        {
           _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddItem(int bookId, int qty)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new UnauthorizedAccessException("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                }
                _db.SaveChanges();
                // cart detail section
                var cartItem = _db.CartDetail
                                  .FirstOrDefault(a => a.ShoppingCart_Id == cart.Id && a.BookId == bookId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var book = _db.Books.Find(bookId);
                    cartItem = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCart_Id = cart.Id,
                        Quantity = qty,
                       // UnitPrice = book.Price  // it is a new line after update
                    };
                    _db.CartDetail.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<int> RemoveItem(int bookId)
        {
            // using var transaction = _db.Database.BeginTransaction();
            string userId = GetUserId();

            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged-In");
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    throw new Exception("Cart is Empty");
                }
                _db.SaveChanges();
                // cart detail section
                var cartItem = _db.CartDetail
                                  .FirstOrDefault(a => a.ShoppingCart_Id == cart.Id && a.BookId == bookId);
                if (cartItem is null)
                   throw new Exception("No Item in cart");
                //remove cart item from CartDetails
                else if(cartItem.Quantity==1)
                {
                 _db.CartDetail.Remove(cartItem);
                }
                //otherwise simply decrease one item
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                _db.SaveChanges();
                //transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Invalid userid");
            var shoppingCart = await _db.ShoppingCarts
                                  .Include(a => a.CartDetails)
                                  .ThenInclude(a => a.Book)
                                  .ThenInclude(a => a.Genre)
                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return shoppingCart;
        }

        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId)) // updated line
            {
                userId = GetUserId();
            }
            var data = await (from cart in _db.ShoppingCarts
                              join cartDetail in _db.CartDetail
                              on cart.Id equals cartDetail.ShoppingCart_Id
                           //   where cart.UserId == userId // updated line
                              select new { cartDetail.Id }
                        ).ToListAsync();
            return data.Count;
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
        public async Task< ShoppingCart> GetCart(string userId)
        {
            var cart=_db.ShoppingCarts.FirstOrDefault(x=>x.UserId == userId);
            return cart;
        }

    }
}
