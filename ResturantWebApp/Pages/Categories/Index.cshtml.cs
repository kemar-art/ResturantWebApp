using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public IEnumerable<Category> Category { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public void OnGet()
        {
            Category = _dbContext.Categories;
        }
    }
}
