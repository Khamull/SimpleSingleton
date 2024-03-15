using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSingleton
{
    // Singleton que representa o gerenciamento de pedidos da cafeteria
    public class OrderManager
    {
        private static Lazy<OrderManager> instance = new Lazy<OrderManager>(() => new OrderManager());
        public static OrderManager Instance => instance.Value;

        private List<MenuItem> orders;

        private OrderManager()
        {
            orders = new List<MenuItem>();
        }

        public void AddOrder(MenuItem item, int quantity)
        {
            // Adiciona o item ao pedido a quantidade especificada
            for (int i = 0; i < quantity; i++)
            {
                orders.Add(item);
            }
        }

        public void DisplayOrders()
        {
            Console.WriteLine("=== Pedidos ===");
            Dictionary<string, int> orderSummary = new Dictionary<string, int>();

            // Criar um resumo dos pedidos
            foreach (var item in orders)
            {
                if (orderSummary.ContainsKey(item.Name))
                {
                    orderSummary[item.Name]++;
                }
                else
                {
                    orderSummary[item.Name] = 1;
                }
            }

            // Exibir o resumo dos pedidos
            foreach (var entry in orderSummary)
            {
                MenuItem item = Menu.Instance.GetMenuItem(entry.Key);
                Console.WriteLine($"{item.Name} - Quantidade: {entry.Value} - Total: R${entry.Value * item.Price}");
            }

            // Calcular e exibir o total do pedido
            double total = CalculateTotal();
            Console.WriteLine($"Total do Pedido: R${total}");
            Console.WriteLine("==============");
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in orders)
            {
                total += item.Price;
            }
            return total;
        }

        public void ClearOrders()
        {
            orders.Clear();
        }
    }
}
