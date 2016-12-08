using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using UIP_Webstore.Models;
using UIP_Webstore.Logic;
using System.Net.Mail;

namespace UIP_Webstore.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            string email = Email.Text;
            string password = Password.Text;
            string username = new MailAddress(email).User;

            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                using (UserActions userActions = new UserActions())
                {
                    bool valid = userActions.ValidateUser(email, password);

                    // If the user exists then give acces

                    if (valid)
                    {
                        if (IsValid)
                        {
                            // Validate the user password
                            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                            // This doen't count login failures towards account lockout
                            // To enable password failures to trigger lockout, change to shouldLockout: true
                            var result = signinManager.PasswordSignIn(email, password, RememberMe.Checked, shouldLockout: false);

                            switch (result)
                            {
                                case SignInStatus.Success:
                                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                                    var user = new User();
                                    user.UserEmail = email;
                                    user.UserName = username;
                                    user.Password = password;
                                    user.Role = "3";
                                    HttpContext.Current.Session["User"] = user;
                                    
                                    break;
                                case SignInStatus.LockedOut:
                                    Response.Redirect("/Account/Lockout");
                                    break;
                                case SignInStatus.RequiresVerification:
                                    Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                                    Request.QueryString["ReturnUrl"],
                                                                    RememberMe.Checked),
                                                      true);
                                    break;
                                case SignInStatus.Failure:
                                default:
                                    FailureText.Text = "Invalid login attempt";
                                    ErrorMessage.Visible = true;
                                    break;
                            }
                        }
                    }

                    else
                    {
                        FailureText.Text = "Invalid email or password";
                        ErrorMessage.Visible = true;
                    }
                }
            }
            
        }
    }
}