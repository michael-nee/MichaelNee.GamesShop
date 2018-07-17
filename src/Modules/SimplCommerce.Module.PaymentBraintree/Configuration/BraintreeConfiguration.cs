using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braintree;
using System.Configuration;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Payments.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using SimplCommerce.Module.PaymentBraintree.ViewModels;
using SimplCommerce.Module.PaymentBraintree.Models;

namespace SimplCommerce.Module.PaymentBraintree.Configuration
{
    public class BraintreeConfiguration : IBraintreeConfiguration
    {
        public string Environment { get; set; }
        public string MerchantId { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        private IBraintreeGateway BraintreeGateway { get; set; }
        private readonly IRepositoryWithTypedId<PaymentProvider, string> _paymentProviderRepository;

        public BraintreeConfiguration(IRepositoryWithTypedId<PaymentProvider, string> paymentProviderRepository)
        {
            _paymentProviderRepository = paymentProviderRepository;
        }

        public IBraintreeGateway CreateGateway()
        {
            var stripeProvider = _paymentProviderRepository.Query().FirstOrDefault(x => x.Id == PaymentProviderHelper.BraintreeProviderId);
            var model = JsonConvert.DeserializeObject<BraintreeConfigForm>(stripeProvider.AdditionalSettings);

            return new BraintreeGateway("sandbox", model.MerchantID, model.PublicKey, model.PrivateKey);
        }

        public IBraintreeGateway GetGateway()
        {
            if (BraintreeGateway == null)
            {
                BraintreeGateway = CreateGateway();
            }

            return BraintreeGateway;
        }
    }
}
