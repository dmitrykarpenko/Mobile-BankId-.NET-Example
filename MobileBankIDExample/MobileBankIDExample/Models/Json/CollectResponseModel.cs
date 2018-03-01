using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBankIDExample.Models.Json
{
    public class CollectResponseModel
    {
        /// <summary>
        /// The orderRef from request.
        /// </summary>
        [JsonProperty("orderRef")]
        public string OrderRef { get; set; }

        /// <summary>
        /// pending: The order is being processed. hintCode describes the status of the order.
        /// failed: Something went wrong with the order.hintCode describes the error.
        /// complete: The order is complete.completionData holds user information.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Only present for pending and failed orders. Fixed set of values.
        /// </summary>
        [JsonProperty("hintCode")]
        public string HintCode { get; set; }

        /// <summary>
        /// Only present for complete orders. Fixed set of values.
        /// </summary>
        [JsonProperty("completionData")]
        public CompletionDataModel CompletionData { get; set; }
    }

    public class CompletionDataModel
    {
        /// <summary>
        /// Information related to the user.
        /// </summary>
        [JsonProperty("user")]
        public BankIdUserModel User { get; set; }

        /// <summary>
        /// Information related to the device.
        /// </summary>
        [JsonProperty("device")]
        public DeviceModel Device { get; set; }

        /// <summary>
        /// Information related to the users certificate (BankID).
        /// </summary>
        [JsonProperty("cert")]
        public CertificateModel Cert { get; set; }

        /// <summary>
        /// The signature. The content of the signature is described in
        /// BankID Signature Profile specification.
        /// String. Base64-encoded. XML signature.
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }

        /// <summary>
        /// The OCSP response. 
        /// The OCSP response is signed by a certificate that has the same issuer as the certificate being verified.
        /// The OSCP response has an extension for Nonce.
        /// 
        /// The nonce is calculated as:
        ///  - SHA-1 hash over the base 64 XML signature encoded as UTF-8.
        ///  - 12 random bytes is added after the hash
        ///  - The nonce is 32 bytes(20 + 12).
        /// 
        /// String. Base64-encoded.
        /// </summary>
        [JsonProperty("ocspResponse")]
        public string OcspResponse { get; set; }
    }

    public class BankIdUserModel
    {
        /// <summary>
        /// The personal number. String.
        /// </summary>
        [JsonProperty("personalNumber")]
        public string PersonalNumber { get; set; }

        /// <summary>
        /// The given name and surname of the user. String.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The given name of the user. String.
        /// </summary>
        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        /// <summary>
        /// The surname of the user. String.
        /// </summary>
        [JsonProperty("surname")]
        public string Surname { get; set; }
    }

    public class DeviceModel
    {
        /// <summary>
        /// The IP address of the user agent as the BankID server discovers it. String.
        /// </summary>
        [JsonProperty("ipAddress")]
        public string IpAddress { get; set; }
    }

    /// <summary>
    /// Certificate model.
    /// Note: notBefore and notAfter are the number of milliseconds since the UNIX Epoch,
    /// a.k.a. "UNIX time" in milliseconds. It was chosen over ISO8601
    /// for its simplicity and lack of error prone conversions to/from
    /// string representations on the server and client side.
    /// </summary>
    public class CertificateModel
    {
        /// <summary>
        /// Start of validity of the users BankID. String, Unix ms.
        /// </summary>
        [JsonProperty("notBefore")]
        public string NotBefore { get; set; }

        /// <summary>
        /// End of validity of the Users BankID. String, Unix ms.
        /// </summary>
        [JsonProperty("notAfter")]
        public string NotAfter { get; set; }
    }
}
