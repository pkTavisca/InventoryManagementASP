using InventoryManagement.Database;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement
{
    public partial class InventoryPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatabaseManager dbManager = new DatabaseManager();
            dbManager.Connect();
            if (Request.Form.AllKeys.Contains("Add"))
            {
                dbManager.InsertNewProduct(Request.Form["newItemName"].ToString(),
                    int.Parse(Request.Form["newItemQuantity"].ToString()),
                    int.Parse(Request.Form["newItemPrice"].ToString()));
            }
            if (Request.Form.AllKeys.Contains("update"))
            {
                foreach (string key in Request.Form.AllKeys)
                {
                    if (key.Contains("prod"))
                    {
                        int productId = int.Parse(key.Substring(4));
                        int productQuantity = int.Parse(Request.Form[key]);
                        dbManager.UpdateProduct(productId, productQuantity);
                    }
                }
            }
            if (AnyRequestContains("delete"))
            {

            }
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.Contains("delete"))
                {
                    int productId = int.Parse(key.Substring(6));
                    try
                    {
                        dbManager.DeleteProduct(productId);
                    }
                    catch (Exception ex)
                    {
                        dbManager.UpdateProduct(productId, 0);
                    }
                }
            }
            List<Product> products = dbManager.GetAllProducts();
            listOfItems.InnerHtml = string.Empty;
            foreach (var product in products)
            {
                listOfItems.InnerHtml += $"<li>{product.Name} : " +
                    $"<input type='number' value='{product.Quantity}' name='prod{product.Id}' />" +
                    $" <input type='submit' value='Save' name='update' />" +
                    $"<input type='submit' value='Delete' name ='delete{product.Id}' /></li>";
            }
            dbManager.Disconnect();
        }

        private bool AnyRequestContains(string searchString)
        {
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.Contains(searchString))
                    return true;
            }
            return false;
        }
    }
}