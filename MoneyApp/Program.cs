using System;

namespace MoneyApp
{
    // Класс для работы с денежными суммами
    class Money
    {
        // Поля для хранения рублей и копеек
        private long rubles; // Хранит рубли (целая часть суммы)
        private byte kopecks; // Хранит копейки (дробная часть, от 0 до 99)

        // Конструктор для создания суммы
        public Money(long rubles, byte kopecks)
        {
            // Нормализуем: если копеек больше 99, переводим их в рубли
            this.rubles = rubles + kopecks / 100; // Например, 150 копеек = 1 рубль + 50 копеек
            this.kopecks = (byte)(kopecks % 100); // Оставляем остаток (до 99)
        }

        // Метод для вывода суммы в формате "рубли,копейки"
        public override string ToString()
        {
            // :D2 обеспечивает две цифры для копеек (например, 05 вместо 5)
            return rubles + "," + kopecks.ToString("D2");
        }

        // Сложение двух сумм
        public static Money operator +(Money a, Money b)
        {
            // Складываем рубли
            long newRubles = a.rubles + b.rubles;
            // Складываем копейки
            byte newKopecks = (byte)(a.kopecks + b.kopecks);
            // Создаем новую сумму
            return new Money(newRubles, newKopecks);
        }

        // Вычитание двух сумм
        public static Money operator -(Money a, Money b)
        {
            // Вычитаем рубли
            long newRubles = a.rubles - b.rubles;
            byte newKopecks;
            // Если копеек не хватает, занимаем 1 рубль
            if (a.kopecks < b.kopecks)
            {
                newRubles--; // Уменьшаем рубли на 1
                newKopecks = (byte)(a.kopecks + 100 - b.kopecks);
            }
            else
            {
                newKopecks = (byte)(a.kopecks - b.kopecks);
            }
            // Создаем новую сумму
            return new Money(newRubles, newKopecks);
        }

        // Умножение двух сумм
        public static Money operator *(Money a, Money b)
        {
            // Переводим обе суммы в копейки для умножения
            double aKopecks = a.rubles * 100 + a.kopecks;
            double bKopecks = b.rubles * 100 + b.kopecks;
            // Умножаем
            double totalKopecks = aKopecks * bKopecks / 100; // Делим на 100, чтобы учесть копейки
            // Вычисляем новые рубли
            long newRubles = (long)(totalKopecks / 100);
            // Вычисляем новые копейки
            byte newKopecks = (byte)(totalKopecks % 100);
            // Создаем новую сумму
            return new Money(newRubles, newKopecks);
        }

        // Деление одной суммы на другую (результат — дробное число)
        public static double operator /(Money a, Money b)
        {
            // Переводим суммы в копейки для точного деления
            double aKopecks = a.rubles * 100 + a.kopecks;
            double bKopecks = b.rubles * 100 + b.kopecks;
            // Проверяем деление на ноль
            if (bKopecks == 0)
            {
                throw new Exception("Нельзя делить на нулевую сумму!");
            }
            // Возвращаем результат деления
            return aKopecks / bKopecks;
        }

        // Деление суммы на дробное число
        public static Money operator /(Money a, double number)
        {
            // Проверяем деление на ноль
            if (number == 0)
            {
                throw new Exception("Нельзя делить на ноль!");
            }
            // Переводим сумму в копейки
            double totalKopecks = (a.rubles * 100 + a.kopecks) / number;
            // Вычисляем новые рубли
            long newRubles = (long)(totalKopecks / 100);
            // Вычисляем новые копейки
            byte newKopecks = (byte)(totalKopecks % 100);
            // Создаем новую сумму
            return new Money(newRubles, newKopecks);
        }

        // Умножение суммы на дробное число
        public static Money operator *(Money a, double number)
        {
            // Переводим сумму в копейки
            double totalKopecks = (a.rubles * 100 + a.kopecks) * number;
            // Вычисляем новые рубли
            long newRubles = (long)(totalKopecks / 100);
            // Вычисляем новые копейки
            byte newKopecks = (byte)(totalKopecks % 100);
            // Создаем новую сумму
            return new Money(newRubles, newKopecks);
        }

        // Сравнение: равно
        public static bool operator ==(Money a, Money b)
        {
            // Проверяем, совпадают ли рубли и копейки
            return a.rubles == b.rubles && a.kopecks == b.kopecks;
        }

        // Сравнение: не равно
        public static bool operator !=(Money a, Money b)
        {
            // Противоположно сравнению на равенство
            return !(a == b);
        }

        // Сравнение: меньше
        public static bool operator <(Money a, Money b)
        {
            // Сначала сравниваем рубли
            if (a.rubles < b.rubles)
                return true;
            // Если рубли равны, сравниваем копейки
            if (a.rubles == b.rubles && a.kopecks < b.kopecks)
                return true;
            return false;
        }

        // Сравнение: больше
        public static bool operator >(Money a, Money b)
        {
            // Если b меньше a, то a больше b
            return b < a;
        }

        // Сравнение: меньше или равно
        public static bool operator <=(Money a, Money b)
        {
            // Либо меньше, либо равно
            return (a < b) || (a == b);
        }

        // Сравнение: больше или равно
        public static bool operator >=(Money a, Money b)
        {
            // Либо больше, либо равно
            return (a > b) || (a == b);
        }
    }

    class Program
    {
        // Метод для ввода рублей (string)
        static string GetRubles(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine(); // Принимаем любой ввод как строку
        }

        // Метод для безопасного ввода копеек (byte)
        static byte GetKopecks(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                // Проверяем, что введено число и оно в диапазоне 0–99
                if (int.TryParse(input, out int result) && result >= 0 && result <= 99)
                {
                    return (byte)result; // Возвращаем введенное число как byte
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите число от 0 до 99 (например, 50)!");
                }
            }
        }

        // Метод для безопасного ввода дробного числа (double)
        static double GetDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                // Проверяем, что введено корректное дробное число
                if (double.TryParse(input, out double result))
                {
                    return result; // Возвращаем введенное число
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите число (например, 2,5)!");
                }
            }
        }

        static void Main(string[] args)
        {
            // Приветственное сообщение
            Console.WriteLine("Программа для работы с двумя денежными суммами");

            // Запрашиваем первую сумму
            Console.WriteLine("Введите первую сумму:");
            string rublesInput1 = GetRubles("Рубли: "); // Ввод рублей как строка
            // Пытаемся преобразовать в long, если не удается — устанавливаем 0
            long rubles1 = long.TryParse(rublesInput1, out long result1) ? result1 : 0;
            byte kopecks1 = GetKopecks("Копейки: "); // Безопасный ввод копеек
            Money sum1 = new Money(rubles1, kopecks1); // Создаем первую сумму
            Console.WriteLine("Первая сумма: " + sum1);

            // Запрашиваем вторую сумму
            Console.WriteLine("Введите вторую сумму:");
            string rublesInput2 = GetRubles("Рубли: "); // Ввод рублей как строка
            // Пытаемся преобразовать в long, если не удается — устанавливаем 0
            long rubles2 = long.TryParse(rublesInput2, out long result2) ? result2 : 0;
            byte kopecks2 = GetKopecks("Копейки: "); // Безопасный ввод копеек
            Money sum2 = new Money(rubles2, kopecks2); // Создаем вторую сумму
            Console.WriteLine("Вторая сумма: " + sum2);

            // Запрашиваем дробные числа
            double divisor1 = GetDouble("Введите число для деления первой суммы (например, 2,5): ");
            double divisor2 = GetDouble("Введите число для деления второй суммы (например, 3): ");
            double multiplier = GetDouble("Введите число для умножения (например, 1.5): ");

            // Выполнение операций в требуемом порядке
            try
            {
                // Сложение
                Money sumResult = sum1 + sum2;
                Console.WriteLine("Сложение двух сумм: " + sum1 + " + " + sum2 + " = " + sumResult);

                // Вычитание
                Money subResult = sum1 - sum2;
                Console.WriteLine("Вычитание: " + sum1 + " - " + sum2 + " = " + subResult);

                // Деление сумм
                try
                {
                    double divSumResult = sum1 / sum2;
                    Console.WriteLine("Деление сумм: " + sum1 + " / " + sum2 + " = " + divSumResult);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Нельзя выполнить деление сумм: " + e.Message);
                }

                // Деление первой суммы на число
                try
                {
                    Money divResult1 = sum1 / divisor1;
                    Console.WriteLine("Деление первой суммы: " + sum1 + " / " + divisor1 + " = " + divResult1);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Нельзя выполнить деление первой суммы: " + e.Message);
                }

                // Деление второй суммы на число
                try
                {
                    Money divResult2 = sum2 / divisor2;
                    Console.WriteLine("Деление второй суммы: " + sum2 + " / " + divisor2 + " = " + divResult2);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Нельзя выполнить деление второй суммы: " + e.Message);
                }

                // Умножение первой суммы на число
                Money mulResult1 = sum1 * multiplier;
                Console.WriteLine("Умножение первой суммы на число: " + sum1 + " * " + multiplier + " = " + mulResult1);

                // Умножение второй суммы на число
                Money mulResult2 = sum2 * multiplier;
                Console.WriteLine("Умножение второй суммы на число: " + sum2 + " * " + multiplier + " = " + mulResult2);

                // Сравнения
                Console.WriteLine("Сравнение двух сумм:");
                Console.WriteLine(sum1 + " == " + sum2 + ": " + (sum1 == sum2));
                Console.WriteLine(sum1 + " != " + sum2 + ": " + (sum1 != sum2));
                Console.WriteLine(sum1 + " < " + sum2 + ": " + (sum1 < sum2));
                Console.WriteLine(sum1 + " > " + sum2 + ": " + (sum1 > sum2));
                Console.WriteLine(sum1 + " <= " + sum2 + ": " + (sum1 <= sum2));
                Console.WriteLine(sum1 + " >= " + sum2 + ": " + (sum1 >= sum2));
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка: " + e.Message);
            }

            // Ждем, чтобы пользователь увидел результат
            Console.WriteLine("Нажмите Enter, чтобы закрыть...");
            Console.ReadLine();
        }
    }
}
