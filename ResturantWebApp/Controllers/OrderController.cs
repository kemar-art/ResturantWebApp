using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResturantWebApp.DataAccess.Repository.IRepository;

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
        public IActionResult Get()
        {
            var orderHeader = _unitOfWork.OrderHeaders.GetAll(includeProperties: "ApplicationUser");
            return Json(new { data = orderHeader });
        }
    }
}
