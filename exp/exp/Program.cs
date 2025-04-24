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
            try
            {
                UserLogin(saveOrder, ref savedIndex, ref orderNumber);
            }
            catch(FormatException e)
            {
                Console.WriteLine(e.Message);
            }
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

            //DisplayFoodRushMenu(comboMeal, drinks, dessert);

            string menuChoice = "", food = "", meal = "",Drinks = "",Dessert = "";
            double total = 0, itemTotal = 0;
            int category = 0,quantity = 0;
            bool continueOrder = true;

            do
            {
                Console.Clear();
                MenuCategory(category);
                Console.Write("Select Category: ");
                category = Convert.ToInt32(Console.ReadLine());

              
                while(category < 1 || category > 3)
                {
                    Console.WriteLine("Invalid Input Try Again.");

                    Console.Write("Select Category: ");
                    category = Convert.ToInt32(Console.ReadLine());
                }
                if (category == 1)
                {
                    DisplayComboMealMenu(comboMeal);
                    Console.Write("\n\n >>Select Combo Meal  : ");
                    meal = Console.ReadLine();
                    meal = meal.ToUpper();
                    while(meal != "C1" && meal != "C2" && meal != "C3" && meal != "C4" && meal != "C5")
                    {
                        Console.WriteLine("Invalid Input!");
                        Console.Write("\n\n >>Select Combo Meal  : ");
                        meal = Console.ReadLine();
                    }
                }
                if (category == 2)
                {
                    DisplayDrinksMenu(drinks);
                    Console.Write("\n\n >>Select Drinks  : ");
                    Drinks = Console.ReadLine();
                    Drinks = Drinks.ToUpper();
                    while (Drinks != "D1" && Drinks != "D2" && Drinks != "D3" && Drinks != "D4" && Drinks != "D5")
                    {
                        Console.WriteLine("Invalid Input!");
                        Console.Write("\n\n >>Select Drinks  : ");
                        Drinks = Console.ReadLine();
                    }
                }
                if (category == 3)
                {
                    DisplayDessertMenu(dessert);
                    Console.Write("\n\n >>Select Dessert  : ");
                    Dessert = Console.ReadLine();
                    Dessert = Dessert.ToUpper();
                    while (Dessert != "E1" && Dessert != "E2" && Dessert != "E3" && Dessert != "E4" && Dessert != "E5")
                    {
                        Console.WriteLine("Invalid Input!");
                        Console.Write("\n\n >>Select Dessert  : ");
                        Dessert = Console.ReadLine();
                    }
                }

                // Check food
                food = CheckFood(meal,Drinks,Dessert, comboMeal, drinks, dessert);

                // Check item price based on the code
                double itemPrice = CheckItemPrice(meal,Drinks,Dessert);
                
                if (food != "")
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
                    string order = meal;
                    order = Drinks;
                    order = Dessert;
                    // Save the order

                    saveOrder[saveIndex, 0] = order;
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
            NewOrder(saveOrder, ref saveIndex, ref orderNumber);
           
        }

        public static void NewOrder(string[,] saveOrder, ref int saveIndex, ref int orderNumber)
        {
            while (true)
            {
                Console.Write("\n Do you want to order again[Y/N]: ");
                string back = Console.ReadLine();

                while (back != "Y" && back != "y" && back != "N" && back != "n")
                {
                    Console.WriteLine("Invalid Input Try Again.");
                    Console.Write("\n Do you want to order again[Y/N]: ");
                    back = Console.ReadLine();
                }

                if (back == "Y" || back == "y")
                {
                    Console.Clear();
                    CashieringTransaction(saveOrder, ref saveIndex, ref orderNumber);
                }
                else
                {
                    MenuOption(saveOrder, ref saveIndex, ref orderNumber);
                        
                }
            }
        }
        public static void MenuCategory(int select)
        {
            Console.WriteLine("\nCategory");
            Console.WriteLine("[1] Combo Meal");
            Console.WriteLine("[2] Drinks");
            Console.WriteLine("[3] Dessert");

        }
        public static void DisplayComboMealMenu(string[,] comboMeal)
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
        }

        public static void DisplayDrinksMenu(string[,] Drinks)
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine("|                       FoodRush MENU                         |");
            Console.WriteLine("===============================================================");
            Console.WriteLine("|  Code  |                Combo Meal                |  Price  |");
            Console.WriteLine("---------------------------------------------------------------");

            for (int i = 0; i < Drinks.GetLength(0); i++)
            {
                Console.WriteLine("|{0} | {1,-19}  | {2,-7} |", Drinks[i, 0], Drinks[i, 1], Drinks[i, 2]);
            }
        }

        public static void DisplayDessertMenu(string[,] Dessert)
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine("|                       FoodRush MENU                         |");
            Console.WriteLine("===============================================================");
            Console.WriteLine("|  Code  |                Combo Meal                |  Price  |");
            Console.WriteLine("---------------------------------------------------------------");

            for (int i = 0; i < Dessert.GetLength(0); i++)
            {
                Console.WriteLine("|{0} | {1,-19}  | {2,-7} |", Dessert[i, 0], Dessert[i, 1], Dessert[i, 2]);
            }
        }

        public static double checkitemprice(string meal, string drinks, string dessert)
        {
            switch (menuchoice.toupper())
            {
                case "c1": return 125;
                case "c2": return 130;
                case "c3": return 175;
                case "c4": return 160;
                case "c5": return 199;
                case "d1": return 65;
                case "d2": return 60;
                case "d3": return 70;
                case "d4": return 65;
                case "d5": return 60;
                case "e1": return 70;
                case "e2": return 75;
                case "e3": return 80;
                case "e4": return 95;
                case "e5": return 60;
                default:
                    Console.WriteLine("invalid item code.");
                    return 0;
            }
        }

        public static string CheckFood( string meal,string Drinks, string Dessert, string[,] comboMeal, string[,] drinks, string[,] dessert)
        {
            string food = "";
            switch (meal.ToUpper())
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
