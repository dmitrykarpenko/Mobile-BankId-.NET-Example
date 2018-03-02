using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        public RequirementModel Requirement { get; set; }
    }

    public class RequirementModel
    {
        /// <summary>
        /// no value - defaults to "class1".
        /// This condition should be combined with a certificatePolicies for a smart card to avoid undefined behavior.
        /// </summary>
        [JsonProperty("cardReader")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CardReader? CardReader { get; set; }

        /// <summary>
        /// The oid in certificate policies in the user certificate.
        /// List of String.
        /// One wildcard ”*” is allowed from position 5 and forward ie. 1.2.752.78.*
        /// The values for production BankIDs are:
        /// "1.2.752.78.1.1" - BankID on file
        /// "1.2.752.78.1.2" - BankID on smart card
        /// "1.2.752.78.1.5" - Mobile BankID
        /// "1.2.752.71.1.3" - Nordea e-id on file and on smart card.
        /// The values for test BankIDs are:
        /// "1.2.3.4.5" - BankID on file
        /// "1.2.3.4.10" - BankID on smart card
        /// "1.2.3.4.25" - Mobile BankID
        /// "1.2.752.71.1.3" - Nordea e-id on file and on smart card.
        /// “1.2.752.60.1.6” - Test BankID for some BankID Banks
        /// </summary>
        [JsonProperty("certificatePolicies")]
        public string CertificatePolicies { get; set; }

        /// <summary>
        /// The cn (common name) of the issuer. List of String. Wildcards are not allowed. Nordea values for production:
        /// "Nordea CA for Smartcard users 12" - E-id on smart card issued by Nordea CA.
        /// "Nordea CA for Softcert users 13" - E-id on file issued by Nordea CA
        /// Example Nordea values for test:
        /// "Nordea Test CA for Smartcard users 12" - E-id on smart card issued by Nordea CA.
        /// "Nordea Test CA for Softcert users 13" - E-id on file issued by Nordea CA
        /// </summary>
        [JsonProperty("issuerCn")]
        public string IssuerCn { get; set; }

        /// <summary>
        /// If set to true, the client must have been started using the autoStartToken.
        /// To be used if it is important that the BankID App is on the same device as the RP service.
        /// If set to false, the client does not need to be started using the autoStartToken.
        /// Boolean. Default (if not set): false.
        /// </summary>
        [JsonProperty("autoStartTokenRequired")]
        public bool? AutoStartTokenRequired { get; set; }

        /// <summary>
        /// Users of iOS and Android devices may use fingerprint for authentication and signing
        /// if the device supports it and the user configured the device to use it.
        /// No other devices are supported at this point.
        /// If set to true, the users are allowed to use fingerprint.
        /// If set to false, the users are not allowed to use fingerprint.
        /// Boolean. Default (if not set): true for authentication, false for signing.
        /// </summary>
        [JsonProperty("allowFingerprint")]
        public bool? AllowFingerprint { get; set; }
    }

    public enum CardReader
    {
        /// <summary>
        /// The transaction must be performed using a card reader where the PIN code
        /// is entered on the computers keyboard, or a card reader of higher class.
        /// Default.
        /// </summary>
        [EnumMember(Value = "class1")]
        Class1,

        /// <summary>
        /// The transaction must be performed using a card reader where the PIN code
        /// is entered on the reader, or a reader of higher class.
        /// </summary>
        [EnumMember(Value = "class2")]
        Class2,
    }
}
