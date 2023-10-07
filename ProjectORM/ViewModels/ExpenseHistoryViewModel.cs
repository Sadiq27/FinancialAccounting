using ProjectORM.Mediator;
using ProjectORM.Models;
using ProjectORM.Repositories.Base;
using ProjectORM.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjectORM.ViewModels;

public class ExpenseHistoryViewModel : ViewModelBase
{
    private readonly ExpenseMediator mediator;

    private readonly IFinancialTransactionRepository financialTransactionRepository;
    public ObservableCollection<FinancialTransaction> ExpenseHistory { get; set; }

    public ExpenseHistoryViewModel(IFinancialTransactionRepository financialTransactionRepository, ExpenseMediator mediator)
    {
        this.financialTransactionRepository = financialTransactionRepository;
        this.mediator = mediator;
        this.mediator.Subscribe("ExpenseAdded", UpdateExpenseHistory);
        LoadExpenseHistory();
    }

    private void LoadExpenseHistory()
    {
        var expenseHistoryData = financialTransactionRepository.GetAllExpenseTransactions();
        ExpenseHistory = new ObservableCollection<FinancialTransaction>(expenseHistoryData);
    }

    private void UpdateExpenseHistory(object data)
    {
        if (data is FinancialTransaction transaction)
        {
            Application.Current.Dispatcher.Invoke(() => ExpenseHistory.Add(transaction));
        }
    }
}
