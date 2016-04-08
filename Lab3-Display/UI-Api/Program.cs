using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace UI_Api
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:57000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Server running on {0}", baseAddress);
                Console.ReadLine();
            }

        }
    }
}