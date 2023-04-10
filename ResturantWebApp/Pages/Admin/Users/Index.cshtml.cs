using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        /* private readonly IUnitOfWork _unitOfWork;

       public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }*/
        //public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public void OnGet()
        {
            //ApplicationUsers = _unitOfWork.ApplicationUsers.GetAll();
        }
    }
}
