using Client.ServiceReference3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class CallbackHandler : ICallbackKalkulatorCallback
    {
        public void ZwrotObliczCos(string result)
        {
            Console.WriteLine("Obliczenia = {0}", result);
        }

        public void ZwrotSilnia(double result)
        {
            Console.WriteLine("Silnia = {0}", result);
        }
    }
}
