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
            List<Product> products = dbManager.GetAllProducts();
            listOfItems.InnerHtml = string.Empty;
            foreach (var product in products)
            {
                listOfItems.InnerHtml += $"<li>{product.Name} : {product.Quantity}</li>";
            }
            dbManager.Disconnect();
        }
    }
}