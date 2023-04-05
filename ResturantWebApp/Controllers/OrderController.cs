using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Utility;

namespace ResturantWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public OrderController(IUnitOfWork dbUnitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = dbUnitOfWork;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(string? status = null)
        {
			var OrderHeaderList = _unitOfWork.OrderHeaders.GetAll(includeProperties: "ApplicationUser");


            if (status == "cancelled")
            {
                OrderHeaderList = OrderHeaderList.Where(u => u.OrderStatus == StaticDetail.StatusCancelled || u.OrderStatus == StaticDetail.StatusRejected);
            }
            else
            {
                if (status == "completed")
                {
                    OrderHeaderList = OrderHeaderList.Where(u => u.OrderStatus == StaticDetail.StatusCompleted);
                }
                else
                {
                    if (status == "ready")
                    {
                        OrderHeaderList = OrderHeaderList.Where(u => u.OrderStatus == StaticDetail.StatusReady);
                    }
                    else
                    {
                        OrderHeaderList = OrderHeaderList.Where(u => u.OrderStatus == StaticDetail.StatusSubmitted || u.OrderStatus == StaticDetail.StatusInProcess);
                    }
                }
            }

            return Json(new { data = OrderHeaderList });
		}
    }
}
