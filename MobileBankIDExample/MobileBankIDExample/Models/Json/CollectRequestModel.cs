using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBankIDExample.Models.Json
{
    public class CollectRequestModel
    {
        /// <summary>
        /// The orderRef returned from auth or sign.
        /// </summary>
        [JsonProperty("orderRef")]
        public string OrderRef { get; set; }
    }
}
