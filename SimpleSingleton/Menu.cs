using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleSingleton
{
    // Singleton que representa o cardápio da cafeteria
    public class Menu
    {
        private static Lazy<Menu> instance = new Lazy<Menu>(() => new Menu());
        public static Menu Instance => instance.Value;

        private List<MenuItem> items;

        private Menu()
        {
            items = new List<MenuItem>
            {
                new MenuItem { Name = "Café", Price = 2.50 },
                new MenuItem { Name = "Cappuccino", Price = 3.00 },
                new MenuItem { Name = "Sanduíche de Presunto", Price = 5.00 },
                new MenuItem { Name = "Bolo de Chocolate", Price = 4.50 }
            };
        }

        public void DisplayMenu()
        {
            Console.WriteLine($"=== Singleton Cafeteria - {DateTime.Now} ===");
            Console.WriteLine("=== Menu ===");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Name} - R${item.Price}");
            }
            Console.WriteLine("============");
            DisplayOptions();
        }

        public MenuItem GetMenuItem(string itemName)
        {
            return items.Find(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        // Função para exibir opções e solicitar entrada do usuário
        public static void DisplayOptions()
        {
            Console.WriteLine("'q' para sair, 'v' para visualizar o pedido, 'p' para pagar, 's' para ver o estoque");
            Console.Write("Selecione o item desejado: ");
        }
    }
}
