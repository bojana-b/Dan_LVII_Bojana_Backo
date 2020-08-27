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
            start.CreateArticle();
            Console.ReadKey();
        }
    }
}
