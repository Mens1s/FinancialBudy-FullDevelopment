using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.DTOs.Subscription
{
    public class CreateSubscriptionRequest
    {
        public Guid UserId { get; set; }
        public string ServiceName { get; set; }
        public decimal Amount { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public string Frequency { get; set; }
    }
}
