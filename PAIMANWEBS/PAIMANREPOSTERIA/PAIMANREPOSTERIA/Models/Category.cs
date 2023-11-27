using PAIMANREPOSTERIA.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PAIMANREPOSTERIA.Models
{
    public class Category
    {
        [ScaffoldColumn(false)]
        public int CategoryID { get; set; }

        [Required, StringLength(100), Display(Name = "NOMBRE")]
        public string CategoryName { get; set; }

        [Display(Name = "Descripcion del Producto")]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}