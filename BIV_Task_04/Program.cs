using System;

namespace BIV_Task_04
{
    internal class Program
    {
        internal static int movesCounter = -1;
        private static Random random = new Random();
        private static Boss boss = new Boss();
        private static Player player = new Player();
        private static bool playerTurn = random.NextDouble() >= 0.5;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            StartGame();
            GameMoves();
            EndGame();

            Console.ReadKey();
        }

        private static void EndGame()
        {
            if (player.Health < 0)
            {
                Console.WriteLine("Озверевший босс одолел вас. Быть может, повезёт в другой раз...");
            }
            else
            {
                Console.WriteLine("Поздравляем. Эта битва была легендарной и вы победили!!!");
            }
        }

        private static void StartGame()
        {
            Console.WriteLine("Игра 'Битва с боссом!'");
            Console.WriteLine("Правила игры: ");
            Console.WriteLine("События происходят пошагово, где игрок и босс дерутся друг с другом.");
            Console.WriteLine("У игрока на выбор есть 6 дейтсвий. Подробнее их можно изучить в справке во время игры.");
            Console.WriteLine("У босса и игрока есть показатели урона и здоровья.\nТакже игрок обладает бронёй, котоаря снижает урон по здоровью в два раза.");
            Console.WriteLine("Каждый восьмой ход босс звереет и наносит пятикратный урон игроку. Будьте бдительны!");
            Console.WriteLine("А побеждает тот, кто первым оставит противника без здоровья.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Для начала нажмите enter...");
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleKeyInfo start;
            do
            {
                start = Console.ReadKey();
                Console.Write("\r \r");
            } while (start.Key != ConsoleKey.Enter);
            Console.Clear();
            ShowStats();
            if (playerTurn)
            {
                Console.WriteLine("Удача улыбнулась. Ваш ход.");
            }
            else
            {
                Console.WriteLine("Битву начинает босс.");
            }
        }

        private static void GameMoves()
        {
            while (boss.Health >= 0 && player.Health >= 0)
            {
                movesCounter++;
                if (playerTurn)
                {

                    player.Check();
                    if (player.active)
                    {
                        if (player.Immortal)
                        {
                            player.Immortal = false;
                        }
                        player.ShowOptions();
                        player.ChooseOptions();
                        player.UseSpell(boss);
                    }
                }
                else
                {
                    boss.NewStage();
                    boss.Attack(player);


                }
                ShowStats();
                playerTurn = !playerTurn;
            }
        }

        static void ShowStats()
        {
            var playerStats = new string($"Игрок. Здоровье {player.Health}, атака {player.Damage}, броня {player.Armor}, бессмертие {player.Immortal}");
            var bossStats = new string($"Босс. Здоровье {boss.Health}, атака {boss.Damage}, стадия {boss.Stage}");
            int width, flag, diff;
            if (playerStats.Length > bossStats.Length)
            {
                width = playerStats.Length;
                flag = 0;
                diff = playerStats.Length - bossStats.Length;
            }
            else
            {
                width = bossStats.Length;
                flag = 1;
                diff = bossStats.Length - playerStats.Length;
            }

            Console.WriteLine(new string('-', width + 2));
            if (flag == 0)
            {
                Console.WriteLine('|' + playerStats + '|');
                Console.WriteLine('|' + bossStats + new string(' ', diff) + '|');
            }
            else
            {
                Console.WriteLine('|' + playerStats + new string(' ', diff) + '|');
                Console.WriteLine('|' + bossStats + '|');
            }
            Console.WriteLine(new string('-', width + 2));
        }
    }
}
