using System;

namespace BIV_Task_06
{
    internal class Program
    {
        static string[][] fullName = new string[0][];
        static string[] positions = new string[0];
        static void Main()
        {
            Console.Clear();
            Console.InputEncoding = System.Text.Encoding.Unicode;
            string line;
            while (true)
            {
                ShowMenu();
                Console.Write("Выберите пункт меню: ");
                line = Console.ReadLine();
                switch (line)
                {
                    case "1":
                        AddFile();
                        break;
                    case "2":
                        ShowAllFiles();
                        break;
                    case "3":
                        DeleteFile();
                        break;
                    case "4":
                        SearchFileByLastName();
                        break;
                    case "5":
                        Console.WriteLine("Заверешние программы...");
                        return;
                    default:
                        Console.WriteLine("Вы ввели неверное значение.");
                        break;
                }
                Console.Write("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void AddFile()
        {
            Console.WriteLine("Добавление досье: ");
            Console.Write("Введите фамилию: ");
            var surname = Console.ReadLine();
            Console.Write("Введите имя: ");
            var name = Console.ReadLine();
            Console.Write("Введите отчество: ");
            var patronymic = Console.ReadLine();
            Console.Write("Введите должность: ");
            var position = Console.ReadLine();

            var temp = new string[fullName.Length + 1][];
            fullName.CopyTo(temp, 0);
            temp[^1] = new string[] { surname, name, patronymic };
            fullName = temp;

            var tempPosition = new string[positions.Length + 1];
            positions.CopyTo(tempPosition, 0);
            tempPosition[^1] = position;
            positions = tempPosition;
            Console.WriteLine("Досье успешно добавлено!");
        }

        private static void ShowAllFiles()
        {
            if (FilesIsEmpty())
            {
                return;
            }
            Console.WriteLine("Вывод всех досье: ");
            for (int i = 0; i < fullName.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {fullName[i][0]} {fullName[i][1]} {fullName[i][2]} - {positions[i]}");
            }

        }

        private static void DeleteFile()
        {
            if (FilesIsEmpty())
            {
                return;
            }

            int number;
            do
            {
                Console.Write("Введите номер досье для удаления (0 для отмены): ");
            } while (!int.TryParse(Console.ReadLine(), out number) || number < 0);

            if (number == 0)
            {
                return;
            }
            else if (number > fullName.Length)
            {
                Console.WriteLine("Досье с таким номером не существует.");
            }
            else
            {
                var temp = (string[][])fullName.Clone();
                var tempPos = (string[])positions.Clone();

                fullName = new string[fullName.Length - 1][];
                positions = new string[positions.Length - 1];

                var flag = 0;
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    if (number - 1 == i)
                    {
                        flag = 1;
                    }
                    fullName[i] = temp[i + flag];
                    positions[i] = tempPos[i + flag];
                }
            }
        }

        private static void SearchFileByLastName()
        {
            if (FilesIsEmpty())
            {
                return;
            }
            Console.Write("Введите фамилию для поиска: ");
            var surname = Console.ReadLine();
            var counter = 0;
            for (int i = 0; i < fullName.Length ; i++)
            {
                if (surname == fullName[i][0])
                {
                    counter++;
                    Console.WriteLine($"{counter + 1}. {fullName[i][0]} {fullName[i][1]} {fullName[i][2]} - {positions[i]}");
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("Сотрудников с фамилией " + surname + " нет" );
            }
            else
            {
                Console.WriteLine("Сотрудников с фамилией " + surname + " - " + counter.ToString());
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("1. Добавить досье");
            Console.WriteLine("2. Вывести все досье");
            Console.WriteLine("3. Удалить досье");
            Console.WriteLine("4. Поиск по фамилии");
            Console.WriteLine("5. Выход");
        }

        private static bool FilesIsEmpty()
        {
            if (fullName.Length == 0)
            {
                Console.WriteLine("Досье отсутствуют.");
                return true;
            }
            return false;
        }
    }
}
