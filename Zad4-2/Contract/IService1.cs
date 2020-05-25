using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Contract
{

    [DataContract]
    public class BookFault
    {
        public int ISBN;
        public string message;
    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILibrary 
    {
        [FaultContract(typeof(BookFault))]
        [OperationContract]
        Book FindByISBN(int ISBN);
        [OperationContract]
        List<Book> FindByTitle(string title);

        [OperationContract]
        bool ModifyTitle(int ISBN, string newTitle);

        [OperationContract]
        List<Tuple<string, string>> GetDescriptions();

        [OperationContract]
        double GetCurrentValueOffBooks();


    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Contract.ContractType".
    [DataContract]
    public class Book
    {
        [DataMember]
        public string title;

        [DataMember]
        public string author;

        [DataMember]
        public int ISBN;

        [DataMember]
        public int year;

        [DataMember]
        public double price;

        public Book(string title, string author, int ISBN, int year, double price)
        {
            this.title = title;
            this.author = author;
            this.ISBN = ISBN;
            this.year = year;
            this.price = price;
        }
    }


    [ServiceContract(SessionMode = SessionMode.Required,
        CallbackContract = typeof(ICallbackHandler))]
    public interface ICallbackBooks
    {
        [OperationContract(IsOneWay = true)]
        void GetAllBooks();

        [OperationContract(IsOneWay = true)]
        void Info();
    }

    public interface ICallbackHandler
    {
        [OperationContract(IsOneWay = true)]
        void HandleAllBooks(List<Book> books);
        [OperationContract(IsOneWay = true)]
        void HandleInfo(string info);
    }
}
