using System;

namespace BIV_Task_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            Console.WriteLine("Программа для перемешивания массива.");
            int size;
            do
            {
                Console.Write("Введите размер массива: ");
            } while (!int.TryParse(Console.ReadLine(), out size) || size < 2);
            var array = new int[size];
            Console.WriteLine("Массив заполняется случайными числами от 100 до 1000.");
            Console.WriteLine("Исходный массив: ");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(100, 999);
                Console.Write(array[i] + " ");
            }
            for (int i = size - 1; i > 0; i--)
            {
                var randomIndex = random.Next(0, i);
                var temp = array[randomIndex];
                array[randomIndex] = array[i];
                array[i] = temp;
            }
            Console.WriteLine();
            Console.WriteLine("Перемешанный массив: ");
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
        }
    }
}
