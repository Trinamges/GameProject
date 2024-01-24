using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    internal class ExpCalc
    {

        public static void expIncrease(int gainAmount)
        {
            Console.WriteLine("Congrats! You have gained " + gainAmount + " experience!");
            Program.exp = Program.exp + gainAmount;
            if (Program.exp > 10)
            {
                Program.exp -= 10;
                Program.level += 1;
                Program.energyPointsMax += 1;
                Program.healthPointsMax += 5;
                Console.WriteLine("Congrats! You leveled up! You are now level " + Program.level +" !");
                Console.WriteLine("Your Energy Points increased by 1 and you now have " + Program.energyPointsMax + " points!");
                Console.WriteLine("Your Health Points increased by 5 and you now have " + Program.healthPointsMax + " points!");
            }
        }
    }
}
