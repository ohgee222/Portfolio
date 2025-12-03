namespace FinanceTracker.Services
{
    public class Menu
    {
        private readonly DatabaseService _db;
        private FinanceService? _finance;

        public Menu()
        {
            _db = new DatabaseService();
        }

        public void Start(int userId)
        {
            // create finance service for the logged in user
            _finance = new FinanceService(_db, userId);

            while (true)
            {
                Console.WriteLine("\n==== PERSONAL FINANCE TRACKER ====");
                Console.WriteLine("1. Add Income");
                Console.WriteLine("2. Add Expense");
                Console.WriteLine("3. View All Transactions");
                Console.WriteLine("4. View Monthly Summary");
                Console.WriteLine("5. Logout");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddIncome();
                        break;

                    case "2":
                        AddExpense();
                        break;

                    case "3":
                        ViewTransactions();
                        break;

                    case "4":
                        MonthlySummary();
                        break;

                    case "5":
                        Console.WriteLine("Logging out...");
                        return;

                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }

        private void AddIncome()
        {
            Console.Write("Enter amount: ");
            decimal amount = decimal.Parse(Console.ReadLine()!);

            Console.Write("Enter description: ");
            string? description = Console.ReadLine();

            _finance!.AddIncome(amount, description ?? "Income");
            Console.WriteLine("Income added!");
        }

        private void AddExpense()
        {
            Console.Write("Enter amount: ");
            decimal amount = decimal.Parse(Console.ReadLine()!);

            Console.Write("Enter category (Food, Rent, Travel, Other): ");
            string? category = Console.ReadLine();

            Console.Write("Enter description: ");
            string? description = Console.ReadLine();

            _finance!.AddExpense(amount, category ?? "Other", description ?? "Expense");
            Console.WriteLine("Expense added!");
        }

        private void ViewTransactions()
        {
            var transactions = _finance!.GetTransactions();

            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            Console.WriteLine("\n--- ALL TRANSACTIONS ---");
            foreach (var t in transactions)
            {
                Console.WriteLine($"{t.Date:yyyy-MM-dd} | {t.Type} | £{t.Amount} | {t.Category} | {t.Description}");
            }
        }

        private void MonthlySummary()
        {
            Console.Write("Enter year (e.g., 2025): ");
            int year = int.Parse(Console.ReadLine()!);

            Console.Write("Enter month (1-12): ");
            int month = int.Parse(Console.ReadLine()!);

            var summary = _finance!.GetMonthlySummary(year, month);

            Console.WriteLine("\n--- MONTHLY SUMMARY ---");
            Console.WriteLine($"Total Income  : £{summary.income}");
            Console.WriteLine($"Total Expense : £{summary.expense}");
            Console.WriteLine($"Savings       : £{summary.savings}");

            Console.WriteLine("\nExpense by Category:");
            foreach (var cat in summary.categoryTotals)
                Console.WriteLine($"{cat.Key}: £{cat.Value}");
        }
    }
}
