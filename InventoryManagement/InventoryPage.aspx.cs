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
            resultMessage.InnerText = string.Empty;
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
                        resultMessage.InnerText = "Item could not be deleted so quantity has been set to zero.";
                    }
                }
            }
            List<Product> products = dbManager.GetAllProducts();
            liItems.InnerHtml = string.Empty;
            foreach (var product in products)
            {
                liItems.InnerHtml += $"<li>{product.Name} : " +
                    $"<input type='number' value='{product.Quantity}' name='prod{product.Id}' />" +
                    $" <input type='submit' value='Save' name='update' />" +
                    $"<input type='submit' value='Delete' name ='delete{product.Id}' /></li>";
            }
            dbManager.Disconnect();
        }
    }
}