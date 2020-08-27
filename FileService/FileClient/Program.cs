using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.Service1Client proxy = new ServiceReference1.Service1Client();
            var msg = proxy.GetData(101);
            Console.WriteLine(msg);
            Console.ReadKey();
        }
    }
}
