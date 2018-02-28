using System;
using System.Net;
using System.Text.RegularExpressions;
using MobileBankIDExample.BankIdAuthenticator;
using MobileBankIDExample.BankIDService;

namespace MobileBankIDExample
{
    /// <summary>
    /// Implementation towards the BankID test server
    /// https://github.com/EricHerlitz/Mobile-BankId-.NET-Example
    /// 
    /// </summary>
    class Program
    {
        static readonly IBankIdAuthenticator _bankIdAuthenticator =
            new BankIdAuthenticatorV4();

        static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            try
            {
                // Century must be included
                Console.WriteLine("Enter your ssn, 10 or 12 digits (YY)YYMMDDNNNN");

                // format ssn
                string ssn = "194211113636"; //GetSsn();

                // authenticate request and return order
                var order = _bankIdAuthenticator.Authenticate(ssn);

                // collect the result
                _bankIdAuthenticator.Collect(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }

        /// <summary>
        /// Method to collect a valid SSN
        /// </summary>
        /// <returns></returns>
        private static string GetSsn()
        {
            string ssn = Console.ReadLine();

            string ssnRegex = @"^(\d{6}|\d{8})[-|(\s)]{0,1}\d{4}$";

            if (string.IsNullOrEmpty(ssn) || !Regex.Match(ssn, ssnRegex).Success)
            {
                throw new ArgumentException("Not a valid SSN");
            }

            // Remove any dash
            ssn = ssn.Replace("-", "");

            // if ten digits we are missing the century
            if (ssn.Length <= 10)
            {
                int year = int.Parse(ssn.Substring(0, 2));
                ssn = year > int.Parse(DateTime.Now.ToString("yy")) ? string.Concat("19", ssn) : string.Concat("20", ssn);
            }

            return ssn;
        }
    }
}
