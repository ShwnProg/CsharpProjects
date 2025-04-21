using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string[,] UserOrder = new string[];
            //UserLogin();
            MenuOption();
            MenuChoices();
        }
        public static void UserLogin()
        {
            Console.WriteLine("Log in");
            Console.Write("\n Username: ");
            string username = Console.ReadLine();

            Console.Write("\n Password: ");
            string password = Console.ReadLine();

            LoginAuthentication(username, password);

        }
        public static void LoginAuthentication(string username, string password)
        {
            while (username != "admin" || password != "admin123") 
            {
                Console.WriteLine("Invalid Username or Password try again");

                Console.Write("\n Username: ");
                username = Console.ReadLine();

                Console.Write("\n Password: ");
                password = Console.ReadLine();

                if(username == "admin" || password == "admin123")
                {
                    Console.WriteLine("\n\n Log in Successfuly");

                    break;
                }
            }
            
        }
        public static void MenuOption()
        {
            Console.WriteLine("Menu");

            Console.WriteLine("[1] Cashiering Transaction");
            Console.WriteLine("[2] View Customer Order");
            Console.WriteLine("[3] View Sales");

            Console.Write("Input Here: ");
            int input = Convert.ToInt32(Console.ReadLine());

            while(input < 1 && input > 3)
            {
                Console.WriteLine("Invalid Input Try again");

                Console.Write("Input Here: ");
                input = Convert.ToInt32(Console.ReadLine());
            }
        }
        //public static void CashieringTransaction()
        //{

        //}
        public static void MenuChoices()
        {
            string[,] comboMeal = {   {"[C1]", "Spaghetti with Fries and Fried Chicken", "125 PHP"},
                                      {"[C2]", "2 PC Burger Steak with Spaghetti      ", "130 PHP"},
                                      {"[C3]", "Chicken Fillet with Carbonara         ", "175 PHP"},
                                      {"[C4]", "Chicken Nuggets with Cheese Burger    ", "160 PHP"},
                                      {"[C5]", "Sizzling Sisig with Lumpiang Shanghai ", "199 PHP"}
                                    };

            string[,] Drinks = { {"[D1]", "Pineapple Juice", "  65 PHP"},
                                 {"[D2]", "Sprite         ", "  60 PHP"},
                                 {"[D3]", "Coke           ", "  70 PHP"},
                                 {"[D4]", "Ice Tea        ", "  65 PHP"},
                                 {"[D5]", "Lemon Juice    ", "  60 PHP"}
                               };

            string[,] Dessert = { {"[E1]", "Banana Split", "70 PHP"},
                                 {"[E2]", "Cheesecake   ", "75 PHP"},
                                 {"[E3]", "Banana cake  ", "80 PHP"},
                                 {"[E4]", "Brownies     ", "95 PHP"},
                                 {"[E5]", "Apple Pie    ", "60 PHP"}
                               };


            Console.WriteLine("====================================================================================");
            Console.WriteLine("|                                            Menu                                  |");
            Console.WriteLine("====================================================================================");
            Console.WriteLine("|               Combo Meal          |");
            Console.WriteLine("========================================================================");

            for(int i = 0; i < comboMeal.GetLength(0); i++)
            {
                for(int j = 0; j < comboMeal.GetLength(1); j++)
                {
                    Console.Write("{0}         {1}              {2}",comboMeal[i,j], Drinks[i, j], Dessert[i,j]);
                }
                //Console.WriteLine();
            }
        }

    }
}
