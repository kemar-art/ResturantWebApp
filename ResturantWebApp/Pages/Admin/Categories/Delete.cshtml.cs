using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public Category Category { get; set; }

        public DeleteModel(IUnitOfWork dbUnitOfWork)
        {
            _unitOfWork = dbUnitOfWork;
        }

        public void OnGet(int id)
        {
            Category = _unitOfWork.Categories.GetFirstOrDefault(c => c.Id == id); ;
        }

        public async Task<IActionResult> OnPost()
        {

                var deleteCategory = _unitOfWork.Categories.GetFirstOrDefault(c => c.Id == Category.Id);
                if (deleteCategory != null) 
                {
                    _unitOfWork.Categories.Remove(deleteCategory);
                    _unitOfWork.Save();
                    TempData["success"] = "Deleted successfully";
                    return RedirectToPage("Index");
                }

            return Page();
            
        }
    }
}
