using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileBankIDExample.Models;

namespace MobileBankIDExample.BankIdAuthenticator
{
    public class BankIdAuthenticatorV5 : IBankIdAuthenticator
    {
        public OrderResponseTypeModel Authenticate(string ssn)
        {
            throw new NotImplementedException();
        }

        public void Collect(OrderResponseTypeModel order)
        {
            throw new NotImplementedException();
        }
    }
}
