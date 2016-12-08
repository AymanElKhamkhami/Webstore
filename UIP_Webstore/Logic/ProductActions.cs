using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UIP_Webstore.Models;

namespace UIP_Webstore.Logic
{
    public class ProductActions : IDisposable
    {
        public int ProductId { get; set; }

        private WebstoreEntities _db = new WebstoreEntities();


        public bool AddProduct(string prodName, string release, string publisher, double price, int category)
        {
            var prod = new Product();
            prod.ProductName = prodName;
            prod.ReleaseDate = release;
            prod.Publisher = publisher;
            prod.Price = price;
            prod.CategoryID = category;

            try
            {
                _db.Products.Add(prod);
                _db.SaveChanges();

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public void UpdateProduct(int prodId, string prodName, string description, double price)
        {
            using (var _db = new UIP_Webstore.Models.WebstoreEntities())
            {
                try
                {
                    var myProd = (from p in _db.Products where p.ProductID == prodId select p).FirstOrDefault();
                    myProd.ProductName = prodName;
                    myProd.ReleaseDate = description;
                    myProd.Price = price;
                    _db.SaveChanges();
                }

                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Product in Database - " + exp.Message.ToString(), exp);
                }
            }
        }


        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }
    
    }
}