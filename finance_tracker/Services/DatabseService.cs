using Microsoft.Data.Sqlite;

using finance_tracker.Models;
using Dapper;


namespace FinanceTracker.Services;

public class DatabaseService
{
    private const string ConnectionString = "Data Source=finance.db";

    public DatabaseService()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Transactions (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Amount REAL NOT NULL,
                Type TEXT NOT NULL,
                Category TEXT,
                Description TEXT,
                Date TEXT NOT NULL
            )
        ");
    }

    public void AddTransaction(Transaction t)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Execute(@"
            INSERT INTO Transactions (Amount, Type, Category, Description, Date)
            VALUES (@Amount, @Type, @Category, @Description, @Date)
        ", t);
    }

    public List<Transaction> GetAll()
    {
        using var connection = new SqliteConnection(ConnectionString);
        return connection.Query<Transaction>("SELECT * FROM Transactions ORDER BY Date DESC").ToList();
    }

    public List<Transaction> GetByMonth(int year, int month)
    {
        using var connection = new SqliteConnection(ConnectionString);

        return connection.Query<Transaction>(@"
            SELECT * FROM Transactions
            WHERE strftime('%Y', Date) = @y AND strftime('%m', Date) = @m
        ", new
        {
            y = year.ToString(),
            m = month.ToString("D2")
        }).ToList();
    }
}

