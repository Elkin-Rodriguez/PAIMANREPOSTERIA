<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="PAIMANREPOSTERIA.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div class="container">
            <div class="row justify-content-center align-items-center">
                <div class="col-md-6">
                    <div class="contact-info-box p-4 bg-light text-dark text-center">
                        <h3>Contactos</h3>
                        <address>
                            <strong>PAIMAN REPOSTERIA</strong><br />
                            Carrera 89A No 64c-01, Bogotá<br />
                            <abbr title="Teléfono">Teléfono:</abbr> 3027386762
                        </address>
                        <address>
                            <strong>Correo:</strong> <a href="mailto:Support@example.com">Paimanwebs@gmail.com</a><br />
                        </address>
                    </div>
                </div>
                <div class="col-md-6">
                    <img src="/Catalog/gif/PAIMAN2.gif" class="d-block mx-auto" alt="GIF Description">
                </div>
            </div>
        </div>
    </main>
</asp:Content>
