using InventoryManagement.Database;
using InventoryManagement.Inventory;
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
            dbManager.InsertInOrder();
            foreach (var item in Session.Keys)
            {
                string itemId = item.ToString();
                int itemQuantity = int.Parse(Session[itemId].ToString());
                if (itemQuantity < 1) continue;
                int orderId = dbManager.GetLastOrderId();
                dbManager.InsertInOrderDetails(orderId, itemId, itemQuantity);
            }
            dbManager.Disconnect();
        }
    }
}