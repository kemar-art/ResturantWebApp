using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<FoodType> FoodType { get; set; }

        public IndexModel(IUnitOfWork dbUnitOfWork)
        {
            _unitOfWork = dbUnitOfWork;
        }
        public void OnGet()
        {
            FoodType = _unitOfWork.FoodType.GetAll();
        }
    }
}
