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
            foreach (var item in Request.Form.AllKeys)
            {
                if (item.IndexOf("item_") != 0) continue;
                string itemName = item.Substring(5);
                int itemQuantity = int.Parse(Request.Form[item]);
                _inventoryManager.Remove(itemName, itemQuantity);
            }
        }
    }
}