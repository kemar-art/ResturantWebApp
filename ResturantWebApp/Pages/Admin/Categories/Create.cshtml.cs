using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public Category Category { get; set; }

        public CreateModel(IUnitOfWork dbUnitOfWork)
        {
            _unitOfWork = dbUnitOfWork;
        }

        public void OnGet()
        {
        }

        public async  Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The Name cannot be the same as Dispaly Order");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(Category);
                _unitOfWork.Save();
                TempData["success"] = "Created successfully";
                return RedirectToPage("Index");
            }

            return Page();
            
        }
    }
}
