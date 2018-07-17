using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimplCommerce.Module.PaymentBraintree.ViewModels
{
    public class BraintreeConfigForm
    {
        [Required]
        public string MerchantID { get; set; }

        [Required]
        public string PublicKey { get; set; }

        [Required]
        public string PrivateKey { get; set; }

        public bool IsSandbox { get; set; }

        public string Environment
        {
            get
            {
                return IsSandbox ? "sandbox" : "production";
            }
        }

        public string EnvironmentUrlPart
        {
            get
            {
                return IsSandbox ? ".sandbox" : "";
            }
        }
    }
}
