<%@ Page Title="Game Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="UIP_Webstore.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:FormView ID="productDetail" runat="server" ItemType="UIP_Webstore.Models.Product" SelectMethod="GetProduct" RenderOuterTable="false">
        <ItemTemplate>
            <div>
                <h1><%#:Item.ProductName %></h1>
            </div>
            <br />
            <table>
                <tr>
                    <td>
                        <img src="/Images/<%#:Item.ProductID %>.jpg" style="border: solid; height: 300px" alt="<%#:Item.ProductName %>" />
                    </td>
                    <td>&nbsp;</td>
                    <td style="vertical-align: top; text-align: left;">
                        <b>Release Date:</b><br />
                        <%#:Item.ReleaseDate %>
                        <br />
                        <span><b>Price:</b>&nbsp;<%#: String.Format("{0:c}", Item.Price) %></span>
                        <br />
                        <span><b>Product Number:</b>&nbsp;<%#:Item.ProductID %></span>
                        <br />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>

    <asp:FormView ID="editDetails" runat="server" ItemType="UIP_Webstore.Models.Product" SelectMethod="GetProduct" RenderOuterTable="false">
        <ItemTemplate>
            <div>
                <h1><span>Product Name: </span>
                    <asp:TextBox ID="prodName" runat="server" text="<%#:Item.ProductName %>" /></h1>
            </div>
            <br />
            <table>
                <tr>
                    <td>
                        <img src="/Images/<%#:Item.ProductID %>.jpg" style="border: solid; height: 300px" alt="<%#:Item.ProductName %>" />
                    </td>
                    <td>&nbsp;</td>
                    <td style="vertical-align: top; text-align: left;">
                        <b>Release Date:</b><br />
                        <asp:TextBox ID="desc" runat="server" text="<%#:Item.ReleaseDate %>" />
                        <br />
                        <span><b>Price:</b>&nbsp;<asp:TextBox ID="price" runat="server" text="<%#:Item.Price %>" /></span>
                        <br />
                        <span><b>Product Number:</b>&nbsp;<asp:TextBox ID="prodId" runat="server" text="<%#:Item.ProductID %>" /></span>
                        <br />
                    </td>
                    <td>
                        <asp:Button ID="UpdateBtn" runat="server" Text="Update" OnClick="UpdateBtn_Click" />
                    </td>
                    <td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>

</asp:Content>
