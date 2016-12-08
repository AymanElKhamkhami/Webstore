using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UIP_Webstore.Logic;
using UIP_Webstore.Models;

namespace UIP_Webstore
{
    public partial class AddUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private WebstoreEntities _db = new WebstoreEntities();

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            string email = Email.Text;
            string password = Password.Text;
            string username = new MailAddress(email).User;
            string role = Roles.SelectedValue;

            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                using (UserActions userActions = new UserActions())
                {

                    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = new ApplicationUser() { UserName = email, Email = email };
                    IdentityResult result = manager.Create(user, password);
                    var b = result.Succeeded;
                    bool added = false;

                    if (b)
                    {
                        added = userActions.AddUser(email, username, password, role);
                    }


                    if (added && b)
                    {
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        //string code = manager.GenerateEmailConfirmationToken(user.Id);
                        //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                        //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                        //var cart = _db.Carts.SingleOrDefault(c => c.UserEmail == email);
                        var u = new User();
                        u.UserEmail = email;
                        u.Password = password;
                        u.UserName = username;
                        u.Role = role;
                        HttpContext.Current.Session["User"] = u;

                        //signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                        ///IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        Response.Redirect("ManageUsers.aspx");

                    }

                    else if (!added)
                    {
                        ErrorMessage.Text = "A user already exists with the same User Email you provided.";
                    }
                    else
                    {
                        ErrorMessage.Text = result.Errors.FirstOrDefault();
                    }

                }
            }

            else
            {
                Debug.Fail("ERROR : We should never log in without a user email and a password.");
                throw new Exception("ERROR : It is illegal to load  the user page without setting a user email and a password.");

            }


        }
    }
}