using ProjectORM.Messages.Base;
using System;

namespace ProjectORM.Mediator.Base
{
    public interface IMessenger
    {
        void Subscribe<T>(Action<IMessage> action) where T : IMessage;
        void Send<T>(T message) where T : IMessage;
    }
}
