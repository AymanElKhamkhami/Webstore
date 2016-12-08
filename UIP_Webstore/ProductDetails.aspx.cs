using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UIP_Webstore.Models;
using System.Web.ModelBinding;
using UIP_Webstore.Logic;

namespace UIP_Webstore
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            productDetail.Visible = !IsAdmin() && !IsModerator();
            editDetails.Visible = IsAdmin() || IsModerator();
        }

        public IQueryable<Product> GetProduct([QueryString("productID")] int? productId)
        {
            var _db = new UIP_Webstore.Models.WebstoreEntities();
            IQueryable<Product> query = _db.Products;
            if (productId.HasValue && productId > 0)
            {
                query = query.Where(p => p.ProductID == productId);
            }
            else
            {
                query = null;
            }
            return query;
        }


        public bool IsAdmin()
        {
            var user = ((User)HttpContext.Current.Session["User"]);

            if (user != null && user.Role.Equals("1"))
            {
                return true;
            }

            else return false;
            
        }
        public bool IsModerator()
        {
            var user = ((User)HttpContext.Current.Session["User"]);

            if (user != null && user.Role.Equals("2"))
            {
                return true;
            }

            else return false;

        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            int prodId = Int32.Parse(Request["ProductID"]);

            using (ProductActions prodActions = new ProductActions())
            {
                string prodName = ((TextBox)editDetails.FindControl("prodName")).Text;
                double price = double.Parse(((TextBox)editDetails.FindControl("price")).Text.ToString());
                string desc = ((TextBox)editDetails.FindControl("desc")).Text;

                prodActions.UpdateProduct(prodId, prodName, desc, price);

                Response.Redirect("ManageProducts.aspx");
            }
        }

    }
}