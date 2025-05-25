using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string ServiceName { get; set; }
        public decimal Amount { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public string Frequency { get; set; }
        public bool IsAutoPayment { get; set; } = false;

    }
}