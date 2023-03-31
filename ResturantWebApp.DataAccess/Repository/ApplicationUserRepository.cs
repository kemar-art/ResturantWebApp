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
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
