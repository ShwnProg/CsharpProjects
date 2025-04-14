using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRushMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] cart = new string[100, 3];
            int cartindex = 0;
            string[,] Menu = {      {"[1]", "Pork Sinigang ","       A sour soup with pork and vegetables.                       ","400 PHP" },
                                    {"[2]", "Chicken Adobo ","       Chicken cooked in soy sauce and vinegar                     ","450 PHP"},
                                    {"[3]", "Beef Caldereta","     Stew with Beef, potatoes, and tomato sauce                    ","300 PHP" },
                                    {"[4]", " Crispy Pata  "," Deep-Fried knuckle, crispy on the outside and tender on the inside","650 PHP"}};
            double totalAmountOrder = 0;
            bool istrue = true;
            string message;
            do
            {
                try
                {
                    // Menu Table
                    Console.WriteLine("=============================================================================================================");
                    Console.WriteLine("|                                                 FoodRush Menu                                             |");
                    Console.WriteLine("=============================================================================================================");
                    Console.WriteLine("|          Dish         |                              Description                               |  Price   |");
                    Console.WriteLine("=============================================================================================================");

                    for (int i = 0; i < Menu.GetLength(0); i++)
                    {
                        Console.WriteLine("| {0} {1}    |   {2}  |  {3} |", Menu[i, 0], Menu[i, 1], Menu[i, 2], Menu[i, 3]);
                    }
                    Console.WriteLine("=============================================================================================================");

                    //Ask User Order
                    totalAmountOrder = UserOrder(Menu);
                    Console.WriteLine("\n\n\t\t =0=0=0=0=0=0=0=0=0=0=0=0=0=0=");
                    Console.WriteLine("\t\t |      Total Amount: " + totalAmountOrder +"   |");
                    Console.WriteLine("\t\t =0=0=0=0=0=0=0=0=0=0=0=0=0=0=");

                    if(totalAmountOrder > 0)
                    {
                        ApplyDiscountOpt(totalAmountOrder);
                    }
                    else
                    {
                        Console.WriteLine("\n No order placed. No discount needed.");
                    }

                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    Console.Write("\n\n Input All Over Again[Y/N]: ");
                    message = Console.ReadLine();

                    while(message != "Y" && message != "y" && message != "N" && message != "n")
                    {
                        Console.WriteLine("\n\n\t >>Invalid Input Try Again.");

                        Console.Write("\n\n Input All Over Again[Y/N]: ");
                        message = Console.ReadLine();
                    }
                    if(message == "Y" || message == "y")
                    {
                        istrue = true;
                        Console.Clear();
                    }
                    else
                    {
                        istrue = false;
                    }
                }
            } while (istrue);
        }
        public static double UserOrder(string[,] menu)
        {
            int order;
            double totalAmount = 0;
            while (true)
            {
                Console.Write("\n Order Here[0 to stop]: ");
                order = Convert.ToInt32(Console.ReadLine());
                while (order != 1 && order != 2 && order != 3 && order != 4 && order != 0)
                {
                    Console.WriteLine("\n\n Invalid input try again. ");

                    Console.Write("\n Order Here[Enter 0 to stop]: ");
                    order = Convert.ToInt32(Console.ReadLine());
                }
                if (order == 0)
                {
                    break;
                }
                totalAmount += UserOrder(order,menu);

            }
            return totalAmount;

        }
        public static double UserOrder(int order, string[,] menu)
        {
            int quantity;
            double totalAmount = 0;

            switch (order)
            {
                case 1:
                    while (true)
                    {
                        Console.Write("\n >>Enter Quantity for " + menu[0, 1] + ": ");
                        quantity = Convert.ToInt32(Console.ReadLine());

                        if(quantity > 0)
                        {
                            break;
                        }
                    }
                    totalAmount = quantity * 400;
                    return totalAmount;
                case 2:
                    while (true)
                    {
                        Console.Write("\n >>Enter Quantity for " + menu[1, 1] + ": ");
                        quantity = Convert.ToInt32(Console.ReadLine());
                        if(quantity > 0)
                        {
                            break;
                        }
                    }

                    totalAmount = quantity * 450;
                    return totalAmount;
                case 3:
                    while (true)
                    {
                        Console.Write("\n >>Enter Quantity for " + menu[2, 1] + ": ");
                        quantity = Convert.ToInt32(Console.ReadLine());

                        if(quantity > 0)
                        {
                            break;
                        }
                    }

                    totalAmount = quantity * 300;
                    return totalAmount;
                case 4:
                    while (true)
                    {
                        Console.Write("\n >>Enter Quantity for " + menu[3, 1] + ": ");
                        quantity = Convert.ToInt32(Console.ReadLine());
                        if(quantity > 0)
                        {
                            break;
                        }
                    }

                    totalAmount = quantity * 650;
                    return totalAmount;
                default: 
                    return 0;
            }

        }
        public static double ApplyDiscountOpt(double totalAmount)
        {
            Console.Write("\n Apply Discount [Y/N]: ");
            string applyDis = Console.ReadLine();

            while (applyDis != "Y" && applyDis != "y" && applyDis != "n" && applyDis != "N")
            {
                Console.WriteLine("\n\n Invalid Input try again.");
                Console.Write("\n Apply Discount [Y/N]: ");
                applyDis = Console.ReadLine();
            }
            if (applyDis == "Y" || applyDis == "y")
            {
                Console.Clear();
                totalAmount = CostumerDiscount(totalAmount);
            }
            return totalAmount;
        }
        public static double CostumerDiscount(double totalAmount)
        {
            double discount, discountedAmount = 0;
            string category;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\t\t\t   =================================");
            Console.WriteLine("\t\t\t   |          Apply Discount       | ");
            Console.WriteLine("\t\t\t   =================================");
            Console.WriteLine("\t\t\t   |      [S] Senior Citizen       |");
            Console.WriteLine("\t\t\t   |      [P]      PWD             |");
            Console.WriteLine("\t\t\t   =================================");
            Console.ResetColor();



            Console.Write("\n Enter here: ");
            category = Console.ReadLine();

            while (category != "S" && category != "s" && category != "P" && category != "p")
            {
                Console.WriteLine("\nInvalid input. Try again.");
                Console.Write("\n Enter here: ");
                category = Console.ReadLine();
            }
            if (category == "S" || category == "s")
            {
                discount = totalAmount * 0.20;
                discountedAmount = totalAmount - discount;
            }
            if (category == "P" || category == "p")
            {
                discount = totalAmount * 0.25;
                discountedAmount = totalAmount - discount;
            }
            Console.WriteLine(" \n =0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0");
            Console.WriteLine($" \n    >>Discount Applied! (20% OFF)");
            Console.WriteLine($" \n    >>Discounted Amount: {discountedAmount} PHP");
            Console.WriteLine(" \n =0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0=0");


            return discountedAmount;
        }
        public static 
    }
}
