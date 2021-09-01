using JckShopping.AllPaymentGateways;
using JckShopping.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JckShopping.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IBasePaymentGateway _payuPaymentGateway;

        public PaymentController(IBasePaymentGateway payuPaymentGateway)
        {
            _payuPaymentGateway = payuPaymentGateway;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("GetPaymentRequest")]
        public async Task<IActionResult> GetPaymentRequest([FromBody]PaymentRequestForm paymentRequestForm)
        {
            paymentRequestForm = await _payuPaymentGateway.PreparePaymentFormAsync(User.Identity.Name, paymentRequestForm);
            return Ok(paymentRequestForm);
        }
    }
}
