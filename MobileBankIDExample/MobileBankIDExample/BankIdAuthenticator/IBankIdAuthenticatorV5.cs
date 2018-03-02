using System.Threading.Tasks;
using MobileBankIDExample.Models;

namespace MobileBankIDExample.BankIdAuthenticator
{
    public interface IBankIdAuthenticatorV5 : IBankIdAuthenticator
    {
        Task<OrderResponseTypeModel> AuthenticateAsync(string personalNumber, string endUserIp);
        Task CollectAsync(OrderResponseTypeModel order);
    }
}