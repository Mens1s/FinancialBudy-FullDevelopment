using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User";
        public decimal Balance { get; set; } = 0m;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Transfer> Transfers { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<UserAsset> UserAssets { get; set; }
    }
}
