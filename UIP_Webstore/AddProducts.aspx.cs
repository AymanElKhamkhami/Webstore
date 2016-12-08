using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UIP_Webstore.Logic;
using UIP_Webstore.Models;

namespace UIP_Webstore
{
    public partial class AddProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Category> GetCategories()
        {
            var _db = new UIP_Webstore.Models.WebstoreEntities();
            IQueryable<Category> query = _db.Categories;

            return query;
        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            string imgName = FileUpload.FileName;
            string imgExtension = Path.GetExtension(imgName);
            string imgPath = "Images/" + "last" + imgExtension;

            if(FileUpload.PostedFile != null && FileUpload.PostedFile.FileName != "")
            {
                FileUpload.SaveAs(Server.MapPath(imgPath));
                Image.ImageUrl = "~/" + imgPath;
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            using (ProductActions prodActions = new ProductActions())
            {
                string prodName = ProductName.Text;
                double price = double.Parse(Price.Text.ToString());
                string release = ReleaseDate.Text;
                int category = Int32.Parse(Categories.SelectedValue.ToString());
                string publisher = Publisher.Text;

                var added = prodActions.AddProduct(prodName, release, publisher, price, category);

                if(added)
                {
                    var myImg = Directory.GetFiles(Server.MapPath("~/Images/"), "last.*");
                    var _db = new UIP_Webstore.Models.WebstoreEntities();
                    Product product = _db.Products.OrderByDescending(p => p.ProductID ).FirstOrDefault();
                    string extension = Path.GetExtension(myImg[0]);
                    File.Move(Path.Combine(Server.MapPath("~/Images/"), "last"+extension), Path.Combine(Server.MapPath("~/Images/"), product.ProductID + ".jpg"));

                    Response.Redirect("ManageProducts.aspx");
                }
            }
        }
    }
}