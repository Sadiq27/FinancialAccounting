using ProjectORM.Messages.Base;
using ProjectORM.ViewModels.Base;

namespace ProjectORM.Messages
{
    public class NavigationMessage : IMessage
    {
        public readonly ViewModelBase DestinationViewModel;

        public NavigationMessage(ViewModelBase destinationViewModel)
        {
            this.DestinationViewModel = destinationViewModel;
        }
    }
}
