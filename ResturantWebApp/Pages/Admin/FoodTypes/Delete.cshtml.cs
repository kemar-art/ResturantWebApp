using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodType FoodType { get; set; }

        public DeleteModel(IUnitOfWork dbUnitOfWork)
        {
            _unitOfWork = dbUnitOfWork;
        }
        public void OnGet(int id)
        {
            FoodType = _unitOfWork.FoodType.GetFirstOrDefault(f => f.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {

            var deleteFoodType = _unitOfWork.FoodType.GetFirstOrDefault(f => f.Id == FoodType.Id);
            if (deleteFoodType != null)
            {
                _unitOfWork.FoodType.Remove(deleteFoodType);
                _unitOfWork.Save();
                TempData["success"] = "Deleted successfully";
                return RedirectToPage("Index");
            }

            return Page();

        }
    }
}
