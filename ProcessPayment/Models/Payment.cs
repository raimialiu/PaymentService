using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        public string CardHolderName { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }

        public PaymentState state { get; set; }
    }

    public class PaymentStates
    {
        public const string SUCCESS = "success";
        public const string FAILED = "failed";
        public const string PENDING = "pending";
    }

    public class PaymentState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentStateId { get; set; }
        public string state { get; set; }
        public int paymentId { get; set; }
    }

    public class ProcessPaymentDbContext:DbContext
    {
        public DbSet<Payment> payments { get; set; }
        public DbSet<PaymentState> paymentState { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                Directory.SetCurrentDirectory(".");
                string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                var config = new ConfigurationBuilder().AddJsonFile(path).Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                // C:\Users\raimi.aliu\source\repos\ProcessPayment\ProcessPayment\appsettings.json
            }
        }
    }
}
