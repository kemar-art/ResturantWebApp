using ResturantWebApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IFoodTypeRepository FoodTypes { get; }
        IMenuItemRepository MenuItems { get; }
		IShoppingCartRepository ShoppingCarts { get;}
        IOrderDetailPepository OrderDetails { get; }
        IOrderHeaderRepository OrderHeaders { get; }
        IApplicationUserRepository ApplicationUsers { get; }
		void Save();
    }
}