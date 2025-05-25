using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.DTOs.Transfer
{
    public class CreateTransferRequest
    {
        public Guid UserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public bool IsFast { get; set; }
    }
}
