using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace lab5
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRestLibrary
    {

        [OperationContract]
        [WebGet(UriTemplate = "/books",
                ResponseFormat = WebMessageFormat.Json)]
        List<Book> GetAll();

        [OperationContract]
        [WebGet(UriTemplate = "/books/{id}",
                ResponseFormat = WebMessageFormat.Json)]
        Book GetById(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/books",
              Method = "POST",
              ResponseFormat = WebMessageFormat.Json)]
        string Add(Book book);

        [OperationContract]
        [WebInvoke(UriTemplate = "books/{id}",
                   Method = "DELETE",
                   ResponseFormat = WebMessageFormat.Json)]
        string Delete(string id);


        [OperationContract]
        [WebInvoke(UriTemplate = "books/{id}",
           Method = "PATCH",
           BodyStyle =WebMessageBodyStyle.Wrapped,
           ResponseFormat = WebMessageFormat.Json)]
        string Update(string id, string title, string author, string year);

        [OperationContract]
        [WebGet(UriTemplate = "/info",
                ResponseFormat = WebMessageFormat.Json)]
        string Info();
    }


    [DataContract]
    public class Book
    {
        [DataMember]
        public int id;
        [DataMember]
        public string title;
        [DataMember]
        public string author;
        [DataMember]
        public int year;

        public Book(int id, string title, string author, int year)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.year = year;
        }

    }
}
