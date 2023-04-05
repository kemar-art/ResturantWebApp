using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using ResturantWebApp.Models.ViewModel;
using ResturantWebApp.Utility;

namespace ResturantWebApp.Pages.Admin.Order
{
    [Authorize(Roles = $"{StaticDetail.ManagerRole}, {StaticDetail.KitchenRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public List<OrderDetailsVM>? OrderDetailsVM { get; set; }

        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            OrderDetailsVM = new();

            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeaders.
                GetAll(
                   u => u.OrderStatus == StaticDetail.StatusSubmitted ||
                   u.OrderStatus == StaticDetail.StatusInProcess
                   ).ToList();

            foreach (OrderHeader item in orderHeaders)
            {
                OrderDetailsVM individual = new OrderDetailsVM()
                {
                    OrderHeader = item,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderHeaderId == item.Id).ToList()
                };

                OrderDetailsVM.Add(individual);
            }
        }

        public IActionResult OnPostOrderInprocess(int orderId)
        {
            _unitOfWork.OrderHeaders.updateStatus(orderId, StaticDetail.StatusInProcess);
            _unitOfWork.Save();

            return RedirectToPage("ManageOrder");

        }

        public IActionResult OnPostOrderReady(int orderId)
        {
            _unitOfWork.OrderHeaders.updateStatus(orderId, StaticDetail.StatusReady);
            _unitOfWork.Save();

            return RedirectToPage("ManageOrder");

        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeaders.updateStatus(orderId, StaticDetail.StatusCancelled);
            _unitOfWork.Save();

            return RedirectToPage("ManageOrder");

        }
    }
}
