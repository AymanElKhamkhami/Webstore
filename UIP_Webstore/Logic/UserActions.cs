using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using UIP_Webstore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace UIP_Webstore.Logic
{
    public class UserActions : IDisposable
    {
        string UserId { get; set; }

        private WebstoreEntities _db = new WebstoreEntities();

        public bool AddUser(string email, string username, string password, string role)
        {
            var user = _db.Users.SingleOrDefault(u => u.UserEmail == email);

            if (user == null)
            {
                // Create a new user if no user with the same email exists.
                user = new User();

                user.UserEmail = email;
                user.UserName = username;
                user.Password = password;
                if (role == null)
                    user.Role = "3";
                else user.Role = role;

                _db.Users.Add(user);
                _db.SaveChanges();

                // Assign a new cart to the new user.
                NewCart(user.UserEmail);

                return true;
            }

            else
            {
                // If the user does exist then don't add it
                return false;
            }
        }

        public void NewCart(string userEmail)
        {
            var cart = new Cart();

            cart.UserEmail = userEmail;
            _db.Carts.Add(cart);
            _db.SaveChanges();
        }

        public bool ValidateUser(string email, string password)
        {
            var user = _db.Users.SingleOrDefault(u => u.UserEmail == email && u.Password == password);

            if(user == null)
            {
                // Prevent login if no users with the same email and password exist.
                return false;
            }

            else
            {
                // Add the user to the session if the username and password exist.
                HttpContext.Current.Session["User"] = user;
                return true;
            }
        }

        public List<User> GetUsers()
        {
            return _db.Users.ToList();
        }

        public struct UserListUpdates
        {
            public string UserEmail;
            public string UserRole;
            public bool RemoveUser;
        }


        public void UpdateUsersInDatabase(UserListUpdates[] UsersUpdates)
        {
            using (var db = new UIP_Webstore.Models.WebstoreEntities())
            {
                try
                {
                    int UsersCount = UsersUpdates.Count();
                    List<User> myUsers = GetUsers();
                    foreach (var user in myUsers)
                    {
                        // Iterate through all rows within shopping cart list
                        for (int i = 0; i < UsersCount; i++)
                        {
                            if (user.UserEmail.Equals(UsersUpdates[i].UserEmail))
                            {
                                if (UsersUpdates[i].RemoveUser == true)
                                {
                                    RemoveUser(user.UserEmail);
                                }
                                else
                                {
                                    UpdateUser(user.UserEmail, UsersUpdates[i].UserRole);
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


        public void RemoveUser(string removeUserEmail)
        {
            using (var _db = new UIP_Webstore.Models.WebstoreEntities())
            {
                try
                {
                    var user = (from u in _db.Users where u.UserEmail.Equals(removeUserEmail) select u).FirstOrDefault();
                    if (user != null)
                    {
                        var cart = (from c in _db.Carts where c.UserEmail.Equals(removeUserEmail) select c).FirstOrDefault();
                        var productcart = (from pc in _db.ProductCarts where pc.CartID.Equals(cart.CartID) select pc).FirstOrDefault();

                        if(productcart != null)
                            _db.ProductCarts.Remove(productcart);

                        _db.Carts.Remove(cart);
                        _db.Users.Remove(user);
                        _db.SaveChanges();

                        
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void UpdateUser(string updateUserEmail, string role)
        {
            using (var _db = new UIP_Webstore.Models.WebstoreEntities())
            {
                try
                {
                    var user = (from u in _db.Users where u.UserEmail.Equals(updateUserEmail)  select u).FirstOrDefault();
                    if (user != null)
                    {
                        user.Role = role;

                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
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