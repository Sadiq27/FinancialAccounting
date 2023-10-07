using ProjectORM.Mediator.Base;
using ProjectORM.Messages;
using ProjectORM.ViewModels.Base;

namespace ProjectORM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMessenger messenger;


        private ViewModelBase activeViewModel;

        public ViewModelBase ActiveViewModel
        {
            get => activeViewModel;
            set => base.PropertyChangeMethod(out activeViewModel, value);
        }

        public MainViewModel(IMessenger messenger)
        {
            this.messenger = messenger;

            this.messenger.Subscribe<NavigationMessage>((message) =>
            {
                if (message is NavigationMessage navigationMessage)
                {
                    this.ActiveViewModel = navigationMessage.DestinationViewModel;
                }
            });
        }
    }
}
