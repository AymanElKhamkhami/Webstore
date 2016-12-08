using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UIP_Webstore.Logic;
using UIP_Webstore.Models;

namespace UIP_Webstore
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public List<User> GetUsers()
        {
            UserActions actions = new UserActions();
            return actions.GetUsers();
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateUsers();
        }

        public List<User> UpdateUsers()
        {
            using (UserActions userActions = new UserActions())
            {

                UserActions.UserListUpdates[] usersUpdates = new UserActions.UserListUpdates[UsersList.Rows.Count];
                for (int i = 0; i < UsersList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(UsersList.Rows[i]);
                    usersUpdates[i].UserEmail = rowValues["UserEmail"].ToString();

                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)UsersList.Rows[i].FindControl("Remove");
                    usersUpdates[i].RemoveUser = cbRemove.Checked;

                    DropDownList roleList = new DropDownList();
                    roleList = (DropDownList)UsersList.Rows[i].FindControl("UserRole");
                    usersUpdates[i].UserRole = roleList.Text.ToString();

                    
                    
                }

                userActions.UpdateUsersInDatabase(usersUpdates);
                UsersList.DataBind();

                //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

                //foreach (var row in usersUpdates)
                //{

                //    if (row.RemoveUser)
                //    {
                //        var appUsers = new ApplicationDbContext().Users.ToList();
                //        foreach (var user in appUsers)
                //        {
                //            if (user.Email.Equals(row.UserEmail))
                //            {
                //                manager.Delete(user);

                //            }

                //        }

                //    }

                //}

                return userActions.GetUsers();
            }

        }

            public static IOrderedDictionary GetValues(GridViewRow row)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.Visible)
                {
                    // Extract values from the cell.
                    cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true);
                }
            }
            return values;
        }


    
    }
}