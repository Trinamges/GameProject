using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GameProject
{
    internal class Shop
    {
        public static void Start()
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("The shopkeeper greets you");
            Console.WriteLine("\"Welcome to the Shop! So you buyin' or sellin'?\"");
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine(" 1. Buy \n 2. Sell \n 3. Back");
            Console.WriteLine("\n-------------------------------------");
            Console.Write("> ");
            string shopChoice = Console.ReadLine();
            switch (shopChoice)
            {
                case "1": // Buy
                    Shop.Buy();
                    break;
                case "2":  // Sell
                    Shop.Sell();
                    break;
                case "3":  // Return
                    Program.Menu();
                    break;
                default:  // Wrong option
                    Console.WriteLine("Invalid Option!");
                    Shop.Start();
                    break;

            }
        }


        public static void Buy()
        {
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("Whatcha buyin'?");
            Console.WriteLine(" 1. Potion - Heals for 5 (Requires: 4 Herbs and 5 Gold)");
            if (Program.weapon != "Iron Sword")
                Console.WriteLine(" 2. Iron Sword - Increases your damage by 2 (Requires: 10 Iron and 10 Gold");
            Console.WriteLine(" \n 3. Back");
            Console.Write("> ");
            string shopChoice = Console.ReadLine();

            if(shopChoice == "1")
            {
                Console.WriteLine("Are you sure you want to buy (1) Potion? (Costs: 4 Herbs and 5 Gold) ");
                Console.WriteLine(" 1. Yes \n 2. No");
                Console.Write("> ");
                string shopConfirmChoice = Console.ReadLine();
                if(shopConfirmChoice == "1")
                {
                    var buySuccess = Shop.PurchaseCheck(Program.herbCount, 4, 5, "Herb");
                    if (buySuccess == true)
                    {
                        Program.herbCount -= 4;
                    }
                }
                else if (shopConfirmChoice == "2")
                {
                    Shop.Buy();
                }

            }
            else if (shopChoice == "2" && Program.weapon != "Iron Sword")
            {
                Console.WriteLine("Are you sure you want to buy (1) Iron Sword? (Costs: 10 Iron and 10 Gold) ");
                Console.WriteLine(" 1. Yes \n 2. No");
                Console.Write("> ");
                string shopConfirmChoice = Console.ReadLine();
                if (shopConfirmChoice == "1")
                {
                    bool buySuccess = Shop.PurchaseCheck(Program.ironCount, 10, 10, "Iron");
                    if (buySuccess == true)
                    {
                        Program.weapon = "Iron Sword";
                        Program.bonusDmg += 2;
                        Program.ironCount -= 10;
                    }
                }
                else if (shopConfirmChoice == "2")
                {
                    Shop.Buy();
                }

            }
            else if (shopChoice == "2" && Program.weapon == "Iron Sword")
            {
                Console.WriteLine("You have already purchased an Iron Sword!");
                Shop.Buy();
            }
            else if (shopChoice == "3")
            {
                Shop.Start();
            }
            else
            {
                Console.WriteLine("Invalid Option!");
                Shop.Buy();
            }
        }


        public static void Sell()
        {
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("Whatcha sellin'?");
            Console.WriteLine(" 1. Herb - Sells for 1 gold \n 2. Iron - Sells for 2 gold \n 3. Back");
            Console.Write("> ");
            string shopChoice = Console.ReadLine();

            if (shopChoice == "1")
            {
                Console.WriteLine("Are you sure you want to sell (1) Herb? (Gain 1 gold) ");
                Console.WriteLine(" 1. Yes \n 2. No");
                Console.Write("> ");
                string shopConfirmChoice = Console.ReadLine();


                if (shopConfirmChoice == "1")
                {
                    if(Program.herbCount >= 1)
                    {
                        Program.gold += 1;
                        Program.herbCount -= 1;
                        Console.WriteLine("Thank you for selling!");
                        Console.WriteLine("You now have: " + Program.gold + " gold!");
                    }
                    else
                    {
                        Console.WriteLine("You don't seem to have any herbs to sell!");
                        Shop.Sell();
                    }
                }
                else if (shopConfirmChoice == "2")
                {
                    Shop.Buy();
                }

            }
            else if (shopChoice == "2")
            {
                Console.WriteLine("Are you sure you want to sell (1) Iron? (Gain 2 gold) ");
                Console.WriteLine(" 1. Yes \n 2. No");
                Console.Write("> ");
                string shopConfirmChoice = Console.ReadLine();
                if (shopConfirmChoice == "1")
                {
                    if (Program.ironCount >= 1)
                    {
                        Program.gold += 2;
                        Program.ironCount -= 1;
                        Console.WriteLine("Thank you for selling!");
                        Console.WriteLine("You now have: " + Program.gold + " gold!");
                    }
                    else
                    {
                        Console.WriteLine("You don't seem to have any iron to sell!");
                        Shop.Sell();
                    }
                }
                else if (shopConfirmChoice == "2")
                {
                    Shop.Buy();
                }

            }
            else if (shopChoice == "3")
            {

            }
            else
            {
                Console.WriteLine("Invalid Option!");
                Shop.Sell();
            }
        }

        public static bool PurchaseCheck(int itemOwned, int requiredAmount, int goldRequired, string itemName)
        {
            if (itemOwned < requiredAmount)
            {
                int itemNeeded = requiredAmount - itemOwned;
                Console.WriteLine("You have insufficient " + itemName + "! You need " + itemNeeded + " " + itemName + "!");
                return false;
            }
            if (Program.gold >= goldRequired && itemOwned >= requiredAmount)
            {
                Program.gold -= goldRequired;
                Console.WriteLine("Thank you for your purchase!");
                return true;
            }
            else
            {
                int itemNeeded = requiredAmount - Program.gold;
                Console.WriteLine("You have insufficient gold! You need " + itemNeeded + " gold!");
                return false;
            }
        }
    }
}
