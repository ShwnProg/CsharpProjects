// Cashiering System using C# (Beginner-friendly version - local variables, basic placeholders only)
using System;

namespace CashieringSystem
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("LOGIN");
                Console.Write("Enter username: ");
                string user = Console.ReadLine();
                Console.Write("Enter password: ");
                string pass = Console.ReadLine();
                if (user == "admin" && pass == "1234")
                    MainMenu();
                else
                {
                    Console.WriteLine("Invalid credentials!");
                    Console.ReadKey();
                }
            }
        }

        static void MainMenu()
        {
            string[,] orders = new string[100, 6];
            int orderIndex = 0;
            int orderNumber = 1001;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("MAIN MENU");
                Console.WriteLine("1. Cashiering Transaction");
                Console.WriteLine("2. View Customer Order");
                Console.WriteLine("3. View Sales");
                Console.WriteLine("4. Exit");
                Console.Write("Select option: ");
                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1": Cashiering(ref orders, ref orderIndex, ref orderNumber); break;
                    case "2": ViewCustomerOrder(orders, orderIndex); break;
                    case "3": ViewSales(orders, orderIndex); break;
                    case "4": return;
                    default: Console.WriteLine("Invalid option"); Console.ReadKey(); break;
                }
            }
        }

        static void DisplayMenu(string[] items, double[] prices)
        {
            Console.WriteLine("\n--- MENU ---");
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine("{0}. {1} - {2}", (i + 1), items[i], prices[i]);
            }
        }

        static void Cashiering(ref string[,] orders, ref int orderIndex, ref int orderNumber)
        {
            string[] items = {
                "Spaghetti", "Burger", "Chicken", "Pizza", "Steak",
                "Coke", "Sprite", "Water", "Iced Tea", "Juice",
                "Cake", "Ice Cream", "Pie", "Donut", "Brownie"
            };
            double[] prices = {
                100, 80, 120, 150, 200,
                30, 30, 20, 35, 40,
                60, 50, 45, 25, 55
            };

            bool anotherTransaction;
            do
            {
                double total = 0;
                string today = DateTime.Now.ToShortDateString();
                int currentOrder = orderNumber++;

                do
                {
                    Console.Clear();
                    DisplayMenu(items, prices);
                    Console.Write("Enter item number: ");
                    int item = int.Parse(Console.ReadLine());
                    Console.Write("Enter quantity: ");
                    int qty = int.Parse(Console.ReadLine());

                    string name = items[item - 1];
                    double price = prices[item - 1];
                    double itemTotal = price * qty;
                    total += itemTotal;

                    orders[orderIndex, 0] = currentOrder.ToString();
                    orders[orderIndex, 1] = today;
                    orders[orderIndex, 2] = name;
                    orders[orderIndex, 3] = qty.ToString();
                    orders[orderIndex, 4] = price.ToString();
                    orders[orderIndex, 5] = itemTotal.ToString();
                    orderIndex++;

                    Console.WriteLine("\nCurrent Total: {0}", total);
                    Console.Write("Add another item? [Y/N]: ");
                } while (Console.ReadLine().ToUpper() == "Y");

                Console.Clear();
                Console.WriteLine("ORDER SUMMARY");
                Console.WriteLine("Order Number: {0}", currentOrder);
                Console.WriteLine("Date: {0}", today);
                Console.WriteLine("{0,-15} {1,-10} {2,-10} {3,-10}", "Item", "Qty", "Price", "Total");
                for (int i = 0; i < orderIndex; i++)
                {
                    if (orders[i, 0] == currentOrder.ToString())
                    {
                        Console.WriteLine("{0,-15} {1,-10} {2,-10} {3,-10}", orders[i, 2], orders[i, 3], orders[i, 4], orders[i, 5]);
                    }
                }

                Console.WriteLine("\nTotal Amount: {0}", total);
                Console.Write("Enter cash: ");
                double cash = double.Parse(Console.ReadLine());
                double change = cash - total;
                Console.WriteLine("Change: {0}", change);

                Console.Write("\nAnother transaction? [Y/N]: ");
                anotherTransaction = Console.ReadLine().ToUpper() == "Y";

            } while (anotherTransaction);
        }

        static void ViewCustomerOrder(string[,] orders, int orderIndex)
        {
            Console.Clear();
            Console.Write("Enter Order Number: ");
            string orderNum = Console.ReadLine();
            Console.Write("Enter Date (MM/DD/YYYY): ");
            string date = Console.ReadLine();
            bool found = false;
            double totalAmount = 0;

            Console.WriteLine("\nORDER DETAILS");
            Console.WriteLine("{0,-15} {1,-10} {2,-10} {3,-10}", "Item", "Qty", "Price", "Total");
            for (int i = 0; i < orderIndex; i++)
            {
                if (orders[i, 0] == orderNum && orders[i, 1] == date)
                {
                    found = true;
                    Console.WriteLine("{0,-15} {1,-10} {2,-10} {3,-10}", orders[i, 2], orders[i, 3], orders[i, 4], orders[i, 5]);
                    totalAmount += double.Parse(orders[i, 5]);
                }
            }

            if (found)
            {
                Console.WriteLine("\nTotal Amount: {0}", totalAmount);
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
            Console.ReadKey();
        }

        static void ViewSales(string[,] orders, int orderIndex)
        {
            Console.Clear();
            Console.Write("Enter Date (MM/DD/YYYY): ");
            string date = Console.ReadLine();
            double totalSales = 0;

            Console.WriteLine("\nSALES REPORT for Date: {0}", date);
            Console.WriteLine("{0,-15} {1,-10} {2,-10}", "Item", "Qty", "Total");
            for (int i = 0; i < orderIndex; i++)
            {
                if (orders[i, 1] == date)
                {
                    Console.WriteLine("{0,-15} {1,-10} {2,-10}", orders[i, 2], orders[i, 3], orders[i, 5]);
                    totalSales += double.Parse(orders[i, 5]);
                }
            }

            Console.WriteLine("\nTotal Sales for {0}: {1}", date, totalSales);
            Console.ReadKey();
        }
    }
}
