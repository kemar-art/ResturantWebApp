using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The Name cannot be the same as Dispaly Order");
            }
            if (ModelState.IsValid)
            {
                await _dbContext.Categories.AddAsync(Category);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Created successfully";
                return RedirectToPage("Index");
            }

            return Page();
            
        }
    }
}
