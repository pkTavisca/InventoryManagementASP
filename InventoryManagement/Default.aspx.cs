using InventoryManagement.Database;
using InventoryManagement.Inventory;
using System;

namespace InventoryManagement
{
    public partial class Default : System.Web.UI.Page
    {
        private InventoryManager _inventoryManager = new InventoryManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            listOfItems.InnerHtml = string.Empty;
            var inventory = _inventoryManager.GetInventory();
            foreach (var item in inventory)
            {
                listOfItems.InnerHtml += $"<li>{item.Key} <input type='number' name='item_{item.Key}'/></li>";
            }
            DatabaseManager dbManager = new DatabaseManager();
            dbManager.Connect();
        }
    }
}