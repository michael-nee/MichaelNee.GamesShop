using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplCommerce.Infrastructure;
using SimplCommerce.Module.PaymentBraintree.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplCommerce.Module.PaymentBraintree
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {

        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBraintreeConfiguration, BraintreeConfiguration>();
        }
    }
}
