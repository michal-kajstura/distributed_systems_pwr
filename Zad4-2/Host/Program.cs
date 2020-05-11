using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {

        const int PORT = 10000;
        const string NAME = "library";

        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Library));
            var hostCB = new ServiceHost(typeof(CallbackBooks)) ;
            try
            {

                Console.WriteLine("START");
                //var endpoint1 = host.Description.Endpoints.Find(
                //    new Uri($"http://localhost:{PORT}/{NAME}/endpoint")
                //);

                host.Open();
                hostCB.Open();
                Console.WriteLine("Biblioteka START");
                Console.ReadLine();
                host.Close();
                hostCB.Close();
                Console.WriteLine("Biblioteka KONIEC");
            } catch (CommunicationException ce)
            {
                Console.WriteLine("Wyjatek {0}", ce.Message);
                host.Abort();
            }
        }
    }
}
