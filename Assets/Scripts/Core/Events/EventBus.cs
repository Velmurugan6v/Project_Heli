using System;
using System.Collections.Generic;
using UnityEngine;

namespace HelicopterTag.Core.Events
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> _listeners = new();


        public static void Subscribe<T>(Action<T> listener)
        {
            Type eventType = typeof(T);

            if (_listeners.TryGetValue(eventType, out Delegate existing))
            {
                _listeners[eventType] = Delegate.Combine(existing, listener);

                return;
            }

            _listeners[eventType] = listener;
        }

        public static void Publish<T>(T eventData)
        {
            Type eventType = typeof(T);

            if (!_listeners.TryGetValue(eventType, out Delegate listeners))
                return;

            if (listeners is Action<T> callback)
                callback.Invoke(eventData);
        }

        public static void Unsubscribe<T>(Action<T> listener)
        {
            Type eventType = typeof(T);
            
            if(!_listeners.TryGetValue(eventType, out Delegate existing))
                return;
            
            Delegate updated=Delegate.Remove(existing, listener);

            if (updated == null)
            {
                _listeners.Remove(eventType);
                return;
            }
            
            _listeners[eventType] = updated;
        }
    }
}