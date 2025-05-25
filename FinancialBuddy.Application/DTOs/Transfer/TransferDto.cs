namespace FinancialBuddy.Application.DTOs.Transfer
{
    public class TransferDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
