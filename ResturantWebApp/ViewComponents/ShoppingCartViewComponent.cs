using Microsoft.AspNetCore.Mvc;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Utility;
using System.Security.Claims;

namespace ResturantWebApp.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;  
            if (claim != null)
            {
                // User is logged in and getting cart value
                if (HttpContext.Session.GetInt32(StaticDetail.SessionCart) != null)
                {
                    return View((HttpContext.Session.GetInt32(StaticDetail.SessionCart)));
                }
                else
                {
                    count = _unitOfWork.ShoppingCarts.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                    HttpContext.Session.SetInt32(StaticDetail.SessionCart, count);
                    return View(count);
                }
            }
            else
            {
                //USer not logged in
                HttpContext.Session.Clear();
                return View(count);
            }
        }
    }
}
