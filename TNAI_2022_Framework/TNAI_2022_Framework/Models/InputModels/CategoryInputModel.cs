using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TNAI_2022_Framework.Models.InputModels
{
    public class CategoryInputModel
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}