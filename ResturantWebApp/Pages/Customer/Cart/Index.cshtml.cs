using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using ResturantWebApp.Utility;
using System.Security.Claims;

namespace ResturantWebApp.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public double CartTotal { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public void OnGet()
        {
			var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCarts.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");

                foreach (var cartItem in ShoppingCartList)
                {
                    CartTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }
            }
        }

        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCarts.GetFirstOrDefault(u  => u.Id == cartId);
            _unitOfWork.ShoppingCarts.IncrementCount(cart, 1);
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCarts.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count == 1)
            {
                var count = _unitOfWork.ShoppingCarts
               .GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;

                _unitOfWork.ShoppingCarts.Remove(cart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);
            }
            else
            {
                _unitOfWork.ShoppingCarts.DecrementCount(cart, 1);
            }
            
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCarts.GetFirstOrDefault(u => u.Id == cartId);

            var count = _unitOfWork.ShoppingCarts
                .GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;

            _unitOfWork.ShoppingCarts.Remove(cart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);
            return RedirectToPage("/Customer/Cart/Index");
        }

    }
}
