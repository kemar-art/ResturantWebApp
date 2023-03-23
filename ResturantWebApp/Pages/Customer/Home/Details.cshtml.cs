using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ResturantWebApp.Pages.Customer.Home
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MenuItem MenuItem { get; set; }
        [Range(1, 100, ErrorMessage = "Please select a count from 1 to 100")]
        public int Count { get; set; }

        public void OnGet(int id)
        {
            MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(m => m.Id == id, includeProperties: "Category,FoodType");
        }
    }
}
