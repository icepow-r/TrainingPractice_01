using System;

namespace BIV_Task_04
{
    internal class Boss
    {
        public Boss()
        {
            health = random.Next(1500, 2000);
            damage = random.Next(200, 250);
            stage = 1;
        }
        Random random = new Random();
        int health;
        int damage;
        int stage;
        double damageModifier;

        public int Health { get => health; }
        public int Damage { get => damage; }
        public double DamageModifier { get => damageModifier; set => damageModifier = value; }
        public int Stage { get => stage; set => stage = value; }

        public void LoseHealth(double number)
        {
            health -= Convert.ToInt32(Math.Floor(number));
        }

        public void Attack(Player player)
        {
            var luck = random.Next(-20, 20);
            Console.WriteLine($"Босс выполняет мощнейший удар на {Math.Floor(damage * damageModifier + luck)} единиц урона.");
            if (!player.Immortal)
            {
                player.LoseHealth(damage * damageModifier + luck, this);
            }
            else
            {
                Console.WriteLine("Вы успешно скрылись в разломе. Атака прошла мимо вас.");
            }
        }

        public void NewStage()
        {
            if (Stage == 8)
            {
                Stage = 1;
                DamageModifier = 5.0d;
            }
            else
            {
                damageModifier = 1.0d;
                Stage++;
            }
        }
    }
}
