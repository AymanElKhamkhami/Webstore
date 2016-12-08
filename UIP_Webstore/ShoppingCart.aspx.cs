using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UIP_Webstore.Logic;
using UIP_Webstore.Models;
using System.Collections.Specialized;
using System.Collections;
using System.Web.ModelBinding;

namespace UIP_Webstore
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = ((User)HttpContext.Current.Session["User"]);

            if(user != null)
            {
                using (CartActions userCart = new CartActions())
                {
                    decimal cartTotal = 0;
                    cartTotal = userCart.GetTotal();
                    if (cartTotal > 0)
                    {
                        // Display Total.
                        lblTotal.Text = String.Format("{0:c}", cartTotal);
                    }
                    else
                    {
                        LabelTotalText.Text = "";
                        lblTotal.Text = "";
                        ShoppingCartTitle.InnerText = "Shopping Cart is Empty";
                        UpdateBtn.Visible = false;
                    }
                }
            }

            else
            {
                Response.Redirect("Account/Login.aspx");
            }
            
        }

        public List<ProductCart> GetProductCarts()
        {
            CartActions actions = new CartActions();
            return actions.GetProductCarts();
        }

        public List<ProductCart> UpdateCartItems()
        {
            using (CartActions userCart = new CartActions())
            {
                int cartId = userCart.GetCartID();

                CartActions.ShoppingCartUpdates[] cartUpdates = new CartActions.ShoppingCartUpdates[CartList.Rows.Count];
                for (int i = 0; i < CartList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(CartList.Rows[i]);
                    cartUpdates[i].ProductId = Convert.ToInt32(rowValues["ProductID"]);

                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)CartList.Rows[i].FindControl("Remove");
                    cartUpdates[i].RemoveItem = cbRemove.Checked;

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity");
                    cartUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
                }
                userCart.UpdateShoppingCartDatabase(cartId, cartUpdates);
                CartList.DataBind();
                lblTotal.Text = String.Format("{0:c}", userCart.GetTotal());
                return userCart.GetProductCarts();
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

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateCartItems();
        }
    
}
}