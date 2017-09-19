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
            if (Request.Form.AllKeys.Contains("buttonAdd"))
            {
                dbManager.InsertNewProduct(Request.Form["inputItemName"].ToString(),
                    int.Parse(Request.Form["inputItemQuantity"].ToString()),
                    int.Parse(Request.Form["inputItemPrice"].ToString()));
            }
            if (Request.Form.AllKeys.Contains("buttonUpdate"))
            {
                foreach (string key in Request.Form.AllKeys)
                {
                    if (key.Contains("buttonProd"))
                    {
                        int productId = int.Parse(key.Substring(10));
                        int productQuantity = int.Parse(Request.Form[key]);
                        dbManager.UpdateProduct(productId, productQuantity);
                    }
                }
            }
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.Contains("buttonDelete"))
                {
                    int productId = int.Parse(key.Substring(12));
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
                    $"<input type='number' value='{product.Quantity}' name='buttonProd{product.Id}' />" +
                    $" <input type='submit' value='Save' name='buttonUpdate' />" +
                    $"<input type='submit' value='Delete' name ='buttonDelete{product.Id}' /></li>";
            }
            dbManager.Disconnect();
        }
    }
}