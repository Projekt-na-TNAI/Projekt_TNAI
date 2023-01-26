using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI.Model.Entities;
using TNAI.Repository.Abstract;

namespace TNAI.Repository.Concrete
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository() : base() { }

        public async Task<Order> GetOrderAsync(int id)
        {
            return await Context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await Context.Orders.ToListAsync();
        }

        public async Task<bool> SaveOrderAsync(Order Order)
        {
            if (Order == null)
                return false;

            Context.Entry(Order).State = Order.Id == default(int) ? EntityState.Added : EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var Order = await GetOrderAsync(id);
            if (Order == null)
                return true;

            Context.Orders.Remove(Order);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
