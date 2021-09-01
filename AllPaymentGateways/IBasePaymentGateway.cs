using JckShopping.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JckShopping.AllPaymentGateways
{

    /// <summary>
    /// This is base class of all payment gateways
    /// </summary>
    public interface IBasePaymentGateway
    {
        Task<PaymentRequestForm> PreparePaymentFormAsync(string userName, PaymentRequestForm paymentRequestForm);

    }
}
