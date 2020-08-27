using ConsoleArticle.ServiceReferenceArticle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFArticle;

namespace ConsoleArticle
{
    class TxtFileArticle
    {
        // UI for creation of new article by client
        public void CreateArticle()
        {
            ServiceReferenceArticle.FileArticle article = new ServiceReferenceArticle.FileArticle();
            bool repeat;
            uint quantity;
            double price;
            do
            {
                Console.Write("Article name -> ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\nYou must provide some input!");
                    repeat = true;
                }
                else
                {
                    article.Name = input;
                    repeat = false;
                }
            } while (repeat);
            do
            {
                Console.Write("Article quantity -> ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\nYou must provide some input!");
                    repeat = true;
                }
                else if (!UInt32.TryParse(input, out quantity))
                {
                    Console.WriteLine("\nRead instructions!!!!");
                    repeat = true;
                }
                else
                {
                    quantity = UInt32.Parse(input);
                    article.Quantity = (int)quantity;
                    repeat = false;
                }
            } while (repeat);

            do
            {
                Console.Write("Article price -> ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\nYou must provide some input!");
                    repeat = true;
                }
                else if (!Double.TryParse(input, out price))
                {
                    Console.WriteLine("\nRead instructions!!!!");
                    repeat = true;
                }
                else
                {
                    price = Double.Parse(input);
                    article.Price = price;
                    repeat = false;
                }
            } while (repeat);
            using (Service1Client proxy = new Service1Client())
            {
                proxy.AddArticleToFile(article);
            }
        }
    }
}
