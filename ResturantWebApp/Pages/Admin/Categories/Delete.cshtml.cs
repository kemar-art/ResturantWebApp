using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet(int id)
        {
            Category = _dbContext.Categories.FirstOrDefault(c => c.Id == id); ;
        }

        public async Task<IActionResult> OnPost()
        {

                var deleteCategory = _dbContext.Categories.Find(Category.Id);
                if (deleteCategory != null) 
                { 
                    _dbContext.Remove(deleteCategory);
                    await _dbContext.SaveChangesAsync();
                    TempData["success"] = "Deleted successfully";
                    return RedirectToPage("Index");
                }

            return Page();
            
        }
    }
}
