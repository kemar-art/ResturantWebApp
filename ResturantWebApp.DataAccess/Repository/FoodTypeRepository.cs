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
    public class FoodTypeRepository : GenericRepository<FoodType>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FoodTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(FoodType foodType)
        {
            var updateFoodType = _dbContext.FoodTypes.FirstOrDefault(f =>  f.Id == foodType.Id);
            updateFoodType.Name = foodType.Name;
        }
    }
}
