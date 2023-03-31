using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MenuItem> MenuItemList { get; set; }
		public IEnumerable<Category> CategoryList { get; set; }

		public void OnGet()
        {
            MenuItemList = _unitOfWork.MenuItems.GetAll(includeProperties: "Category,FoodType");
                                                       // Which category you want to display first
            CategoryList = _unitOfWork.Categories.GetAll(orderBy: o => o.OrderBy(o => o.DisplayOrder));

        }
    }
}
