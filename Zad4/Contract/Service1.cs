using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Contract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class KalkulatorZ : IKalkulatorLZ
    {
        public LiczbaZ DodajZ(LiczbaZ n1, LiczbaZ n2)
        {
            Console.WriteLine("...wywolano DodajZ(...)");
            return new LiczbaZ(n1.czescR + n2.czescR,
                               n1.czescU + n2.czescU);
        }
    }

    public class MojSerwis2 : IOWSerwice
    {
        public void Funkcja1(string s1)
        {
            Console.WriteLine("... {0}: funkcja1 - start", s1);
            Thread.Sleep(4000);
            Console.WriteLine("... {0}: funkcja1 - stop ", s1);
        }

        public void Funkcja2(string s2)
        {
            Console.WriteLine("... {0}: funkcja2 - start", s2);
            Thread.Sleep(2000);
            Console.WriteLine("... {0}: funkcja2 - stop ", s2);
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MojCallbackKalkulator : ICallbackKalkulator
    {
        double result;
        ICallbackHandler callback = null;
        public MojCallbackKalkulator()
        {
            callback = OperationContext.Current.GetCallbackChannel<ICallbackHandler>();
        }
        public void ObliczCos(int sek)
        {
            Console.WriteLine("..wywolano Oblicz({0})", sek);
            if (sek < 10)
                Thread.Sleep(sek * 1000);
            else
                Thread.Sleep(1000);
            callback.ZwrotObliczCos("Obliczenia trwały " + (sek + 1) + " sekund");
        }

        public void Silnia(double n)
        {
            Console.WriteLine("..wywolano Silnia({0})", n);
            Thread.Sleep(1000);
            result = 1;
            for (int i = 1; i <= n; i++)
                result *= i;
            callback.ZwrotSilnia(result);
        }
    }
}
