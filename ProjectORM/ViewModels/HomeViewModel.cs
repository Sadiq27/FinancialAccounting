using ProjectORM.Commands.Base;
using ProjectORM.Mediator.Base;
using ProjectORM.Messages;
using ProjectORM.ViewModels.Base;
using System;
using System.Windows;

namespace ProjectORM.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IMessenger messenger;

        private ViewModelBase activeSubViewModel;
        public ViewModelBase ActiveSubViewModel
        {
            get => activeSubViewModel;
            set => base.PropertyChangeMethod(out activeSubViewModel, value);
        }


        private CommandBase? dashboardCommand;
        public CommandBase DashboardCommand => this.dashboardCommand ??= new CommandBase(
            execute: () =>
            {
                try
                {
                    this.messenger.Send(new NavigationMessage2(App.Container1.GetInstance<DashboardViewModel>()));

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            },
            canExecute: () => true
        );


        private CommandBase? incomeCommand;
        public CommandBase IncomeCommand => this.incomeCommand ??= new CommandBase(
            execute: () =>
            {
                try
                {
                    this.messenger.Send(new NavigationMessage2(App.Container1.GetInstance<IncomeViewModel>()));

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            },
            canExecute: () => true
        );


        private CommandBase? expenseCommand;
        public CommandBase ExpenseCommand => this.expenseCommand ??= new CommandBase(
            execute: () =>
            {
                try
                {
                    this.messenger.Send(new NavigationMessage2(App.Container1.GetInstance<ExpenseViewModel>()));

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            },
            canExecute: () => true
        );

        private CommandBase? viewIncomeCommand;
        public CommandBase ViewIncomeCommand => this.viewIncomeCommand ??= new CommandBase(
            execute: () =>
            {
                try
                {
                    this.messenger.Send(new NavigationMessage2(App.Container1.GetInstance<IncomeHistoryViewModel>()));

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            },
            canExecute: () => true
        );

        private CommandBase? viewExpenseCommand;
        public CommandBase ViewExpenseCommand => this.viewExpenseCommand ??= new CommandBase(
            execute: () =>
            {
                try
                {
                    this.messenger.Send(new NavigationMessage2(App.Container1.GetInstance<ExpenseHistoryViewModel>()));

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            },
            canExecute: () => true
        );


        public HomeViewModel(IMessenger messenger)
        {
            this.messenger = messenger;

            this.messenger.Subscribe<NavigationMessage2>((message) =>
            {
                if (message is NavigationMessage2 navigationMessage)
                {
                    this.ActiveSubViewModel = navigationMessage.DestinationViewModel;
                }
            });
        }

    }
}
