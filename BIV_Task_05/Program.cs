using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BIV_Task_05
{
    internal class Program
    {
        static string[] file;
        static char[,] map;
        static List<Enemy> enemies;
        static Player player;
        static int collectedFood = 0, targetFood;
        static readonly Random rand = new();

        struct Player
        {
            public int X, Y, Vx, Vy;
        }
        struct Enemy
        {
            public char Symbol;
            public int X, Y, Vx, Vy;
            public bool isEmptyField;
        }

        static void Main()
        {
            ChooseMap();
            DrawMap();
            var stopWatch = new Stopwatch();
            var playerSW = new Stopwatch();
            playerSW.Start();
            while (true)
            {
                stopWatch.Restart();
                while (stopWatch.ElapsedMilliseconds <= 1000)
                {
                    if (UserInput() && playerSW.ElapsedMilliseconds >= 200)
                    {
                        playerSW.Restart();
                        ChangePlayerCoords();
                    }
                    DrawMap();
                }
                ChangeEnemyCoords();
            }
        }

        private static void ChooseMap()
        {
            bool correct = false;
            
            string name;
            do
            {
                Console.Clear();
                Console.WriteLine("Список карт:");
                var files = Directory.GetFiles("maps\\");
                foreach (var file in files)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }
                Console.Write("Введите имя карты: ");
                name = Console.ReadLine();
                foreach (var file in files)
                {
                    if (Path.GetFileName(file) == name)
                    {
                        correct = true;
                    }
                }
            } while (!correct);
            file = File.ReadAllLines("maps\\" + name);
            map = ReadMap();
            Console.CursorVisible = false;
            Console.Clear();
        }

        private static bool UserInput()
        {
            if (!Console.KeyAvailable)
                return false;

            player.Vx = 0;
            player.Vy = 0;

            var move = Console.ReadKey(true).Key;
            switch (move)
            {
                case ConsoleKey.UpArrow:
                    player.Vx = -1;
                    return true;
                case ConsoleKey.DownArrow:
                    player.Vx = +1;
                    return true;
                case ConsoleKey.LeftArrow:
                    player.Vy = -1;
                    return true;
                case ConsoleKey.RightArrow:
                    player.Vy = +1;
                    return true;
            }
            return false;
        }

        private static void ChangePlayerCoords()
        {
            if (map[player.X + player.Vx, player.Y + player.Vy] == '█')
            {
                player.Vx = 0;
                player.Vy = 0;
            }
            map[player.X, player.Y] = ' ';
            player.X += player.Vx;
            player.Y += player.Vy;
            foreach (var item in enemies)
            {
                if (player.X == item.X && player.Y == item.Y)
                {
                    Defeat();
                }
            }
            if (map[player.X, player.Y] == '.')
            {
                collectedFood++;
                if (collectedFood == targetFood)
                {
                    Console.Clear();
                    Console.SetCursorPosition(13, 5);

                    Console.WriteLine("Вы выиграли!");
                    Console.SetCursorPosition(13, 6);
                    Console.WriteLine("Ваш счёт: {0}", collectedFood);
                    Console.SetCursorPosition(13, 7);
                    Console.WriteLine("Нажмите q для выхода");
                    while (Console.ReadKey(true).Key != ConsoleKey.Q);
                    Environment.Exit(0);
                }
            }
            map[player.X, player.Y] = '☺';
        }

        private static void Defeat()
        {
            Console.Clear();
            Console.SetCursorPosition(13, 5);

            Console.WriteLine("Вы проиграли!");
            Console.SetCursorPosition(13, 6);
            Console.WriteLine("Ваш счёт: {0}", collectedFood);
            Console.SetCursorPosition(13, 7);
            Console.WriteLine("Нажмите q для выхода");
            while (Console.ReadKey(true).Key != ConsoleKey.Q) ;
            Environment.Exit(0);
        }

        private static void ChangeEnemyCoords()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                var directions = new List<int>
                {
                    1,
                    2,
                    3,
                    4,
                };
                var currentEnemy = enemies[i];
                while (directions.Count != 0)
                {
                    var index = rand.Next(directions.Count);
                    switch (directions[index])
                    {
                        case 1:
                            currentEnemy.Vy = -1;
                            break;
                        case 2:
                            currentEnemy.Vy = 1;
                            break;
                        case 3:
                            currentEnemy.Vx = -1;
                            break;
                        case 4:
                            currentEnemy.Vx = 1;
                            break;
                    }
                    if (map[currentEnemy.X + currentEnemy.Vx, currentEnemy.Y + currentEnemy.Vy] != ' ' &&
                        map[currentEnemy.X + currentEnemy.Vx, currentEnemy.Y + currentEnemy.Vy] != '.' &&
                        map[currentEnemy.X + currentEnemy.Vx, currentEnemy.Y + currentEnemy.Vy] != '☺')
                    {
                        currentEnemy.Vx = 0;
                        currentEnemy.Vy = 0;
                        directions.RemoveAt(index);
                    }
                    else
                    {
                        if (map[currentEnemy.X + currentEnemy.Vx, currentEnemy.Y + currentEnemy.Vy] == '☺')
                        {
                            Defeat();
                        }
                        if (currentEnemy.isEmptyField)
                        {
                            map[currentEnemy.X, currentEnemy.Y] = ' ';
                        }
                        else
                        {
                            map[currentEnemy.X, currentEnemy.Y] = '.';
                        }
                        currentEnemy.X += currentEnemy.Vx;
                        currentEnemy.Y += currentEnemy.Vy;
                        currentEnemy.isEmptyField = map[currentEnemy.X, currentEnemy.Y] != '.';
                        map[currentEnemy.X, currentEnemy.Y] = currentEnemy.Symbol;
                        currentEnemy.Vx = 0;
                        currentEnemy.Vy = 0;
                        enemies[i] = currentEnemy;
                        break;
                    }
                }
            }
        }



        private static void DrawMap()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
            var percent = Convert.ToInt32(Math.Floor(collectedFood / (targetFood / 100d)));
            var bar = new StringBuilder("[..........]");
            for (int i = 0; i < percent / 10; i++)
            {
                bar[i + 1] = '#';
            }
            Console.WriteLine("Количество собранной еды: {0}%", percent);
            Console.WriteLine(bar);
        }

        private static char[,] ReadMap()
        {
            targetFood = 0;
            enemies = new List<Enemy>();
            char[,] temp = new char[file.Length, file[0].Length];
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[0].Length; j++)
                {
                    switch (file[i][j])
                    {
                        case '█':
                            temp[i, j] = '█';
                            break;
                        case ' ':
                            temp[i, j] = '.';
                            targetFood++;
                            break;
                        case 'p':
                            temp[i, j] = '☺';
                            player.X = i;
                            player.Y = j;
                            break;
                        case 'e':
                            var enemy = new Enemy();
                            enemy.X = i;
                            enemy.Y = j;
                            enemy.Symbol = (char)(0x2460 + enemies.Count);
                            enemy.isEmptyField = true;
                            enemies.Add(enemy);
                            temp[i, j] = enemy.Symbol;
                            break;
                    }
                }
            };
            return temp;
        }
    }
}
