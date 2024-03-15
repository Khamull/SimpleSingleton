using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSingleton
{
    // Singleton que representa o gerenciamento de estoque da cafeteria
    public class InventoryManager
    {
        private static Lazy<InventoryManager> instance = new Lazy<InventoryManager>(() => new InventoryManager());
        public static InventoryManager Instance => instance.Value;

        private Dictionary<string, int> stock;

        private InventoryManager()
        {
            stock = new Dictionary<string, int>
            {
                { "Café", 100 },
                { "Cappuccino", 100 },
                { "Sanduíche de Presunto", 50 },
                { "Bolo de Chocolate", 20 }
            };
        }

        public void DisplayStock()
        {
            Console.WriteLine("=== Estoque ===");
            foreach (var item in stock)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            Console.WriteLine("===============");
        }

        public bool CheckStock(string itemName, int quantity)
        {
            if (stock.ContainsKey(itemName) && stock[itemName] >= quantity)
            {
                return true;
            }
            return false;
        }

        public void UpdateStock(string itemName, int quantity)
        {
            if (stock.ContainsKey(itemName))
            {
                stock[itemName] -= quantity;
            }
        }
    }
}
