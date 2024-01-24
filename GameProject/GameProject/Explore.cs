// Print story
using GameProject;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

internal class Explore
{
    // If player faints, do not deduct Energy Points after event ends.
    public static bool faintFlag = false;
    public static void Start()
    {
        // Exploring start
        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        Console.WriteLine("You go exploring...");
        int exploreDice = new Random() .Next(1, 3);

        if (exploreDice == 1)
        {
            // Encounter
            Explore.Encounter();
            if (faintFlag == false)
            {
                EnergyCalc.energyDeduct(1);
            }
            else
            {
                // Reset faint flag
                faintFlag = false;
            }
        }
        else if (exploreDice == 2)
        {
            // Gain Item
            Explore.ItemGet();
            if (faintFlag == false)
            {
                EnergyCalc.energyDeduct(1);
            }
            else
            {
                // Reset faint flag
                faintFlag = false;
            }
        }
        else
        {
            // Gained nothing
            EnergyCalc.energyDeduct(1);
            Console.WriteLine("You wandered around the forest but found nothing...");
        }
        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

        // Return to main menu
        Program.Menu();


    }

    // Player encounters a monster
    public static void Encounter()
    {
        // Gets the monster from the monster list and its HP.
        (string, int) monster = Explore.EncounterMonsterList();
        int monsterHealth = monster.Item2;

        // Flag to escape the while loop after user either defeats the monster or flees.
        bool exitEncounter = true;

        while (monsterHealth > 0 && exitEncounter == true)
        {
            // Monster name
            string monsterName = monster.Item1;
            Console.WriteLine("You have encountered a " + monsterName + "!");
            Console.WriteLine("It has " + monsterHealth + " Health!");
            Console.WriteLine("Current Health: " + Program.healthPoints + " Health!");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine(" 1. Fight \n 2. Use Potion \n 3. Flee");
            Console.WriteLine("\n-------------------------------------");
            Console.Write("> ");

            // Checks for player choice.
            string encounterChoice = Console.ReadLine();
            switch (encounterChoice)
            {
                case "1": // Fight
                    monsterHealth = Explore.FightEncounter(monsterHealth, monsterName);
                    if (Program.healthPoints <= 0)
                    {
                        // Player faints
                        EnergyCalc.healthDeath(monsterName);
                        exitEncounter = false;
                        faintFlag = true;
                    }

                    break;
                case "2":  // Use Potion
                    EnergyCalc.healthRestore(5);
                    break;

                case "3": // Flee
                    int fleeBattleRand = new Random().Next(2);
                    Console.WriteLine(fleeBattleRand);
                    if (fleeBattleRand == 1)
                    {
                        // Player flees the battle (2)
                        Console.WriteLine("You have fled the battle...");
                        exitEncounter = false;
                    } else
                    {
                        // Player fails to flee the battle (0, 1)
                        Console.WriteLine("You tried to flee the battle but failed!");
                        Explore.EnemyFightTurn(monsterHealth, monsterName);
                    }

                    if (Program.healthPoints <= 0)
                    {
                        // Player fails to flee the battle and faints
                        EnergyCalc.healthDeath(monsterName);
                        faintFlag = true;
                        exitEncounter = false;
                    }
                    break;

                default:
                    // Catch for when player doesn't pick the right option.
                    Console.WriteLine("That is not an option, please select again.");
                    break;
            }

            // Monster death
            if (monsterHealth <= 0 && faintFlag == false)
            {
                Console.WriteLine("You have successfully defeated the " + monsterName + "!");

                ExpCalc.expIncrease(Program.level+1);
                int goldIncrease = new Random().Next(2, 11);
                Program.gold += goldIncrease;
                
                // Monster drops gold.
                Console.WriteLine("The " + monsterName + " dropped " + goldIncrease + " gold!");
                Console.WriteLine("You now have: " + Program.gold + " gold!");
            }
        }
    }

        public static void ItemGet()
        {
            String[] itemList = { "Gold", "Herbs", "Iron" };
            int itemAmtDice = new Random().Next(1, 3);

            var itemDice = itemList[new Random().Next(0, itemList.Length)];
            Console.WriteLine("You have found: " + itemAmtDice + " " + itemDice + "!");

            if(itemDice == "Gold")
            {
                Program.gold += itemAmtDice;
                Console.WriteLine("You now have: " + Program.gold + " gold!");

            }
            else if(itemDice == "Herbs")
            {
                Program.herbCount += itemAmtDice;
                Console.WriteLine("You now have: " + Program.herbCount + " herbs!");

            }
            else if (itemDice == "Iron")
            {
                Program.ironCount += itemAmtDice;
                Console.WriteLine("You now have: " + Program.ironCount + " iron!");

            }

    }


        public static (string, int) EncounterMonsterList()
        {
            Dictionary<string, int> monsterList;
            if (Program.level < 2)
            {
                monsterList = new Dictionary<string, int> {
                    { "Goblin", 5 },
                    { "Imp", 6 },
                    { "Wolf", 7 }
                };
            }
            else
            {
                monsterList = new Dictionary<string, int> {
                    { "Orc", 10 },
                    { "Bear", 12 },
                    { "Giant Spider", 14 }
                };

            }
            List<string> monsterKeyList = new List<string>(monsterList.Keys);
            string monsterName = monsterKeyList[new Random().Next(monsterKeyList.Count)];
            return (monsterName, monsterList[monsterName]);
        }

        public static int FightEncounter(int monsterHealth, string monsterName)
        {
            Console.WriteLine("You swing your sword at the enemy " + monsterName +"!");
            int damageDice = new Random().Next(6) + Program.bonusDmg;
            
            // Player turn
            if (damageDice == 0)
            {
                Console.WriteLine("You missed...");
            }
            else if (damageDice == 5)
            {
                Console.WriteLine("Critical Hit!!!");
                Console.WriteLine("You deal " + damageDice + "!");

            }
            else
            {
                Console.WriteLine("You deal " + damageDice + " Damage!");
            }
            monsterHealth = monsterHealth - damageDice;

            if(Program.healthPoints <= 0)
            {
                Program.healthPoints = 0;
                Program.energyPoints = 0;
                EnergyCalc.energyDeduct(Program.energyPointsMax);
            }

            if(monsterHealth > 0)
            {
                Explore.EnemyFightTurn(monsterHealth, monsterName);
            }


            return monsterHealth;
        }

        public static void EnemyFightTurn(int monsterHealth, string monsterName)
    {
            int enemyDamageDice = new Random().Next(5);
            // Enemy turn
            if (enemyDamageDice == 0)
            {
                Console.WriteLine("Enemy " + monsterName + " missed...");
            }
            else if (enemyDamageDice == 5)
            {
                Console.WriteLine("Critical Hit!!!");
                Console.WriteLine("Enemy " + monsterName + " deals " + enemyDamageDice + " Damage!");

            }
            else
            {
                Console.WriteLine("Enemy " + monsterName + " deals " + enemyDamageDice + " Damage!");
            }

            Program.healthPoints -= enemyDamageDice;
        }
}