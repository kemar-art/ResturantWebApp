using ResturantWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantWebApp.DataAccess.Repository;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.DataAccess.Data;

namespace ResturantWebApp.DataAccess.Repository
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _dbContext;


        public MenuItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(MenuItem menuItem)
        {
            var updateMenuItem = _dbContext.MenuItems.FirstOrDefault(f =>  f.Id == menuItem.Id);
            updateMenuItem.Name = menuItem.Name;
            updateMenuItem.Description = menuItem.Description;
            updateMenuItem.Price = menuItem.Price;
            updateMenuItem.CategoryTypeId = menuItem.CategoryTypeId;
            updateMenuItem.FoodTypeId = menuItem.FoodTypeId;
            if (menuItem.Image != null)
            {
                updateMenuItem.Image = menuItem.Image;
            }
        }
    }
}
