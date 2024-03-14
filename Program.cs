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

            Console.WriteLine("Hello " + currentPlayer.name + ", you need to help me get my skibidi rizz back please!!!");
            Console.WriteLine("I lost it to some giga chad inside this dungeon, please help me get it back...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You begin walking into the dungeon when something hairy brushes your leg...");
            


            Console.ReadKey();
        }
    }
    class Player
    {
    public string name;
    public int coins = 0;
    public int health = 10;
    public int damage = 1;
    public int armorValue = 0;
    public int potion = 5;
    public int weaponValue = 1;
    }

    class Encounters
    {
        static Random rand = new Random();
        //Encounter Generic



        //Encounters

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

        public static void WomenEncounter()
        {
            Console.Clear();
            Console.WriteLine("A sexy baddie appears...");
            Console.ReadKey();
            Combat(false,"Sexy Baddie",1000,5000);
        }

        //Encounter Tools

        public static void RandomEncounter()
        {
            switch (rand.Next(0, 2))
            {
                case 0:
                    BasicEncounter();
                    break;
                case 1:
                    WomenEncounter();
                    break;
            }
        }
        
        //The Combat method first decides if these enounter is a special type or a random enemy.
        //If its not a random enemy we get the name, power level, and health of that enemy.
        public static void Combat(bool random, string name, int power, int health)
        {
            string n = ""; // name
            int p = 0; // Power
            int h = 0; // Health

            if(random) // Sets the Type of random mob and their stats
            {
                n = GetName();
                p = rand.Next(1,5);
                h = rand.Next(1, 8);
            }
            else
            {
                n = name;
                p = power;
                h = health;
            }
            while( h > 0 )
            {
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
                    //Attack
                    Console.WriteLine("You swing your weapon at "+n);
                    int damage = p - Program.currentPlayer.armorValue;
                    if(damage < 0)
                    {
                        damage = 0; //makes sure we dont heal the enemy if we subtract by a negetive number
                    }
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1,4); // upper number is excluded (1-3) <<< true weapon value
                    Console.WriteLine("You Deal: " + attack + " damage");
                    Console.WriteLine("You Take: " + damage + " damage from " + n);
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (decision.ToLower() == "d" || decision.ToLower() == "defend")
                {
                    //Defend
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
                    //Run
                    if (rand.Next(0,2) == 0) // picks random number between 0-1 <<<< Give players 50/50 shot of running away
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
                        //Go back to Store

                    }
                }
                else if (decision.ToLower() == "h" || decision.ToLower() == "heal")
                {
                    //Heal
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
                        int potionValue = 5;
                        Console.WriteLine("You healed for " +potionValue+ "health");
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
                    //On Death
                    Console.Clear();
                    Console.WriteLine("You have been Slain by "+n);
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("                                      ....:-+*###%%%%##*+=:...          ..    ......................");
                    Console.WriteLine("                                  ....-#@@@@@@@@@@@@@@@@@@@@@%=...     .      ......................");
                    Console.WriteLine("                              .....+@@@@@@@@@@@@@@@@@@@@@@@@@@@@@+:...           ...................");
                    Console.WriteLine("                            ....=@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=..          ...................");
                    Console.WriteLine("                         ....:#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#:..   ..   ..................");
                    Console.WriteLine("                        ...-%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#:....         .............");
                    Console.WriteLine("                        .:#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@+..         ...   ........");
                    Console.WriteLine("                      ..:%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#..        ..............");
                    Console.WriteLine("                     ..:@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#..       ..............");
                    Console.WriteLine("       ...           ..%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=..  .... .............");
                    Console.WriteLine("     ..*@@@#:...   ...-@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#...  .....-#@@*:..... ");
                    Console.WriteLine("    ..#@@@@@@@%-..  ..#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%-.. ...+@@@@@@@@:..   ");
                    Console.WriteLine("    .*@@@@@@@@@@=.. .:#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@+....:#@@@@@@@@@#..  .");
                    Console.WriteLine("  ..*@@@@@@@@@@@@+...:%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@*...-%@@@@@@@@@@@*... ");
                    Console.WriteLine("..+@@@@@@@@@@@@@@@@:.=@@@@@@%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#@@@@@@*..*@@@@@@@@@@@@@@@=..");
                    Console.WriteLine(":@@@@@@@@@@@@@@@@@@@*=@@@@@@@*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#@@@@@@*=@@@@@@@@@@@@@@@@@@%:");
                    Console.WriteLine("+@@@@@@@@@@@@@@@@@@@*=@@@@@@%=@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@##@@@@@@*+@@@@@@@@@@@@@@@@@@@+");
                    Console.WriteLine(".#@@@@@@@@@@@@@@@@@@#=@@@@@*%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@+@@@@@**@@@@@@@@@@@@@@@@@@%:");
                    Console.WriteLine(".:#@@@@@@@@@@@@@@@@@%-#@@@#%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%*@@@%=#@@@@@@@@@@@@@@@@@@-.");
                    Console.WriteLine("..:%@@@@@@@@@@@@@@@@@++@@%*@@@@@@@@@%#*#@@@@@@@@@@@@@@@@@@@@%##%@@@@@@@@@##@@#=@@@@@@@@@@@@@@@@@@=..");
                    Console.WriteLine("....-+*#####%@@@@@@@@#-@@##@@@%=:........:+@@@@@@@@@@@@@@*-........:-#@@@#*@@-*@@@@@@@@@@@@@@@%+:.. ");
                    Console.WriteLine("    ..............:+@%-%@*@@@*..          ..=@@@@@@@@@@+...        ...+@@@*%@-#@#-...............   ");
                    Console.WriteLine("                   ....+%+@@@..             .-%@@@@@@@+..           ...%@@+%#.....                  ");
                    Console.WriteLine("                     ...:*@@#.             ..=@@@@@@@@+.            ...#@@#:..                      ");
                    Console.WriteLine("                     ..:+@@@#.             ..*@@@@@@@@#:.            ..*@@@*:..                     ");
                    Console.WriteLine("                     .*@@@@@@.             .*@@@@@@@@@@#..           ..%@@@@@#..                    ");
                    Console.WriteLine("                     .=@@@@@@+..    .....:+@@@@*.=+.*@@@@*:....    ...=@@@@@@*..                    ");
                    Console.WriteLine("                     ..*@@@@@@*:.....-+#@@@@@@-..=+..:@@@@@@#+-.....:+@@@@@@#..                     ");
                    Console.WriteLine("                      ..%@@@@@@@@@@@@@@@@@@@@=...=+...=@@@@@@@@@@@@@@@@@@@@@..                      ");
                    Console.WriteLine("                        :@@@@@@@@@@@@@@@@@@@#.. .**. ..*@@@@@@@@@@@@@@@@@@@-..                      ");
                    Console.WriteLine("                        ..+%@@@@@@@@@@@@@@@@*. ..%@:  .*@@@@@@@@@@@@@@@@@+..                        ");
                    Console.WriteLine("                         ...:+%@@%%@@@@@@@@@#:..:@@-..:#@@@@@@@@@@@@@%+:...                         ");
                    Console.WriteLine("                         ....-%%%#...-@@@@@@@@=.@@@@.-%@@@@@@@=...#%%%-...                          ");
                    Console.WriteLine("                      ...:*@*-@@@@...-@@@@@@@@@@@@@@@@@@@@@@@@+...@@@@=+@*.....                     ");
                    Console.WriteLine("               ......:+@@@@@:*@@@@+..:%%@@@@@@@@@@@@@@@@@@@@@@-..-@@@@#.@@@@%=:......               ");
                    Console.WriteLine("   ..........:-++*#@@@@@@@@@.#@@@@@+:.#***@#@@@@@@@@@@@@%@#*##:.=%@@@@@.@@@@@@@@@#*++-:...........  ");
                    Console.WriteLine("  .=%@@@@@@@@@@@@@@@@@@@@@@@:+@@@@@@@+%=#@@@#%**@%%%+*#*@@%#+%*@@@@@@@#.@@@@@@@@@@@@@@@@@@@@@@@%=...");
                    Console.WriteLine("..*@@@@@@@@@@@@@@@@@@@@@@@@@@:#@@@@@@@#@#=@@%@#@@**@@=@#@@=*%#@@@@@@@#:%@@@@@@@@@@@@@@@@@@@@@@@@@#:.");
                    Console.WriteLine(".-@@@@@@@@@@@@@@@@@@@@@@@@@@*..+@@@@@@@%#%#*%@#@@#*@@+@@*%%@%@@@@@@@+..*%@@@@@@@@@@@@@@@@@@@@@@@@@+.");
                    Console.WriteLine(".:#@@@@@@@@@@@@@@@@@@@@@@#-..  .*@@@@@@@@#%*@###@**@+%%@%%%@@@@@@@@#.....:*@@@@@@@@@@@@@@@@@@@@@@@-.");
                    Console.WriteLine(" .-%@@@@@@@@@@@@@@@@@@@=....   ..%@@@@@@@@@@%*%*@#*@:@#@%@@@@@@@@@@..     ..-%@@@@@@@@@@@@@@@@@@@*..");
                    Console.WriteLine("  ...%@@@@@@@@@@@@@@%-.         ..@@@@@@@@@@@@#%*#*#@#@@@@@@@@@@@@-.        ...*@@@@@@@@@@@@@@@-... ");
                    Console.WriteLine("    .=@@@@@@@@@@@@%-...           :%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=.         .....#@@@@@@@@@@@@#..   ");
                    Console.WriteLine("     :@@@@@@@@@@@+..              ..+@@@@@@@@@@@@@@@@@@@@@@@@@@#-.              ..-%@@@@@@@@@@=..   ");
                    Console.WriteLine("     .-@@@@@@@@%-..                ...*@@@@@@@@@@@@@@@@@@@@@@%:...               ...*@@@@@@@@*..    ");
                    Console.WriteLine("        ..::::...                     ..=%@@@@@@@@@@@@@@@@@#:..                     ...::::....     ");
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("Press Enter to quit...");
                    System.Environment.Exit(0);
                }
                Console.ReadKey();
            }
            int coin = rand.Next(10, 50);
            Console.WriteLine("You defeated "+n);
            Console.Clear();
            Console.WriteLine("You Gained: "+coin+ " Gold Coins");
            Program.currentPlayer.coins += coin;
            Console.ReadKey();
        }

        public static string GetName()
        {
            switch (rand.Next(0, 4))
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
            }
            return "Goblin";
        }
    }
}

// ------------------ CITATIONS ------------------
// https://www.c-sharpcorner.com/article/c-sharp-list/
// https://learn.microsoft.com/en-us/dotnet/csharp/
// https://www.w3schools.com/cs/index.php
// https://youtu.be/wxznTygnRfQ?si=36g79w0njOLUFKzr
// -----------------------------------------------
