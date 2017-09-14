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
    public partial class SuccessPage : System.Web.UI.Page
    {
        private InventoryManager _inventoryManager = new InventoryManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form.AllKeys.Contains("checkout") == false) return;
            DatabaseManager dbManager = new DatabaseManager();
            dbManager.Connect();

            var products = dbManager.GetAllProducts();
            decimal price = 0;
            foreach (var item in Session.Keys)
            {
                string itemId = item.ToString();
                int itemQuantity = int.Parse(Session[itemId].ToString());
                if (itemQuantity < 1) continue;
                int productId = int.Parse(itemId);
                Product product = products.Where(x => x.Id == productId).First();
                price += product.Price * itemQuantity;
            }
            dbManager.InsertInOrder(price);
            foreach (var item in Session.Keys)
            {
                string itemId = item.ToString();
                int itemQuantity = int.Parse(Session[itemId].ToString());
                if (itemQuantity < 1) continue;
                int orderId = dbManager.GetLastOrderId();
                Product product = products.Where(x => x.Id == int.Parse(itemId)).First();
                dbManager.InsertInOrderDetails(orderId, itemId, itemQuantity, product.Price * itemQuantity);
                dbManager.RemoveFromProductStore(itemId, itemQuantity);
            }
            dbManager.Disconnect();
        }
    }
}