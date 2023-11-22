<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="PAIMANREPOSTERIA.Admin.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administrador</h1>
    <hr />

    <!-- Dropdown para seleccionar la acción -->
    <h3>Opciones del Administrador:</h3>
    <asp:DropDownList ID="DropDownManageProducts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownManageProducts_SelectedIndexChanged">
        <asp:ListItem Text="Seleccione una Opcion" Value="" />
        <asp:ListItem Text="Editar Producto" Value="EditProductPanel" />
        <asp:ListItem Text="Agregar Producto" Value="AddProduct" />
        <asp:ListItem Text="Eliminar Producto" Value="RemoveProduct" />
    </asp:DropDownList>

    <!-- Panel para agregar producto -->
    <asp:Panel ID="AddProductPanel" runat="server" Visible="false">
        <h3>Agregar Producto:</h3>
        <table>
            <tr>
                <td><asp:Label ID="LabelAddCategory" runat="server">Categoria:</asp:Label></td>
                <td>
                    <asp:DropDownList ID="DropDownAddCategory" runat="server" 
                        ItemType="PAIMANREPOSTERIA.Models.Category" 
                        SelectMethod="GetCategories" DataTextField="CategoryName" 
                        DataValueField="CategoryID" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="LabelAddName" runat="server">Name:</asp:Label></td>
                <td>
                    <asp:TextBox ID="AddProductName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="* El Nombre Del Producto Es Requerido." ControlToValidate="AddProductName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="LabelAddDescription" runat="server">Descripción:</asp:Label></td>
                <td>
                    <asp:TextBox ID="AddProductDescription" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="* La Descripcion es Requerida" ControlToValidate="AddProductDescription" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="LabelAddPrice" runat="server">Precio:</asp:Label></td>
                <td>
                    <asp:TextBox ID="AddProductPrice" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="* El Precio es Requerido." ControlToValidate="AddProductPrice" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="*Debe ser un precio válido sin $." ControlToValidate="AddProductPrice" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="LabelAddImageFile" runat="server">Archivo de la Imagen:</asp:Label></td>
                <td>
                    <asp:FileUpload ID="ProductImage" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*El Archivo de la Imagen es Requerido" ControlToValidate="ProductImage" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <p></p>
        <p></p>
        <asp:Button ID="AddProductButton" runat="server" Text="Agregar Producto" OnClick="AddProductButton_Click" CausesValidation="true" />
        <asp:Label ID="LabelAddStatus" runat="server" Text=""></asp:Label>
    </asp:Panel>

    <!-- Panel para remover producto -->
    <asp:Panel ID="RemoveProductPanel" runat="server" Visible="false">
        <h3>Eliminar Producto:</h3>
        <table>
            <tr>
                <td><asp:Label ID="LabelRemoveProduct" runat="server">Producto:</asp:Label></td>
                <td>
                    <asp:DropDownList ID="DropDownRemoveProduct" runat="server" ItemType="PAIMANREPOSTERIA.Models.Product" 
                        SelectMethod="GetProducts" AppendDataBoundItems="true" 
                        DataTextField="ProductName" DataValueField="ProductID" >
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <p></p>
        <asp:Button ID="RemoveProductButton" runat="server" Text="Eliminar Producto" OnClick="RemoveProductButton_Click" CausesValidation="false" />
        <asp:Label ID="LabelRemoveStatus" runat="server" Text=""></asp:Label>
    </asp:Panel>


   <asp:Panel ID="EditProductPanel" runat="server" Visible="false">
    <h3>Edita Producto:</h3>
    <!-- Añadir un DropDownList para seleccionar un producto -->
    <asp:DropDownList ID="DropDownSelectProductForEdit" runat="server" 
            ItemType="PAIMANREPOSTERIA.Models.Product" 
              SelectMethod="GetProducts" DataTextField="ProductName" 
                DataValueField="ProductID" AutoPostBack="true" 
            OnSelectedIndexChanged="DropDownSelectProductForEdit_SelectedIndexChanged">
           <asp:ListItem Text="Selecciona un Producto" Value="" />
</asp:DropDownList>


    <!-- Añadir un botón para cargar los detalles del producto seleccionado -->
    
       <table>
        <tr>
            <td><asp:Label ID="LabelEditName" runat="server">Nombre:</asp:Label></td>
            <td>
                <asp:TextBox ID="EditProductName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditName" runat="server" Text="* Nombre del Producto Requerido" ControlToValidate="EditProductName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelEditDescription" runat="server">Descripción:</asp:Label></td>
            <td>
                <asp:TextBox ID="EditProductDescription" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditDescription" runat="server" Text="* Requiere Descripción" ControlToValidate="EditProductDescription" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelEditPrice" runat="server">Precio:</asp:Label></td>
            <td>
                <asp:TextBox ID="EditProductPrice" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditPrice" runat="server" Text="* Requiere Precio." ControlToValidate="EditProductPrice" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorEditPrice" runat="server" Text="*Debe ser un precio válido sin $." ControlToValidate="EditProductPrice" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <!-- Otros campos si los tienes -->
    </table>
    <p></p>
    <asp:Button ID="UpdateProductButton" runat="server" Text="Actualizar el Producto" OnClick="UpdateProductButton_Click" CausesValidation="true" />
    <asp:Label ID="LabelEditStatus" runat="server" Text=""></asp:Label>
</asp:Panel>

<asp:Panel ID="SelectProductPanel" runat="server" Visible="false">
    <h3>Seleccionar Producto:</h3>
    <asp:DropDownList ID="DropDownSelectProduct" runat="server" 
        ItemType="PAIMANREPOSTERIA.Models.Product" 
        SelectMethod="GetProducts" DataTextField="ProductName" 
        DataValueField="ProductID" AppendDataBoundItems="true">
        <asp:ListItem Text="Selecciona un Producto" Value="" />
    </asp:DropDownList>
    <asp:Button ID="SelectProductButton" runat="server" Text="Seleccionar Producto" OnClick="SelectProductButton_Click" />
</asp:Panel>




</asp:Content>
