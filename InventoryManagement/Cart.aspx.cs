using InventoryManagement.Database;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;

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
            liCartItems.InnerHtml = string.Empty;
            foreach (var itemId in Request.Form.AllKeys)
            {
                if (itemId.IndexOf("item_") != 0) continue;
                int productId = int.Parse(itemId.Substring(5));
                int itemQuantity = 0;
                itemQuantity = int.Parse(Request.Form[itemId]);
                Product product = GetProductById(products, productId);
                if (itemQuantity > product.Quantity) itemQuantity = product.Quantity;
                Session[productId.ToString()] = itemQuantity;
            }
            foreach (string productId in Session.Keys)
            {
                int quantity = int.Parse(Session[productId].ToString());
                if (quantity < 1) continue;
                Product product = GetProductById(products, int.Parse(productId));
                liCartItems.InnerHtml += $"<div>{GetProductById(products, int.Parse(productId)).Name} : {quantity} - {product.Price * quantity}" +
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