using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.DTOs.Asset
{
    public class SellAssetRequest
    {
        public Guid UserId { get; set; }
        public Guid AssetId { get; set; }
        public decimal Quantity { get; set; }
    }
}
