using Client.ServiceReference1;
using Client.ServiceReference2;
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

            var klient1 = new LibraryClient("endpoint");
            Console.WriteLine("... wywoluje FindByISBN(...)");
            var result1 = klient1.FindByISBN(123);
            Console.WriteLine("{0} {1}", result1.title, result1.author);

            Console.WriteLine("... wywoluje FindByISBN(...) dla nieistniejcej ksiazki");
            try 
            {
                klient1.FindByISBN(9999);
            } catch (FaultException<Contract.BookFault> faultEx)
            {
                Console.WriteLine(faultEx.Detail.ISBN);
                Console.WriteLine(faultEx.Detail.message);
            }

            Console.WriteLine("... wywoluje CurrentValueOfAllBooks(...)");
            var result2 = klient1.GetCurrentValueOffBooks();
            Console.WriteLine(result2);


            //-----------------------------------------------
            var handler = new CallbackHandler();
            var instanceContext = new InstanceContext(handler);
            var klientCB = new CallbackBooksClient(instanceContext);

            Console.WriteLine("... wywoluje GetAllBooks(...)");
            klientCB.GetAllBooks(); // TODO: Async?

            Console.WriteLine("... wywoluje Info(...)");
            klientCB.Info();


            Console.ReadLine();

        }
    }
}
