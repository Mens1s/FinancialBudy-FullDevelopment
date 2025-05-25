using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Domain.Entities
{
    public class ValueAsset
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // metal Borsa Döviz
        public decimal CurrentPrice { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
