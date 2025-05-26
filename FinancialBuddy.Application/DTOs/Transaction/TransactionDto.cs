using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.DTOs.Transaction
{
    public class TransactionDto
    {
        public Guid Id { get; set; } // verici ana hesap
        public Guid UserId { get; set; } // alici ana hesap
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
