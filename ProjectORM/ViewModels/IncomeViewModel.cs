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
    public class IncomeViewModel : ViewModelBase
    {
        private readonly IFinancialTransactionRepository financialTransactionRepository;
        private readonly IIncomeCategoryDapperRepository incomeCategoryDapperRepository;

        private readonly IncomeMediator mediator;
        public event EventHandler TransactionAdded;

        public ObservableCollection<IncomeCategory> IncomeCategories { get; set; }

        private string? incomeName;
        public string? IncomeName
        {
            get { return incomeName; }
            set
            {
                if (incomeName != value)
                {
                    incomeName = value;
                    OnPropertyChanged(nameof(incomeName));
                }
            }
        }

        private IncomeCategory? selectedCategory;
        public IncomeCategory? SelectedCategory
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

        private CommandBase? saveIncomeCommand;
        public CommandBase SaveIncomeCommand => this.saveIncomeCommand ??= new CommandBase(
            execute: () =>
            {
                try
                {
                    if (string.IsNullOrEmpty(IncomeName))
                    {
                        MessageBox.Show("Income Name is required.");
                        return;
                    }

                    if (string.IsNullOrEmpty(SelectedCategory.Name))
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
                        Name = IncomeName,
                        IsIncome = true,
                        Amount = Amount,
                        Description = Description,
                        TransactionDate = SelectedDate.Date,
                        Category = SelectedCategory.Name
                    };

                    financialTransactionRepository.Add(newTransaction);
                    financialTransactionRepository.SaveChanges();
                    mediator.Publish("IncomeAdded", newTransaction);
                    TransactionAdded?.Invoke(this, EventArgs.Empty);

                    MessageBox.Show($"Income saved successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            },
            canExecute: () => true
        );


        public IncomeViewModel(IFinancialTransactionRepository financialTransactionRepository,
                                    IIncomeCategoryDapperRepository incomeCategoryDapperRepository, IncomeMediator mediator)
        {
            this.financialTransactionRepository = financialTransactionRepository;
            this.incomeCategoryDapperRepository = incomeCategoryDapperRepository;

            this.mediator = mediator;
            var allIncomeCategories = this.incomeCategoryDapperRepository.GetAll();
            this.IncomeCategories = new ObservableCollection<IncomeCategory>(allIncomeCategories);
        }

    }
}
