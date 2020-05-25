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
        const string NAME = "test1";

        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(KalkulatorZ));
            var host2 = new ServiceHost(typeof(MojSerwis2));
            var host3 = new ServiceHost(typeof(MojCallbackKalkulator));
            try
            {
                var basicHttpEndpoint = host.Description.Endpoints.Find(
                    new Uri($"http://localhost:{PORT}/{NAME}/basicHttpEndpoint")
                );
                var endpoint2 = host2.Description.Endpoints.Find(
                    new Uri("http://localhost:10002/test2/endpoint2")
                );
                var endpoint3 = host3.Description.Endpoints.Find(
                    new Uri("http://localhost:10003/test3/endpoint3")
                );


                host.Open();
                host2.Open();
                host3.Open();
                Console.ReadLine();
                host.Close();
                host2.Close();
                host3.Close();
                Console.WriteLine("KONIEC");
            } catch (CommunicationException ce)
            {
                Console.WriteLine("Wyjatek {0}", ce.Message);
                host.Abort();
                host2.Abort();
                host3.Abort();
            }
        }
    }
}
