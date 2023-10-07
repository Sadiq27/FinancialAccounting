namespace ProjectORM.Mediator
{
    using System;
    using System.Collections.Generic;

    public class IncomeMediator
    {
        private Dictionary<string, Action<object>> subscribers = new Dictionary<string, Action<object>>();

        public void Subscribe(string message, Action<object> action)
        {
            if (!subscribers.ContainsKey(message))
            {
                subscribers[message] = action;
            }
            else
            {
                subscribers[message] += action;
            }
        }

        public void Unsubscribe(string message, Action<object> action)
        {
            if (subscribers.ContainsKey(message))
            {
                subscribers[message] -= action;

                if (subscribers[message] == null)
                {
                    subscribers.Remove(message);
                }
            }
        }

        public void Publish(string message, object data = null)
        {
            if (subscribers.ContainsKey(message))
            {
                subscribers[message]?.Invoke(data);
            }
        }
    }
}
