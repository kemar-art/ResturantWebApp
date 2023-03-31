using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.DataAccess.Repository;
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
			Categories = new CategoryRepository(_dbContext);
            FoodTypes = new FoodTypeRepository(_dbContext);
            MenuItems = new MenuItemRepository(_dbContext);
			ShoppingCarts = new ShoppingCartRepository(_dbContext);
            OrderDetails = new OrderDetailPepository(_dbContext);
            OrderHeaders = new OrderHeaderRepository(_dbContext);
            ApplicationUsers = new ApplicationUserRepository(_dbContext);
        }

        public ICategoryRepository Categories { get; private set; }

        public IFoodTypeRepository FoodTypes { get; private set; }

        public IMenuItemRepository MenuItems { get; private set; }

		public IShoppingCartRepository ShoppingCarts { get; private set; }

        public IOrderDetailPepository OrderDetails { get; private set; }

        public IOrderHeaderRepository OrderHeaders { get; private set; }

        public IApplicationUserRepository ApplicationUsers { get; private set; }

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
