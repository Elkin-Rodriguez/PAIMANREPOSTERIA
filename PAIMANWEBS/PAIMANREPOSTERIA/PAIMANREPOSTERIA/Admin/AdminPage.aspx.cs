using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAIMANREPOSTERIA.Models;
using PAIMANREPOSTERIA.Logic;
using System.Diagnostics;

using System.IO;

namespace PAIMANREPOSTERIA.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string productAction = Request.QueryString["ProductAction"];
            if (productAction == "add")
            {
                LabelAddStatus.Text = "Product added!";
            }

            if (productAction == "remove")
            {
                LabelRemoveStatus.Text = "Product removed!";
            }

        }

        protected void DropDownManageProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tu lógica cuando se selecciona un elemento en el DropDownList
            Debug.WriteLine("Selected Value: " + DropDownManageProducts.SelectedValue);

            if (DropDownManageProducts.SelectedValue == "AddProduct")
            {
                AddProductPanel.Visible = true;
                RemoveProductPanel.Visible = false;
                EditProductPanel.Visible = false;
                SelectProductPanel.Visible = false; // Asegúrate de agregar esta línea
            }
            else if (DropDownManageProducts.SelectedValue == "RemoveProduct")
            {
                RemoveProductPanel.Visible = true;
                AddProductPanel.Visible = false;
                EditProductPanel.Visible = false;
                SelectProductPanel.Visible = false; // Asegúrate de agregar esta línea
            }
            else if (DropDownManageProducts.SelectedValue == "EditProductPanel")
            {
                EditProductPanel.Visible = true;
                AddProductPanel.Visible = false;
                RemoveProductPanel.Visible = false;
                SelectProductPanel.Visible = false; // Asegúrate de agregar esta línea

                // Cargar la lista de productos en el DropDownList
                DropDownRemoveProduct.DataBind();

                // Obtener el ID del producto seleccionado para editar
                int productIdToEdit = Convert.ToInt32(DropDownRemoveProduct.SelectedValue);

                // Lógica para cargar los detalles del producto en controles de edición
                LoadProductDetailsForEditing(productIdToEdit);
            }
            else if (DropDownManageProducts.SelectedValue == "SelectProductPanel")
            {
                // Asegúrate de agregar la lógica necesaria para mostrar el panel de selección de productos
                SelectProductPanel.Visible = true;
                AddProductPanel.Visible = false;
                RemoveProductPanel.Visible = false;
                EditProductPanel.Visible = false;
            }
            else
            {
                AddProductPanel.Visible = false;
                RemoveProductPanel.Visible = false;
                EditProductPanel.Visible = false;
                SelectProductPanel.Visible = false; // Asegúrate de agregar esta línea
            }
        }



        protected void AddProductButton_Click(object sender, EventArgs e)
        {
            Boolean fileOK = false;
            String path = Server.MapPath("~/Catalog/Images/");
            if (ProductImage.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(ProductImage.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    // Save to Catalog/Catalog folder.
                    string catalogPath = Server.MapPath("~/Catalog/");
                    string subFolderPath = Path.Combine(catalogPath, "Catalog");

                    // Verificar si la subcarpeta existe, y si no, crearla.
                    if (!Directory.Exists(subFolderPath))
                    {
                        Directory.CreateDirectory(subFolderPath);
                    }

                    ProductImage.PostedFile.SaveAs(Path.Combine(subFolderPath, ProductImage.FileName));

                    // Save to Catalog/Thumbs folder.
                    ProductImage.PostedFile.SaveAs(Path.Combine(subFolderPath, "Thumbs", ProductImage.FileName));
                }
                catch (Exception ex)
                {
                    LabelAddStatus.Text = ex.Message;
                }

                // Add product data to DB.
                AddProducts products = new AddProducts();
                bool addSuccess = products.AddProduct(AddProductName.Text, AddProductDescription.Text,
                    AddProductPrice.Text, DropDownAddCategory.SelectedValue, ProductImage.FileName);
                if (addSuccess)
                {
                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?ProductAction=add");
                }
                else
                {
                    LabelAddStatus.Text = "No se puede agregar un nuevo producto a la base de datos.";
                }
            }
            else
            {
                LabelAddStatus.Text = "No se puede aceptar el tipo de archivo.";
            }
        }

        public IQueryable GetCategories()
        {
            var _db = new PAIMANREPOSTERIA.Models.ProductContext();
            IQueryable query = _db.Categories;
            return query;
        }

        public IQueryable GetProducts()
        {
            var _db = new PAIMANREPOSTERIA.Models.ProductContext();
            IQueryable query = _db.Products;
            return query;
        }

        protected void RemoveProductButton_Click(object sender, EventArgs e)
        {
            using (var _db = new PAIMANREPOSTERIA.Models.ProductContext())
            {
                int productId = Convert.ToInt16(DropDownRemoveProduct.SelectedValue);
                var myItem = (from c in _db.Products where c.ProductID == productId select c).FirstOrDefault();
                if (myItem != null)
                {
                    // Rutas de archivos
                    string imagePath = Server.MapPath("~/Catalog/Images/" + myItem.ImagePath);
                    string removedFolderPath = Server.MapPath("~/Removed/");
                    string newFilePath = Path.Combine(removedFolderPath, myItem.ImagePath);

                    // Verificar si la carpeta "Removed" existe, si no, crearla
                    if (!Directory.Exists(removedFolderPath))
                    {
                        Directory.CreateDirectory(removedFolderPath);
                    }

                    // Verificar si el archivo ya existe en la carpeta "Removed"
                    if (!File.Exists(newFilePath))
                    {
                        try
                        {
                            // Mover el archivo a la carpeta "Removed"
                            File.Move(imagePath, newFilePath);
                        }
                        catch (Exception ex)
                        {
                            // Manejar la excepción de manera apropiada
                            Console.WriteLine("Error al mover el archivo: " + ex.ToString());
                        }
                    }

                    // Eliminar el producto de la base de datos
                    _db.Products.Remove(myItem);
                    _db.SaveChanges();

                    // Recargar la página.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?ProductAction=remove");
                }
                else
                {
                    LabelRemoveStatus.Text = "No se pudo encontrar el producto.";
                }
            }
        }

        protected void SelectProductButton_Click(object sender, EventArgs e)
        {
            int productIdToEdit = Convert.ToInt32(DropDownSelectProduct.SelectedValue);

            if (productIdToEdit > 0)
            {
                // Lógica para cargar los detalles del producto en controles de edición
                LoadProductDetailsForEditing(productIdToEdit);
                EditProductPanel.Visible = true;
                AddProductPanel.Visible = false;
                RemoveProductPanel.Visible = false;
                SelectProductPanel.Visible = false;
            }
            else
            {
                // Manejar el caso en el que no se ha seleccionado un producto
                LabelEditStatus.Text = "Por Favor Seleccione Un Producto";
            }
        }

        protected void EditProductButton_Click(object sender, EventArgs e)
        {
             // Cargar la lista de productos en el DropDownList
    DropDownRemoveProduct.DataBind();

    // Obtener el ID del producto seleccionado para editar
    int productIdToEdit = Convert.ToInt32(DropDownRemoveProduct.SelectedValue);

    // Lógica para cargar los detalles del producto en controles de edición
    LoadProductDetailsForEditing(productIdToEdit);
    SelectProductPanel.Visible = true;
    EditProductPanel.Visible = true;
    AddProductPanel.Visible = false;
    RemoveProductPanel.Visible = false;
        }

        private void LoadProductDetailsForEditing(int productId)
        {
            using (var _db = new PAIMANREPOSTERIA.Models.ProductContext())
            {
                // Obtener el producto de la base de datos usando el ID
                var productToEdit = _db.Products.Find(productId);

                // Verificar si el producto existe
                if (productToEdit != null)
                {
                    // Cargar los detalles del producto en los controles de edición
                    EditProductName.Text = productToEdit.ProductName;
                    EditProductDescription.Text = productToEdit.Description;
                    EditProductPrice.Text = productToEdit.UnitPrice.ToString();
                    DropDownRemoveProduct.SelectedValue = productToEdit.ProductID.ToString();
                }
                else
                {
                    // Manejar el caso en el que el producto no se encuentra
                    // Puedes mostrar un mensaje o realizar otras acciones según tus necesidades
                    LabelEditStatus.Text = "Producto no encontrado.";
                }
            }
        }

        protected void UpdateProductButton_Click(object sender, EventArgs e)
        {
            // Obtener el ID del producto que se está editando
            int productIdToEdit = Convert.ToInt32(DropDownRemoveProduct.SelectedValue);

            using (var _db = new PAIMANREPOSTERIA.Models.ProductContext())
            {
                // Obtener el producto de la base de datos usando el ID
                var productToEdit = _db.Products.Find(productIdToEdit);

                // Verificar si el producto existe
                if (productToEdit != null)
                {
                    // Validar y actualizar los detalles del producto con la información de los controles de edición
                    if (ValidateAndUpdateProduct(productToEdit))
                    {
                        // Guardar los cambios en la base de datos
                        _db.SaveChanges();

                        // Mostrar un mensaje de éxito u otras acciones según tus necesidades
                        LabelEditStatus.Text = "Producto actualizado exitosamente.";

                        // Recargar la lista de productos después de la edición
                        // Esto actualiza el DropDownList con los productos actualizados
                        DropDownRemoveProduct.DataBind();
                    }
                }
                else
                {
                    // Manejar el caso en el que el producto no se encuentra
                    // Puedes mostrar un mensaje o realizar otras acciones según tus necesidades
                    LabelEditStatus.Text = "Producto no encontrado.";
                }
            }
        }

        private bool ValidateAndUpdateProduct(Product product)
        {
            // Validar los datos antes de la actualización
            if (IsValidProductData())
            {
                // Actualizar los detalles del producto con la información de los controles de edición
                product.ProductName = EditProductName.Text;
                product.Description = EditProductDescription.Text;

                decimal price;
                if (Decimal.TryParse(EditProductPrice.Text, out price))
                {
                    product.UnitPrice = (double?)price;
                    // Otros campos si los tienes

                    return true; // La validación y actualización fueron exitosas
                }
                else
                {
                    // Manejar el caso en el que el precio no es válido
                    LabelEditStatus.Text = "Formato de precio no válido.";
                    return false;
                }
            }
            else
            {
                // Manejar el caso en el que los datos no son válidos
                LabelEditStatus.Text = "Datos inválidos.";
                return false;
            }
        }

        private bool IsValidProductData()
        {
            // Agregar lógica de validación según tus necesidades
            // Puedes validar que los campos no estén vacíos, que cumplan con ciertos formatos, etc.
            return true; // Cambiar según tus necesidades de validación
        }
        
        protected void DropDownSelectProductForEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedProductId = Convert.ToInt32(DropDownSelectProductForEdit.SelectedValue);

            if (selectedProductId > 0)
            {
                // Cargar los detalles del producto para la edición
                LoadProductDetailsForEditing(selectedProductId);

                // Mostrar el panel de edición
                EditProductPanel.Visible = true;
            }
            else
            {
                // Manejar el caso en que no se ha seleccionado un producto
                LabelEditStatus.Text = "Por favor, selecciona un producto.";
            }
        }



    }
}