using JckShopping.Data;
using JckShopping.Data.Entities;
using JckShopping.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JckShopping.AllPaymentGateways
{
    public class PayUBizPaymentGateway : IBasePaymentGateway
    {
        private readonly JKCContext _jKCContext;
        private readonly UserManager<JKCStoreUser> _userManager;
        private PaymentGateways _paymentGatewaysConfig;        
        private PaymentRequestForm _paymentRequestForm;

        public PayUBizPaymentGateway(JKCContext jKCContext, UserManager<JKCStoreUser> userManager)
        {
            _jKCContext = jKCContext;
            _userManager = userManager;
        }

        public  async Task<PaymentRequestForm> PreparePaymentFormAsync(string userName, PaymentRequestForm paymentRequestForm)
        {
            _paymentRequestForm = paymentRequestForm;
            _paymentGatewaysConfig = _jKCContext.PaymentGateways.Where(g => g.User.UserName == userName).FirstOrDefault();
            
            var user = await _userManager.FindByNameAsync(userName);
            Random rnd = new Random();

            _paymentRequestForm.KeyValue = _paymentGatewaysConfig.MERCHANT_KEY;
            _paymentRequestForm.PayuBaseUrl = _paymentGatewaysConfig.PAYU_BASE_URL;
            _paymentRequestForm.TxnidValue = Generatehash512(rnd.ToString() + DateTime.Now).Substring(0, 20);
            _paymentRequestForm.AmountValue = Convert.ToDecimal(paymentRequestForm.AmountValue).ToString("g29");
            _paymentRequestForm.ProductInfoValue = string.IsNullOrEmpty(paymentRequestForm.ProductInfoValue) ? string.Empty : paymentRequestForm.ProductInfoValue;
            _paymentRequestForm.FirstNameValue = user.FirstName;
            _paymentRequestForm.EmailValue = user.Email;
            _paymentRequestForm.Udf1Value = string.IsNullOrEmpty(_paymentRequestForm.Udf1Value) ? string.Empty : _paymentRequestForm.Udf1Value;
            _paymentRequestForm.Udf2Value = string.IsNullOrEmpty(_paymentRequestForm.Udf2Value) ? string.Empty : _paymentRequestForm.Udf2Value;
            _paymentRequestForm.Udf3Value = string.IsNullOrEmpty(_paymentRequestForm.Udf3Value) ? string.Empty : _paymentRequestForm.Udf3Value;
            _paymentRequestForm.Udf4Value = string.IsNullOrEmpty(_paymentRequestForm.Udf4Value) ? string.Empty : _paymentRequestForm.Udf4Value;
            _paymentRequestForm.Udf5Value = string.IsNullOrEmpty(_paymentRequestForm.Udf5Value) ? string.Empty : _paymentRequestForm.Udf5Value;

            _paymentRequestForm.LastNameValue = user.LastName;            
            _paymentRequestForm.PhoneValue = string.IsNullOrEmpty(user.PhoneNumber) ? "" : user.PhoneNumber;            
            _paymentRequestForm.SurlValue = _paymentGatewaysConfig.SUrl;
            _paymentRequestForm.FurlValue = _paymentGatewaysConfig.FUrl;
            _paymentRequestForm.CurlValue = _paymentGatewaysConfig.CUrl;
            _paymentRequestForm.Address1Value = string.IsNullOrEmpty(_paymentRequestForm.Address1Value) ? string.Empty : _paymentRequestForm.Address1Value;
            _paymentRequestForm.Address2Value = string.IsNullOrEmpty(_paymentRequestForm.Address2Value) ? string.Empty : _paymentRequestForm.Address2Value;
            _paymentRequestForm.CityValue = string.IsNullOrEmpty(_paymentRequestForm.CityValue) ? string.Empty : _paymentRequestForm.CityValue;
            _paymentRequestForm.StateValue = string.IsNullOrEmpty(_paymentRequestForm.StateValue) ? string.Empty : _paymentRequestForm.StateValue;
            _paymentRequestForm.CountryValue = string.IsNullOrEmpty(_paymentRequestForm.CountryValue) ? string.Empty : _paymentRequestForm.CountryValue;
            _paymentRequestForm.ZipCodeValue = string.IsNullOrEmpty(_paymentRequestForm.ZipCodeValue) ? string.Empty : _paymentRequestForm.ZipCodeValue;
            _paymentRequestForm.PgValue = string.IsNullOrEmpty(_paymentRequestForm.PgValue) ? string.Empty : _paymentRequestForm.PgValue;
            


            _paymentRequestForm.HashValue = PrepareHashVarSequence();

            return _paymentRequestForm;
        }


        private string  PrepareHashVarSequence()
        {
            string[] hashVarsSeq;
            string hash_string = string.Empty;
            string hash1 = string.Empty;
            hashVarsSeq = _paymentGatewaysConfig.HashSequence.Split('|');

            foreach (string hash_var in hashVarsSeq)
            {
                if (hash_var == "key")
                {
                    hash_string = hash_string + _paymentGatewaysConfig.MERCHANT_KEY;
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "txnid")
                {
                    hash_string = hash_string + _paymentRequestForm.TxnidValue;
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "amount")
                {
                    hash_string = hash_string + Convert.ToDecimal(_paymentRequestForm.AmountValue).ToString("g29");
                    hash_string = hash_string + '|';
                }
                else if(hash_var == "productinfo")
                {

                    hash_string = hash_string + (_paymentRequestForm.ProductInfoValue != null ? _paymentRequestForm.ProductInfoValue.Trim() : "");// isset if else
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "firstname")
                {

                    hash_string = hash_string + (_paymentRequestForm.FirstNameValue != null ? _paymentRequestForm.FirstNameValue.Trim() : "");// isset if else
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "email")
                {

                    hash_string = hash_string + (_paymentRequestForm.EmailValue != null ? _paymentRequestForm.EmailValue.Trim() : "");// isset if else
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "udf1")
                {

                    hash_string = hash_string + (_paymentRequestForm.Udf1Value != null ? _paymentRequestForm.Udf1Value.Trim() : "");// isset if else
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "udf2")
                {

                    hash_string = hash_string + (_paymentRequestForm.Udf2Value != null ? _paymentRequestForm.Udf2Value.Trim() : "");// isset if else
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "udf3")
                {

                    hash_string = hash_string + (_paymentRequestForm.Udf3Value != null ? _paymentRequestForm.Udf3Value.Trim() : "");// isset if else
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "udf4")
                {

                    hash_string = hash_string + (_paymentRequestForm.Udf4Value != null ? _paymentRequestForm.Udf4Value.Trim() : "");// isset if else
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "udf5")
                {

                    hash_string = hash_string + (_paymentRequestForm.Udf5Value != null ? _paymentRequestForm.Udf5Value.Trim() : "");// isset if else
                    hash_string = hash_string + '|';
                }
                else 
                {

                    hash_string = hash_string +  "";// isset if else
                    hash_string = hash_string + '|';
                }
                
            }

            hash_string += _paymentGatewaysConfig.SALT;// appending SALT
            hash1 = Generatehash512(hash_string).ToLower();         //generating hash

            return hash1;
        }

        /// <summary>
        /// Get the has code based on random number
        /// </summary>
        /// <returns>Hashed Key</returns>
        private string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

      
    }
}
