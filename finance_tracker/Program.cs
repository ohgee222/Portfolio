using FinanceTracker.Services;
using finance_tracker.Models;
using Microsoft.Data.Sqlite;


var db = new DatabaseService();
var finance = new FinanceService(db);

while (true)
{
    Console.WriteLine("\n==== PERSONAL FINANCE TRACKER ====");
    Console.WriteLine("1. Add Income");
    Console.WriteLine("2. Add Expense");
    Console.WriteLine("3. View All Transactions");
    Console.WriteLine("4. View Monthly Summary");
    Console.WriteLine("5. Exit");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddIncome(finance);
            break;

        case "2":
            AddExpense(finance);
            break;

        case "3":
            ViewTransactions(finance);
            break;

        case "4":
            MonthlySummary(finance);
            break;

        case "5":
            Console.WriteLine("Exiting...");
            return;

        default:
            Console.WriteLine("Invalid option, try again.");
            break;
    }
}

void AddIncome(FinanceService finance)
{
    Console.Write("Enter amount: ");
    decimal amount = decimal.Parse(Console.ReadLine()!);

    Console.Write("Enter description: ");
    string? description = Console.ReadLine();

    finance.AddIncome(amount, description ?? "Income");

    Console.WriteLine("Income added!");
}

void AddExpense(FinanceService finance)
{
    Console.Write("Enter amount: ");
    decimal amount = decimal.Parse(Console.ReadLine()!);

    Console.Write("Enter category (Food, Rent, Travel, Other): ");
    string? category = Console.ReadLine();

    Console.Write("Enter description: ");
    string? description = Console.ReadLine();

    finance.AddExpense(amount, category ?? "Other", description ?? "Expense");

    Console.WriteLine("Expense added!");
}

void ViewTransactions(FinanceService finance)
{
    var transactions = finance.GetTransactions();

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

void MonthlySummary(FinanceService finance)
{
    Console.Write("Enter year (e.g., 2025): ");
    int year = int.Parse(Console.ReadLine()!);

    Console.Write("Enter month (1-12): ");
    int month = int.Parse(Console.ReadLine()!);

    var summary = finance.GetMonthlySummary(year, month);

    Console.WriteLine("\n--- MONTHLY SUMMARY ---");
    Console.WriteLine($"Total Income  : £{summary.income}");
    Console.WriteLine($"Total Expense : £{summary.expense}");
    Console.WriteLine($"Savings       : £{summary.savings}");

    Console.WriteLine("\nExpense by Category:");
    foreach (var cat in summary.categoryTotals)
        Console.WriteLine($"{cat.Key}: £{cat.Value}");
}
