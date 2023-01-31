using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TNAI_2022_Framework.Models.InputModels
{
    public class ProductInputModel
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }
    }
}