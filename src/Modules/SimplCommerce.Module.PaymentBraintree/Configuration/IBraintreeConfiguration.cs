using Braintree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplCommerce.Module.PaymentBraintree.Configuration
{
    public interface IBraintreeConfiguration
    {
        IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}
