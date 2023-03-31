using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ResturantWebApp.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }


        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            

            ShoppingCart = new()
            {
                ApplicationUserId = claim.Value,
                MenuItem = _unitOfWork.MenuItems.GetFirstOrDefault(m => m.Id == id, includeProperties: "Category,FoodType"),
                MenuItemId = id
			};
		}

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCarts.GetFirstOrDefault(
                filter: s => s.ApplicationUserId == ShoppingCart.ApplicationUserId &&
                        s.MenuItemId == ShoppingCart.MenuItemId
                 );

                if (shoppingCartFromDb == null)
                {
                     _unitOfWork.ShoppingCarts.Add(ShoppingCart);
                     _unitOfWork.Save();
                }
                else
                {
                    _unitOfWork.ShoppingCarts.IncrementCount(shoppingCartFromDb, ShoppingCart.Count);
                }


                return RedirectToPage("Index");
            }
            
            return Page();
        }
    }
}
