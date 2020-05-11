using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Contract
{

    public class Database
    {
        private static Database instance;
        public List<Book> books = new List<Book>
        {
            new Book("1", "a", 1233, 2020, 123),
            new Book("2", "a", 123, 2020, 123),
            new Book("3", "b", 123123, 2020, 123),
            new Book("4", "c", 12, 2020, 123),
        };

        private Database() { }
        public static Database Instance
        {
            get
            {
                if (instance == null)
                    instance = new Database();
                return instance;
            }
        }

    }
    public class Library : ILibrary
    {

        public Book FindByISBN(int ISBN)
        {
            foreach (var book in Database.Instance.books)
            {
                if (book.ISBN == ISBN)
                    return book;
            }
            var bookFault = new BookFault()
            {
                ISBN = ISBN, message = "Nie znaleziono ksiazki o podanym numerze ISBN"
            };
            throw new FaultException<BookFault>(bookFault);
        }

        public List<Book> FindByTitle(string title)
        {
            return Database.Instance.books.FindAll(book => { return book.title.StartsWith(title); });
        }

        public double GetCurrentValueOffBooks()
        {
            return Database.Instance.books.Sum(book => { return book.price; });
        }

        public List<Tuple<string, string>> GetDescriptions()
        {
            return Database.Instance.books.ConvertAll(book => { return Tuple.Create(book.title, book.author); });
        }

        public bool ModifyTitle(int ISBN, string newTitle)
        {
            var book = FindByISBN(ISBN);
            if (book == null)
                return false;

            book.title = newTitle;
            return true;
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class CallbackBooks : ICallbackBooks
    {

        private ICallbackHandler callback;

        public CallbackBooks()
        {
            callback = OperationContext.Current.GetCallbackChannel<ICallbackHandler>();
        }

        private const int delayInSeconds = 11;
        public void GetAllBooks()
        {
            wait();
            callback.HandleAllBooks(Database.Instance.books);
        }

        public void Info()
        {
            wait();
            var info = "TODO: Return info";
            callback.HandleInfo(info);
        }

        private void wait()
        {
            Thread.Sleep(delayInSeconds * 1000);
        }

    }
}
