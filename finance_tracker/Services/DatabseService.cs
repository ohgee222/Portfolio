using Microsoft.Data.Sqlite;

using finance_tracker.Models;
using Dapper;
using static System.Net.Mime.MediaTypeNames;


namespace FinanceTracker.Services;

public class DatabaseService
{
    private const string ConnectionString = "Data Source=finance.db";

    public DatabaseService()
    {
        using var connection = new SqliteConnection(ConnectionString);

        // Create Users table with full name  email,username, password, butpassword has to be hashed to be securte
        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT NOT NULL,
                Email TEXT NOT NULL UNIQUE,
                Username TEXT NOT NULL UNIQUE,
                PasswordHash TEXT NOT NULL
            );
        ");

        // Create Transactions table with UserId
        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Transactions (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER NOT NULL,
                Amount REAL NOT NULL,
                Type TEXT NOT NULL,
                Category TEXT,
                Description TEXT,
                Date TEXT NOT NULL,
                FOREIGN KEY(UserId) REFERENCES Users(Id)
            );
        ");
    }

    public void AddTransaction(Transaction t)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Execute(@"
            INSERT INTO Transactions (UserId, Amount, Type, Category, Description, Date)
            VALUES (@UserId, @Amount, @Type, @Category, @Description, @Date)
        ", t);
    }

    public List<Transaction> GetTransactionsByUser(int userId)
    {
        using var connection = new SqliteConnection(ConnectionString);
        return connection.Query<Transaction>(
            "SELECT * FROM Transactions WHERE UserId = @userId ORDER BY Date DESC",
            new { userId }
        ).ToList();
    }

    public List<Transaction> GetTransactionsByUserAndMonth(int userId, int year, int month)
    {
        using var connection = new SqliteConnection(ConnectionString);
        return connection.Query<Transaction>(@"
        SELECT * FROM Transactions
        WHERE UserId = @userId 
          AND strftime('%Y', Date) = @y 
          AND strftime('%m', Date) = @m
    ", new
        {
            userId,
            y = year.ToString(),
            m = month.ToString("D2")
        }).ToList();
    }
    public void RegisterUser(string fullName, string email, string username, string password)
    {
        string hashed = BCrypt.Net.BCrypt.HashPassword(password);

        using var connection = new SqliteConnection(ConnectionString);
        connection.Execute(@"
            INSERT INTO Users (FullName, Email, Username, PasswordHash)
            VALUES (@FullName, @Email, @Username, @PasswordHash)
        ", new { FullName = fullName, Email = email, Username = username, PasswordHash = hashed });
    }

    public User? LoginUser(string username, string password)
    {
        using var connection = new SqliteConnection(ConnectionString);
        var user = connection.QuerySingleOrDefault<User>(
            "SELECT * FROM Users WHERE Username = @Username",
            new { Username = username }
        );

        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return user;

        return null;
    }

}

