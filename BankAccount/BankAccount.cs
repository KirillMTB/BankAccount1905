using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class BankAccount
    {
        internal string Name { get; private set; } 
        protected string _pinCode;

        protected decimal _money;

        
        public BankAccount(decimal money, string pin, string name) //чтобы был обязательный параметр это пинкод


        {
            _pinCode = pin;
            _money = money;
            Name = name;
        }

        internal static bool Validate(decimal money) // проверка на корректность money, чтоб не было минуса, добавляем static, чтоб можно было вызывать метод ко всему классу, как статичный!!!!
        { 
        //   if (money>=0) можно записывать так, условием, а можно короче, как ниже
        //    {
        //        return true;
        //    }
        return money>=0;
        }

        internal virtual (bool, int) Remove(string pin, decimal sum)
        {
            if (pin == _pinCode)
            {
                if (sum > 0)
                {   if (sum<0)
                    {
                        Console.WriteLine($"Недостаточно средств на счете");
                        return (true, 0);
                    }
                    else
                    {
                        _money -= sum;
                        Console.WriteLine($"Со счета сняли: {sum}, Ваш баланс {_money}");
                        return (true, (int)sum);
                    }
                   
                }
                else
                {
                    Console.WriteLine($"Неизвестная ошибка");
                    return (true, 0);
                }
            }
            else
            {
                Console.WriteLine($"Введенный Вами пин-код неверный!");
                return (false, 0);
            }
        }

        internal virtual (bool, int) Put(string pin, decimal sum) //в скобочках кортежи, вместо воид
        {
            if (pin==_pinCode)
            {
                if (sum>0)
                {
                    _money += sum;
                    Console.WriteLine($"На счет добавили: {sum}, Ваш баланс {_money}");
                    return (true, (int)sum);//здесь мы возвращаем условие, где итем 1-тру или фолс, итем2-сумма
                }
                else
                {
                    Console.WriteLine($"Неизвестная ошибка");
                    return (true, 0);//здесь мы возвращаем условие, где итем 1-тру или фолс, итем2-сумма
                }
            }
            else
            {
                Console.WriteLine($"Введенный Вами пин-код неверный!");
                return (false, 0);//здесь мы возвращаем условие, где итем 1-тру или фолс, итем2-сумма
            }
        }

        internal virtual (bool, int) Check(string pin)
        {
            if (pin==_pinCode)
            {
                Console.WriteLine($"Сумма на счете: { _money}");
                return (true, (int)_money);
            }
            else
            {
                //throw new Exception("Введенный Вами пин-код неверный!");
                Console.WriteLine($"Введенный Вами пин-код неверный!");
                return (false, 0);
            }
        }
    }
}
