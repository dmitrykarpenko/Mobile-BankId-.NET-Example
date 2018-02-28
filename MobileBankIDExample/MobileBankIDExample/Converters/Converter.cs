using MobileBankIDExample.BankIDService;
using MobileBankIDExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBankIDExample.Converters
{
    public static partial class Converter
    {
        public static OrderResponseTypeModel Map(OrderResponseType dto)
        {
            return new OrderResponseTypeModel
            {
                AutoStartToken = dto.autoStartToken,
                OrderRef = dto.orderRef,
            };
        }
    }
}
