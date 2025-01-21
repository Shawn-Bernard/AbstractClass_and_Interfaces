using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AbstactClass_And_Interfaces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Enemy enemy = new Enemy();
            HealingPotion healingPotion = new HealingPotion(3);

            //Player calls
            Console.ForegroundColor = ConsoleColor.Blue;
            CallAttack(player);
            player.Move();

            //Enemy calls
            Console.ForegroundColor = ConsoleColor.Red;
            CallAttack(enemy);
            enemy.Move();

            //Healing potion calls
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 3; i++)
            {
                healingPotion.Use();
                if (healingPotion.UsesLeft == 0)
                {
                    healingPotion.OnCollect();
                    healingPotion.Use();
                }
            }
            Console.ReadLine();
        }
        public static void CallAttack(GameEntity entity)
        {
            //Calls my player or Enemy attack method  
            entity.Attack();
        }
    }

    public abstract class GameEntity
    {
        int health;
        public int Health
        {
            get { return health; }
            set
            {
                if (value < 0) value = 0;
                if (value > 100) value = 100;
                health = value;
            }
        }
        //No need to override this because were using the base method
        public void Move()
        {
            Console.WriteLine($"IM ON THE MOVE");
        }
        public abstract void Attack();
    }

    //Will give an error if attack methods isn't in player
    public class Player : GameEntity
    {
        //Overriding this because were giving the method a new method
        public override void Attack()
        {
            Console.WriteLine("player wack attack");
        }
    }

    public class Enemy : GameEntity
    {
        public override void Attack()
        {
            Console.WriteLine("Enemy smack attack");
        }
    }

    public interface ICollectible
    {
        void OnCollect();
    }

    public interface IUsable
    {
        void Use();
        int UsesLeft { get; set; }

    }
    //When using interface always gotta add the methods used 
    public class HealingPotion : ICollectible, IUsable
    {
        public HealingPotion(int uses)
        {
            usesLeft = uses;
        }

        int usesLeft;

        public int UsesLeft
        {
            get => usesLeft;
            set
            {
                if (value < 0) value = 0;
                usesLeft = value;
            }
        }

        //Use public void OnCollect and not ICollectible.OnCollect()
        public void OnCollect()
        {
            usesLeft++;
            Console.WriteLine($"Potion found, only {usesLeft} left");
        }

        //Use public void use and not IUsable.Use()
        public void Use()
        {
            if (usesLeft > 0)
            {
                usesLeft--;
                Console.WriteLine($"Healing potion used, only {usesLeft} left");
            }
            else
            {
                Console.WriteLine("Out of healing potions");
            }
        }
    }
}
