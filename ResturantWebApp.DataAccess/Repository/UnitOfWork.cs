using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.DataAccess.Repository;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Category = new CategoryRepository(_dbContext);
            FoodType = new FoodTypeRepository(_dbContext);
            MenuItem = new MenuItemRepository(_dbContext);
			ShoppingCart = new ShoppingCartRepository(_dbContext);
		}

        public ICategoryRepository Category { get; private set; }

        public IFoodTypeRepository FoodType { get; private set; }

        public IMenuItemRepository MenuItem { get; private set; }

		public IShoppingCartRepository ShoppingCart { get; private set; }


		public void Dispose()
        {
            _dbContext.Dispose(); 
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}   
