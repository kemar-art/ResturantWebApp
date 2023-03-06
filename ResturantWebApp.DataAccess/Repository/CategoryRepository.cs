using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.DataAccess.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Category category)
        {
            var updateCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            updateCategory.Name = category.Name;
            updateCategory.DisplayOrder = category.DisplayOrder;

        }
    }
}
