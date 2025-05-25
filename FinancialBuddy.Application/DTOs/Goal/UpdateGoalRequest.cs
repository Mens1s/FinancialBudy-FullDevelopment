using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.DTOs.Goal
{
    public class UpdateGoalRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal SavedAmount { get; set; }
        public DateTime TargetDate { get; set; }
    }
}
