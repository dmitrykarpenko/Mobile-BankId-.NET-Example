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
    public class CollectResponseModel
    {
        /// <summary>
        /// The orderRef from request.
        /// </summary>
        [JsonProperty("orderRef")]
        public string OrderRef { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Known status or null
        /// </summary>
        [JsonIgnore]
        public CollectStatus? CollectStatus
        {
            get
            {
                return GetNullableEnum<CollectStatus>(Status);
            }
        }

        /// <summary>
        /// Only present for pending and failed orders.
        /// </summary>
        [JsonProperty("hintCode")]
        public string HintCode { get; set; }

        [JsonIgnore]
        public CollectHintCode? CollectHintCode
        {
            get
            {
                return GetNullableEnum<CollectHintCode>(HintCode);
            }
        }

        private static TEnum? GetNullableEnum<TEnum>(string stringValue) where TEnum : struct, IConvertible
        {
            TEnum value;
            if (Enum.TryParse(stringValue, true, out value))
                return value;
            else
                return null;
        }

        /// <summary>
        /// Only present for complete orders. Nested object.
        /// </summary>
        [JsonProperty("completionData")]
        public CompletionDataModel CompletionData { get; set; }
    }

    public enum CollectStatus
    {
        /// <summary>
        /// The order is being processed. hintCode describes the status of the order.
        /// </summary>
        [EnumMember(Value = "pending")]
        Pending,

        /// <summary>
        /// Something went wrong with the order.hintCode describes the error.
        /// </summary>
        [EnumMember(Value = "failed")]
        Failed,

        /// <summary>
        /// The order is complete.completionData holds user information.
        /// </summary>
        [EnumMember(Value = "complete")]
        Complete,
    }

    /// <summary>
    /// See respective actions by RP in bankid-relying-party-guidelines-v3.0
    /// </summary>
    public enum CollectHintCode
    {
        // Hint codes for pending orders.
        // The order is pending. RP should use the hintCode to provide the user
        // with details and instructions and keep on calling collect until failed or complete:

        /// <summary>
        /// The order is pending. The client has not yet received the order.
        /// The hintCode will later change to noClient, started or userSign.
        /// </summary>
        [EnumMember(Value = "outstandingTransaction")]
        OutstandingTransaction,

        /// <summary>
        /// The order is pending. The client has not yet received the order.
        /// </summary>
        [EnumMember(Value = "noClient")]
        NoClient,

        /// <summary>
        /// The order is pending.
        /// A client has been started with the autostarttoken but a usable ID
        /// has not yet been found in the started client.
        /// When the client starts there may be a short delay until all IDs are registered.
        /// The user may not have any usable ID:s at all, or has not yet inserted their smart card.
        /// </summary>
        [EnumMember(Value = "started")]
        Started,

        /// <summary>
        /// The order is pending. The client has received the order.
        /// </summary>
        [EnumMember(Value = "userSign")]
        UserSign,

        // HintCode for failed orders.
        // This is a final state. The order failed.
        // RP should use the hintCode to provide the user with details and instructions.
        // The same orderRef must not be used for additional collect requests:

        /// <summary>
        /// The order has expired. The BankID security app/program did not start,
        /// the user did not finalize the signing or the RP called collect too late.
        /// </summary>
        [EnumMember(Value = "expiredTransaction")]
        ExpiredTransaction,

        /// <summary>
        /// This error is returned if:
        // 1) The user has entered wrong security code too many times. The BankID cannot be used.
        // 2) The users BankID is revoked.
        // 3) The users BankID is invalid.
        /// </summary>
        [EnumMember(Value = "certificateErr")]
        CertificateErr,

        /// <summary>
        /// The user decided to cancel the order.
        /// </summary>
        [EnumMember(Value = "userCancel")]
        UserCancel,

        /// <summary>
        /// The order was cancelled. The system received a new order for the user.
        /// </summary>
        [EnumMember(Value = "cancelled")]
        Cancelled,

        /// <summary>
        /// The user did not provide her ID, or the RP requires autoStartToken to be used,
        /// but the client did not start within a certain time limit. The reason may be:
        /// 1) RP did not use autoStartToken when starting BankID security program/app.
        ///    RP must correct this in their implementation.
        /// 2) The client software was not installed or other problem with the user’s computer.
        /// </summary>
        [EnumMember(Value = "startFailed")]
        StartFailed,
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
