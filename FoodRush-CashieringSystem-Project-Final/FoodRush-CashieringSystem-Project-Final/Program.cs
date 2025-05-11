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
        //---------------------------------------------------------------------------------------------------------------//

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\t\t\t\t\t\r\n                        ███████╗ ██████╗  ██████╗ ██████╗     ██████╗ ██╗   ██╗███████╗██╗  ██╗\r\n                        ██╔════╝██╔═══██╗██╔═══██╗██╔══██╗    ██╔══██╗██║   ██║██╔════╝██║  ██║\r\n                        █████╗  ██║   ██║██║   ██║██║  ██║    ██████╔╝██║   ██║███████╗███████║\r\n                        ██╔══╝  ██║   ██║██║   ██║██║  ██║    ██╔══██╗██║   ██║╚════██║██╔══██║\r\n                        ██║     ╚██████╔╝╚██████╔╝██████╔╝    ██║  ██║╚██████╔╝███████║██║  ██║\r\n                        ╚═╝      ╚═════╝  ╚═════╝ ╚═════╝     ╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═╝  ╚═╝\r\n                                                                                               \r\n");
            Console.ResetColor();

            // Call the login method
            PerformLogin();
        }

        // Let the user log in//
        //---------------------------------------------------------------------------------------------------------------//


        public static void PerformLogin()
        {
            bool isAccess = true;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\t\t\t\t ═════════════════════════════════");
            Console.WriteLine("\t\t\t\t\t ║              Login            ║");
            Console.WriteLine("\t\t\t\t\t ═════════════════════════════════");
            Console.ResetColor();
            while (isAccess)
            {
                Console.Write("\n\t\t\t\t\t >>Enter password : ");
                string password = Console.ReadLine();

                if (password == "admin123")
                {
                    isAccess = false;

                    // Show the main menu
                    ShowMainMenu();
                }
                else
                {

                    Console.WriteLine("\n\t\t\t\t\t Invalid username or password. Please try again.");
                    Console.ReadKey();
                }
            }
        }

        // Show the main menu options//
        //---------------------------------------------------------------------------------------------------------------//

        public static void ShowMainMenu()
        {
            string[,] orders = new string[1000, 13];

            int orderIndex = 0;
            int orderNumber = 1;
            bool inputAgain = true;

            // Load menu data
            InitializeFoodData(orders);

            while (inputAgain)
            {
                Console.Clear();
                //display main menu
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\t\t\t\t\t        \r\n                                                _                                     \r\n                                  /\\/\\    __ _ (_) _ __     /\\/\\    ___  _ __   _   _ \r\n                                 /    \\  / _` || || '_ \\   /    \\  / _ \\| '_ \\ | | | |\r\n                                / /\\/\\ \\| (_| || || | | | / /\\/\\ \\|  __/| | | || |_| |\r\n                                \\/    \\/ \\__,_||_||_| |_| \\/    \\/ \\___||_| |_| \\__,_|\r\n                                                                                      \r\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\t\t\t\t\t ═════════════════════════════════\n");
                Console.WriteLine("\t\t\t\t\t ╔═══════════════════════════════╗");
                Console.WriteLine("\t\t\t\t\t ║ 1. Cashiering Transaction     ║");
                Console.WriteLine("\t\t\t\t\t ║ 2. View Customer Order        ║");
                Console.WriteLine("\t\t\t\t\t ║ 3. View Sales                 ║");
                Console.WriteLine("\t\t\t\t\t ║ 4. EXIT                       ║");
                Console.WriteLine("\t\t\t\t\t ╚═══════════════════════════════╝");
                
                Console.ResetColor();
                // Prompt the user for an option
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n\t\t\t\t >>Select an option: ");
                string userOpt = Console.ReadLine();

                //validate user input
                while (userOpt != "1" && userOpt != "2" && userOpt != "3" && userOpt != "4")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\t\t\t\tInvalid Input.Try Again.");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n\t\t\t >>Select an option: ");
                    userOpt = Console.ReadLine();
                    Console.ResetColor();
                }

                //
                switch (userOpt)
                {
                    case "1":
                        Console.Clear();
                        // Start the cashiering process
                        ProcessCashieringTransaction(orders, ref orderIndex, ref orderNumber);
                        break;
                    case "2":
                        Console.Clear();
                        // Start the view custmer order
                        ViewCustomerOrder(orders, orderIndex);
                        break;
                    case "3":
                        // Start the view sales
                        ViewSales(orders, orderIndex);
                        break;
                    case "4":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n\t\t\t\t Thank you for using FoodRush Cashiering System!");
                        Console.WriteLine("\t\t\t\t --------------------------------------------------");
                        Console.WriteLine("\n\t\t\t\t Exiting the system...");
                        Console.ResetColor();

                        inputAgain = false;
                        break;
                }
            }
        }

        // Handle the cashiering transaction //
        //---------------------------------------------------------------------------------------------------------------//

        public static void ProcessCashieringTransaction(string[,] order, ref int orderIndex, ref int orderNumber)
        {

            bool anotherTransaction = true;
            bool validOrder;

            do
            {
                // Initialize variables
                double total = 0, grandTotal = 0;
                string food;
                string today = DateTime.Now.ToString("MM/dd/yyyy");
                int transactionOrderNumber = orderNumber, quantity = 0;
                bool orderAgain = true;
                validOrder = false;


                Console.Clear();

                // Display the Menu
                DisplayMenu(order);

                while (orderAgain)
                {
                    Console.Clear();
                    DisplayMenu(order);


                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n\n >>Enter Item Code : ");
                    string itemNumber = Console.ReadLine();
                    Console.ResetColor();

                    // Get the food name
                    food = GetItemName(itemNumber, order);

                    // Get the price of the item
                    double price = GetItemPrice(itemNumber, order);

                    if (price != 0 && food != "")
                    {
                        quantity = GetValidQuantity();
                        Console.ResetColor();

                        total = price * quantity;
                        grandTotal += total;

                        // Show total price
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n ----------------------------------");
                        //Console.WriteLine("  Subtotal for this item : " + total + " PHP");
                        Console.WriteLine("  Running Total          : " + grandTotal + " PHP");
                        Console.WriteLine(" ----------------------------------");
                        Console.ResetColor();
                        // Save the order
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n\n\t\t\t\t[Order Saved]");
                        SaveOrderData(order, ref orderIndex, transactionOrderNumber, today, itemNumber, food, price, quantity, total);
                        validOrder = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\n\t\tItem not found, please try again.");

                    }
                    // Ask if the user wants to order again
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n\n\t\t >>Do you want to order again? (Y/N): ");
                    string continueOpt = Console.ReadLine();

                    // Validate user input
                    while (continueOpt != "Y" && continueOpt != "y" && continueOpt != "N" && continueOpt != "n")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n\t\t\t Invalid input. Please enter any key to try again.");
                        Console.ReadKey();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\n\n\t\t >>Do you want to order again? (Y/N): ");
                        continueOpt = Console.ReadLine();
                        Console.ResetColor();
                    }
                    if (continueOpt == "Y" || continueOpt == "y")
                    {

                        orderAgain = true;
                    }
                    else
                    {
                        orderAgain = false;
                    }
                }

                // Check if a valid order was made
                if (validOrder == true && total > 0)
                {
                    Console.Clear();
                    orderNumber++;
                    // Show order summary
                    Console.ForegroundColor = ConsoleColor.White;
                    DisplayOrderSummary(order, ref orderIndex, transactionOrderNumber, today, grandTotal);
                    Console.ResetColor();
                    // Ask for payment
                    double cash = 0;

                    // Calculate the change
                    CalculateChange(cash, grandTotal);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\n [Transaction Saved]");


                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\t\t No valid order was made.");
                }
                // Ask if user wants another transaction
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n\n >>Start New Transaction (Y/N): ");
                string anotherTrans = Console.ReadLine();

                // Validate user input
                while (anotherTrans != "Y" && anotherTrans != "y" && anotherTrans != "N" && anotherTrans != "n")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n\t\t\t Invalid input. Please enter Y or N: ");
                    anotherTrans = Console.ReadLine();
                }
                if (anotherTrans == "Y" || anotherTrans == "y")
                {
                    Console.Clear();
                    // Display the menu again
                    DisplayMenu(order);
                    anotherTransaction = true;
                }
                else
                {
                    anotherTransaction = false;
                }

            } while (anotherTransaction);
        }

        // Set up the menu data // - Menu Display
        //---------------------------------------------------------------------------------------------------------------//


        public static void InitializeFoodData(string[,] order)
        {
            // Combo Meals
            order[0, 0] = "C1"; order[0, 1] = "Classic Ham Burger + Fries                   "; order[0, 2] = "159.00";
            order[1, 0] = "C2"; order[1, 1] = "Chicken Nuggets w/rice + Double Cheese Burger"; order[1, 2] = "199.00";
            order[2, 0] = "C3"; order[2, 1] = "Chicken Burger + Spaghetti                   "; order[2, 2] = "179.00";
            order[3, 0] = "C4"; order[3, 1] = "Pork Steak w/rice + Classic Ham Burger       "; order[3, 2] = "189.00";
            order[4, 0] = "C5"; order[4, 1] = "Fish Fillet w/rice + Rice + Mango Juice      "; order[4, 2] = "169.00";

            // Drinks
            order[5, 0] = "D1"; order[5, 1] = "Iced Tea                                     "; order[5, 2] = "39.00";
            order[6, 0] = "D2"; order[6, 1] = "Soft Drinks                                  "; order[6, 2] = "49.00";
            order[7, 0] = "D3"; order[7, 1] = "Bottled Water                                "; order[7, 2] = "29.00";
            order[8, 0] = "D4"; order[8, 1] = "Mango Juice                                  "; order[8, 2] = "59.00";
            order[9, 0] = "D5"; order[9, 1] = "Coffee                                       "; order[9, 2] = "45.00";

            // Desserts
            order[10, 0] = "DE1"; order[10, 1] = "Ice Cream                                    "; order[10, 2] = "59.00";
            order[11, 0] = "DE2"; order[11, 1] = "Brownies                                     "; order[11, 2] = "69.00";
            order[12, 0] = "DE3"; order[12, 1] = "Cheesecake                                   "; order[12, 2] = "89.00";
            order[13, 0] = "DE4"; order[13, 1] = "Fruit Salad                                  "; order[13, 2] = "79.00";
            order[14, 0] = "DE5"; order[14, 1] = "Leche Flan                                   "; order[14, 2] = "99.00";
        }

        // Show the menu items to the user // - Cashiering Transaction 
        //---------------------------------------------------------------------------------------------------------------//


        public static void DisplayMenu(string[,] order)
        {
            // Display the menu header
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ╔════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine(" ║                            FoodRush Menu                                   ║");
            Console.WriteLine(" ╚════════════════════════════════════════════════════════════════════════════╝");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n   Menu Code      Meal Name                                        Price  ");
            Console.WriteLine(" ╔════════════════════════════════════════════════════════════════════════════╗");

            // Display Combo Meals
            Console.WriteLine("\n [Combo Meals]---------------------------------------------------------\n");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("    [{0}]          {1,-30}   {2,7}", order[i, 0], order[i, 1], order[i, 2]);
            }

            // Display Drinks
            Console.WriteLine("\n [Drinks]---------------------------------------------------------------\n");
            for (int i = 5; i < 10; i++)
            {
                Console.WriteLine("    [{0}]          {1,-30}   {2,7}", order[i, 0], order[i, 1], order[i, 2]);
            }

            // Display Desserts
            Console.WriteLine("\n [Desserts]--------------------------------------------------------------\n");
            for (int i = 10; i < 15; i++)
            {
                Console.WriteLine("    [{0}]         {1,-30}   {2,7}", order[i, 0], order[i, 1], order[i, 2]);
            }

            Console.WriteLine(" ╚════════════════════════════════════════════════════════════════════════════╝");
        }


        // Get the item name using the code // - Cashiering Transaction
        //---------------------------------------------------------------------------------------------------------------//

        public static string GetItemName(string orderChoice, string[,] order)
        {
            string food = "";
            orderChoice = orderChoice.ToUpper();
            for (int i = 0; i < order.GetLength(0); i++)
            {
                // Check if the item code matches the order choice
                if (order[i, 0] == orderChoice)
                {
                    food = order[i, 1]; // Get the food name
                    break;
                }
            }
            return food;
        }

        // Get the price using the code // - Cashiering Transaction
        //---------------------------------------------------------------------------------------------------------------//

        public static double GetItemPrice(string orderChoice, string[,] order)
        {
            // Initialize variables
            double price = 0;
            // Initialize a bool to check if the price was found
            bool priceFound = false;

            // Convert the order choice to uppercase for case-insensitive comparison
            orderChoice = orderChoice.ToUpper();

            // Loop through the order array to find the price
            for (int a = 0; a < 15; a++)
            {
                if (orderChoice == order[a, 0])
                {
                    // If the item code is found, get the price
                    price = Convert.ToDouble(order[a, 2]);
                    priceFound = true;
                    break;
                }
            }
            if (!priceFound)
            {
                // If the item code is not found, display an error message
                return 0;
            }
            return price;
        }

        // Get a valid quantity // - Cashiering Transaction
        //---------------------------------------------------------------------------------------------------------------//

        public static int GetValidQuantity()
        {
            bool validQuantity = true;
            int quantity = 0;
            while (validQuantity)
            {
                try
                {
                    // Prompt the user for the quantity
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n >>Enter quantity  : ");
                    quantity = int.Parse(Console.ReadLine());
                    if (quantity <= 0)
                    {
                        // If the quantity is invalid, prompt the user to enter a valid amount
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\t\t Invalid quantity. Please enter a valid amount! ");
                        validQuantity = true;
                    }
                    else
                    {
                        // If the quantity is valid, exit the loop
                        validQuantity = false;
                    }
                }
                catch (FormatException)
                {
                    // Handle invalid input
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\t\t Invalid input. Please enter a valid number! ");
                    validQuantity = true;
                }
            }
            return quantity;
        }

        // Method to save order data// - 2D Array
        //---------------------------------------------------------------------------------------------------------------//

        public static void SaveOrderData(string[,] order, ref int orderIndex, int transactionOrderNumber, string today, string itemNumber, string food, double price, int quantity, double total)
        {
            order[orderIndex, 3] = Convert.ToString(transactionOrderNumber);  // Order Number
            order[orderIndex, 4] = today;                                    // Date
            order[orderIndex, 5] = itemNumber.ToUpper();                    // Item Number
            order[orderIndex, 6] = food;                                   // Food Item
            order[orderIndex, 7] = Convert.ToString(price);               // Price
            order[orderIndex, 8] = Convert.ToString(quantity);           // Quantity
            order[orderIndex, 9] = Convert.ToString(total);             // Total * quantity = total


            orderIndex++;  // Increment the order index for the next order
        }

        // Show the full summary of the order// - Cashiering Transaction
        //---------------------------------------------------------------------------------------------------------------//

        public static void DisplayOrderSummary(string[,] orders, ref int orderIndex, int orderNumber, string today, double grandTotal)
        {
            //display order summary header
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                  ORDER SUMMARY                                                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|  Item Number  |         Food Item                             |   Unit Price      |    Quantity    |   Sub Total  |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");


            for (int i = 0; i < orderIndex; i++)
            {
                if (orders[i, 3] == orderNumber.ToString())
                {
                    Console.WriteLine("|      {0,-8} | {1,-16} |     {2,-10}PHP |        {3,-7} |   {4,-5}PHP   |", orders[i, 5], orders[i, 6], orders[i, 7], orders[i, 8], orders[i, 9]);
                }
            }
            //display order summary details
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|Order Number : {0,93}       |", orderNumber);
            Console.WriteLine("|Date         :    {0,93}    |", today);
            Console.WriteLine("|Amount Due   : {0,91} PHP     |", grandTotal);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

        }

        // Ask for payment and show change // - Cashiering Transaction
        //---------------------------------------------------------------------------------------------------------------//

        public static bool CalculateChange(double cash, double grandTotal)
        {
            bool validCash = true;
            double change;

            while (validCash)
            {
                try
                {
                    //prompt user for cash amount
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n >>Enter Cash : ");
                    cash = double.Parse(Console.ReadLine());

                    //validate cash amount
                    if (cash < grandTotal)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\t\t\t Insufficient cash. Please enter a valid amount! ");
                    }
                    else
                    {
                        validCash = false;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\t\t\t Invalid input. Please enter a valid amount: ");
                }
            }
            change = cash - grandTotal; //compute change
            if (cash == grandTotal)
            {
                //  display message if no change is required
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\t\t\t Exact amount received. No change required.");
            }
            else
            {
                // display change
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n ------------------------");
                Console.WriteLine(" | Total Change : " + change + "   |");
                Console.WriteLine(" ------------------------");
                Console.ResetColor();
            }
            return validCash;
        }

        //view customer order method//
        //---------------------------------------------------------------------------------------------------------------//

        public static void ViewCustomerOrder(string[,] orders, int orderIndex)
        {
            bool continueTransaction = false;
            do
            {
                Console.Clear();

                //validate order number
                Console.Write("\n\t\t >>Enter order number      : ");
                string orderNumber = Console.ReadLine();

                //validate date format
                Console.Write("\n\t\t >>Enter date [MM/DD/YYYY] : ");
                string dateOrder = Console.ReadLine();
                Console.ResetColor();
                //check if order number and date exist
                if (IsValidOrder(orderNumber, dateOrder, orders, orderIndex))
                {
                    //display order report header
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\n[Order Found]");
                    DisplayOrderDetails(orders, orderIndex, orderNumber, dateOrder);

                    //display order report details
                    double totalAmount = CalculateTotalAmount(orders, orderIndex, orderNumber, dateOrder);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("| Grand Total  :      {0,88}  PHP |", totalAmount);
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

                }
                else
                {
                    //display message if transaction not found
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\t\t Transaction not found for the given order number and date.");
                }
                // Ask if user wants another transaction
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n\t >>Start New Transaction? (Y/N): ");
                string anotherTrans = Console.ReadLine();

                //validate user input
                while (anotherTrans != "Y" && anotherTrans != "y" && anotherTrans != "N" && anotherTrans != "n")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n\n\t\tOops! Something went wrong with your input. Please try again.");
                    Console.ReadKey();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n\n\t >>Start New Transaction? (Y/N): ");
                    anotherTrans = Console.ReadLine();
                }

                if (anotherTrans == "Y" || anotherTrans == "y")
                {
                    continueTransaction = false;
                }
                else
                {
                    continueTransaction = true;
                }

            } while (!continueTransaction);
        }

        //validate order// - View Customer Order
        //---------------------------------------------------------------------------------------------------------------//

        public static bool IsValidOrder(string orderNumber, string dateOrder, string[,] orders, int orderIndex)
        {
            //check if order number and date exist
            for (int j = 0; j < orderIndex; j++)
            {
                if (orders[j, 3] == orderNumber && orders[j, 4] == dateOrder)
                {
                    return true;
                }
            }
            return false;
        }

        //display overall order details // - View Customer Order
        //---------------------------------------------------------------------------------------------------------------//

        public static void DisplayOrderDetails(string[,] orders, int orderIndex, string orderNumber, string dateOrder)
        {
            //display order report header
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                  ORDER DETAILS                                                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|               |                                               |                   |                |              |");
            Console.WriteLine("|  Item Number  |         Food Item                             |    Unit Price     |    Quantity    |    Total     |");
            Console.WriteLine("|               |                                               |                   |                |              |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

            for (int j = 0; j < orderIndex; j++)
            {
                if (orders[j, 3] == orderNumber && orders[j, 4] == dateOrder)
                {
                    //display order report details
                    Console.WriteLine("|      {0,-8} | {1,-16} |     {2,-5}PHP      |        {3,-7} |     {4,-5}PHP |", orders[j, 5],
                                                                                                                        orders[j, 6],
                                                                                                                        orders[j, 7],
                                                                                                                        orders[j, 8],
                                                                                                                        orders[j, 9]);
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

                }
            }


        }
        //calculate overall total// - View Customer Order
        //---------------------------------------------------------------------------------------------------------------//

        public static double CalculateTotalAmount(string[,] orders, int orderIndex, string orderNumber, string dateOrder)
        {
            double totalAmount = 0;
            //calculate grand total
            for (int j = 0; j < orderIndex; j++)
            {
                if (orders[j, 3] == orderNumber && orders[j, 4] == dateOrder)
                {
                    totalAmount += Convert.ToDouble(orders[j, 9]);
                }
            }
            return totalAmount;
        }

        //view sales method//
        //---------------------------------------------------------------------------------------------------------------//

        public static void ViewSales(string[,] orders, int orderIndex)
        {
            bool continueTransaction = false;
            do
            {
                Console.Clear();

                //prompt user for date
                Console.Write("\n\t\t >>Enter date [MM/DD/YYY]: ");
                string date = Console.ReadLine();

                //validate date format
                if (IsTransactionFound(date, orders, orderIndex))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\n[Order Found]");
                    DisplayOverallTotalSales(orders, orderIndex, date);

                    //display order report details
                    double totalAmount = CalculateOverAllSales(orders, orderIndex, date);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("| Total Sales   :      {0,75}  PHP |", totalAmount);
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------");

                }
                else
                {
                    //display message if transaction not found
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\t\t Transaction not found for the given date.");
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n\t\t >>Start New Transaction? (Y/N): ");
                string anotherTrans = Console.ReadLine();

                //validate user input
                while (anotherTrans != "Y" && anotherTrans != "y" && anotherTrans != "N" && anotherTrans != "n")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n\n\t\t Oops! Something went wrong with your input. Please try again.");
                    Console.ReadKey();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n\t\t >>Start New Transaction? (Y/N): ");
                    anotherTrans = Console.ReadLine();
                }
                if (anotherTrans == "Y" || anotherTrans == "y")
                {
                    continueTransaction = false;
                }
                else
                {
                    continueTransaction = true;
                }
            } while (!continueTransaction);


        }

        //validate order// - View Sales
        //---------------------------------------------------------------------------------------------------------------//

        public static bool IsTransactionFound(string dateOrder, string[,] orders, int orderIndex)
        {
            //check if date exist
            for (int j = 0; j < orderIndex; j++)
            {
                if (orders[j, 4] == dateOrder)
                {
                    return true;
                }
            }
            return false;
        }

        //display overall total sales// - View Sales
        //---------------------------------------------------------------------------------------------------------------//

        public static void DisplayOverallTotalSales(string[,] orders, int orderIndex, string dateOrder)
        {
            //display sales report header
            // Header with Smooth Box and Color
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                SALES REPORT                                           ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|                                               |                   |                    |              |");
            Console.WriteLine("|         Food Item                             |    Unit Price     |      Quantity      |    Total     |");
            Console.WriteLine("|                                               |                   |                    |              |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");

            for (int j = 0; j < orderIndex; j++)
            {
                if (orders[j, 4] == dateOrder)
                {
                    //display sales report details
                    Console.WriteLine("| {1,-16} |     {2,-5}PHP      |        {3,-7}     |     {4,-5}PHP |", orders[j, 5],
                                                                                                                        orders[j, 6],
                                                                                                                        orders[j, 7],
                                                                                                                        orders[j, 8],
                                                                                                                        orders[j, 9]);
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------");

                }
            }


        }

        //calculate overall total sales// - View Sales
        //---------------------------------------------------------------------------------------------------------------//

        public static double CalculateOverAllSales(string[,] orders, int orderIndex, string dateOrder)
        {
            double totalAmount = 0;
            //calculate total sales
            for (int j = 0; j < orderIndex; j++)
            {
                if (orders[j, 4] == dateOrder)
                {
                    totalAmount += Convert.ToDouble(orders[j, 9]);
                }
            }
            return totalAmount;
        }






    }


}


