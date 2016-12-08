using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using UIP_Webstore.Models;
using UIP_Webstore.Logic;
using System.Diagnostics;
using System.Net.Mail;

namespace UIP_Webstore.Account
{
    public partial class Register : Page
    {
        private WebstoreEntities _db = new WebstoreEntities();

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            string email = Email.Text;
            string password = Password.Text;
            string username = new MailAddress(email).User;

            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                using (UserActions userActions = new UserActions())
                {

                    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                    var user = new ApplicationUser() { UserName = email, Email = email };
                    IdentityResult result = manager.Create(user, password);
                    var b = result.Succeeded;
                    bool added = false;

                    if (b)
                    {
                        added = userActions.AddUser(email, username, password, null);
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
                        u.Role = "3";
                        HttpContext.Current.Session["User"] = u;

                        signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        
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