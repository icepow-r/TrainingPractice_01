using System;

namespace BIV_Task_01
{
    internal class Program
    {
        private const int cost = 15;
        static void Main()
        {
            int gold;
            do
            {
                Console.Write("Введите количество вашего золота: ");
            }
            while (!int.TryParse(Console.ReadLine(), out gold) || gold <= 0);
            var choose = string.Empty;
            var gems = 0;
            while (choose != "n")
            {
                Console.WriteLine($"Хотите купить кристаллы по {cost} золота?");
                Console.Write("Введите количество кристаллов для покупки: ");
                int temp;
                try
                {
                    temp = int.Parse(Console.ReadLine()); 
                    while (temp < 0)
                    {
                        throw new Exception();
                    }
                    while (temp * cost < gold)
                    {
                        gold -= temp * cost;
                        gems += temp;
                        break;
                    }
                    while (temp * cost >= gold)
                    {
                        Console.WriteLine("Вам не хватает золота.");
                        break;                   
                    }
                }
                catch (Exception)
                {
                   Console.WriteLine("Введено некорретное число кристаллов.");
                }
                    
                Console.WriteLine($"Ваш баланс: {gold} золота и {gems} кристаллов.");
                choose = string.Empty;
                while (choose != "y" && choose != "n")
                {
                    Console.Write("Продолжаем? (y/n): ");
                    choose = Console.ReadLine();
                }
            }
        }
    }
}
