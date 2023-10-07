using ProjectORM.Mediator;
using ProjectORM.Models;
using ProjectORM.Repositories.Base;
using ProjectORM.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjectORM.ViewModels;

public class IncomeHistoryViewModel : ViewModelBase
{
    private readonly IncomeMediator mediator;


    private readonly IFinancialTransactionRepository financialTransactionRepository;
    public ObservableCollection<FinancialTransaction> IncomeHistory { get; set; }

    public IncomeHistoryViewModel(IFinancialTransactionRepository financialTransactionRepository, IncomeMediator mediator)
    {
        this.financialTransactionRepository = financialTransactionRepository;
        this.mediator = mediator;
        this.mediator.Subscribe("IncomeAdded", UpdateIncomeHistory);
        LoadIncomeHistory();
    }

    private void LoadIncomeHistory()
    {
        var incomeHistoryData = financialTransactionRepository.GetAllIncomeTransactions();
        IncomeHistory = new ObservableCollection<FinancialTransaction>(incomeHistoryData);
    }

    private void UpdateIncomeHistory(object data)
    {
        if (data is FinancialTransaction transaction)
        {
            Application.Current.Dispatcher.Invoke(() => IncomeHistory.Add(transaction));
        }
    }
}

