using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBankIDExample.Models.Json
{
    /// <summary>
    /// Response from auth and sign
    /// </summary>
    public class AuthResponseModel
    {
        /// <summary>
        /// Used as reference to this order when the client is started automatically.
        /// String (e.g. GUID).
        /// </summary>
        [JsonProperty("autoStartToken")]
        public string AutoStartToken { get; set; }

        /// <summary>
        /// Used to collect the status of the order.
        /// String (e.g. GUID).
        /// </summary>
        [JsonProperty("orderRef")]
        public string OrderRef { get; set; }
    }
}
