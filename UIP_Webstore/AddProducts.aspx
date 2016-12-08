<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProducts.aspx.cs" Inherits="UIP_Webstore.AddProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4>Add a new product</h4>
        <br />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProductName" CssClass="col-md-2 control-label">Product Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ProductName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ProductName" CssClass="text-danger" ErrorMessage="The product name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ReleaseDate" CssClass="col-md-2 control-label">Release Date</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ReleaseDate" CssClass="form-control" TextMode="Date"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ReleaseDate" CssClass="text-danger" ErrorMessage="The release date field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Publisher" CssClass="col-md-2 control-label">Publisher</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Publisher" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Publisher" CssClass="text-danger" ErrorMessage="The publisher date field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Price" CssClass="col-md-2 control-label">Price</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Price" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Price" CssClass="text-danger" ErrorMessage="The price field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Categories" CssClass="col-md-2 control-label">Category</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="Categories" CssClass="form-control" SelectMethod="GetCategories" ItemType="UIP_Webstore.Models.Category" AppendDataBoundItems="true" AutoPostBack="true" DataTextField="CategoryName" DataValueField="CategoryID"/>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FileUpload" CssClass="col-md-2 control-label">Image</asp:Label>
            <div class="col-md-10">
                <asp:FileUpload runat="server" ID="FileUpload" CssClass="btn btn-default" />
                <br />
                <asp:Button ID="UploadBtn" runat="server" Text="Upload" OnClick="UploadBtn_Click" CssClass="btn btn-default"/>
                <br />
                <asp:Image ID="Image" runat="server"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Add Product" OnClick="AddBtn_Click" CssClass="btn btn-default"/>
            </div>
        </div>
    </div>
</asp:Content>
