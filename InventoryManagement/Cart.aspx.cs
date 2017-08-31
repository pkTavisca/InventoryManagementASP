using InventoryManagement.Inventory;
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
        private Dictionary<string, int> _inventory = new InventoryManager().GetInventory();
        protected void Page_Load(object sender, EventArgs e)
        {
            cartItems.InnerHtml = string.Empty;
            foreach (var itemId in Request.Form.AllKeys)
            {
                if (itemId.IndexOf("item_") != 0) continue;
                string itemName = itemId.Substring(5);
                int itemQuantity = int.Parse(Request.Form[itemId]);
                if (itemQuantity > _inventory[itemName]) itemQuantity = _inventory[itemName];

                cartItems.InnerHtml += $"{itemName} : {itemQuantity}<input type='hidden' name='{itemId}' value='{itemQuantity}'";
            }
        }
    }
}