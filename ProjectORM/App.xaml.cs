using ProjectORM.Context;
using ProjectORM.Mediator;
using ProjectORM.Mediator.Base;
using ProjectORM.Repositories;
using ProjectORM.Repositories.Base;
using ProjectORM.ViewModels;
using ProjectORM.ViewModels.Base;
using SimpleInjector;
using System.Windows;

namespace ProjectORM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; } = new Container();
        public static Container Container1 { get; set; } = new Container();

        protected override void OnStartup(StartupEventArgs e)
        {
            this.RegisterContainer();

            this.Start<LoginViewModel>();

            base.OnStartup(e);
        }

        private void Start<T>() where T : ViewModelBase
        {
            var mainView = new MainWindow();
            var mainViewModel = Container.GetInstance<MainViewModel>();
            mainViewModel.ActiveViewModel = Container.GetInstance<T>();

            mainView.DataContext = mainViewModel;

            mainView.ShowDialog();

            var homeView = new MainWindow();
            var homeViewModel = Container1.GetInstance<HomeViewModel>();
            homeViewModel.ActiveSubViewModel = Container1.GetInstance<T>();

            homeView.DataContext = homeViewModel;

            homeView.ShowDialog();

        }
        private void RegisterContainer()
        {
            Container1.RegisterSingleton<IExpenseCategoryDapperRepository, ExpenseCategoryDapperRepository>();
            Container1.RegisterSingleton<IIncomeCategoryDapperRepository, IncomeCategoryDapperRepository>();
            Container1.RegisterSingleton<IFinancialTransactionRepository, FinancialTransactionRepository>();

            Container1.RegisterSingleton<IMessenger, Messenger2>();
            Container1.RegisterSingleton<IncomeMediator>();
            Container1.RegisterSingleton<ExpenseMediator>();

            Container1.RegisterSingleton<DashboardViewModel>();
            Container1.RegisterSingleton<IncomeViewModel>();
            Container1.RegisterSingleton<ExpenseViewModel>();
            Container1.RegisterSingleton<IncomeHistoryViewModel>();
            Container1.RegisterSingleton<ExpenseHistoryViewModel>();


            Container1.Register<AppDbContext>(Lifestyle.Singleton);
            Container1.Verify();


            Container.RegisterSingleton<IUserRepository, UserRepository>();
            Container.RegisterSingleton<IMessenger, Messenger>();

            Container.RegisterSingleton<HomeViewModel>();
            Container.RegisterSingleton<LoginViewModel>();
            Container.RegisterSingleton<MainViewModel>();


            Container.Register<AppDbContext>(Lifestyle.Singleton);
            Container.Verify();

        }
    }
}
