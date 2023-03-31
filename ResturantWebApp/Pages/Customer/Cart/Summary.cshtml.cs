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
    [BindProperties]
    public class SummaryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderHeader OrderHeader { get; set; }
		public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

		public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();
        }

        public void OnGet()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }

                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                OrderHeader.PickupName = applicationUser.FirstName + " " + applicationUser.LastName;
                OrderHeader.PhoneNumber = applicationUser.PhoneNumber;

            }
        }

		public void OnPost()
		{
			var claimsIdentity = User.Identity as ClaimsIdentity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
					includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");

				foreach (var cartItem in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
				}

                OrderHeader.OrderStatus = StaticDetail.StatusPending;
                OrderHeader.OrderDate = System.DateTime.Now;
                OrderHeader.UserId = claim.Value;
                //The pick update date is not in database I am using the pick up time that was set to DATETIME
                // In the OrderHeader Model
                OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " +
                    OrderHeader.PickUpTime.ToShortTimeString());
                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();

                foreach (var item in ShoppingCartList)
                {
                    OrderDetail orderDetail = new()
                    {
                        MenuItemId = item.MenuItemId,
						OrderHeaderId = OrderHeader.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Count
					};

                    _unitOfWork.OrderDetails.Add(orderDetail);
                    _unitOfWork.Save();
                }

                _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
                _unitOfWork.Save();
            }
		}
	}
}
