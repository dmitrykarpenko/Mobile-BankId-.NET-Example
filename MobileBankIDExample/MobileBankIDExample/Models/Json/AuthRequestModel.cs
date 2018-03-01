using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBankIDExample.Models.Json
{
    public class SignRequestModel : AuthRequestModel
    {
        /// <summary>
        /// The text to be displayed and signed. String. The text can be formatted using CR, LF and CRLF for new lines.
        /// The text must be encoded as UTF-8 and then base 64 encoded. 1--40 000 characters after base 64 encoding.
        /// Required.
        /// </summary>
        [JsonProperty("userVisibleData")]
        public string UserVisibleData { get; set; }

        /// <summary>
        /// Data not displayed to the user. String. The value must be base 64-encoded.
        /// 1-200 000 characters after base 64-encoding.
        /// Optional.
        /// </summary>
        [JsonProperty("userNonVisibleData")]
        public string UserNonVisibleData { get; set; }
    }

    public class AuthRequestModel
    {
        /// <summary>
        /// The personal number of the user. String. 12 digits. Century must be included.
        /// If the personal number is excluded, the client must be started with the autoStartToken returned in the response.
        /// Optional.
        /// </summary>
        [JsonProperty("personalNumber")]
        public string PersonalNumber { get; set; }

        /// <summary>
        /// The user IP address as seen by RP. String. IPv4 and IPv6 is allowed.
        /// Note the importance of using the correct IP address.
        /// It must be the IP address representing the user agent (the end user device) as seen by the RP.
        /// If there is a proxy for inbound traffic, special considerations may need to be taken to get the correct address.
        /// In some use cases the IP address is not available, for instance for voice based services.
        /// In this case, the internal representation of those systems IP address is ok to use.
        /// Required.
        /// </summary>
        [JsonProperty("endUserIp")]
        public string EndUserIp { get; set; }

        /// <summary>
        /// Requirements on how the auth or sign order must be performed. See below.
        /// Optional.
        /// </summary>
        [JsonProperty("requirement")]
        public string Requirement { get; set; }
    }
}
