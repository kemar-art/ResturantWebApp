using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using ResturantWebApp.Models.ViewModel;
using ResturantWebApp.Utility;
using Stripe;

namespace ResturantWebApp.Pages.Admin.Order
{
	public class OrderDetailsModel : PageModel
    {
		private readonly IUnitOfWork _unitOfWork;
		
		public OrderDetailsVM? OrderDetailsVM { get; set; }

		public OrderDetailsModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void OnGet(int id)
		{
			OrderDetailsVM = new()
			{
				OrderHeader = _unitOfWork.OrderHeaders.GetFirstOrDefault(x => x.Id == id, includeProperties: "ApplicationUser"),
				OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderHeaderId == id).ToList()
			};
		}

        public IActionResult OnPostOrderCompleted(int orderId)
        {
            _unitOfWork.OrderHeaders.updateStatus(orderId, StaticDetail.StatusCompleted);
            _unitOfWork.Save();
            return RedirectToPage("OrderList");

        }

        public IActionResult OnPostOrderRefund(int orderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeaders.GetFirstOrDefault(i => i.Id == orderId);
            var sendRefund = new RefundCreateOptions
            {
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = orderHeader.PaymentIntentId
            };

            var service = new RefundService();
            Refund refund = service.Create(sendRefund);

            _unitOfWork.OrderHeaders.updateStatus(orderId, StaticDetail.StatusRefunded);
            _unitOfWork.Save();
            return RedirectToPage("OrderList");

        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeaders.updateStatus(orderId, StaticDetail.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("OrderList");

        }
    }
}
