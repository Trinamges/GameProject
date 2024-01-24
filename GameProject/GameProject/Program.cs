
using GameProject;
using System.Reflection.Metadata.Ecma335;
public class Program
{
    public static int level = 1;
    public static int gold = 10;
    public static int exp = 0;
    public static int energyPoints = 5;
    public static int energyPointsMax = 5;
    public static int bonusDmg = 0;
    public static string weapon = "Wooden Sword";
    public static int healthPointsMax = 5 + level*5;
    public static int healthPoints = healthPointsMax;
    public static int herbCount = 4;
    public static int ironCount = 10;
    public static int potionCount = 1;
    public static void Main(String[] args)
    {
        // Intro
        Console.WriteLine("Welcome to Adventure Game!");
        Console.WriteLine("You are an adventurer who has just begun their journey.");
        Console.WriteLine("As a Level 1 Adventurer, you have 5 Stamina, as you level up, you will gain 1 extra stamina.");
        Console.WriteLine("As you explore, you may fight monsters, pick up loot and become a more powerful adventurer!");
        Program.Menu();
    }

    public static void Menu()
    {
        while(true)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("\n-------------------------------------  " +
                "\n 1. Explore the forest " +
                "\n 2. Go to shop " +
                "\n 3. Rest (Restore Hitpoints) " +
                "\n 4. Check stats. \n");

            Console.WriteLine("------------------------------------- \n Hit [ENTER] to select option.");

            Console.Write("> ");
            string actionChoice = Console.ReadLine();
            switch (actionChoice)
            {
                case "1": // Explore
                    Explore.Start();
                    break;
                case "2":  // Shop
                    Shop.Start();
                    break;

                case "3":
                    if(Program.gold >= 3)
                    {
                        EnergyCalc.energyRestoreMax();
                        Console.WriteLine("You pay 3 gold to the innkeeper!");
                        Program.gold -= 3;
                        Console.WriteLine("You now have: " + Program.gold + " gold!");
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough gold to rest...");
                    }
                    break;

                case "4":
                    Console.WriteLine("Stats");
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Health:" + Program.healthPoints);
                    Console.WriteLine("Exp:" + Program.exp +"/10");
                    Console.WriteLine("Level:" + Program.level);
                    Console.WriteLine("Energy:" + energyPoints + "/" + Program.energyPointsMax);
                    Console.WriteLine("Weapon:" + Program.weapon);
                    Console.WriteLine("Gold:" + Program.gold);
                    Console.WriteLine("Inventory: \n Herbs: " + Program.herbCount + "\n Iron: " + Program.ironCount + "\n Potions: " + Program.potionCount);
                    break;

                default:
                    Console.WriteLine("That is not an option, please select again.");
                    break;

            }
        }
    }
}