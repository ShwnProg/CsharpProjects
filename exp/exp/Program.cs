using System;

namespace PROJECT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] saveOrder = new string[10, 10];  // Added an extra column for cash and change
            int savedIndex = 0;
            int orderNumber = 1;
            UserLogin(saveOrder, ref savedIndex, ref orderNumber);
            Console.Read();
        }

        public static void UserLogin(string[,] saveOrder, ref int saveIndex, ref int orderNumber)
        {
            Console.WriteLine("Log in");
            string username = "", password = "";

            do
            {
                Console.Write("\nUsername: ");
                username = Console.ReadLine();
                Console.Write("Password: ");
                password = Console.ReadLine();

                if (username == "admin" && password == "admin123")
                {
                    Console.WriteLine("\nLogin Successful!");
                    Console.WriteLine("\n\n\t Enter any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    MenuOption(saveOrder, ref saveIndex, ref orderNumber);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid username or password. Try again.");
                }
            } while (username != "admin" || password != "admin123");
        }

        public static void MenuOption(string[,] saveOrder, ref int saveIndex, ref int orderNumber)
        {
            Console.WriteLine("Menu Option");
            Console.WriteLine("[1] Cashiering Transaction");
            Console.WriteLine("[2] View Customer Order");
            Console.WriteLine("[3] View Sales");

            Console.Write("Input Here: ");
            int input = Convert.ToInt32(Console.ReadLine());

            while (input < 1 || input > 3)
            {
                Console.WriteLine("Invalid Input Try again");
                Console.Write("Input Here: ");
                input = Convert.ToInt32(Console.ReadLine());
            }

            if (input == 1)
            {
                // Cashiering Transactions
                Console.Clear();
                CashieringTransaction(saveOrder, ref saveIndex, ref orderNumber);
            }
            else if (input == 2)
            {
                // View Customer Order (This will need more code if it's implemented)
            }
            else if (input == 3)
            {
                // View Sales (This will also need more code for sales tracking)
            }
        }

        public static void CashieringTransaction(string[,] saveOrder, ref int saveIndex, ref int orderNumber)
        {
            string[,] comboMeal = {
                {"[C1]", "Spaghetti + Fries + Fried Chicken", "125 PHP"},
                {"[C2]", "2 PC Burger Steak + Spaghetti", "130 PHP"},
                {"[C3]", "Chicken Fillet + Carbonara", "175 PHP"},
                {"[C4]", "Chicken Nuggets + Cheese Burger", "160 PHP"},
                {"[C5]", "Sizzling Sisig + Lumpiang Shanghai", "199 PHP"}
            };

            string[,] drinks = {
                {"[D1]", "Pineapple Juice", "65 PHP"},
                {"[D2]", "Sprite", "60 PHP"},
                {"[D3]", "Coke", "70 PHP"},
                {"[D4]", "Ice Tea", "65 PHP"},
                {"[D5]", "Lemon Juice", "60 PHP"}
            };

            string[,] dessert = {
                {"[E1]", "Banana Split", "70 PHP"},
                {"[E2]", "Cheesecake", "75 PHP"},
                {"[E3]", "Banana Cake", "80 PHP"},
                {"[E4]", "Brownies", "95 PHP"},
                {"[E5]", "Apple Pie", "60 PHP"}
            };

            DisplayFoodRushMenu(comboMeal, drinks, dessert);

            string menuChoice, food = "";
            double total = 0, itemTotal = 0;
            int quantity = 0;
            bool continueOrder = true;

            do
            {
                Console.Write("\n\n >>Select Choice  : ");
                menuChoice = Console.ReadLine();
                string choiceCaps = menuChoice.ToUpper();

                // Check food
                food = CheckFood(menuChoice, comboMeal, drinks, dessert);

                // Check item price based on the code
                double itemPrice = CheckItemPrice(menuChoice);

                if (itemPrice > 0)
                {
                    while (true)
                    {
                        Console.Write("\n >>Input Quantity : ");
                        quantity = Convert.ToInt32(Console.ReadLine());

                        if (quantity > 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Quantity must be greater than 0. Please try again.");
                        }
                    }
                    // Calculate total for the item
                    itemTotal = quantity * itemPrice;
                    total += itemTotal;

                    Console.WriteLine("\n\t Total: " + total);

                    // Save the order
                    saveOrder[saveIndex, 0] = choiceCaps;
                    saveOrder[saveIndex, 1] = food;
                    saveOrder[saveIndex, 2] = Convert.ToString(itemPrice);
                    saveOrder[saveIndex, 3] = Convert.ToString(quantity);
                    saveOrder[saveIndex, 4] = Convert.ToString(itemTotal);
                    saveOrder[saveIndex, 6] = Convert.ToString(orderNumber);
                    saveOrder[saveIndex, 7] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    saveIndex++;
                }

                Console.Write("Do you want to order another item? (Y/N): ");
                string orderAgain = Console.ReadLine();

                while (orderAgain != "Y" && orderAgain != "y" && orderAgain != "N" && orderAgain != "n")
                {
                    Console.WriteLine("\n\n\t Invalid Input Try Again.");
                    Console.Write("\n Do you want to order another item? (Y/N): ");
                    orderAgain = Console.ReadLine();
                }

                if (orderAgain == "Y" || orderAgain == "y")
                {
                    continueOrder = true;
                }
                else
                {
                    continueOrder = false;
                }
            } while (continueOrder);

            // Order summary display
            if (saveIndex == 0)
            {
                Console.WriteLine("No items ordered. Press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                // Cashiering change
                Console.Write("Enter Cash: ");
                double cash = Convert.ToDouble(Console.ReadLine());
                double change = CashieringChange(total, cash);
                saveOrder[saveIndex, 5] = Convert.ToString(change);  // Save change
                saveOrder[saveIndex, 8] = Convert.ToString(cash);  // Save cash entered

                if (change >= 0)
                {
                    // Display order summary
                    Console.Clear();
                    Console.WriteLine("===================================================================================================");
                    Console.WriteLine("|                                        Order Summary                                            |");
                    Console.WriteLine("===================================================================================================");
                    Console.WriteLine("|  Code  |                Description                |  Item Price  |  Quantity   |  Total Price |");
                    Console.WriteLine("---------------------------------------------------------------------------------------------------");
                    double grandTotal = 0;

                    for (int i = 0; i < saveIndex; i++)
                    {
                        Console.WriteLine("|  {0,-6}| {1,-42} |   {2,-5}PHP   |    {3,-2} Qty   |    {4,-5}PHP  |", saveOrder[i, 0], saveOrder[i, 1], saveOrder[i, 2], saveOrder[i, 3], saveOrder[i, 4]);
                        grandTotal += Convert.ToDouble(saveOrder[i, 4]);
                    }
                    Console.WriteLine("---------------------------------------------------------------------------------------------------");
                    Console.WriteLine("| TOTAL: {0,81} PHP    |", grandTotal);
                    Console.WriteLine("| ORDER NUMBER: {0,70} |", saveOrder[0, 6]); // Display Order Number
                    Console.WriteLine("| DATE: {0,85} |", saveOrder[0, 7]); // Display Date and Time
                    Console.WriteLine("| CASH: {0,86} PHP   |", saveOrder[saveIndex, 8]); // Display Cash Entered
                    Console.WriteLine("| CHANGE: {0,83} PHP |", saveOrder[saveIndex, 5]); // Display Change
                    Console.WriteLine("===================================================================================================");
                    orderNumber++;
                }
            }

            while (true)
            {
                Console.Write("\nBack to Main Menu[Y/N]: ");
                string back = Console.ReadLine();

                while (back != "Y" && back != "y" && back != "N" && back != "n")
                {
                    Console.WriteLine("Invalid Input Try Again.");
                    Console.Write("\nBack to Main Menu[Y/N]: ");
                    back = Console.ReadLine();
                }

                if (back == "Y" || back == "y")
                {
                    Console.Clear();
                    MenuOption(saveOrder, ref saveIndex, ref orderNumber);
                }
                else
                {
                    break;
                }
            }
        }

        public static void DisplayFoodRushMenu(string[,] comboMeal, string[,] drinks, string[,] dessert)
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine("|                       FoodRush MENU                         |");
            Console.WriteLine("===============================================================");
            Console.WriteLine("|  Code  |                Combo Meal                |  Price  |");
            Console.WriteLine("---------------------------------------------------------------");

            for (int i = 0; i < comboMeal.GetLength(0); i++)
            {
                Console.WriteLine("|{0} | {1,-19}  | {2,-7} |", comboMeal[i, 0], comboMeal[i, 1], comboMeal[i, 2]);
            }

            Console.WriteLine("===============================================================");
            Console.WriteLine("|  Code  |                  Drinks                  |  Price  |");
            Console.WriteLine("---------------------------------------------------------------");

            for (int i = 0; i < drinks.GetLength(0); i++) // Fixed drinks loop
            {
                Console.WriteLine("|{0} | {1,-19}  | {2,-7} |", drinks[i, 0], drinks[i, 1], drinks[i, 2]);
            }

            Console.WriteLine("===============================================================");
            Console.WriteLine("|  Code  |                  Dessert                 |  Price  |");
            Console.WriteLine("---------------------------------------------------------------");

            for (int i = 0; i < dessert.GetLength(0); i++) // Fixed dessert loop
            {
                Console.WriteLine("|{0} | {1,-19}  | {2,-7} |", dessert[i, 0], dessert[i, 1], dessert[i, 2]);
            }

            Console.WriteLine("===============================================================");
        }

        public static double CheckItemPrice(string menuChoice)
        {
            switch (menuChoice.ToUpper())
            {
                case "C1": return 125;
                case "C2": return 130;
                case "C3": return 175;
                case "C4": return 160;
                case "C5": return 199;
                case "D1": return 65;
                case "D2": return 60;
                case "D3": return 70;
                case "D4": return 65;
                case "D5": return 60;
                case "E1": return 70;
                case "E2": return 75;
                case "E3": return 80;
                case "E4": return 95;
                case "E5": return 60;
                default:
                    Console.WriteLine("Invalid item code.");
                    return 0;
            }
        }

        public static string CheckFood(string menuChoice, string[,] comboMeal, string[,] drinks, string[,] dessert)
        {
            string food = "";
            switch (menuChoice.ToUpper())
            {
                case "C1": return comboMeal[0, 1];
                case "C2": return comboMeal[1, 1];
                case "C3": return comboMeal[2, 1];
                case "C4": return comboMeal[3, 1];
                case "C5": return comboMeal[4, 1];
                case "D1": return drinks[0, 1];
                case "D2": return drinks[1, 1];
                case "D3": return drinks[2, 1];
                case "D4": return drinks[3, 1];
                case "D5": return drinks[4, 1];
                case "E1": return dessert[0, 1];
                case "E2": return dessert[1, 1];
                case "E3": return dessert[2, 1];
                case "E4": return dessert[3, 1];
                case "E5": return dessert[4, 1];
                default:
                    return food;
            }
        }

        public static double CashieringChange(double total, double cash)
        {
            double change = 0;
            while (cash < total)
            {
                Console.WriteLine("Insufficient amount to pay. Try again!");
                Console.Write("Enter Cash: ");
                cash = Convert.ToDouble(Console.ReadLine());
            }
            if (cash >= total)
            {
                change = cash - total;
                Console.WriteLine("Change: " + change);
                return change;
            }
            return change;
        }
    }
}
