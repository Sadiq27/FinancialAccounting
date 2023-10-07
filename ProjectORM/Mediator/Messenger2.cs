using ProjectORM.Mediator.Base;
using ProjectORM.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectORM.Mediator
{
    public class Messenger2 : IMessenger
    {
        private Dictionary<Type, List<Action<IMessage>>> dict;

        public Messenger2()
        {
            dict = new Dictionary<Type, List<Action<IMessage>>>();
        }

        public void Subscribe<T>(Action<IMessage> action) where T : IMessage
        {
            var type = typeof(T);

            if (dict.ContainsKey(type) == false)
                dict.Add(type, new List<Action<IMessage>>());

            dict[type].Add(action);
        }

        public void Send<T>(T message) where T : IMessage
        {
            var type = typeof(T);

            if (dict.ContainsKey(type) == false)
                return;

            foreach (var action in dict[type])
            {
                action.Invoke(message);
            }
        }
    }

}
