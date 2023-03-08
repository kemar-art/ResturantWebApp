using Microsoft.AspNetCore.Mvc;
using ResturantWebApp.DataAccess.Repository.IRepository;

namespace ResturantWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MenuItemController(IUnitOfWork dbUnitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = dbUnitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var menuItemslist = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,FoodType");
            return Json(new {data = menuItemslist});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Deleting old image before uploading new image
            var menuItem = _unitOfWork.MenuItem.GetFirstOrDefault(i => i.Id == id);
            var oldImage = Path.Combine(_hostEnvironment.WebRootPath, menuItem.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImage))
            {
                System.IO.File.Delete(oldImage);
            } 
            _unitOfWork.MenuItem.Remove(menuItem);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful." });
        }
    }
}
