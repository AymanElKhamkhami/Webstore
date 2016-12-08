using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UIP_Webstore.Models;

namespace UIP_Webstore.Logic
{
    public class CartActions : IDisposable
    {
        public int CartId { get; set; }

        private WebstoreEntities _db = new WebstoreEntities();

        public const string CartSessionKey = "CartID";

        public bool AddToCart (int id)
        {
            // Retrieve the product from the database.
            
            CartId = GetCartID();

            if (CartId != 0)
            {
                //productCart refers to the purchase instance
                var productCart = _db.ProductCarts.SingleOrDefault(
                    c => c.CartID == CartId && c.ProductID == id);
                if (productCart == null)
                {
                    // Create a new cart item if no cart item exists.
                    productCart = new ProductCart();
                    productCart.CartID = CartId;
                    productCart.ProductID = id;
                    productCart.Quantity = 1;
                    productCart.Product = _db.Products.SingleOrDefault(p => p.ProductID == id);
                    productCart.Cart = _db.Carts.SingleOrDefault(p => p.CartID == id);

                    _db.ProductCarts.Add(productCart);
                }

                else
                {
                    // If the item does exist in the cart, then add one to the quantity.
                    productCart.Quantity++;
                }

                _db.SaveChanges();

                return true;
            }

            else
            {
                return false;
            }

            
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public int GetCartID()
        {
            

            string userEmail;
            var user = (User)HttpContext.Current.Session["User"];

            if(user != null)
            {
                userEmail = ((User)HttpContext.Current.Session["User"]).UserEmail;
                var cart = _db.Carts.SingleOrDefault(c => c.UserEmail == userEmail);
                return cart.CartID;
            }

            else
            {
                return 0;
            }
        }

        public List<ProductCart> GetProductCarts()
        {
            CartId = GetCartID();
            return _db.ProductCarts.Where(
                c => c.CartID == CartId).ToList();
        }

        public decimal GetTotal()
        {
            CartId = GetCartID();
            //CartId = 1;

            // Multiply product price by quantity of that product to get        
            // the current price for each of those products in the cart.  
            // Sum all product price totals to get the cart total.   
            decimal? total = decimal.Zero;
            total = (decimal?)(from productCarts in _db.ProductCarts
                               where productCarts.CartID == CartId
                               select (int?)productCarts.Quantity *
                               productCarts.Product.Price).Sum();
            return total ?? decimal.Zero;
        }

        public CartActions GetCart(HttpContext context)
        {
            using (var cart = new CartActions())
            {
                cart.CartId = cart.GetCartID();
                return cart;
            }
        }

        public void UpdateShoppingCartDatabase(int cartId, ShoppingCartUpdates[] CartItemUpdates)
        {
            using (var db = new UIP_Webstore.Models.WebstoreEntities())
            {
                try
                {
                    int CartItemCount = CartItemUpdates.Count();
                    List<ProductCart> myCart = GetProductCarts();
                    foreach (var cartItem in myCart)
                    {
                        // Iterate through all rows within shopping cart list
                        for (int i = 0; i < CartItemCount; i++)
                        {
                            if (cartItem.Product.ProductID == CartItemUpdates[i].ProductId)
                            {
                                if (CartItemUpdates[i].PurchaseQuantity < 1 || CartItemUpdates[i].RemoveItem == true)
                                {
                                    RemoveItem(cartId, cartItem.ProductID);
                                }
                                else
                                {
                                    UpdateItem(cartId, cartItem.ProductID, CartItemUpdates[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Database - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void RemoveItem(int removeCartID, int removeProductID)
        {
            using (var _db = new UIP_Webstore.Models.WebstoreEntities())
            {
                try
                {
                    var myItem = (from c in _db.ProductCarts where c.CartID == removeCartID && c.Product.ProductID == removeProductID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        // Remove Item.
                        _db.ProductCarts.Remove(myItem);
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void UpdateItem(int updateCartID, int updateProductID, int quantity)
        {
            using (var _db = new UIP_Webstore.Models.WebstoreEntities())
            {
                try
                {
                    var myItem = (from c in _db.ProductCarts where c.CartID == updateCartID && c.Product.ProductID == updateProductID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void EmptyCart()
        {
            CartId = GetCartID();
            var cartItems = _db.ProductCarts.Where(
                c => c.CartID == CartId);
            foreach (var productCart in cartItems)
            {
                _db.ProductCarts.Remove(productCart);
            }
            // Save changes.             
            _db.SaveChanges();
        }

        public int GetCount()
        {
            CartId = GetCartID();

            // Get the count of each item in the cart and sum them up          
            int? count = (from cartItems in _db.ProductCarts
                          where cartItems.CartID == CartId
                          select (int?)cartItems.Quantity).Sum();
            // Return 0 if all entries are null         
            return count ?? 0;
        }

        public struct ShoppingCartUpdates
        {
            public int ProductId;
            public int PurchaseQuantity;
            public bool RemoveItem;
        }
    }
}