using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Linq;
using UIP_Webstore.Models;
using UIP_Webstore.Logic;

namespace UIP_Webstore
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }


            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            using (CartActions userCart = new CartActions())
            {
                string cartStr = string.Format("Cart ({0})", userCart.GetCount());
                cartCount.InnerText = cartStr;
            }

            gamesList.Visible = !IsAdmin() && !IsModerator();
            manageProducts.Visible = IsAdmin() || IsModerator();
            cartCount.Visible = !IsAdmin() && !IsModerator();
            manageUsers.Visible = IsAdmin();
            categoryList.Visible = !IsAdmin() && !IsModerator();
            categoryListAdmin.Visible = IsAdmin() || IsModerator();
        }

        public IQueryable<Category> GetCategories()
        {
            var _db = new UIP_Webstore.Models.WebstoreEntities();
            IQueryable<Category> query = _db.Categories;
            return query;
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.Current.Session["User"] = null;
        }

        public bool IsAdmin()
        {
            var user = (User)HttpContext.Current.Session["User"];

            if (user != null && user.Role.Equals("1"))
                return true;
            else return false;
        }

        public bool IsModerator()
        {
            var user = (User)HttpContext.Current.Session["User"];

            if (user != null && user.Role.Equals("2"))
                return true;
            else return false;
        }
    }

}