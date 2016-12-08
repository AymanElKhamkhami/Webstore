<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="UIP_Webstore.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="UsersListTitle" runat="server" class="ContentHead">
        <h1>Users List</h1>
    </div>

    <asp:GridView ID="UsersList" runat="server" AutoGenerateColumns="False" ShowFooter="True" GridLines="Vertical" CellPadding="4"
        ItemType="UIP_Webstore.Models.User" SelectMethod="GetUsers"
        CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="User Name" />
            <asp:BoundField DataField="UserEmail" HeaderText="Email" />
            <asp:TemplateField HeaderText="Role">
                <ItemTemplate>
                    <asp:DropDownList ID="UserRole" Width="100" runat="server" Text="<%#: Item.Role %>">
                        <asp:ListItem Text="Admin" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Moderator" Value="2"></asp:ListItem>
                        <asp:ListItem Text="User" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Remove User">
                <ItemTemplate>
                    <asp:CheckBox ID="Remove" runat="server"></asp:CheckBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <table>
        <tr>
            <td>
                <asp:Button ID="UpdateBtn" runat="server" Text="Save Changes" OnClick="UpdateBtn_Click" />
            </td>
            <td>
                <!--Checkout Placeholder -->
            </td>
        </tr>
    </table>
</asp:Content>
