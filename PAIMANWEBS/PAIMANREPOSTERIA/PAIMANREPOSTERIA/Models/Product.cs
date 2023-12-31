﻿using PAIMANREPOSTERIA.Models;
using System.ComponentModel.DataAnnotations;

namespace PAIMANREPOSTERIA.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }

        [Required, StringLength(100), Display(Name = "Nombre")]
        public string ProductName { get; set; }

        [Required, StringLength(10000), Display(Name = "Descriptcion del Producto"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Presio")]
        public double? UnitPrice { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}