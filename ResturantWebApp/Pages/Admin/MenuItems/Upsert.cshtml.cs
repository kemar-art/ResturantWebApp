using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;

namespace ResturantWebApp.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }

        public UpsertModel(IUnitOfWork dbUnitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = dbUnitOfWork;
            MenuItem = new MenuItem();
            _hostEnvironment = hostEnvironment;
        }
        public void OnGet(int? id)
        {
            //This codition statement is for the edit button on the Menu List Page
            if (id != null) 
            { 
                MenuItem = _unitOfWork.MenuItems.GetFirstOrDefault(i => i.Id == id);
            }

            //Populating Category list to create a dropdown menu
            CategoryList = _unitOfWork.Categories.GetAll().Select(c => new SelectListItem() 
            { 
                Text = c.Name,
                Value = c.Id.ToString()
            });

            //Populating Food Type list to create a dropdown menu
            FoodTypeList = _unitOfWork.FoodTypes.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (MenuItem.Id == 0)
            {
                //Create Menu Item
                string newFileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\menuitems");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, newFileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                MenuItem.Image = @"\images\menuitems\" + newFileName + extension;
                _unitOfWork.MenuItems.Add(MenuItem);
                _unitOfWork.Save();
            }
            else
            {
                //Edit Menu Item
                var menuItem = _unitOfWork.MenuItems.GetFirstOrDefault(i => i.Id == MenuItem.Id);
                if (files.Count > 0)
                {
                    string newFileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuitems");
                    var extension = Path.GetExtension(files[0].FileName);

                    //Deleting old image before uploading new image
                    var oldImage = Path.Combine(webRootPath, menuItem.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }

                    //Uploading the new Image
                    using (var fileStream = new FileStream(Path.Combine(uploads, newFileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    MenuItem.Image = @"\images\menuitems\" + newFileName + extension;
                }
                else
                {
                    // Even though we are checking check if the image file is empty when updating 
                    // I wrote this just to be on the safe side.
                    MenuItem.Image = menuItem.Image;
                }

                _unitOfWork.MenuItems.Update(MenuItem);
                _unitOfWork.Save();
            }

            return RedirectToPage("./Index");
        }

    }
}
