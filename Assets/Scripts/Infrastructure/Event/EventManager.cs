using System;
using System.Collections.Generic;

namespace Infrastructure.Event
{
    public static class EventManager
    {
        private class EventHandler<T> where T : IEvent
        {
            public readonly List<Action<T>> Subscribers;

            public static EventHandler<T> Instance => GetInstance();

            private static EventHandler<T> GetInstance()
            {
                if (_instance == null)
                {
                    _instance = new EventHandler<T>();
                }

                return _instance;
            }

            private static EventHandler<T> _instance;
            
            private EventHandler()
            {
                Subscribers = new List<Action<T>>();
            }

            public void FireEvent(T eventData)
            {
                for (var i = Subscribers.Count - 1; i >= 0; i--)
                {
                    Subscribers[i].Invoke(eventData);
                }
            }
        }

        public static void Register<T>(Action<T> listener) where T : IEvent
        {
            EventHandler<T>.Instance.Subscribers.Add(listener);
        }

        public static void Unregister<T>(Action<T> listener) where T : IEvent
        {
            EventHandler<T>.Instance.Subscribers.Remove(listener);
        }
        
        public static void Send<T>(T eventData) where T : IEvent
        {
            EventHandler<T>.Instance.FireEvent(eventData);
        }

    }
}