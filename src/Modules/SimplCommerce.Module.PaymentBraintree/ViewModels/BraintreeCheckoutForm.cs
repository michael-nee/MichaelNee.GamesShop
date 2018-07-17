using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplCommerce.Module.PaymentBraintree.ViewModels
{
    public class BraintreeCheckoutForm
    {
        public string ClientID { get; set; }

        public decimal Amount { get; set; }
    }
}
