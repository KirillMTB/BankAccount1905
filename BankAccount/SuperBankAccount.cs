using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{

    internal class SuperBankAccount : BankAccount
    {
        protected int _count;
        public event Action<string> ActionGood;//sobytie
        public event Action<string> ActionError;
        public SuperBankAccount(decimal money, string pin, string name, int count) : base(money, pin, name) //чтобы был обязательный параметр это пинкод


        {
            _count = count;

        }

        internal static bool Validate(decimal money) // проверка на корректность money, чтоб не было минуса, добавляем static, чтоб можно было вызывать метод ко всему классу, как статичный!!!!
        {
            //   if (money>=0) можно записывать так, условием, а можно короче, как ниже
            //    {
            //        return true;
            //    }
            return money >= 0;
        }

        internal override (bool, int) Remove(string pin, decimal sum)
        {   if (_count > 0)
            {
                if (pin == _pinCode)
                {
                    if (sum > 0)
                    {
                        if (sum < 0)
                        {
                            ActionGood?.Invoke($"Недостаточно средств на счете");
                            return (true, 0);
                        }
                        else
                        {
                            _money -= sum;

                            return (true, (int)sum);
                        }

                    }
                    else
                    {
                        ActionError?.Invoke($"Неизвестная ошибка");
                        return (true, 0);
                    }
                }
                else
                {
                    ActionError?.Invoke($"Введенный Вами пин-код неверный! Осталось {_count} попыток");
                    _count--;// обязательно перед ретурном ставить
                    return (false, 0);
                }
            }
            else
            {
                ActionError?.Invoke("Ваш счет заблокирован!");
                return (false,0);
            }
        }

        internal override (bool, int) Put(string pin, decimal sum) //в скобочках кортежи, вместо воид
        {
            if (_count>0)
            { if (pin == _pinCode)
            {
                if (sum > 0)
                {
                    _money += sum;
                    ActionGood?.Invoke($"На счет добавили: {sum}, Ваш баланс {_money}");
                    return (true, (int)sum);//здесь мы возвращаем условие, где итем 1-тру или фолс, итем2-сумма
                }
                else
                {
                        ActionError?.Invoke($"Неизвестная ошибка");
                    return (true, 0);//здесь мы возвращаем условие, где итем 1-тру или фолс, итем2-сумма
                }
            }
            else
            {
                    ActionError?.Invoke($"Введенный Вами пин-код неверный! Осталось {_count} попыток");
                _count--;
                return (false, 0);//здесь мы возвращаем условие, где итем 1-тру или фолс, итем2-сумма
            } }

            else
            {
                ActionError?.Invoke("Ваш счет заблокирован!");
                return (false, 0);
            }
        }

        internal override (bool, int) Check(string pin)
        {
            if (_count>0)
            { if (pin == _pinCode)
            {
                ActionGood?.Invoke($"Сумма на счете: {_money}");
                return (true, (int)_money);
            }
            else
            {
                    //throw new Exception("Введенный Вами пин-код неверный!");
                ActionError?.Invoke($"Введенный Вами пин-код неверный! Осталось {_count} попыток");
                _count--;
                return (false, 0);

            } }

            else {
                ActionError?.Invoke("Ваш счет заблокирован!");
                return (false, 0);
            }
            
        }
    }
}
