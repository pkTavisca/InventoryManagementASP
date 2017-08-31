using InventoryManagement.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement
{
    public partial class InventoryPage : System.Web.UI.Page
    {
        private InventoryManager _inventoryManager = new InventoryManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string itemName = Request.Form["newItemName"];
                int itemQuantity = int.Parse(Request.Form["newItemQuantity"]);
                _inventoryManager.Add(itemName, itemQuantity);
            }
            catch (Exception ex)
            {
            }
            listOfItems.InnerHtml = string.Empty;
            foreach (var item in _inventoryManager.GetInventory())
            {
                listOfItems.InnerHtml += $"<li>{item.Key} : {item.Value}</li>";
            }
        }
    }
}