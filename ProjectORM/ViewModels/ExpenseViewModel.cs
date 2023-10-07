using ProjectORM.Commands.Base;
using ProjectORM.Mediator;
using ProjectORM.Models;
using ProjectORM.Repositories.Base;
using ProjectORM.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjectORM.ViewModels
{
    public class ExpenseViewModel : ViewModelBase
    {
        private readonly IFinancialTransactionRepository financialTransactionRepository;
        private readonly IExpenseCategoryDapperRepository expenseCategoryDapperRepository;
        private readonly ExpenseMediator mediator;
        public event EventHandler TransactionAdded;

        public ObservableCollection<ExpenseCategory> ExpenseCategories { get; set; }

        private string? expenseName;
        public string? ExpenseName
        {
            get { return expenseName; }
            set
            {
                if (expenseName != value)
                {
                    expenseName = value;
                    OnPropertyChanged(nameof(expenseName));
                }
            }
        }

        private ExpenseCategory? selectedCategory;
        public ExpenseCategory? SelectedCategory
        {
            get { return selectedCategory; }
            set { base.PropertyChangeMethod(out selectedCategory, value); }
        }

        private double amount;
        public double Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged(nameof(amount));
                }
            }
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    OnPropertyChanged(nameof(selectedDate));
                }
            }
        }

        private string? description;
        public string? Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(description));
                }
            }
        }

        private CommandBase? saveExpenseCommand;
        public CommandBase SaveExpenseCommand => this.saveExpenseCommand ??= new CommandBase(
            execute: () =>
            {
                try
                {
                    if (string.IsNullOrEmpty(ExpenseName))
                    {
                        MessageBox.Show("Expense Name is required.");
                        return;
                    }

                    if (SelectedCategory == null)
                    {
                        MessageBox.Show("Category is required.");
                        return;
                    }

                    if (Amount <= 0)
                    {
                        MessageBox.Show("Amount must be greater than zero.");
                        return;
                    }

                    var category = SelectedCategory.Name;

                    if (category == null)
                    {
                        MessageBox.Show($"Category '{SelectedCategory}' does not exist.");
                        return;
                    }

                    var newTransaction = new FinancialTransaction
                    {
                        Name = ExpenseName,
                        IsIncome = false,
                        Amount = Amount,
                        Description = Description,
                        TransactionDate = SelectedDate.Date,
                        Category = SelectedCategory.Name
                    };

                    financialTransactionRepository.Add(newTransaction);
                    financialTransactionRepository.SaveChanges();
                    mediator.Publish("ExpenseAdded", newTransaction);
                    TransactionAdded?.Invoke(this, EventArgs.Empty);

                    MessageBox.Show($"Expense saved successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            },
            canExecute: () => true
        );

        public ExpenseViewModel(IFinancialTransactionRepository financialTransactionRepository,
                            IExpenseCategoryDapperRepository expenseCategoryDapperRepository, ExpenseMediator mediator)
        {
            this.financialTransactionRepository = financialTransactionRepository;
            this.expenseCategoryDapperRepository = expenseCategoryDapperRepository;

            this.mediator = mediator;
            var allExpenseCategories = this.expenseCategoryDapperRepository.GetAll();
            this.ExpenseCategories = new ObservableCollection<ExpenseCategory>(allExpenseCategories);
        }
    }
}
