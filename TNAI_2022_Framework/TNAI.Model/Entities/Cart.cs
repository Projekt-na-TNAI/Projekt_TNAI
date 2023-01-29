using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNAI.Model.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public virtual ICollection<Product> ProductsList { get; set;}
    }
}
