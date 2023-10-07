using ProjectORM.Messages.Base;
using ProjectORM.ViewModels.Base;

namespace ProjectORM.Messages
{
    public class NavigationMessage2 : IMessage
    {
        public readonly ViewModelBase DestinationViewModel;

        public NavigationMessage2(ViewModelBase destinationViewModel)
        {
            this.DestinationViewModel = destinationViewModel;
        }
    }
}
