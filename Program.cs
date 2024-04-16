using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
   class Program
    {
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;
        static void Main(string[] args)
        {
            
            Start();
            Encounters.FirstEncounter();
            while (mainLoop)
            {
                Encounters.RandomEncounter();
            }
        }

        static void Start()
        {
            Console.WriteLine("***The Dungeon***");
            Console.WriteLine("====================");
            Console.Write("Enter your name: ");
            currentPlayer.name = Console.ReadLine();

            while (currentPlayer.name == "")
            {
                Console.Clear();
                Console.Write("I said... WHATS YOUR NAME: ");
                currentPlayer.name = Console.ReadLine();
                Console.Clear();
            }

            Console.WriteLine("Hello " + currentPlayer.name + ", you need to help me get my cat back!");
            Console.WriteLine("She's in that cave just over there...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You begin walking into the dungeon when something hairy brushes your leg...");

            Console.ReadKey();
        }
    }
    public class Player 
    {
        Random rand = new Random();

        public string name;
        public int coins = 0;
        public int health = 10;
        public int damage = 1;
        public int armorValue = 0;
        public int potion = 5;
        public int weaponValue = 1;
    
        public int mods = 0; 

        //Upper lower calculations made with the help of third party resources.
         public int GetHealth() 
         {
             int upper = (2 * mods + 5);
             int lower = (mods + 2);
             return rand.Next(lower, upper);
         }
         public int GetPower() 
         {
             int upper = (2 * mods + 2);
             int lower = (mods + 1);
             return rand.Next(lower, upper);
         }
         public int GetCoins()
         {
             int upper = (15 * mods + 50);
             int lower = (10 * mods + 10);
             return rand.Next(lower, upper);
         }
    }

    public class Encounters
    {
        static Random rand = new Random();

        public static void FirstEncounter()
        {
            Console.WriteLine("You look down... A Giant Spider! ...");
            Console.ReadKey();
            Combat(false, "Giant Spider", 1, 4);
        }

        public static void BasicEncounter()
        {
            Console.Clear();
            Console.WriteLine("TIME FOR BATTLE...");
            Console.ReadKey();
            Combat(true, "",0,0);
        }

        public static void DemonEncounter()
        {
            Console.Clear();
            Console.WriteLine("A Dark Demon appears...");
            Console.ReadKey();
            Combat(false,"Dark Demon",5,8);
        }
        
        public static void EyeOfHatred()
        {
            Console.Clear();
            Console.WriteLine("A GIANT EYE...");
            Console.ReadKey();
            Combat(false,"EYE OF HATRED",7,15);
        }
        
        public static void AnorTheGreat()
        {
            Console.Clear();
            Console.WriteLine("Some guy named Anor...");
            Console.ReadKey();
            Combat(false,"ANOR THE GREAT",10,20);
        }

        public static void RandomEncounter()
        {
            switch (rand.Next(0, 3))
            {
                case 0:
                    BasicEncounter();
                    break;
                case 1:
                    DemonEncounter();
                    break;
                case 2:
                    EyeOfHatred();
                    break;
                case 3:
                    AnorTheGreat();
                    break;
            }
        }
        public static void Combat(bool random, string name, int power, int health)
        {
            string n = ""; 
            int p = 0; 
            int h = 0; 

            if(random) 
            {
                n = GetName();
                p = Program.currentPlayer.GetPower();
                h = Program.currentPlayer.GetHealth();
            }
            else
            {
                n = name;
                p = power;
                h = health;
            }
            while( h > 0 )
            {
                // The loop below was made with the help of third party resources.
                Console.Clear();
                Console.WriteLine(n);
                Console.WriteLine(p + "/" + h);
                Console.WriteLine("======================");
                Console.WriteLine("| (A)ttack  (D)efend |");
                Console.WriteLine("|   (R)un     (H)eal |");
                Console.WriteLine("======================");
                Console.WriteLine(" Potions: " + Program.currentPlayer.potion+ "  Health: "+ Program.currentPlayer.health);
                string decision = Console.ReadLine();
                if (decision.ToLower() == "a" || decision.ToLower() == "attack")
                {
                    Console.WriteLine("You swing your weapon at "+n);
                    int damage = p - Program.currentPlayer.armorValue;
                    if(damage < 0)
                    {
                        damage = 0;
                    }
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1,4); 
                    Console.WriteLine("You Deal: " + attack + " damage");
                    Console.WriteLine("You Take: " + damage + " damage from " + n);
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (decision.ToLower() == "d" || decision.ToLower() == "defend")
                {
                    Console.WriteLine("You brace yourself against " + n);
                    int damage = (p/4) - Program.currentPlayer.armorValue;
                    if(damage < 0)
                    {
                        damage = 0;
                    }
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) / 2;
                    Console.WriteLine("You Deal: " + attack + " damage");
                    Console.WriteLine("You Take: " + damage + " damage from " + n);
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (decision.ToLower() == "r" || decision.ToLower() == "run")
                {
                    if (rand.Next(0,2) == 0)
                    {
                        Console.WriteLine("You failed to run from the "+n+", its attack hits you in the back...");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        Console.WriteLine("You Lose " + damage + " health and were unable to escape.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("You run away like a coward from the " +n);
                        Console.ReadKey();
                        shop.LoadShop(Program.currentPlayer);
                    }
                }
                else if (decision.ToLower() == "h" || decision.ToLower() == "heal")
                {
                    if(Program.currentPlayer.potion == 0)
                    {
                        Console.WriteLine("Bro tried to heal with nothing XD! " + "You dont have any potions");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        Console.WriteLine("The "+n+ "strikes you for " +damage+"health");
                    }
                    else
                    {
                        int potionValue = 3;
                        Console.WriteLine("You healed for " +potionValue+ " health");
                        Program.currentPlayer.health += potionValue;
                        Console.WriteLine("As you were healing, the "+n+" attacked you!");
                        int damage = (p / 2) - Program.currentPlayer.armorValue;
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        Console.WriteLine("You lose "+damage+" health");
                    }
                    Console.ReadLine();
                }

                if (Program.currentPlayer.health <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("You have been Slain by "+n);
                    Console.WriteLine("Press Enter to quit...");
                    Console.ReadKey();
                    System.Environment.Exit(0);
                }
                Console.ReadKey();
            }
            int coin = Program.currentPlayer.GetCoins();
            Console.WriteLine("You defeated "+n);
            Console.Clear();
            Console.WriteLine("You Gained: "+coin+ " Gold Coins");
            Program.currentPlayer.coins += coin;
            Console.ReadKey();
        }
        public static string GetName()
        {
            switch (rand.Next(0, 5))
            {
                case 0:
                    return "Giant Spider";
                    break;
                case 1:
                    return "Big Man";
                case 2:
                    return "Skeleton";
                case 3:
                    return "Warden";
                case 4:
                    return "Bat";
            }
            return "Goblin";
        }
    }

    public class shop
    {
        public static void LoadShop(Player player)
        {
            RunShop(player);
        }
        public static void RunShop(Player player) //shop was made with the help of third party resources.
        {
            int potionPrice;
            int armorPrice;
            int weaponPrice;
            int difficultyPrice;
            
            while (true)
            {
                potionPrice = 20 + 10 * player.mods;
                armorPrice = 100 * player.armorValue + 1;
                weaponPrice = 100 * (player.weaponValue);
                difficultyPrice = 300 + 100 * player.mods;
                Console.Clear();
               Console.WriteLine("<<<<<<<<<*SHOP*>>>>>>>>>>");
               Console.WriteLine("=========================");
               Console.WriteLine("(W)eapons:             $" + weaponPrice);
               Console.WriteLine("------------------------");
               Console.WriteLine("(A)rmor:               $" + armorPrice); 
               Console.WriteLine("------------------------");
               Console.WriteLine("(P)otions:             $" + potionPrice);
               Console.WriteLine("------------------------");
               Console.WriteLine("(D)Mods:               $" + difficultyPrice);
               Console.WriteLine("========================="); 
               Console.WriteLine("");
               Console.WriteLine("");
               Console.WriteLine("<<<<<<<<<*STATS*>>>>>>>>>>");
               Console.WriteLine("=========================");
               Console.WriteLine("Current Health: " + player.health);
               Console.WriteLine("Coins: " + player.coins);
               Console.WriteLine("Attack: " + player.weaponValue);
               Console.WriteLine("Defense: " + player.armorValue);
               Console.WriteLine("Potions: " + player.potion);
               Console.WriteLine("Difficulty mods: " + player.mods);
               Console.WriteLine("========================="); 
               
               string input = Console.ReadLine().ToLower();
               if (input == "w" || input == "weapon")
               {
                   Buy("weapon", weaponPrice, player);
                   Console.Clear();
               }
               else if (input == "a" || input == "armor")
               {
                   Buy("armor", armorPrice, player);
                   Console.Clear();
               }
               else if (input == "p" || input == "potion")
               {
                   Buy("potion", potionPrice, player);
                   Console.Clear();
               }
               else if (input == "d" || input == "mod")
               {
                   Buy("mod", difficultyPrice, player);
                   Console.Clear();
               }
               else if (input == "e" || input == "exit")
               {
                   break;
               }
            }

            static void Buy(string item, int cost, Player player)
            {
                if (player.coins >= cost) 
                {
                    if (item == "weapon")
                    {
                        player.weaponValue++;
                    }
                    else if (item == "armor")
                    {
                        player.armorValue++;
                    }
                    else if (item == "potion")
                    {
                        player.potion++;
                    }
                    else if (item == "armor")
                    {
                        player.mods++;
                    }
                    player.coins -= cost;
                }
                else
                {
                    Console.WriteLine("You dont have enough Coins.");
                    Console.ReadKey();
                }
            }
            
            
        }
    }
}

// ------------------ CITATIONS ------------------
// https://www.c-sharpcorner.com/article/c-sharp-list/
// https://learn.microsoft.com/en-us/dotnet/csharp/
// https://www.w3schools.com/cs/index.php
// https://youtu.be/wxznTygnRfQ?si=36g79w0njOLUFKzr
// EnderUnknown
// -----------------------------------------------
