using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimplCommerce.Infrastructure;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Infrastructure.Web;
using SimplCommerce.Module.Core.Extensions;
using SimplCommerce.Module.PaymentBraintree.Configuration;
using SimplCommerce.Module.PaymentBraintree.Models;
using SimplCommerce.Module.PaymentBraintree.ViewModels;
using SimplCommerce.Module.Payments.Models;
using SimplCommerce.Module.ShoppingCart.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimplCommerce.Module.PaymentBraintree.Components
{
    public class BraintreeLandingViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly IWorkContext _workContext;
        private readonly IRepositoryWithTypedId<PaymentProvider, string> _paymentProviderRepository;
        private IBraintreeConfiguration _config;

        public BraintreeLandingViewComponent(ICartService cartService, IWorkContext workContext, IRepositoryWithTypedId<PaymentProvider, string> paymentProviderRepository)
        {
            _cartService = cartService;
            _workContext = workContext;
            _paymentProviderRepository = paymentProviderRepository;
            _config = new BraintreeConfiguration(paymentProviderRepository);
    }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var braintreeProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelper.BraintreeProviderId);
            var braintreeSetting = JsonConvert.DeserializeObject<BraintreeConfigForm>(braintreeProvider.AdditionalSettings);
            var curentUser = await _workContext.GetCurrentUser();
            var cart = await _cartService.GetCart(curentUser.Id);
            var zeroDecimalAmount = cart.OrderTotal;
            if (!CurrencyHelper.IsZeroDecimalCurrencies())
            {
                zeroDecimalAmount = zeroDecimalAmount * 100;
            }

            var gateway = _config.GetGateway();
            var clientToken = gateway.ClientToken.Generate();

            var regionInfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            var model = new BraintreeCheckoutForm();
            model.ClientID = clientToken;
            model.Amount = cart.OrderTotal;

            return View(this.GetViewPath(), model);
        }
    }
}
