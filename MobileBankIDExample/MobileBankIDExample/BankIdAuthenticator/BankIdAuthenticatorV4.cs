using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileBankIDExample.Models;
using System.Text.RegularExpressions;
using MobileBankIDExample.BankIDService;
using MobileBankIDExample.Converters;

namespace MobileBankIDExample.BankIdAuthenticator
{
    /// <summary>
    /// This implementation uses BankIDService which was created using the following WSDL:
    /// https://appapi2.test.bankid.com/rp/v4?wsdl
    /// </summary>
    public class BankIdAuthenticatorV4 : IBankIdAuthenticator
    {
        public OrderResponseTypeModel Authenticate(string ssn)
        {
            using (var client = new RpServicePortTypeClient())
            {
                // RequirementType is optional
                // This will ensure only mobile BankID can be used
                // https://www.bankid.com/bankid-i-dina-tjanster/rp-info/guidelines
                RequirementType conditions = new RequirementType
                {
                    condition = new[]
                    {
                        new ConditionType()
                        {
                            key = "certificatePolicies",
                            value = new[] {"1.2.3.4.25"} // Mobile BankID
                        }}
                };

                // Set the parameters for the authentication
                AuthenticateRequestType authenticateRequestType = new AuthenticateRequestType()
                {
                    personalNumber = ssn,
                    requirementAlternatives = new[] { conditions }
                };

                // ...authenticate
                var response = client.Authenticate(authenticateRequestType);

                return Converter.Map(response);
            }
        }

        public void Collect(OrderResponseTypeModel order)
        {
            using (var client = new RpServicePortTypeClient())
            {
                Console.WriteLine("{0}Start the BankID application and sign in", Environment.NewLine);

                CollectResponseType result = null;

                // Wait for the client to sign in 
                do
                {
                    // ...collect the response
                    result = client.Collect(order.OrderRef);

                    Console.WriteLine(result.progressStatus);
                    System.Threading.Thread.Sleep(1000);

                } while (result.progressStatus != ProgressStatusType.COMPLETE);


                do
                {
                    Console.WriteLine("Hi {0}, please press [ESC] to exit", result.userInfo.givenName);
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }
        }
    }
}
