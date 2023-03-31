using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.DataAccess.Repository.IRepository;
using ResturantWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.DataAccess.Repository
{
    internal class OrderDetailPepository : GenericRepository<OrderDetail>, IOrderDetailPepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderDetailPepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(OrderDetail orderDetail)
        {
            _dbContext.OrderDetails.Update(orderDetail);
        }
    }
}
