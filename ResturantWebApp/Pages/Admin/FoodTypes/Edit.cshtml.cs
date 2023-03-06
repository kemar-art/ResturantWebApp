using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public FoodType FoodType { get; set; }

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(int id)
        {
            FoodType = _dbContext.FoodTypes.FirstOrDefault(f => f.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _dbContext.FoodTypes.Update(FoodType);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Updataed successfully";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
