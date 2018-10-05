using CashMachine.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Operation operation = new Operation();
            string userInput = string.Empty;

            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("Bem-vindo ao Caixa Eletrônico do banco Zenvia.");
            Console.WriteLine("Cédulas disponíveis " +  string.Join(", ", operation.getOptionsValues()) + ".");
            Console.WriteLine("Esse caixa possuí um limite de " + operation.getMaxValuePerOperation().ToString() + " por saque.");
            Console.WriteLine("--------------------------------------------------------------------------");

            try
            {
                while (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("");
                    Console.Write("Valor do Saque (R$): ");
                    userInput = Console.ReadLine();

                    decimal value = 0;

                    if (decimal.TryParse(userInput, out value))
                    {
                        string result =  string.Join(", ", operation.CashOut(value));
                        Console.WriteLine("Resultado: " + result);
                    }
                    else
                    {
                        Console.WriteLine("Valor incorreto, os valores permitidos pelo sistema são entre 1 e 3000.");
                        userInput = string.Empty;
                    }
                }

                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("Pressione qualquer tecla para fechar");
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.ReadKey();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
