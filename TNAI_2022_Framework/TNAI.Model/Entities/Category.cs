using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNAI.Model.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
