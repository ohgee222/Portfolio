using System;
namespace finance_tracker.Models
{
	public class Transaction
	{
        
            public decimal Amount { get; set; }
            public string Type { get; set; }  // "Income" or "Expense"
            public string Category { get; set; } // For expenses, otherwise null
            public string Description { get; set; }
            public DateTime Date { get; set; }
        
    }
}

