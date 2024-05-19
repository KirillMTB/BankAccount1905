using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal startMoney = 100;
            bool res = BankAccount.Validate(startMoney);
            if (res)
            {
            var bankAccount = new SuperBankAccount (startMoney, "1234", "Кирилл К.", 3);
                bankAccount.ActionGood += WriteGood;
                bankAccount.ActionError += WriteBad;
                bankAccount.ActionError += WriteBadServer;
                bankAccount.Put("1234", 200M);
                bankAccount.Check("1234");
                bankAccount.Remove("1234", 50M);
                bankAccount.Check("1234");
                bankAccount.Check("12324");
                bankAccount.Check("12324");
                bankAccount.Check("12324");
                bankAccount.Check("12324");
                Console.WriteLine(bankAccount.Name);
            var answercheck=bankAccount.Check("1234");// можем посмотреь только с воводом нашего пинкода
                if (answercheck.Item1)
                {
                    Console.WriteLine($"Ваш баланс:{answercheck.Item2}");
                }
                var answer = bankAccount.Put("1234", 100M); // M- index decimal
                if (answer.Item1)
                {
                    Console.WriteLine($"На счет добавили: {answer.Item2}");
                    
                }
                answercheck = bankAccount.Check("1234");
                if (answercheck.Item1)
                {
                    Console.WriteLine($"Ваш баланс:{answercheck.Item2}");
                }
                //Console.WriteLine(answer.Item1);//здесь мы возвращаем условие, где итем 1-тру или фолс
                //Console.WriteLine(answer.Item2);//здесь мы возвращаем условие, где итем2-сумма
                var answerremove=bankAccount.Remove("1234", 20M);
                if (answerremove.Item1)
                {
                    Console.WriteLine($"Вы сняли:{answerremove.Item2}");
                }
                answercheck = bankAccount.Check("1234");
                if (answercheck.Item1)
                {
                    Console.WriteLine($"Ваш баланс:{answercheck.Item2}");
                }
                Console.WriteLine();


                answercheck = bankAccount.Check("12341");
                if (answercheck.Item1)
                {
                    Console.WriteLine($"Ваш баланс:{answercheck.Item2}");
                }
                Console.WriteLine();
                answercheck = bankAccount.Check("12341");
                if (answercheck.Item1)
                {
                    Console.WriteLine($"Ваш баланс:{answercheck.Item2}");
                }
                Console.WriteLine();
                answercheck = bankAccount.Check("12341");
                if (answercheck.Item1)
                {
                    Console.WriteLine($"Ваш баланс:{answercheck.Item2}");
                }
                answercheck = bankAccount.Check("12341");
                if (answercheck.Item1)
                {
                    Console.WriteLine($"Ваш баланс:{answercheck.Item2}");
                }
                Console.WriteLine();
                Console.WriteLine();

            }

            Console.ReadKey();

        }

        internal static void WriteGood(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void WriteBad(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void WriteBadServer(string error)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Server-{error}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
