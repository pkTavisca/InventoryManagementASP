using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace InventoryManagement.Inventory
{
    public class InventoryManager
    {
        private string _storeFileName;
        public InventoryManager(string storeFileName = "D:/inventory.txt")
        {
            if (File.Exists(storeFileName) == false)
                File.WriteAllText(storeFileName, JsonConvert.SerializeObject(new Dictionary<string, int>() { { "item1", 2 } }));
            _storeFileName = storeFileName;
        }

        public void Add(string item, int numberOfItems = 1)
        {
            Dictionary<string, int> inventory = GetInventory();
            if (inventory.ContainsKey(item) == false) inventory[item] = 0;
            inventory[item] += numberOfItems;
            UpdateInventory(inventory);
        }

        public void Remove(string item, int numberOfItems = 1)
        {
            Dictionary<string, int> inventory = GetInventory();
            inventory[item] -= numberOfItems;
            if (inventory[item] == 0) inventory.Remove(item);
            UpdateInventory(inventory);
        }
        private void UpdateInventory(Dictionary<string, int> inventory)
        {
            File.WriteAllText(_storeFileName, JsonConvert.SerializeObject(inventory));
        }

        public Dictionary<string, int> GetInventory()
        {
            string fileContent = File.ReadAllText(_storeFileName);
            Dictionary<string, int> inventory = JsonConvert.DeserializeObject<Dictionary<string, int>>(fileContent);
            return inventory;
        }
    }
}