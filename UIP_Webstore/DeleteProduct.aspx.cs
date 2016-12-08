using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UIP_Webstore
{
    public partial class DeleteProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rawId = Request.QueryString["ProductID"];
            int productId;

            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productId))
            {
                using (var _db = new UIP_Webstore.Models.WebstoreEntities())
                {

                    var myItem = (from p in _db.Products where p.ProductID == productId select p).FirstOrDefault();
                    if (myItem != null)
                    {
                        var myItemInCarts = (from p in _db.ProductCarts where p.ProductID == productId select p).FirstOrDefault();
                        if (myItemInCarts != null)
                        {
                            _db.ProductCarts.Remove(myItemInCarts);
                            _db.SaveChanges();
                        }
                        // Remove Item.
                        _db.Products.Remove(myItem);
                        _db.SaveChanges();
                    }

                    Response.Redirect("ManageProducts.aspx");
                    
                }
            }
            else
            {
                Debug.Fail("ERROR: We should never get to AddToCart.aspx without a ProductId.");
                throw new Exception("ERROR: It is illegal to load AddToCart.aspx without setting a ProductId.");
            }
        }
    }
}