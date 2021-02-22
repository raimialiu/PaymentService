using Newtonsoft.Json;
using ProcessPayment.Models;
using ProcessPayment.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.DTOs
{
    public class PaymentDTOs
    {
        [Required]
        public string CardHolderName { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        [MustNotBeInPast]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public string SecurityCode { get; set; }
        [Required]
        
        public decimal Amount { get; set; }

        public static explicit operator Payment(PaymentDTOs s)
        {
            return JsonConvert.DeserializeObject<Payment>(JsonConvert.SerializeObject(s));
        }
    }
}
