<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
         CodeBehind="ProductDetails.aspx.cs" Inherits="PAIMANREPOSTERIA.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="productDetail" runat="server" ItemType="PAIMANREPOSTERIA.Models.Product" SelectMethod="GetProduct" RenderOuterTable="false" OnPageIndexChanging="productDetail_PageIndexChanging">
        <ItemTemplate>
            <div class="product-container">
                <h1><%#: Item.ProductName %></h1>
                <br />
                <div class="product-details">
                    <div class="product-image">
                        <img src="/Catalog/Catalog/<%#: Item.ImagePath %>" style="border: solid; height: 400px" alt="<%#: Item.ProductName %>" />
                    </div>
                    <div class="product-info">
                        <b>Descripcion:</b><br /><%#: Item.Description %>
                        <br />
                        <span><b>Precio:</b>&nbsp;<%#: String.Format("{0:c}", Item.UnitPrice) %></span>
                        <br />
                        <span><b>Numero de Producto:</b>&nbsp;<%#: Item.ProductID %></span>
                        <br />
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
