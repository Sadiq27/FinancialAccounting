using ProjectORM.Context;
using ProjectORM.ViewModels.Base;
using System;
using System.Linq;

namespace ProjectORM.ViewModels;

public class DashboardViewModel : ViewModelBase
{
    private double totalIncome;
    public double TotalIncome
    {
        get { return totalIncome; }
        set
        {
            if (totalIncome != value)
            {
                totalIncome = value;
                OnPropertyChanged(nameof(TotalIncome));
            }
        }
    }

    private int totalIncomeTransactionsCount;
    public int TotalIncomeTransactionsCount
    {
        get { return totalIncomeTransactionsCount; }
        set
        {
            if (totalIncomeTransactionsCount != value)
            {
                totalIncomeTransactionsCount = value;
                OnPropertyChanged(nameof(TotalIncomeTransactionsCount));
            }
        }
    }

    private double totalExpense;
    public double TotalExpense
    {
        get { return totalExpense; }
        set
        {
            if (totalExpense != value)
            {
                totalExpense = value;
                OnPropertyChanged(nameof(TotalExpense));
            }
        }
    }

    private int totalExpenseTransactionsCount;
    public int TotalExpenseTransactionsCount
    {
        get { return totalExpenseTransactionsCount; }
        set
        {
            if (totalExpenseTransactionsCount != value)
            {
                totalExpenseTransactionsCount = value;
                OnPropertyChanged(nameof(TotalExpenseTransactionsCount));
            }
        }
    }

    private double maximumIncome;
    public double MaximumIncome
    {
        get { return maximumIncome; }
        set
        {
            if (maximumIncome != value)
            {
                maximumIncome = value;
                OnPropertyChanged(nameof(MaximumIncome));
            }
        }
    }

    private double maximumExpense;
    public double MaximumExpense
    {
        get { return maximumExpense; }
        set
        {
            if (maximumExpense != value)
            {
                maximumExpense = value;
                OnPropertyChanged(nameof(MaximumExpense));
            }
        }
    }

    private double minimumExpense;
    public double MinimumExpense
    {
        get { return minimumExpense; }
        set
        {
            if (minimumExpense != value)
            {
                minimumExpense = value;
                OnPropertyChanged(nameof(MinimumExpense));
            }
        }
    }

    private double minimumIncome;
    public double MinimumIncome
    {
        get { return minimumIncome; }
        set
        {
            if (minimumIncome != value)
            {
                minimumIncome = value;
                OnPropertyChanged(nameof(MinimumIncome));
            }
        }
    }

    private DateTime? lastIncome;
    public DateTime? LastIncome
    {
        get { return lastIncome; }
        set
        {
            if (lastIncome != value)
            {
                lastIncome = value;
                OnPropertyChanged(nameof(LastIncome));
            }
        }
    }


    private DateTime? lastExpense;
    public DateTime? LastExpense
    {
        get { return lastExpense; }
        set
        {
            if (lastExpense != value)
            {
                lastExpense = value;
                OnPropertyChanged(nameof(LastExpense));
            }
        }
    }

    private double balance;
    public double Balance
    {
        get { return balance; }
        set
        {
            if (balance != value)
            {
                balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }
    }

    private string? bestIncomeCategory;
    public string? BestIncomeCategory
    {
        get { return bestIncomeCategory; }
        set
        {
            if (bestIncomeCategory != value)
            {
                bestIncomeCategory = value;
                OnPropertyChanged(nameof(BestIncomeCategory));
            }
        }
    }

    public DashboardViewModel(IncomeViewModel incomeViewModel, ExpenseViewModel expenseViewModel)
    {
        LoadDataFromDatabase();
        incomeViewModel.TransactionAdded += UpdateData;
        expenseViewModel.TransactionAdded += UpdateData;
    }

    private void UpdateData(object sender, EventArgs e)
    {
        LoadDataFromDatabase();
    }


    private void LoadDataFromDatabase()
    {
        using (var dbContext = new AppDbContext())
        {
            TotalIncome = dbContext.FinancialTransactions
                .Where(ft => ft.IsIncome)
                .Sum(ft => (double?)ft.Amount) ?? 0.0;

            TotalIncomeTransactionsCount = dbContext.FinancialTransactions
                .Count(ft => ft.IsIncome);

            TotalExpense = dbContext.FinancialTransactions
                .Where(ft => !ft.IsIncome)
                .Sum(ft => (double?)ft.Amount) ?? 0.0;

            TotalExpenseTransactionsCount = dbContext.FinancialTransactions
                .Count(ft => !ft.IsIncome);

            MaximumIncome = dbContext.FinancialTransactions
                .Where(ft => ft.IsIncome)
                .Max(ft => (double?)ft.Amount) ?? 0.0;

            MinimumIncome = dbContext.FinancialTransactions
                .Where(ft => ft.IsIncome)
                .Min(ft => (double?)ft.Amount) ?? 0.0;

            MaximumExpense = dbContext.FinancialTransactions
                .Where(ft => !ft.IsIncome)
                .Max(ft => (double?)ft.Amount) ?? 0.0;

            MinimumExpense = dbContext.FinancialTransactions
                .Where(ft => !ft.IsIncome)
                .Min(ft => (double?)ft.Amount) ?? 0.0;

            Balance = TotalIncome - TotalExpense;

            LastIncome = dbContext.FinancialTransactions
                .Where(ft => ft.IsIncome)
                .OrderByDescending(ft => ft.TransactionDate)
                .Select(ft => (DateTime?)ft.TransactionDate.Date)
                .FirstOrDefault();

            LastExpense = dbContext.FinancialTransactions
                .Where(ft => !ft.IsIncome)
                .OrderByDescending(ft => ft.TransactionDate)
                .Select(ft => (DateTime?)ft.TransactionDate.Date)
                .FirstOrDefault();

            BestIncomeCategory = dbContext.FinancialTransactions
                .Where(ft => ft.IsIncome)
                .GroupBy(ft => ft.Category)
                .OrderByDescending(group => group.Sum(ft => (double?)ft.Amount))
                .Select(group => group.Key)
                .FirstOrDefault();

        }
    }

}
