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
                Console.Clear(); // Limpa a tela antes de exibir o menu

                menu.DisplayMenu();
                string input = Console.ReadLine();

                Console.Clear(); // Limpa a tela antes de processar a entrada do usuário

                if (input.ToLower() == "q")
                    break;
                else if (input.ToLower() == "v")
                {
                    // Exibir o pedido atual
                    orderManager.DisplayOrders();
                }
                else if (input.ToLower() == "p")
                {
                    // Finalizar o pedido e processar o pagamento
                    double total = orderManager.CalculateTotal();
                    if (total > 0)
                    {
                        Console.WriteLine("=== Processando Pagamento ===");
                        Console.WriteLine($"Total do Pedido: R${total}");
                        PaymentProcessor.Instance.ProcessPayment(total);
                        orderManager.ClearOrders();
                        Console.WriteLine("Obrigado por sua compra!");
                    }
                    else
                    {
                        Console.WriteLine("=== Processando Pagamento ===");
                        Console.WriteLine("Não há itens no pedido. Por favor, faça um pedido antes de pagar.");
                    }
                }
                else if (input.ToLower() == "s")
                {

                    // Exibir o estoque
                    inventoryManager.DisplayStock();
                }
                else
                {
                    // Verificar se o item está no menu
                    MenuItem item = menu.GetMenuItem(input);
                    if (item == null)
                    {
                        Console.WriteLine("Item não encontrado no menu.");
                    }
                    else
                    {
                        Console.WriteLine(item.Name + " - " + item.Price);
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
                        }
                        else
                        {
                            // Adicionar o item ao pedido
                            orderManager.AddOrder(item, quantity);

                            // Atualizar o estoque
                            inventoryManager.UpdateStock(item.Name, quantity);
                        }
                    }
                }

                // Aguardar entrada do usuário antes de continuar
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
                Console.ReadKey(true);
            }
        }
    }
}
