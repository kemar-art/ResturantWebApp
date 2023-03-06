using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public FoodType FoodType { get; set; }

        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(int id)
        {
            FoodType = _dbContext.FoodTypes.FirstOrDefault(f => f.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {

            var deleteFoodType = _dbContext.FoodTypes.Find(FoodType.Id);
            if (deleteFoodType != null)
            {
                _dbContext.Remove(deleteFoodType);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Deleted successfully";
                return RedirectToPage("Index");
            }

            return Page();

        }
    }
}
