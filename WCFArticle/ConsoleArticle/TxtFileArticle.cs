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
                Console.Write("\nArticle name -> ");
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
        // Method to show all articles to console form text file
        public void ShowAllArticles()
        {
            using (Service1Client proxy = new Service1Client())
            {
                var listOfArticles = proxy.GetAllFileArticles();
                Console.WriteLine("\nList of articles from txt file: \n");
                int i = 0;
                foreach (var item in listOfArticles)
                {
                    Console.WriteLine("[{0}] {1}; {2}; {3:N2}", i++, item.Name, item.Quantity, item.Price);
                }
            }
        }
        // Method for modify the article price
        public void PriceModify()
        {
            using (Service1Client proxy = new Service1Client())
            {
                var listOfArticles = proxy.GetAllFileArticles();
                ShowAllArticles();
                bool repeat;
                uint choosenNo = 0;
                double newPrice;
                do
                {
                    Console.Write("\nEnter the number of article you want to change the price -> ");
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
                        if (choosenNo > listOfArticles.Count() || choosenNo < 0)
                        {
                            Console.WriteLine("\nRead instructions!!!!");
                            repeat = true;
                        }
                        else
                        {
                            repeat = false;
                        }
                    }
                } while (repeat);
                {
                    Console.Write("\nNew price -> ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("\nYou must provide some input!");
                        repeat = true;
                    }
                    else if (!Double.TryParse(input, out newPrice))
                    {
                        Console.WriteLine("\nRead instructions!!!!");
                        repeat = true;
                    }
                    else
                    {
                        newPrice = Double.Parse(input);
                        proxy.ModifyPrice(listOfArticles.ElementAt((int)choosenNo), newPrice);
                        repeat = false;
                    }
                } while (repeat);
            }
        }
        // Method for bill creation
        public void BillCreation()
        {
            using (Service1Client proxy = new Service1Client())
            {
                List<ServiceReferenceArticle.FileArticle> listForBill = new List<ServiceReferenceArticle.FileArticle>();
                var listOfArticles = proxy.GetAllFileArticles();
                ShowAllArticles();
                bool repeat;
                bool end = true;
                uint choosenNo = 0;
                uint quantity;
                do
                {
                    ServiceReferenceArticle.FileArticle article = new ServiceReferenceArticle.FileArticle();
                    do
                    {
                        Console.Write("\nEnter the number of article you want to buy -> ");
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
                            if (choosenNo > listOfArticles.Count() || choosenNo < 0)
                            {
                                Console.WriteLine("\nRead instructions!!!!");
                                repeat = true;
                            }
                            else
                            {
                                article.Name = listOfArticles.ElementAt((int)choosenNo).Name;
                                repeat = false;
                            }
                        }
                    } while (repeat);
                    {
                        Console.Write("\nQuantity -> ");
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
                            listOfArticles.ElementAt((int)choosenNo).Quantity -= (int)quantity;
                            article.Price = listOfArticles.ElementAt((int)choosenNo).Price;
                            listForBill.Add(article);
                            proxy.AddAllArticlesToFile(listOfArticles);

                            Console.Write("\nAnother article: Y / N -> ");
                            string yn = Console.ReadLine();
                            if (string.IsNullOrEmpty(yn))
                            {
                                Console.WriteLine("\nYou must provide some input!");
                                repeat = true;
                            }
                            else if (yn.Equals("N") || yn.Equals("n"))
                            {
                                end = false;
                            }
                            else if (yn.Equals("Y") || yn.Equals("y"))
                            {
                                end = true;
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("\nRead instructions!!!!");
                                repeat = true;
                            }
                        }
                    } while (repeat) ;
                } while (end);
                proxy.CreateABill(listForBill.ToArray());
            }
        }
    }
}
