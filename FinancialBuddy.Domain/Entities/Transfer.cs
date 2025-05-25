﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Domain.Entities
{
    public class Transfer
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid ReceiverUserId { get; set; }
        public bool IsFast { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
