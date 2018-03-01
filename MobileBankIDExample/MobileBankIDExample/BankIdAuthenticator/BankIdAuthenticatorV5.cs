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
using System.Net.Http;
using Newtonsoft.Json;
using MobileBankIDExample.Models.Json;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;

namespace MobileBankIDExample.BankIdAuthenticator
{
    public class BankIdAuthenticatorV5 : IBankIdAuthenticator
    {
        private const string _baseUrl = "https://appapi2.test.bankid.com/rp/v5/";
        private const string _serverCertificateName = "FP Testcert 2";

        public OrderResponseTypeModel Authenticate(string ssn)
        {
            return AuthenticateAsync(ssn).Result;
        }

        public async Task<OrderResponseTypeModel> AuthenticateAsync(string personalNumber)
        {
            var requestModel = new AuthRequestModel()
            {
                PersonalNumber = personalNumber,
                // TODO: make a parameter
                EndUserIp = "192.168.1.106",
            };

            var responseModel = await PostAsync<AuthRequestModel, AuthResponseModel>(
                _baseUrl + "auth", requestModel);

            return Converter.Map(responseModel);
        }

        private StringContent ObjectToJsonContent(object obj, out string objStr)
        {
            objStr = JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            var content = new StringContent(objStr);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        public void Collect(OrderResponseTypeModel order)
        {
            CollectAsync(order).Wait();
        }

        public async Task CollectAsync(OrderResponseTypeModel order)
        {
            Console.WriteLine("{0}Start the BankID application and sign in", Environment.NewLine);

            CollectResponseModel responseModel;
            var requestModel = new CollectRequestModel()
            {
                OrderRef = order.OrderRef,
            };

            // Wait for the client to sign in 
            do
            {
                responseModel = await PostAsync<CollectRequestModel, CollectResponseModel>(
                    _baseUrl + "collect", requestModel);

                Console.WriteLine(responseModel.Status);
                System.Threading.Thread.Sleep(1000);
            }
            while (responseModel.Status != "complete");

            do
            {
                Console.WriteLine("Hi {0}, please press [ESC] to exit", responseModel.CompletionData.User.Name);
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest requestModel)
        {
            WebRequestHandler handler = new WebRequestHandler();
            X509Certificate2 certificate = GetX509Certifacate(_serverCertificateName);
            handler.ClientCertificates.Add(certificate);

            using (var httpClient = new HttpClient(handler))
            {
                string objStr;
                var content = ObjectToJsonContent(requestModel, out objStr);
                var uri = new Uri(url);
                var httpResponse = await httpClient.PostAsync(uri, content);

                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<TResponse>(responseString);

                return responseModel;
            }
        }

        private X509Certificate2 GetX509Certifacate(string name)
        {
            //StoreLocation.LocalMachine
            X509Store store = new X509Store(StoreLocation.LocalMachine);

            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection cers = store.Certificates.Find(X509FindType.FindBySubjectName, name, false);
            store.Close();

            if (cers.Count > 0)
                return cers[0];

            return null;
        }
    }
}
