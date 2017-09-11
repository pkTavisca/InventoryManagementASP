using InventoryManagement.Database;
using InventoryManagement.Inventory;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatabaseManager dbManager = new DatabaseManager();
            dbManager.Connect();
            var products = dbManager.GetAllProducts();
            dbManager.Disconnect();
            cartItems.InnerHtml = string.Empty;
            foreach (var itemId in Request.Form.AllKeys)
            {
                if (itemId.IndexOf("item_") != 0) continue;
                int productId = int.Parse(itemId.Substring(5));
                int itemQuantity = 0;
                try
                {
                    itemQuantity = int.Parse(Request.Form[itemId]);
                }
                catch { }
                Product product = GetProductById(products, productId);
                if (itemQuantity > product.Quantity) itemQuantity = product.Quantity;
                Session[productId.ToString()] = itemQuantity;
            }
            foreach (string productId in Session.Keys)
            {
                cartItems.InnerHtml += $"<div>{GetProductById(products, int.Parse(productId)).Name} : {Session[productId]}" +
                    $"<input type='hidden' name='item_{productId}' value='{Session[productId]}'></div>";
            }
        }

        private Product GetProductById(List<Product> products, int productId)
        {
            foreach (var product in products)
            {
                if (product.Id == productId)
                    return product;
            }
            return null;
        }
    }
}