# SKitLs.Payments.Lava

_README version: 2024.04.17, [Source Code](https://github.com/SKitLs-dev/SKitLs.Payments.Lava)_

The SKitLs.Payments.Lava is a comprehensive toolkit designed to facilitate seamless integration with the [Lava payment service](https://lava.ru/payments/bank-cards).


## Requirements

- Newtonsoft.Json
- RestSharp


## Usage

All methods are implemented in accordance with the specifications provided in the [official documentation](https://dev.lava.ru/) and automatically sign the request headers with an [HMAC SHA256 signature](https://dev.lava.ru/api-invoice-sign).

### Create LavaShop instance

```C#
var shop = new LavaShop("shopId", "secret_key");
```

### Create Invoice

```C#
var invoiceResponse = await shop.CreateInvoiceAsync("orderId_220102", 100);
if (invoiceResponse.StatusCheck)
{
    var invoice = invoiceResponse.Data;
    // Assuming invoice.InvoiceId = 9adea8c8-f91e-47ba-1be2-93b52e78329a
    var payUrl = invoice.URL;
}
else
{
    var error = invoiceResponse.Error;
}
```

### Get Invoice Status

```C#
var invoiceResponse = await shop.GetInvoiceStatusByInvoiceIdAsync("9adea8c8-f91e-47ba-1be2-93b52e78329a");
// OR var invoiceResponse = await shop.GetInvoiceStatusByOrderIdAsync("orderId_220102");
if (invoiceResponse.StatusCheck)
{
    var invoice = invoiceResponse.Data;
}
else
{
    var error = invoiceResponse.ErrorData;
}
```

### WebHooks Model

For the management of WebHooks, the class `LavaInvoiceWebHook` is available.
To authenticate received data, utilize the methods `LavaShop.SetAdditionalSecret(string)` and `LavaShop.CheckSignature(string, string)`.

### Rest Facilities

Currently supported:
* [Shop Endpoint](https://dev.lava.ru/shop-balance)
* [Invoices Endpoint](https://dev.lava.ru/api-invoice-create)
* [Payoffs Endpoint](https://dev.lava.ru/business-payoff-create)
 
Currently **not** supported:
* [Recurrent Endpoint](https://dev.lava.ru/recurrent-payments-product-list)

## Contributors

Currently, there are no contributors actively involved in this project.
However, our team is eager to welcome contributions from anyone interested in advancing the project's development.

We value every contribution and look forward to collaborating with individuals who share our vision and passion for this endeavor.
Your participation will be greatly appreciated in moving the project forward.

Thank you for considering contributing to our project.


## License

This project is distributed under the terms of the MIT License.

Copyright (c) 2024, SKitLs


## Developer contact

For any issues related to the project, please feel free to reach out to us through the project's GitHub page.
We welcome bug reports, feedback, and any other inquiries that can help us improve the project.

You can also contact the project owner directly via their GitHub profile at the [following link](https://github.com/SKitLs-dev) or email: skitlsdev@gmail.com

Your collaboration and support are highly appreciated, and we will do our best to address any concerns or questions promptly and professionally.
Thank you for your interest in our project.


## Notes

Thank you for choosing our solution for your needs, and we look forward to contributing to your project's success.
