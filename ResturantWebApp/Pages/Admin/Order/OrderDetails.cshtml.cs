using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using ResturantWebApp.Models.ViewModel;

namespace ResturantWebApp.Pages.Admin.Order
{
	public class OrderDetailsModel : PageModel
    {
		private readonly IUnitOfWork _unitOfWork;
		
		public OrderDetailsVM OrderDetailsVM { get; set; }

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
    }
}
