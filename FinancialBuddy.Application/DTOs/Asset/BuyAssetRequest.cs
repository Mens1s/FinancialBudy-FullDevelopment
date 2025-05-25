

namespace FinancialBuddy.Application.DTOs.Asset
{
    public class BuyAssetRequest
    {
        public Guid UserId { get; set; }
        public Guid AssetId { get; set; }
        public decimal Quantity { get; set; }
    }
}
