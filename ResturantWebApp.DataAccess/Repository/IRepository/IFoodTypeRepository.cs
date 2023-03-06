using ResturantWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.DataAccess.Repository.IRepository
{
    public interface IFoodTypeRepository : IGenericRepository<FoodType>
    {
        void Update(FoodType foodType);
    }
}
