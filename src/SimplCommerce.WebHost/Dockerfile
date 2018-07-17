FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/SimplCommerce.WebHost/SimplCommerce.WebHost.csproj src/SimplCommerce.WebHost/
COPY src/Modules/SimplCommerce.Module.DinkToPdf/SimplCommerce.Module.DinkToPdf.csproj src/Modules/SimplCommerce.Module.DinkToPdf/
COPY src/SimplCommerce.Infrastructure/SimplCommerce.Infrastructure.csproj src/SimplCommerce.Infrastructure/
COPY src/Modules/SimplCommerce.Module.Core/SimplCommerce.Module.Core.csproj src/Modules/SimplCommerce.Module.Core/
COPY src/Modules/SimplCommerce.Module.Reviews/SimplCommerce.Module.Reviews.csproj src/Modules/SimplCommerce.Module.Reviews/
COPY src/Modules/SimplCommerce.Module.Catalog/SimplCommerce.Module.Catalog.csproj src/Modules/SimplCommerce.Module.Catalog/
COPY src/Modules/SimplCommerce.Module.Tax/SimplCommerce.Module.Tax.csproj src/Modules/SimplCommerce.Module.Tax/
COPY src/Modules/SimplCommerce.Module.ShippingPrices/SimplCommerce.Module.ShippingPrices.csproj src/Modules/SimplCommerce.Module.ShippingPrices/
COPY src/Modules/SimplCommerce.Module.Shipping/SimplCommerce.Module.Shipping.csproj src/Modules/SimplCommerce.Module.Shipping/
COPY src/Modules/SimplCommerce.Module.ProductRecentlyViewed/SimplCommerce.Module.ProductRecentlyViewed.csproj src/Modules/SimplCommerce.Module.ProductRecentlyViewed/
COPY src/Modules/SimplCommerce.Module.Search/SimplCommerce.Module.Search.csproj src/Modules/SimplCommerce.Module.Search/
COPY src/Modules/SimplCommerce.Module.ShippingTableRate/SimplCommerce.Module.ShippingTableRate.csproj src/Modules/SimplCommerce.Module.ShippingTableRate/
COPY src/Modules/SimplCommerce.Module.Payments/SimplCommerce.Module.Payments.csproj src/Modules/SimplCommerce.Module.Payments/
COPY src/Modules/SimplCommerce.Module.Orders/SimplCommerce.Module.Orders.csproj src/Modules/SimplCommerce.Module.Orders/
COPY src/Modules/SimplCommerce.Module.ShoppingCart/SimplCommerce.Module.ShoppingCart.csproj src/Modules/SimplCommerce.Module.ShoppingCart/
COPY src/Modules/SimplCommerce.Module.Pricing/SimplCommerce.Module.Pricing.csproj src/Modules/SimplCommerce.Module.Pricing/
COPY src/Modules/SimplCommerce.Module.ShippingFree/SimplCommerce.Module.ShippingFree.csproj src/Modules/SimplCommerce.Module.ShippingFree/
COPY src/Modules/SimplCommerce.Module.News/SimplCommerce.Module.News.csproj src/Modules/SimplCommerce.Module.News/
COPY src/Modules/SimplCommerce.Module.EmailSenderSmtp/SimplCommerce.Module.EmailSenderSmtp.csproj src/Modules/SimplCommerce.Module.EmailSenderSmtp/
COPY src/Modules/SimplCommerce.Module.SampleData/SimplCommerce.Module.SampleData.csproj src/Modules/SimplCommerce.Module.SampleData/
COPY src/Modules/SimplCommerce.Module.Cms/SimplCommerce.Module.Cms.csproj src/Modules/SimplCommerce.Module.Cms/
COPY src/Modules/SimplCommerce.Module.PaymentStripe/SimplCommerce.Module.PaymentStripe.csproj src/Modules/SimplCommerce.Module.PaymentStripe/
COPY src/Modules/SimplCommerce.Module.ProductComparison/SimplCommerce.Module.ProductComparison.csproj src/Modules/SimplCommerce.Module.ProductComparison/
COPY src/Modules/SimplCommerce.Module.Vendors/SimplCommerce.Module.Vendors.csproj src/Modules/SimplCommerce.Module.Vendors/
COPY src/Modules/SimplCommerce.Module.Contacts/SimplCommerce.Module.Contacts.csproj src/Modules/SimplCommerce.Module.Contacts/
COPY src/Modules/SimplCommerce.Module.PaymentBraintree/SimplCommerce.Module.PaymentBraintree.csproj src/Modules/SimplCommerce.Module.PaymentBraintree/

COPY src/Modules/SimplCommerce.Module.PaymentPaypalExpress/SimplCommerce.Module.PaymentPaypalExpress.csproj src/Modules/SimplCommerce.Module.PaymentPaypalExpress/
COPY src/Modules/SimplCommerce.Module.Inventory/SimplCommerce.Module.Inventory.csproj src/Modules/SimplCommerce.Module.Inventory/
COPY src/Modules/SimplCommerce.Module.PaymentCoD/SimplCommerce.Module.PaymentCoD.csproj src/Modules/SimplCommerce.Module.PaymentCoD/
COPY src/Modules/SimplCommerce.Module.Shipments/SimplCommerce.Module.Shipments.csproj src/Modules/SimplCommerce.Module.Shipments/
COPY src/Modules/SimplCommerce.Module.ActivityLog/SimplCommerce.Module.ActivityLog.csproj src/Modules/SimplCommerce.Module.ActivityLog/
COPY src/Modules/SimplCommerce.Module.Localization/SimplCommerce.Module.Localization.csproj src/Modules/SimplCommerce.Module.Localization/
RUN dotnet restore src/SimplCommerce.WebHost/SimplCommerce.WebHost.csproj
COPY . .
WORKDIR /src/src/SimplCommerce.WebHost
RUN dotnet build SimplCommerce.WebHost.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SimplCommerce.WebHost.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SimplCommerce.WebHost.dll"]
