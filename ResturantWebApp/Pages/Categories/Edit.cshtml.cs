using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.Data;
using ResturantWebApp.Model;

namespace ResturantWebApp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public Category Category { get; set; }

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet(int id)
        {
            Category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The Name cannot be the same as Dispaly Order");
            }
            if (ModelState.IsValid)
            {
                 _dbContext.Categories.Update(Category);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Updataed successfully";
                return RedirectToPage("Index");
            }

            return Page();
            
        }
    }
}
