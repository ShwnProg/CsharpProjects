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
            Console.WriteLine("\n\t\t\t\t\t FoodRush Cashiering System");
            Console.WriteLine("\t\t\t\t -----------------------------------------");

            // Call the login method
            PerformLogin();
        }

                                                    // Let the user log in//
        //---------------------------------------------------------------------------------------------------------------//


        public static void PerformLogin()
        {
            bool isAccess = true;
            Console.WriteLine("\n\t\t\t\t\t --------*LOGIN*-------- \n\n");

            while (isAccess)
            {
                Console.Write("\n\t\t\t\t\t  >>Enter username : ");
                string username = Console.ReadLine();

                Console.Write("\n\t\t\t\t\t  >>Enter password : ");
                string password = Console.ReadLine();

                if (username == "admin" && password == "admin123")
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
                Console.WriteLine("\n\t\t\t\t\t        FoodRush Main Menu");
                Console.WriteLine("\t\t\t\t\t --------------------------------\n");
                Console.WriteLine("\t\t\t\t\t ===============================");
                Console.WriteLine("\t\t\t\t\t | 1. Cashiering Transaction   |");
                Console.WriteLine("\t\t\t\t\t | 2. View Customer Order      |");
                Console.WriteLine("\t\t\t\t\t | 3. View Sales               |");
                Console.WriteLine("\t\t\t\t\t | 4. EXIT                     |");
                Console.WriteLine("\t\t\t\t\t ===============================");


                Console.Write("\n\t\t\t\t >>Select an option: ");
                string userOpt = Console.ReadLine();
                while (userOpt != "1" && userOpt != "2" && userOpt != "3" && userOpt != "4")
                {
                    Console.WriteLine("\n\t\t\tInvalid Input.Try Again.");
                    Console.Write("\n\t\t\t >>Select an option: ");
                    userOpt = Console.ReadLine();
                }
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
                        // (To be added: View sales report)
                        break;
                    case "4":
                        Console.WriteLine("\n\t\t\t Thank you for using FoodRush Cashiering System!");
                        Console.WriteLine("\t\t\t--------------------------------------------------");
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
                double total = 0,grandTotal = 0;
                string food;
                string today = DateTime.Now.ToString("MM/dd/yyyy");
                int transactionOrderNumber = orderNumber, quantity = 0;
                bool orderAgain = true;
                validOrder = false;


                Console.Clear();

                DisplayMenu(order);


                while (orderAgain)
                {


                    Console.Write("\n >>Enter item number : ");
                    string itemNumber = Console.ReadLine();

                    // Get the food name
                    food = GetItemName(itemNumber, order);

                    // Get the price of the item
                    double price = GetItemPrice(itemNumber, order);

                    if (price != 0 && food != "")
                    {
                        quantity = GetValidQuantity();

                        total = price * quantity;
                        grandTotal += total;

                        // Show total price
                        Console.WriteLine("\n =0=0=0=0=0=0=0=0=");
                        Console.WriteLine("  | Total : " + grandTotal + " |");
                        Console.WriteLine(" =0=0=0=0=0=0=0=0=");

                        // Save the order
                        SaveOrderData(order, ref orderIndex, transactionOrderNumber, today, itemNumber, food, price, quantity, total);
                        validOrder = true;
                    }
                    else
                    {
                        Console.WriteLine("\n\n\t\tItem not found, please try again.");

                    }
                    Console.Write("\n\n\t\t >>Do you want to order again? (Y/N): ");
                    string continueOpt = Console.ReadLine();

                    while (continueOpt != "Y" && continueOpt != "y" && continueOpt != "N" && continueOpt != "n")
                    {
                        Console.Write("\n\t\t\t Invalid input. Please enter any key to try again.");
                        Console.ReadKey();
                        Console.Write("\n\n\t\t >>Do you want to order again? (Y/N): ");
                        continueOpt = Console.ReadLine();
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

                if (validOrder == true && total > 0)
                {
                    orderNumber++;
                    // Show order summary
                    DisplayOrderSummary(order, ref orderIndex, transactionOrderNumber, today,grandTotal);

                    // Ask for payment
                    double cash = 0;

                    // Calculate the change
                    CalculateChange(cash, grandTotal);



                }
                else
                {
                    Console.WriteLine("\n\n\t\t No valid order was made.");
                }
                // Ask if user wants another transaction
                Console.Write("\n\n >>Do another transaction? (Y/N): ");
                string anotherTrans = Console.ReadLine();

                while (anotherTrans != "Y" && anotherTrans != "y" && anotherTrans != "N" && anotherTrans != "n")
                {
                    Console.Write("\n\t\t\t Invalid input. Please enter Y or N: ");
                    anotherTrans = Console.ReadLine();
                }
                if (anotherTrans == "Y" || anotherTrans == "y")
                {


                    Console.Clear();

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
            Console.WriteLine("|*------------------------------------FoodRush Menu--------------------------------*|");

            Console.WriteLine("\n   Menu Code      Meal Name                                        Price  ");
            Console.WriteLine(" ------------------------------------------------------------------------------");

            Console.WriteLine("\n [Combo Meals]---------------------------------------------------------\n");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("    [{0}]          {1,-30}   {2,7}", order[i, 0], order[i, 1], order[i, 2]);
            }

            Console.WriteLine("\n [Drinks]---------------------------------------------------------------\n");
            for (int i = 5; i < 10; i++)
            {
                Console.WriteLine("    [{0}]          {1,-30}   {2,7}", order[i, 0], order[i, 1], order[i, 2]);
            }

            Console.WriteLine("\n [Desserts]--------------------------------------------------------------\n");
            for (int i = 10; i < 15; i++)
            {
                Console.WriteLine("    [{0}]         {1,-30}   {2,7}", order[i, 0], order[i, 1], order[i, 2]);
            }

            Console.WriteLine("\n ------------------------------------------------------------------------------");
        }

                                            // Get the item name using the code // - Cashiering Transaction
        //---------------------------------------------------------------------------------------------------------------//

        public static string GetItemName(string orderChoice, string[,] order)
        {
            string food = "";
            orderChoice = orderChoice.ToUpper();
            for (int i = 0; i < order.GetLength(0); i++)
            {
                if (order[i, 0] == orderChoice)
                {
                    food = order[i, 1];
                    break;
                }
            }
            return food;
        }

                                                   // Get the price using the code // - Cashiering Transaction
        //---------------------------------------------------------------------------------------------------------------//

        public static double GetItemPrice(string orderChoice, string[,] order)
        {
            double price = 0;
            bool priceFound = false;
            orderChoice = orderChoice.ToUpper();
            for (int a = 0; a < 15; a++)
            {
                if (orderChoice == order[a, 0])
                {
                    price = Convert.ToDouble(order[a, 2]);
                    priceFound = true;
                    break;
                }
            }
            if (!priceFound)
            {
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
                    Console.Write("\n >>Enter quantity : ");
                    quantity = int.Parse(Console.ReadLine());
                    if (quantity <= 0)
                    {
                        Console.WriteLine("\n\t\t Invalid quantity. Please enter a valid amount! ");
                        validQuantity = true;
                    }
                    else
                    {
                        validQuantity = false;
                    }
                }
                catch (FormatException)
                {
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

        public static void DisplayOrderSummary(string[,] orders, ref int orderIndex, int orderNumber, string today,double grandTotal)
        {
            Console.WriteLine("\n|--------------------------------------------------*Order Summary*--------------------------------------------------|\n");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|  Item Number  |         Food Item                             |   Unit Price      |    Quantity    |   Sub Total  |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

            for (int i = 0; i < orderIndex; i++)
            {
                if (orders[i, 3] == orderNumber.ToString())
                {
                    Console.WriteLine("|      {0,-8} | {1,-16} |     {2,-10}PHP |        {3,-7} |   {4,-5}PHP   |", orders[i, 5], orders[i, 6], orders[i, 7], orders[i, 8], orders[i,9]);
                }
            }

            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|Order Number : {0,93}       |", orderNumber);
            Console.WriteLine("|Date         :    {0,93}    |", today);
            Console.WriteLine("|Grand Total  : {0,91} PHP     |", grandTotal);
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
                    Console.Write("\n >>Enter Cash : ");
                    cash = double.Parse(Console.ReadLine());

                    if (cash < grandTotal)
                    {
                        Console.WriteLine("\n\t\t\t Insufficient cash. Please enter a valid amount! ");
                    }
                    else
                    {
                        validCash = false;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n\t\t\t Invalid input. Please enter a valid amount: ");
                }
            }
            change = cash - grandTotal; //compute change
            if (cash == grandTotal)
            {
                Console.WriteLine("\n\t\t\t Exact amount received. No change required.");
            }
            else
            {

                Console.WriteLine("\n=0=0=0=0=0=0=0=0=0=0=0=0=");
                Console.WriteLine("  | Total Change : " + change + "  |");
                Console.WriteLine("  =0=0=0=0=0=0=0=0=0=0=0=0=");
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
                Console.Write("\n\t >>Enter order number      : ");
                string orderNumber = Console.ReadLine();

                Console.Write("\n\t >>Enter date [MM/DD/YYYY] : ");
                string dateOrder = Console.ReadLine();

                if (IsValidOrder(orderNumber, dateOrder, orders, orderIndex))
                {
                    Console.WriteLine("\n[Order Found]");
                    DisplayOrderDetails(orders, orderIndex, orderNumber, dateOrder);

                    double totalAmount = CalculateTotalAmount(orders, orderIndex, orderNumber, dateOrder);
                    Console.WriteLine("| Grand Total  :      {0,88}  PHP |", totalAmount);
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

                }
                else
                {
                    Console.WriteLine("\n\n\t Transaction not found for the given order number and date.");
                }

                Console.Write("\n\t >>Do you want to process another transaction? (Y/N): ");
                string anotherTrans = Console.ReadLine();

                while (anotherTrans != "Y" && anotherTrans != "y" && anotherTrans != "N" && anotherTrans != "n")
                {
                    Console.Write("\n\n\tOops! Something went wrong with your input. Please try again.");
                    Console.ReadKey();

                    Console.Write("\n\t >>Do you want to process another transaction? (Y/N): ");
                    anotherTrans = Console.ReadLine();
                }

                if(anotherTrans == "Y" || anotherTrans == "y")
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
            Console.WriteLine("\n|-------------------------------------------------*Order Details*---------------------------------------------------|\n");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|               |                                               |                   |                |              |");
            Console.WriteLine("|  Item Number  |         Food Item                             |    Unit Price     |    Quantity    |    Total     |");
            Console.WriteLine("|               |                                               |                   |                |              |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

            for (int j = 0; j < orderIndex; j++)
            {
                if (orders[j, 3] == orderNumber && orders[j, 4] == dateOrder)
                {
                    Console.WriteLine("|      {0,-8} | {1,-16} |     {2,-5}PHP      |        {3,-7} |     {4,-5}PHP |", orders[j, 5], orders[j, 6], orders[j, 7], orders[j, 8], orders[j,9]);
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

                }
            }


        }
                                                 //calculate overall total// - View Customer Order
        //---------------------------------------------------------------------------------------------------------------//

        public static double CalculateTotalAmount(string[,] orders, int orderIndex, string orderNumber, string dateOrder)
        {
            double totalAmount = 0;
            for (int j = 0; j < orderIndex; j++)
            {
                if (orders[j, 3] == orderNumber && orders[j, 4] == dateOrder)
                {
                    totalAmount += Convert.ToDouble(orders[j, 9]);
                }
            }
            return totalAmount;
        }
        //---------------------------------------------------------------------------------------------------------------//







    }


}


