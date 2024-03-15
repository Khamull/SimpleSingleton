using System;

namespace SimpleSingleton
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = Menu.Instance;
            InventoryManager inventoryManager = InventoryManager.Instance;
            OrderManager orderManager = OrderManager.Instance;

            while (true)
            {
                menu.DisplayMenu();
                string input = Console.ReadLine();
                if (input.ToLower() == "q")
                    break;
                else if (input.ToLower() == "v")
                {
                    // Exibir o pedido atual
                    orderManager.DisplayOrders();
                    continue;
                }
                else if (input.ToLower() == "p")
                {
                    // Finalizar o pedido e processar o pagamento
                    double total = orderManager.CalculateTotal();
                    if (total > 0)
                    {
                        Console.WriteLine($"Total do Pedido: R${total}");
                        PaymentProcessor.Instance.ProcessPayment(total);
                        orderManager.ClearOrders();
                        Console.WriteLine("Obrigado por sua compra!");
                    }
                    else
                    {
                        Console.WriteLine("Não há itens no pedido. Por favor, faça um pedido antes de pagar.");
                    }
                    continue;
                }
                else if (input.ToLower() == "s")
                {
                    // Exibir o estoque
                    inventoryManager.DisplayStock();
                    continue;
                }

                // Verificar se o item está no menu
                MenuItem item = menu.GetMenuItem(input);
                if (item == null)
                {
                    Console.WriteLine("Item não encontrado no menu.");
                    continue;
                }

                // Solicitar ao usuário a quantidade desejada
                Console.Write($"Quantidade de '{item.Name}': ");
                int quantity;
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Quantidade inválida. Por favor, insira um número inteiro positivo.");
                    Console.Write($"Quantidade de '{item.Name}': ");
                }

                // Verificar se há estoque suficiente
                if (!inventoryManager.CheckStock(item.Name, quantity))
                {
                    Console.WriteLine("Desculpe, não há estoque suficiente para este item.");
                    continue;
                }

                // Adicionar o item ao pedido
                orderManager.AddOrder(item, quantity);

                // Atualizar o estoque
                inventoryManager.UpdateStock(item.Name, quantity);
            }
        }

        // Função para exibir opções e solicitar entrada do usuário
        static void DisplayOptions()
        {
            Console.WriteLine($"'q' para sair, 'v' para visualizar o pedido, 'p' para pagar, 's' para ver o estoque - {DateTime.Now}");
            Console.Write("Selecione o item desejado: ");
        }
    }
}
