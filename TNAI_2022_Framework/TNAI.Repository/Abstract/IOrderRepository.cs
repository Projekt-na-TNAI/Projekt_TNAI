using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TNAI.Model.Entities;

namespace TNAI.Repository.Abstract
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderAsync(int id);
        Task<List<Order>> GetAllOrdersAsync();
        Task<bool> SaveOrderAsync(Order Order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
