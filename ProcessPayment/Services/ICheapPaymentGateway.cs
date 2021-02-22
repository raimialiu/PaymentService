using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Services
{
    public interface ICheapPaymentGateway
    {
    }

    public interface ProcessPayment
    {
        public bool MakePayment();
    }
}
