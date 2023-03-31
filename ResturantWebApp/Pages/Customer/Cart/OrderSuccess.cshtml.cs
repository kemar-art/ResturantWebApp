using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using ResturantWebApp.Utility;
using Stripe.Checkout;

namespace ResturantWebApp.Pages.Customer.Cart
{
    public class OrderSuccessModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public int OrderId { get; set; }

        public OrderSuccessModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeaders.GetFirstOrDefault(x => x.Id == id);
            if (orderHeader.SessionId != null)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    orderHeader.OrderStatus = StaticDetail.StatusSubmitted;
                    _unitOfWork.Save();
                }
            }

            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCarts
                .GetAll(u => u.ApplicationUserId == orderHeader.UserId).ToList();
            _unitOfWork.ShoppingCarts.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            OrderId = id;
        }
    }
}
