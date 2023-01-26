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
    public class CartRepository: BaseRepository, ICartRepository
    {
        public CartRepository() : base() { }

        public async Task<Cart> GetCartAsync(int id)
        {
            return await Context.Carts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Cart>> GetAllCartsAsync()
        {
            return await Context.Carts.ToListAsync();
        }

        public async Task<bool> SaveCartAsync(Cart cart)
        {
            if (cart == null)
                return false;

            Context.Entry(cart).State = cart.Id == default(int) ? EntityState.Added : EntityState.Modified;

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

        public async Task<bool> DeleteCartAsync(int id)
        {
            var cart = await GetCartAsync(id);
            if (cart == null)
                return true;

            Context.Carts.Remove(cart);

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
