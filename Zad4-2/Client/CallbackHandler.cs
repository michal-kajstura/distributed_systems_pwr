using Client.ServiceReference2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class CallbackHandler : ICallbackBooksCallback
    {
        public void HandleAllBooks(Book[] books)
        {
            foreach (var book in books)
            {
                Console.WriteLine($"Book(title: {book.title}, author: {book.author}," +
                    $" ISBN: {book.ISBN}, Year: {book.year}, Price: {book.price})");
            }
        }

        public void HandleInfo(string info)
        {
            Console.WriteLine(info);
        }
    }
}
