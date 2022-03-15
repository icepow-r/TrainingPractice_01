using System;

namespace BIV_Task_04
{
    internal class Player
    {
        public bool active = true;
        int numberOfSpells = 6;
        int health, damage, armor;
        int currentSpell;
        int shadowSpirit = -1, demon = -1, mirror = -1;
        double damageModifier = 1.0d;
        bool immortal = false, damageBoost = false, reflectDamage = false;
        Random random = new Random();
        public Player()
        {
            health = random.Next(800, 1500);
            damage = random.Next(100, 250);
            armor = random.Next(50, 150);
        }


        public int Health { get => health; }
        public int Damage { get => damage; }
        public bool Immortal { get => immortal; set => immortal = value; }
        public int Armor { get => armor; }

        public void ShowSpells()
        {
            Console.WriteLine("1. Обычная атака – удар по боссу. Тут как повезёт - иногда сильный, иногда не очень...");
            Console.WriteLine("2. Рашамон – призывает теневого духа для усиления атаки (Отнимает 100 хп игроку)");
            Console.WriteLine("3. Зеркало-двойник. Забирает весь урон босса на себя и возвращает его обратно. Для подготовки нужно 2 хода.");
            Console.WriteLine("4. Межпространственный разлом – позволяет скрыться в разломе и восстановить 250 хп. Урон босса по вам не проходит");
            Console.WriteLine("5. Дьявольское заклятье – через ход призывается демон, удваивающий урон по боссу.");
            Console.WriteLine("6. Усилить щит – добавить 300 единиц брони, которая уменьшает урон по здоровью в половину.");
        }

        internal void Check()
        {
            if (Program.movesCounter == shadowSpirit)
            {
                Console.WriteLine("Теневой дух призван! В этот ход на 100 единиц урона больше...");
                damage += 100;
                damageBoost = true;
            }
            if (Program.movesCounter == demon)
            {
                Console.WriteLine("Сам демон пришёл на поле боя, в этот ход урон удвоен!!!");
                damageModifier = 2.0d;
            }
            if (mirror != -1)
            {
                if (mirror == Program.movesCounter + 2)
                {
                    Console.WriteLine("Продолжаем подготовку двойника...");
                    active = false;
                }
                if (mirror == Program.movesCounter)
                {
                    Console.WriteLine("Двойник призван! Атака босса вернётся ему же.");
                    active = true;
                    reflectDamage = true;
                    mirror = -1;
                }
            }
        }

        internal void ChooseOptions()
        {
            bool again = true;
            while (again)
            {

                if (int.TryParse(Console.ReadLine(), out int choose) && choose > -1 && choose < numberOfSpells + 1)
                {
                    if (choose == 0)
                    {
                        ShowSpells();
                        ShowOptions();
                    }
                    else
                    {
                        currentSpell = choose;
                        again = false;
                    }
                }
                else
                {
                    Console.WriteLine("Номера заклинаний перепутались и ход перешёл к боссу.");
                    again = false;
                }
            }
        }

        internal void ShowOptions()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Выберите номер заклинания (1-6) или посмотрите справку (0): ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UseSpell(Boss boss)
        {
            switch (currentSpell)
            {
                case 1:
                    var luck = random.Next(-20, 20);
                    Console.WriteLine($"Удар мечом по боссу. Неплохо, целых {damage * damageModifier + luck} единиц урона!");
                    boss.LoseHealth(damage * damageModifier + luck);
                    break;
                case 2:
                    Console.WriteLine("Вы потеряли 100 единиц здоровья.\nНо на следующий ход атака на 100 единиц больше!");
                    health -= 100;
                    shadowSpirit = Program.movesCounter + 2;
                    break;
                case 3:
                    Console.WriteLine("Призыв двойника сложный, этот и следующий ход все остальные действия заблокированы.");
                    mirror = Program.movesCounter + 4;
                    break;
                case 4:
                    immortal = true;
                    health += 250;
                    break;
                case 5:
                    demon = Program.movesCounter + 4;
                    break;
                case 6:
                    Console.WriteLine("Щит усилен. Добавлено 300 единиц брони.");
                    armor += 300;
                    break;

            }

            ResetDamage();
        }

        private void ResetDamage()
        {
            if (damageBoost)
            {
                damageBoost = false;
                damage -= 100;
            }
            damageModifier = 1.0d;
        }

        public void LoseHealth(double number, Boss boss)
        {
            var realDamage = Convert.ToInt32(Math.Floor(number));
            if (reflectDamage)
            {
                Console.WriteLine($"Атака отражена! Босс получает {realDamage} единиц урона!");
                boss.LoseHealth(realDamage);
            }
            else
            {
                if (armor - realDamage >= 0)
                {
                    health -= realDamage / 2;
                    armor -= realDamage;
                }
                else
                {
                    health -= (armor / 2 + realDamage - armor);
                    armor = 0;
                }
            }
        }
    }
}
