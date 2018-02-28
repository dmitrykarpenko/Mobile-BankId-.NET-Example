using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBankIDExample.Models
{
    public class OrderResponseTypeModel
    {
        public string OrderRef { get; set; }
        public string AutoStartToken { get; set; }
    }
}
