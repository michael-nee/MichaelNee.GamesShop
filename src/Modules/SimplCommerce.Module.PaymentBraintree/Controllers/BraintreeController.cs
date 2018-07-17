using Braintree;
using Braintree.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimplCommerce.Infrastructure;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Core.Extensions;
using SimplCommerce.Module.Orders.Models;
using SimplCommerce.Module.Orders.Services;
using SimplCommerce.Module.PaymentBraintree.Configuration;
using SimplCommerce.Module.PaymentBraintree.Models;
using SimplCommerce.Module.Payments.Models;
using SimplCommerce.Module.ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimplCommerce.Module.PaymentBraintree.Controllers
{
    public class BraintreeController : Controller
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly IRepositoryWithTypedId<PaymentProvider, string> _paymentProviderRepository;
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IBraintreeConfiguration _braintreeConfiguration;
        public BraintreeController(
            IRepository<Cart> cartRepository,
            IOrderService orderService,
            IWorkContext workContext,
            IRepositoryWithTypedId<PaymentProvider, string> paymentProviderRepository,
            IRepository<Payment> paymentRepository,
            IBraintreeConfiguration braintreeConfiguration)
        {
            _cartRepository = cartRepository;
            _orderService = orderService;
            _workContext = workContext;
            _paymentProviderRepository = paymentProviderRepository;
            _paymentRepository = paymentRepository;
            _braintreeConfiguration = braintreeConfiguration;
        }


        public async Task<IActionResult> Charge(string payment_method_nonce)
        {
            var gateway = _braintreeConfiguration.GetGateway();

            var currentUser = await _workContext.GetCurrentUser();
            var orderCreationResult = await _orderService.CreateOrder(currentUser, "Braintree", 0, OrderStatus.PendingPayment);
            if (!orderCreationResult.Success)
            {
                TempData["Error"] = orderCreationResult.Error;
                return Redirect("~/checkout/payment");
            }

            var order = orderCreationResult.Value;
            var zeroDecimalOrderAmount = order.OrderTotal;
            if (!CurrencyHelper.IsZeroDecimalCurrencies())
            {
                zeroDecimalOrderAmount = zeroDecimalOrderAmount * 100;
            }

            var regionInfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);

            var request = new TransactionRequest
            {
                Amount = zeroDecimalOrderAmount,
                PaymentMethodNonce = payment_method_nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            var payment = new Payment()
            {
                OrderId = order.Id,
                Amount = order.OrderTotal,
                PaymentMethod = "Braintree",
                CreatedOn = DateTimeOffset.UtcNow
            };

            Braintree.Result<Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                Transaction transaction = result.Target;

                payment.GatewayTransactionId = transaction.Id;
                payment.Status = PaymentStatus.Succeeded;
                order.OrderStatus = OrderStatus.PaymentReceived;
                _paymentRepository.Add(payment);
                await _paymentRepository.SaveChangesAsync();
                return Redirect("~/checkout/congratulation");
            }
            else if (result.Transaction != null)
            {
                payment.Status = PaymentStatus.Failed;
                payment.FailureMessage = result.Message;
                order.OrderStatus = OrderStatus.PaymentFailed;

                _paymentRepository.Add(payment);
                await _paymentRepository.SaveChangesAsync();
                return Redirect("~/checkout/payment");
            }
            else
            {
                payment.Status = PaymentStatus.Failed;
                payment.FailureMessage = result.Message;
                order.OrderStatus = OrderStatus.PaymentFailed;

                _paymentRepository.Add(payment);
                await _paymentRepository.SaveChangesAsync();
                return Redirect("~/checkout/payment");
            }             
        }
    }
}
