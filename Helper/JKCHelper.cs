using JckShopping.Data;
using JckShopping.Data.Entities;
using JckShopping.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JckShopping.Helper
{
    public static class JKCHelper
    {
        
         
        /// <summary>
        /// Get payment form
        /// </summary>
        /// <returns></returns>
        public static PaymentRequestForm PreparePaymentForm(PaymentGateways paymentConfig)
        {
            PaymentRequestForm paymentRequestForm = new PaymentRequestForm();   


            return paymentRequestForm;
        }

    }
}
