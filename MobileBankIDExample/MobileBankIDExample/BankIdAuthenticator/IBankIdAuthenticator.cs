using MobileBankIDExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBankIDExample.BankIdAuthenticator
{
    public interface IBankIdAuthenticator
    {
        OrderResponseTypeModel Authenticate(string ssn);
        void Collect(OrderResponseTypeModel order);
    }
}
