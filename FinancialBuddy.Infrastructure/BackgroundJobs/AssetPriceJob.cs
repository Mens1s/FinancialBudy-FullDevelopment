using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;

namespace FinancialBuddy.Infrastructure.BackgroundJobs
{
    public class AssetPriceJob
    {
        private readonly IGenericRepository<ValueAsset> _assetRepository;

        public AssetPriceJob(IGenericRepository<ValueAsset> assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task UpdateAssetPrices()
        {
            var mockAssets = new List<ValueAsset>
            {
                new ValueAsset { Name = "Altın", Type = "Metal", CurrentPrice = RandomPrice(), LastUpdated = DateTime.UtcNow },
                new ValueAsset { Name = "Gümüş", Type = "Metal", CurrentPrice = RandomPrice(), LastUpdated = DateTime.UtcNow },
                new ValueAsset { Name = "Platin", Type = "Metal", CurrentPrice = RandomPrice(), LastUpdated = DateTime.UtcNow },
                new ValueAsset { Name = "HVLSN", Type = "Borsa-BIST100", CurrentPrice = RandomPrice(), LastUpdated = DateTime.UtcNow },
                new ValueAsset { Name = "HPSBRD", Type = "Borsa-NASDAQ", CurrentPrice = RandomPrice(), LastUpdated = DateTime.UtcNow },
                new ValueAsset { Name = "YGT", Type = "Borsa-BIST100", CurrentPrice = RandomPrice(), LastUpdated = DateTime.UtcNow }
            };

            foreach (var mock in mockAssets)
            {
                var existing = (await _assetRepository.FindAsync(a => a.Name == mock.Name)).FirstOrDefault();
                if (existing != null)
                {
                    existing.CurrentPrice = mock.CurrentPrice;
                    existing.LastUpdated = DateTime.UtcNow;
                    _assetRepository.Update(existing);
                }
                else
                {
                    mock.Id = Guid.NewGuid();
                    await _assetRepository.AddAsync(mock);
                }

                Console.WriteLine($"[AssetPriceJob] Updated {mock.Name}: {mock.CurrentPrice}");
            }

            await _assetRepository.SaveChangesAsync();
        }

        private decimal RandomPrice()
        {
            var random = new Random();
            return Math.Round((decimal)(random.NextDouble() * 1000 + 50), 2);
        }
    }
}
