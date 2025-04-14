using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity10_Galdo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] AccountName = new string[100];
            string[] Username = new string[100];
            string[] Password = new string[100];
            int i = 0;
            string inputAgain;
            bool isValid = false;
            do
            {
                isValid = true;
                Console.Write("\n Enter Account Name: ");
                string accName = Console.ReadLine();

                Console.Write("\n Enter Username    : ");
                string userName = Console.ReadLine();

                Console.Write("\n Enter Password    : ");
                string userPass = Console.ReadLine();
                
                for (int j = 0; j < i; j++)
                {
                    if (AccountName[j] == accName || Username[j] == userName)
                    {
                        Console.WriteLine("\n\n\t Duplicate Account Name or Username found. Please try again.");
                        isValid = false;
                     
                    }
                }
                if(isValid == true)
                {
                    AccountName[i] = accName;
                    Username[i] = userName;
                    Password[i] = userPass;
                    Console.WriteLine("\n\t\t Account Successfully Created!");
                    i++;
                }
                Console.Write("\n\n\n >>Input Again? (Y/N): ");
                inputAgain = Console.ReadLine();

                while(inputAgain != "Y" && inputAgain != "y" && inputAgain != "N" && inputAgain != "n")
                {
                    Console.WriteLine("\n\t Invalid input. Please enter Y or N.");
                    Console.Write("\n\n >>Input Again? (Y/N): ");
                    inputAgain = Console.ReadLine();
                }
             
            } while (inputAgain == "Y" || inputAgain == "y");

            //Console.Clear();
            Console.WriteLine("============================================================================================");
            Console.WriteLine("|                                        Account List                                      |");
            Console.WriteLine("============================================================================================");
            Console.WriteLine("|          Account Name          |          Username          |          Password          |");
            Console.WriteLine("============================================================================================");

            for(int j = 0; j < i; j++)
            {
                Console.WriteLine("| {0,-30} | {1,-25}  | {2,-25}  |", AccountName[j], Username[j], Password[j]);
                Console.WriteLine("============================================================================================");
            }

        }
    }
}
