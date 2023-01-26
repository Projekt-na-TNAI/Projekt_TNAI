using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TNAI.Model.Entities;

namespace TNAI.Repository.Abstract
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(int id);
        Task<List<Cart>> GetAllCartsAsync();
        Task<bool> SaveCartAsync(Cart Cart);
        Task<bool> DeleteCartAsync(int id);
    }
}
