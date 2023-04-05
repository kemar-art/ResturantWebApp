using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using ResturantWebApp.Utility;
using Stripe.Checkout;
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
                ShoppingCartList = _unitOfWork.ShoppingCarts.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }

                ApplicationUser applicationUser = _unitOfWork.ApplicationUsers.GetFirstOrDefault(u => u.Id == claim.Value);
                OrderHeader.PickupName = applicationUser.FirstName + " " + applicationUser.LastName;
                OrderHeader.PhoneNumber = applicationUser.PhoneNumber;

            }
        }

		public IActionResult OnPost()
		{
			var claimsIdentity = User.Identity as ClaimsIdentity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCarts.GetAll(filter: u => u.ApplicationUserId == claim.Value,
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
                _unitOfWork.OrderHeaders.Add(OrderHeader);
                _unitOfWork.Save();

                foreach (var item in ShoppingCartList)
                {

					OrderDetail orderDetail = new()
                    {
                        MenuItemId = item.MenuItemId,
						OrderHeaderId = OrderHeader.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Count,
                        //Image = item.MenuItem.Image
					};


					_unitOfWork.OrderDetails.Add(orderDetail);
                }

				//_unitOfWork.ShoppingCarts.RemoveRange(ShoppingCartList);
                _unitOfWork.Save();

               
				var domain = "https://localhost:44305/";
				var options = new SessionCreateOptions
				{
					LineItems = new List<SessionLineItemOptions>(),
                    PaymentMethodTypes = new List<string>
				{
				  "card",
				},
					Mode = "payment",
					SuccessUrl = domain + $"Customer/Cart/OrderSuccess?id={OrderHeader.Id}",
					CancelUrl = domain + "Customer/Cart/Index",
				};

                //Add line items
                foreach (var item in ShoppingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.MenuItem.Price * 100),
                            Currency = "jmd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.MenuItem.Name
                                //Images = item.MenuItem.Image
                            },
                        },

                        Quantity = item.Count
                    };

                    options.LineItems.Add(sessionLineItem);

				}


				var service = new SessionService();
				Session session = service.Create(options);

				Response.Headers.Add("Location", session.Url);
                OrderHeader.SessionId = session.Id;
                _unitOfWork.Save();
				return new StatusCodeResult(303);
			}

            return Page();
		}
	}
}
