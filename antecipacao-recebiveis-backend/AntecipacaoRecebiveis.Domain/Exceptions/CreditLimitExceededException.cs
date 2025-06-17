using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Domain.Exceptions
{
    public class CreditLimitExceededException : Exception
    {
        public CreditLimitExceededException(string message) : base(message) { }

    }
}
