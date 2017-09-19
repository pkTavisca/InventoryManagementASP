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
            liItems.InnerHtml = string.Empty;

            DatabaseManager dbManager = new DatabaseManager();
            dbManager.Connect();
            var products = dbManager.GetAllProducts();

            foreach (var product in products)
            {
                if (product.Quantity < 1) continue;
                liItems.InnerHtml += $"<li>{product.Name} " +
                    $"<input type='number' name='item_{product.Id}' value=0 /></li>";
            }

            dbManager.Disconnect();
        }
    }
}