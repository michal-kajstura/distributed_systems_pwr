using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Contract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IKalkulatorLZ 
    {
        [OperationContract]
        LiczbaZ DodajZ(LiczbaZ n1, LiczbaZ n2);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Contract.ContractType".
    [DataContract]
    public class LiczbaZ
    {
        string opis = "Liczba zespolona";

        [DataMember]
        public double czescR;

        [DataMember]
        public double czescU;

        [DataMember]
        public string Opis
        {
            get { return opis; }
            set { opis = value; }
        }

        public LiczbaZ(double czesc_rz, double czesc_ur)
        {
            this.czescR = czesc_rz;
            this.czescU = czesc_ur;
        }
    }

    [ServiceContract]
    public interface IOWSerwice 
    {
        [OperationContract(IsOneWay = true)]
        void Funkcja1(String s1);

        [OperationContract]
        void Funkcja2(String s2);
    }


    [ServiceContract(SessionMode = SessionMode.Required,
        CallbackContract = typeof(ICallbackHandler))]
    public interface ICallbackKalkulator
    {
        [OperationContract(IsOneWay = true)]
        void Silnia(double n);

        [OperationContract(IsOneWay = true)]
        void ObliczCos(int sek);

    }

    public interface ICallbackHandler
    {
        [OperationContract(IsOneWay = true)]
        void ZwrotSilnia(double result);
        [OperationContract(IsOneWay = true)]
        void ZwrotObliczCos(string result);
    }
}
