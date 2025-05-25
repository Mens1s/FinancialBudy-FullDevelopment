using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Domain.Entities
{
    public class UserAsset
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid AssetId { get; set; }
        public ValueAsset Asset { get; set; }
        public decimal Quantity { get; set; }
        public decimal AveragePrice { get; set; }
    }
}
