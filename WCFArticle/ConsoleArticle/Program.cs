using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleArticle
{
    class Program
    {
        static void Main(string[] args)
        {
            TxtFileArticle start = new TxtFileArticle();
            while (true)
            {
                Console.WriteLine("\n*********************************** \t WELCOME \t ***********************************\n");
                Console.WriteLine("\n You can: [1] Create new aricle \n\t  [2] See all articles " +
                    "\n\t  [3] Edit Price  \n\t  [4] Create a bill \n\t  [5] Terminate the program");
                bool repeat;
                uint choosenNo;
                do
                {
                    Console.Write("\nChoose 1 to 5 -> ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("\nYou must provide some input!");
                        repeat = true;
                    }
                    else if (!UInt32.TryParse(input, out choosenNo))
                    {
                        Console.WriteLine("\nRead instructions!!!!");
                        repeat = true;
                    }
                    else
                    {
                        choosenNo = UInt32.Parse(input);
                        if (choosenNo == 1)
                        {
                            start.CreateArticle();
                            repeat = false;
                        }
                        else if (choosenNo == 2)
                        {
                            start.ShowAllArticles();
                            repeat = false;
                        }
                        else if (choosenNo == 3)
                        {
                            start.PriceModify();
                            repeat = false;
                        }
                        else if (choosenNo == 4)
                        {
                            start.BillCreation();
                            repeat = false;
                        }
                        else if (choosenNo == 5)
                        {
                            repeat = false;
                            System.Environment.Exit(1);
                        }
                        else
                        {
                            Console.WriteLine("\nRead instructions!!!!");
                            repeat = true;
                        }
                    }
                } while (repeat);

                Console.ReadKey();
            }
        }
    }
}
