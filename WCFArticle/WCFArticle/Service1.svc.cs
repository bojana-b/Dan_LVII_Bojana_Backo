using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFArticle
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private readonly string fileArticles = AppDomain.CurrentDomain.BaseDirectory + @"\Articles.txt";

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        // Function that add one article to Articles.txt file
        public void AddArticleToFile(FileArticle article)
        {
            if (!File.Exists(fileArticles))
            {
                File.Create(fileArticles).Close();
            }
            try
            {
                using (StreamWriter sw = File.AppendText(fileArticles))
                {
                    sw.WriteLine(article.ToString());
                }
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine($"The file was not found: '{e}'");
            }
            catch (IOException e)
            {
                Debug.WriteLine($"The file could not be opened: '{e}'");
            }
        }

        // Function that read all articles from Articles.txt file
        public List<FileArticle> GetAllFileArticles()
        {
            List<FileArticle> listOfArticles = new List<FileArticle>();
            if (!File.Exists(fileArticles))
            {
                File.Create(fileArticles).Close();
            }

            try
            {
                using (StreamReader sr = File.OpenText(fileArticles))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        char delimiter = ';';
                        string[] data = line.Split(delimiter);
                        FileArticle newArticle = new FileArticle
                        {
                            Name = data[0],
                            Quantity = Convert.ToInt32(data[1]),
                            Price = Convert.ToDouble(data[2])
                        };
                        listOfArticles.Add(newArticle);
                    }
                }
                return listOfArticles;
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine($"The file was not found: '{e}'");
                return null;
            }
            catch (IOException e)
            {
                Debug.WriteLine($"The file could not be opened: '{e}'");
                return null;
            }
        }

        // Function that modify article price
        public void ModifyPrice(FileArticle article, double newPrice)
        {
            List<FileArticle> articles = GetAllFileArticles();
            for (int i = 0; i < articles.Count; i++)
            {
                if (articles.ElementAt(i).Name == article.Name)
                {
                    articles.ElementAt(i).Price = newPrice;
                }
            }
            File.Delete(fileArticles);
            foreach (var item in articles)
            {
                AddArticleToFile(item);
            }
        }

        //
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
