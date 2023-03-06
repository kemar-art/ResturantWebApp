using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public IEnumerable<FoodType> FoodType { get; set; }

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            FoodType = _dbContext.FoodTypes;
        }
    }
}
