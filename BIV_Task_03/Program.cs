using System;

namespace BIV_Task_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var superSecretPassword = "pkgh2022";
            var superSecretMessage = "Это тайное сообщение!!!";

            Console.WriteLine("Программа для просмотра тайного сообщения.");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Введите пароль: ");
                if (Console.ReadLine() == superSecretPassword)
                {
                    Console.WriteLine("Пароль верный!");
                    Console.WriteLine(superSecretMessage);
                    return;
                }
                else
                {
                    Console.WriteLine("Пароль неверный!");
                    Console.WriteLine($"Осталось попыток: {3 - i - 1}");
                }
            }
            Console.WriteLine("Попытки кончились. Приложение закрывается.");
        }
    }
}
