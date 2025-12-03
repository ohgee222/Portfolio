using FinanceTracker.Services;
using finance_tracker.Models;

var db = new DatabaseService();
var menu = new Menu();

while (true)
{
    Console.Clear();
    Console.WriteLine("==== FINANCE TRACKER ====");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Register");
    Console.WriteLine("3. Exit");
    Console.Write("Choose option: ");
    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            var user = Login(db);
            if (user != null)
            {
                Console.WriteLine($"\nWelcome {user.FullName}!");
                menu.Start(user.Id);   // ← This enters YOUR menu class
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
                Console.ReadKey();
            }
            break;

        case "2":
            Register(db);
            break;

        case "3":
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid option.");
            Console.ReadKey();
            break;
    }
}

User? Login(DatabaseService db)
{
    Console.Clear();
    Console.WriteLine("==== LOGIN ====");
    Console.Write("Username: ");
    string username = Console.ReadLine()!;

    Console.Write("Password: ");
    string password = ReadPassword();

    return db.LoginUser(username, password);
}

void Register(DatabaseService db)
{
    Console.Clear();
    Console.WriteLine("==== REGISTER ====");

    Console.Write("Full Name: ");
    string fullName = Console.ReadLine()!;

    Console.Write("Email: ");
    string email = Console.ReadLine()!;

    Console.Write("Username: ");
    string username = Console.ReadLine()!;

    Console.Write("Password: ");
    string password = ReadPassword();

    try
    {
        db.RegisterUser(fullName, email, username, password);
        Console.WriteLine("\nRegistration successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nRegistration failed: {ex.Message}");
    }

    Console.ReadKey();
}

string ReadPassword()
{
    string pass = "";
    ConsoleKey key;

    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
    {
        if (key == ConsoleKey.Backspace && pass.Length > 0)
        {
            pass = pass[..^1];
            Console.Write("\b \b");
        }
        else if (!char.IsControl((char)key))
        {
            pass += (char)key;
            Console.Write("*");
        }
    }

    Console.WriteLine();
    return pass;
}
