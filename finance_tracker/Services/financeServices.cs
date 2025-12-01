using finance_tracker.Models;

namespace FinanceTracker.Services
{
    public class FinanceService
    {
        private readonly DatabaseService _db;

        public FinanceService(DatabaseService db)
        {
            _db = db;
        }

        public void AddIncome(decimal amount, string description)
        {
            var t = new Transaction
            {
                Amount = amount,
                Type = "Income",
                Category = null,
                Description = description,
                Date = DateTime.Now
            };

            _db.AddTransaction(t);
        }

        public void AddExpense(decimal amount, string category, string description)
        {
            var t = new Transaction
            {
                Amount = amount,
                Type = "Expense",
                Category = category,
                Description = description,
                Date = DateTime.Now
            };

            _db.AddTransaction(t);
        }

        public List<Transaction> GetTransactions()
        {
            return _db.GetAll();
        }

        public (decimal income, decimal expense, decimal savings, Dictionary<string, decimal> categoryTotals)
            GetMonthlySummary(int year, int month)
        {
            var monthly = _db.GetByMonth(year, month);

            decimal income = monthly
                .Where(t => t.Type == "Income")
                .Sum(t => t.Amount);

            decimal expense = monthly
                .Where(t => t.Type == "Expense")
                .Sum(t => t.Amount);

            var categoryTotals = monthly
                .Where(t => t.Type == "Expense")
                .GroupBy(t => t.Category ?? "Other")
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));

            return (income, expense, income - expense, categoryTotals);
        }
    }
}
