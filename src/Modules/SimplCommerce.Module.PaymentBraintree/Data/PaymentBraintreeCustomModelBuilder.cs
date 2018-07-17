using Microsoft.EntityFrameworkCore;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplCommerce.Module.PaymentBraintree.Data
{
    public class PaymentBraintreeCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentProvider>().HasData(
                new PaymentProvider("Braintree") { Name = "Braintree", LandingViewComponentName = "BraintreeLanding", ConfigureUrl = "payments-braintree-config", IsEnabled = true, AdditionalSettings = "{\"PublicKey\": \"6j4d7qspt5n48kx4\", \"PrivateKey\" : \"bd1c26e53a6d811243fcc3eb268113e1\", \"MerchantID\" : \"ncsh7wwqvzs3cx9q\"}" }
            );
        }
    }
}
