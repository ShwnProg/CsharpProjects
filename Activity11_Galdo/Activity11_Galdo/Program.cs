using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity11_Galdo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] laptopSpecification = new string[100, 4];
            string[,] weeklyexpenses = new string[100, 7];
            string[,] studentGrades = new string[100, 3];

            CollectionOfArray();

            Console.Read();
        }
        public static void CollectionOfArray()
        {
            string[,] StudentGrades ={
                                       {"Kyle  ","3.0","PASSED"},
                                       {"Shawn ","1.0","PASSED"},
                                       {"John  ","5.0","FAILED"}, 
                                       {"Mariah","2.8","PASSED"},
                                       {"Rex   ","3.1","FAILED"}
                                       };

            string[,] SubjectDetails = { 
                                      {"GE-CC08","Ethics"},
                                      {"GE-CC05","Purposive Communication"},
                                      {"GE-CC04","Mathematics in the Modern World"},
                                      {"IT 103L","Computer Programming 1"},
                                      {"IT 103" ," Computer Programming 1"}
                                      };


            Console.WriteLine("\nStudent Grades");
            Console.WriteLine("\n------------------------------------------------------------");
            Console.WriteLine("Student Name      Student Average Grade     Student Remarks");
            Console.WriteLine("------------------------------------------------------------");

            for (int i = 0; i < StudentGrades.GetLength(0); i++)
            {

                Console.WriteLine("  {0}              {1}                       {2} ", StudentGrades[i, 0], 
                                                                                       StudentGrades[i, 1],    
                                                                                       StudentGrades[i, 2]);
            }
            Console.WriteLine("------------------------------------------------------------");

            Console.WriteLine("\n\nSubject Details");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Course Code            Subject Title");
            Console.WriteLine("------------------------------------------------------------");


            for (int x = 0; x < SubjectDetails.GetLength(0); x++)
            {

                Console.WriteLine("  {0}               {1}  ", SubjectDetails[x, 0],
                                                                 SubjectDetails[x, 1]);
            }
     
        }
    }
}
