using ResturantWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IGenericRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
    }
}
