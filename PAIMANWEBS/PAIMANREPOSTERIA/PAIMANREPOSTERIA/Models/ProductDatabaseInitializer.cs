using PAIMANREPOSTERIA.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace PAIMANREPOSTERIA.Models
{
    public class ProductDatabaseInitializer : DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetProducts().ForEach(p => context.Products.Add(p));
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category> {
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "Postres"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Pasteles"
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName = "Galletas"
                },
            };

            return categories;
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product> {
                new Product
                {
                    ProductID = 1,
                    ProductName = "Tiramisú",
                    Description = "Cubierto con crema, chocolate y pastel" +
                                  "La capa superior está espolvoreada con cacao en polvo y tiene una ramita de menta encima",
                    ImagePath="Cake.png",
                    UnitPrice = 16900,
                    CategoryID = 1
               },
                new Product
                {
                    ProductID = 2,
                    ProductName = "Parfait de frutas",
                    Description = "Vasos están llenos de capas de yogur, granola y fruta fresca.\r\nEl fruto se compone de fresas y moras.",
                    ImagePath="Parfait.png",
                    UnitPrice = 8300,
                     CategoryID = 1
               },
                new Product
                {
                    ProductID = 3,
                    ProductName = "Profiteroles",
                    Description = "Profiteroles están rellenos de crema batida y fresas en rodajas con fresas y frutos rojos.",
                    ImagePath="profiteroles.png",
                    UnitPrice = 2000,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 4,
                    ProductName = "Tarta",
                    Description = " La tarta tiene una corteza dorada y está rellena de una capa de crema pastelera amarilla. La tarta está cubierta con una capa de frambuesas frescas",
                    ImagePath="tarta.png",
                    UnitPrice = 40000,
                    CategoryID = 1
                },

                new Product
                {
                    ProductID = 5,
                    ProductName = "Pastel Rosa",
                    Description = "Pastel rosa tiene glaseado rosa con remolinos blancos se puede escojer el sabor",
                    ImagePath="Pastel rosa.png",
                    UnitPrice = 50000,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 6,
                    ProductName = "Pastel de queso",
                    Description = "Pastel de queso cubierto con frutas y chocolate. El pastel de queso es de color amarillo claro con una corteza desmenuzable. El pastel de queso está cubierto con mango, fresas, arándanos y nueces de macadamia cubiertas de chocolate.",
                    ImagePath="Pastel de queso.png",
                    UnitPrice = 50000,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 7,
                    ProductName = "Pastel de chocolate",
                    Description = "Pastel de chocolate con rodajas de naranja y una flor blanca encima. El pastel es redondo y está cubierto con un glaseado de chocolate brillante. La base del pastel es desmenuzable y de color marrón oscuro. En la parte superior del pastel hay dos rodajas de naranja y una flor blanca. Las rodajas de naranja están dispuestas en forma de media luna",
                    ImagePath="Pastel de chocolate.png",
                    UnitPrice = 50000,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 8,
                    ProductName = "Pastel de frutas",
                    Description = "Pastel con frutas y nueces encima redondo con una corteza dorada. El pastel está cubierto con glaseado blanco, almendras en rodajas y un montón de arándanos en el centro",
                    ImagePath="Pastel de frutas.png",
                    UnitPrice = 50000,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 9,
                    ProductName = "Galletas Macarons ",
                    Description = "Macarons rosados con frambuesas y hojas de menta encima. Los macarons son de color rosa claro con un relleno de color rosa oscuro",
                    ImagePath="Galletas Macarons.png",
                    UnitPrice = 2700,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 10,
                    ProductName = "Galletas estrella",
                    Description = "Las galletas son de estrella y tienen un glaseado blanco encima.",
                    ImagePath="Galleta estrella.png",
                    UnitPrice = 3000,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 11,
                    ProductName = "Pretzels",
                    Description = "Los pretzels son de color marrón dorado, tienen sal encimaHorneado y retorcido en forma de lazo.",
                    ImagePath="Pretzels.png",
                    UnitPrice = 8000,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 12,
                    ProductName = "Galletas de jengibre",
                    Description = "Las galletas están decoradas con glaseado, chispas y dulces " +
                                  "Galletas de jengibre en forma de personas",
                    ImagePath="Galletas de jengibre.png",
                    UnitPrice = 5000,
                    CategoryID = 3
                },
            };

            return products;
        }
    }
}