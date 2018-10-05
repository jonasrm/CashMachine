using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine.CustomException
{
    public class CashOutException : System.Exception
    {
        public CashOutException() : base() { }
        public CashOutException(string message) : base(message) { }
        public CashOutException(string message, System.Exception inner) : base(message, inner) { }
    }
}
