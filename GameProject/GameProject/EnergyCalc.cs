using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    internal class EnergyCalc
    {

        public static void energyDeduct(int deductAmount)
        {
            Program.energyPoints -= deductAmount;

            if(Program.energyPoints < 0)
            {
                Program.energyPoints = 0;
            }

            if ((Program.energyPoints) == 0)
            {
                Console.WriteLine("You have: " + Program.energyPoints + "/" + Program.energyPointsMax + " Energy points!");
                Console.WriteLine("You passed out...");
                Console.WriteLine("You lost 5 gold as a result...");
                Program.gold -= 5;
                Program.healthPoints = Program.healthPointsMax;

                if (Program.gold == 0)
                {
                    Console.WriteLine("You don't have enough money!");
                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                    Console.WriteLine("        GAME OVER            ");
                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                    Environment.Exit(0);

                }
                EnergyCalc.energyRestoreMax();
            } 
            else if ((Program.energyPoints) == 1)
            {
                Console.WriteLine("You now have: " + Program.energyPoints + "/" + Program.energyPointsMax + " Energy points!");
                Console.WriteLine("You are starting to feel tired...");
                Console.WriteLine("Consider resting before going out again!");
            }
            else
            {
                Console.WriteLine("You now have: " + Program.energyPoints + "/" + Program.energyPointsMax + " Energy points!");

            }

        }
        public static void healthRestore(int gainAmount)
        {
            Console.WriteLine("You have consumed a Potion and regained " + gainAmount + " Health!");
            Program.healthPoints += 5;

            if (Program.healthPoints > 10)
                Program.healthPoints = 10;
            

            Console.WriteLine("You now have: " + Program.healthPoints + " Health!");


        }
        public static void energyRestoreMax()
        {
            Program.healthPoints = Program.healthPointsMax;
            Console.WriteLine("You have rested at an Inn and regained all your Energy points and Health points!");
            Program.energyPoints = Program.energyPointsMax;

            Console.WriteLine("You now have: " + Program.energyPoints + "/" + Program.energyPointsMax + " Energy points!");


        }
        public static void healthDeath(string monsterName)
        {
            Console.WriteLine("You were defeated by enemy " + monsterName + "!");
            Console.WriteLine("You lost 5 gold as a result...");
            Program.gold -= 5;
            Program.healthPoints = Program.healthPointsMax;
            EnergyCalc.energyRestoreMax(); 

        }


    }
}
