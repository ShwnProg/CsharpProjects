using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRush_CashieringSystem_Project_Final
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("\n\t\t\t\t FoodRush Cashiering System");
            Console.WriteLine("\t\t\t---------------------------------------------");
            SecurityLoginAccess();

        }
        public static void SecurityLoginAccess()
        {
            bool isAccess = true;
            Console.WriteLine("\n\t\t\t\t\t LOGIN");

            while (isAccess)
            {
                Console.Write("\n\t\t\t Enter username: ");
                string username = Console.ReadLine();

                Console.Write("\n\t\t\t Enter password: ");
                string password = Console.ReadLine();

                if (username == "admin" && password == "admin123")
                {
                    isAccess = false;
                    //Main Menu

                    MainMenuOption();
                }
                else
                {
                    Console.WriteLine("\n\t\tInvalid username or password. Please try again.");
                    Console.ReadKey();
                }
            }
        }

        public static void MainMenuOption()
        {
            string[,] orders = new string[100, 9];
            int orderIndex = 0;
            int orderNumber = 1;
            bool inputAgain = true;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\n\t\t\t\t        FoodRush Main Menu");
                    Console.WriteLine("\t\t\t---------------------------------------------");
                    Console.WriteLine("\n\t\t\t 1. Cashiering Transaction");
                    Console.WriteLine("\t\t\t 2. View Customer Order");
                    Console.WriteLine("\t\t\t 3. View Sales");
                    Console.Write("\n\t\t\t Select an option: ");
                    string userOpt = Console.ReadLine();

                    if (userOpt == "1")
                    {
                        //Cashiering System
                        Console.Clear();
                        FoodRushCashieringSystem(orders, orderIndex, orderNumber);
                    }
                    if (userOpt == "2")
                    {
                        // View Customer Order
                    }
                    if (userOpt == "3")
                    {
                        // View Sales
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("\n\t\t" + ex.Message);
                    Console.WriteLine("\n\t\t\t ---------------------------------------");
                }
                catch (IndexOutOfRangeException limit)
                {
                    Console.WriteLine("\n\t\t" + limit.Message);
                    Console.WriteLine("\n\t\t\t ---------------------------------------");
                }
                finally
                {
                    Console.Write("\n\t\t\t Do you want to continue? (Y/N): ");
                    string continueOpt = Console.ReadLine();

                    while (continueOpt != "Y" && continueOpt != "y" && continueOpt != "N" && continueOpt != "n")
                    {
                        Console.Write("\n\t\t\t Invalid input. Please enter Y or N: ");
                        continueOpt = Console.ReadLine();
                    }
                    if (continueOpt == "Y" || continueOpt == "y")
                    {
                        inputAgain = true;
                    }
                    else if (continueOpt == "N" || continueOpt == "n")
                    {
                        inputAgain = false;
                        Console.WriteLine("\n\t\t\t Thank you for using FoodRush Cashiering System!");
                        Console.WriteLine("\t\t\t---------------------------------------------");
                        Console.ReadKey();
                    }
                }
            } while (inputAgain);
        }
       
        public static void FoodRushCashieringSystem(string[,] order, int orderIndex, int orderNumber)
        {
            double total = 0;
            string food = "";
            int currentOrder = orderNumber;
            orderNumber++;
            bool orderAgain = true;
            InitializeFoodData(order);
            Console.WriteLine("Cashiering Transaction");
            DisplayFoodRushMenu(order);

            do
            {
                //Console.Clear();

                Console.Write("Enter item number : ");
                string itemNumber = Console.ReadLine();
                food = CheckFood(itemNumber, order);
                double price = CheckOrder(itemNumber);

                if (price != 0 && food != "")
                {
                    Console.Write("Enter quantity : ");
                    int quantity = int.Parse(Console.ReadLine());

                    total += price * quantity;
                    Console.WriteLine(total);

                    order[orderIndex, 3] = Convert.ToString(currentOrder);
                    order[orderIndex, 4] = itemNumber;
                    order[orderIndex, 5] = food;
                    order[orderIndex, 6] = Convert.ToString(price);
                    order[orderIndex, 7] = Convert.ToString(quantity);
                    order[orderIndex, 8] = Convert.ToString(total);
                    orderIndex++;
                }
                Console.Write("Do you want to order again? (Y/N): ");
                string continueOpt = Console.ReadLine();

                while (continueOpt != "Y" && continueOpt != "y" && continueOpt != "N" && continueOpt != "n")
                {
                    Console.Write("\n\t\t\t Invalid input. Please enter Y or N: ");
                    continueOpt = Console.ReadLine();
                }
                if (continueOpt == "Y" || continueOpt == "y")
                {
                    orderAgain = true;
                }
                else
                {
                    orderAgain = false;
                    //MainMenuOption();
                }

            } while (orderAgain);

            DisplaySummaryofOrder(order, orderIndex);
        }
        public static void DisplaySummaryofOrder(string[,] orders, int orderIndex)
        {
            Console.WriteLine("Order Summary");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("|  Item Number  |         Food          |    Price     |    Quantity    |");
            Console.WriteLine("-------------------------------------------------------------------------");

            for (int i = 0; i < orderIndex; i++)
            {
                Console.WriteLine("|      {0,-8} | {1,-16} | {2,-6} | {3,-8} |", orders[i, 4], orders[i, 5], orders[i, 6], orders[i, 7]);
            }
        }
        public static void InitializeFoodData(string[,] order)
        {
            // Combo Meal Code Number
            order[0, 0] = "[C1]";
            order[1, 0] = "[C2]";
            order[2, 0] = "[C3]";
            order[3, 0] = "[C4]";
            order[4, 0] = "[C5]";

            // Combo Meal Food
            order[0, 1] = "Classic Ham Burger + Fries                   ";
            order[1, 1] = "Chicken Nuggets w/rice + Double Cheese Burger";
            order[2, 1] = "Chicken Burger + Spaghetti                   ";
            order[3, 1] = "Pork Steak w/rice + Class Ham Burger         ";
            order[4, 1] = "Fish Fillet w/rice + Rice + Mango Juice      ";

            // Combo Meal Price
            order[0, 2] = "159.00";
            order[1, 2] = "199.00";
            order[2, 2] = "179.00";
            order[3, 2] = "189.00";
            order[4, 2] = "169.00";

            // Drinks Code Number
            order[5, 0] = "[D1]";
            order[6, 0] = "[D2]";
            order[7, 0] = "[D3]";
            order[8, 0] = "[D4]";
            order[9, 0] = "[D5]";

            // Drinks Name
            order[5, 1] = "Iced Tea                                     ";
            order[6, 1] = "Soft Drinks                                  ";
            order[7, 1] = "Bottled Water                                ";
            order[8, 1] = "Mango Juice                                  ";
            order[9, 1] = "Coffee                                       ";

            // Drinks Price
            order[5, 2] = "39.00";
            order[6, 2] = "49.00";
            order[7, 2] = "29.00";
            order[8, 2] = "59.00";
            order[9, 2] = "45.00";

            // Desserts Code Number
            order[10, 0] = "[DE1]";
            order[11, 0] = "[DE2]";
            order[12, 0] = "[DE3]";
            order[13, 0] = "[DE4]";
            order[14, 0] = "[DE5]";

            // Desserts Name
            order[10, 1] = "Ice Cream                                    ";
            order[11, 1] = "Brownies                                     ";
            order[12, 1] = "Cheesecake                                   ";
            order[13, 1] = "Fruit Salad                                  ";
            order[14, 1] = "Leche Flan                                   ";

            // Desserts Price
            order[10, 2] = "59.00";
            order[11, 2] = "69.00";
            order[12, 2] = "89.00";
            order[13, 2] = "79.00";
            order[14, 2] = "99.00";
        }
        public static void DisplayFoodRushMenu(string[,] order)
        {

          
            Console.WriteLine("|*------------------------------------FoodRush Menu--------------------------------*|");

            // Combo Meals
            Console.WriteLine("\n   Menu Code      Food Name                                        Price  ");
            Console.WriteLine(" ------------------------------------------------------------------------------");
            Console.WriteLine("\n [Combo Meals]---------------------------------------------------------\n"); ;


            for (int i = 0; i < 5; i++) // Loop for Combo Meals
            {
                Console.WriteLine("    {0,-5}       {1,-30}   {2,7}", order[i, 0],
                                                                      order[i, 1],
                                                                      order[i, 2]);
            }

            // Drinks
            Console.WriteLine("\n [Drinks]---------------------------------------------------------------\n");

            for (int i = 5; i < 10; i++) // Loop for Drinks
            {
                Console.WriteLine("    {0,-5}       {1,-30}   {2,7}", order[i, 0],
                                                                      order[i, 1],
                                                                      order[i, 2]);
            }

            // Desserts
            Console.WriteLine("\n [Desserts]--------------------------------------------------------------\n");
            for (int i = 10; i < 15; i++) // Loop for Desserts
            {
                Console.WriteLine("    {0,-5}       {1,-30}   {2,7}", order[i, 0],
                                                                      order[i, 1],
                                                                      order[i, 2]);
            }
            Console.WriteLine("\n ------------------------------------------------------------------------------");


        }
        public static double CheckOrder(string orderNumber)
        {
            double price = 0;
            orderNumber = orderNumber.ToUpper();

            switch (orderNumber)
            {
                case "C1":
                    return price = 159.00;
                case "C2":
                    return price = 199.00;
                case "C3":
                    return price = 179.00;
                case "C4":
                    return price = 189.00;
                case "C5":
                    return price = 169.00;
                case "D1":
                    return price = 39.00;
                case "D2":
                    return price = 49.00;
                case "D3":
                    return price = 29.00;
                case "D4":
                    return price = 59.00;
                case "D5":
                    return price = 45.00;
                case "DE1":
                    return price = 59.00;
                case "DE2":
                    return price = 69.00;
                case "DE3":
                    return price = 89.00;
                case "DE4":
                    return price = 79.00;
                case "DE5":
                    return price = 99.00;
                default:
                    Console.WriteLine("\n\t\t\t Invalid item number. Please try again.");
                    return 0;
            }
        }
        public static string CheckFood(string orderNumber, string[,] order)
        {
            string food = "";
            orderNumber = orderNumber.ToUpper();

            for (int i = 0; i < order.GetLength(0); i++)
            {
                if (order[i, 0] == orderNumber)
                {
                    food = order[i, 1];
                    break;
                }
            }

            return food;
        }
    }
}
