<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="UIP_Webstore.ManageProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
       <div>
           <hgroup>
               <h2><%: Page.Title %></h2>
           </hgroup>
    <asp:ListView ID="productList" runat="server" DataKeyNames="ProductID" GroupItemCount="4" ItemType="UIP_Webstore.Models.Product" SelectMethod="GetProducts">
               <EmptyDataTemplate>
                   <table >
                       <tr>
                           <td>No data was returned.</td>
                       </tr>
                   </table>
               </EmptyDataTemplate>
               <EmptyItemTemplate>
                   <td/>
               </EmptyItemTemplate>
               <GroupTemplate>
                   <tr id="itemPlaceholderContainer" runat="server">
                       <td id="itemPlaceholder" runat="server"></td>
                   </tr>
               </GroupTemplate>
               <ItemTemplate>
                   <td runat="server">
                       <table>
                           <tr>
                               <td>
                                   <a href="ProductDetails.aspx?productID=<%#:Item.ProductID%>">
                                       <img src="/Images/<%#:Item.ProductID%>.jpg"
                                           width="300" height="170" style="border: none" /></a>
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   <a href="#">
                                       <span>
                                           <%#:Item.ProductName%>
                                       </span>
                                   </a>
                                   <br />
                                   <span>
                                       <b>Price: </b><%#:String.Format("{0:c}", Item.Price)%>
                                   </span>
                                   <br />
                                   <span>
                                       <a href="ProductDetails.aspx?productID=<%#:Item.ProductID%>">
                                       <span>
                                           Edit &nbsp;
                                       </span>
                                       </a>
                                       <a onclick="javascript:return confirm('Are you sure you want to delete ?');" href="/DeleteProduct.aspx?productID=<%#:Item.ProductID %>">    
                                       <span>
                                           &nbsp; Delete
                                       </span>
                                       </a>
                                       
                                   </span>
                               </td>
                           </tr>
                           <tr>
                               <td>&nbsp;</td>
                           </tr>
                       </table>
                       </p>
                   </td>
               </ItemTemplate>
               <LayoutTemplate>
                   <table style="width:100%;">
                       <tbody>
                           <tr>
                               <td>
                                   <table id="groupPlaceholderContainer" runat="server" style="width:100%">
                                       <tr id="groupPlaceholder"></tr>
                                   </table>
                               </td>
                           </tr>
                           <tr>
                               <td></td>
                           </tr>
                           <tr></tr>
                       </tbody>
                   </table>
               </LayoutTemplate>
           </asp:ListView>
       </div>
   </section>
</asp:Content>
