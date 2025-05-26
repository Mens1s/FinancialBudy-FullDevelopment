using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.DTOs.User
{
    public class UserDashboardGeneralInfoDto
    {
        public decimal autoPaymentCount { get; set; }
        public decimal accountBalance { get; set; }
        public decimal roundedBalance { get; set; }
        public decimal investmentAmount { get; set; }
    }
}
