using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace lab5
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RestLibrary : IRestLibrary
    {
        private static List<Book> books;
        const int INDEX1 = 242491;
        const int INDEX2 = 242508;

        RestLibrary()
        {
            books = new List<Book>()
            {
                new Book(1, "Czysty kod", "Rober C. Martin", 2014),
                new Book(2, "Czystszy kod", "Rober RR. Martin", 2000),
                new Book(3, "Test", "Testest", 2014),
            };
        }
        public string Add(Book book)
        {
            if (book == null)
                throw new WebFaultException<string>("400: Bad request", System.Net.HttpStatusCode.BadRequest);

            var bookIdx = books.FindIndex(b => b.id == book.id);
            if (bookIdx != -1)
                throw new WebFaultException<string>("409: Book exists", System.Net.HttpStatusCode.Conflict);

            if (book.author.Length < 3 
                || book.author.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))
                || !char.IsUpper(book.author.ToCharArray()[0]) )
                throw new WebFaultException<string>("400: Author name does not match the format", System.Net.HttpStatusCode.BadRequest);


            books.Add(book);
            return "Added item with id:" + book.id;
        }

        public string Delete(string id)
        {
            var intId = int.Parse(id);
            var book = books.Find((b) => { return b.id == intId; });
            if (book == null)
                throw new WebFaultException<string>("404: Book not found", System.Net.HttpStatusCode.NotFound);

            books.Remove(book);
            return "Removed item with id:" + book.id;
        }

        public List<Book> GetAll()
        {
            return books;
        }

        public Book GetById(string id)
        {
            var intId = int.Parse(id);
            var book = books.Find(b => b.id == intId);
            if (book == null)
                throw new WebFaultException<string>("404: Book not found", System.Net.HttpStatusCode.NotFound);
            return book;
        }

        public string Info()
        {
            var date = DateTime.Now.ToString("dd/MM/yyyy h:mm tt");
            return $"Imiona: Michał, Jakub\nIndeksy: {INDEX1}, {INDEX2}\nData i czas: {date}\n";
        ;
        }

        public string Update(string id, string title, string author, string year)
        {
            var intId = int.Parse(id);
            var book = books.Find(b => b.id == intId);
            if (book == null)
                throw new WebFaultException<string>("404: Book not found", System.Net.HttpStatusCode.NotFound);

            book.title = title;
            book.author = author;
            book.year = int.Parse(year);

            if (book.author.Length < 3
                || book.author.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c))
                || !char.IsUpper(book.author.ToCharArray()[0]))
                throw new WebFaultException<string>("400: Author name does not match the format", System.Net.HttpStatusCode.BadRequest);

            return "Modified item with id:" + book.id;
        }
    }
}
