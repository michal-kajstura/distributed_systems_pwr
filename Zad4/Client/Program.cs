using Client.ServiceReference1;
using Client.ServiceReference2;
using Client.ServiceReference3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            var klient1 = new KalkulatorLZClient("endpoint1");
            var l1 = new LiczbaZ()
            {
                czescR = 1.2,
                czescU = 3.4
            };
            var l2 = new LiczbaZ()
            {
                czescR = 5.6,
                czescU = -7.8
            };
            Console.WriteLine("KLIENT1");
            Console.WriteLine("... wywoluje DodajZ(...)");
            var result1 = klient1.DodajZ(l1, l2);
            Console.WriteLine("DodajLZ(...) = ({0}, {1})", result1.czescR, result1.czescU);

            //-----------------------------------------------

            var klient2 = new OWSerwiceClient("endpoint2");
            Console.WriteLine("KLIENT2");
            Console.WriteLine("... wywoluje funkcja 1:");
            klient2.Funkcja1("Klient2");
            Thread.Sleep(10);
            Console.WriteLine("... kontynuacja po funkcji 1");
            Console.WriteLine("... wywoluje funkcja 2:");
            klient2.Funkcja2("Klient2");
            Thread.Sleep(10);
            Console.WriteLine("... kontynuacja po funkcji 2");
            klient2.Close();
            Console.WriteLine("KONIEC KLIENT2");

            //-----------------------------------------------

            Console.WriteLine("KLIENT3");
            var handler = new CallbackHandler();
            var instanceContext = new InstanceContext(handler);
            var klient3 = new CallbackKalkulatorClient(instanceContext);
            Console.WriteLine("... wywoluje Silnia(10)");
            klient3.Silnia(10);
            Console.WriteLine("... wywoluje Silnia(20)");
            klient3.Silnia(20);
            Console.WriteLine("... wywoluje obliczenia cosia...."); 
            klient3.ObliczCos(2);
            Console.WriteLine("... poczekaj chwilę na odbiór wyników");
            Console.WriteLine("... nacisnij <ENTER> aby zakończyć");
            Thread.Sleep(5000);
            klient3.Close();
            Console.WriteLine("KONIEC KLIENT3");
            Console.ReadLine();
        }
    }
}
