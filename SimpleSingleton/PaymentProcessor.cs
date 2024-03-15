using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSingleton
{
    // Singleton que representa o processador de pagamentos da cafeteria
    public class PaymentProcessor
    {
        private static Lazy<PaymentProcessor> instance = new Lazy<PaymentProcessor>(() => new PaymentProcessor());
        public static PaymentProcessor Instance => instance.Value;

        private PaymentProcessor()
        {
        }

        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Pagamento de R${amount} processado com sucesso!");
        }
    }
}
