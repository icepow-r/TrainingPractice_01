using System;
using System.Text;

namespace BIV_Task_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            string line;
            Console.WriteLine("Программа-повторитель. Для выхода введите 'exit'");
            while (true)
            {
                Console.Write("Введите слово: ");
                line = Console.ReadLine();
                if (line == "exit")
                {
                    Console.WriteLine("Завершаю выполнение...");
                    return;
                }
                else
                {
                    Console.WriteLine($"Мой ответ: {line}");
                }
            }
        }
    }
}
