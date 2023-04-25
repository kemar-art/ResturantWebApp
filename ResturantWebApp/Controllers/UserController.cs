using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Utility;

namespace ResturantWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserController(IUnitOfWork dbUnitOfWork)
        {
            _unitOfWork = dbUnitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
			var user = _unitOfWork.ApplicationUsers.GetAll();

            return Json(new { data = user });
		}

        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var userFromDb = _unitOfWork.ApplicationUsers.GetFirstOrDefault(x => x.Id == id);
            if (userFromDb == null)
            {
                return Json(new { success = false, message = "Error while locking/Unlcoking" });
            }
            // Lock user 
            if (userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
            {
                userFromDb.LockoutEnd = DateTime.Now;
            }
            // unlock user
            else
            {
                userFromDb.LockoutEnd = DateTime.Now.AddYears(50);
            }

            _unitOfWork.Save();

            return Json(new { success = true, message = "Operation Successful." });
        }
    }
}
