using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public FoodType FoodType { get; set; }

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _dbContext.FoodTypes.AddAsync(FoodType);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Create successfully";
                return RedirectToPage("Index");
            }

            return Page();
        }

    }
}
