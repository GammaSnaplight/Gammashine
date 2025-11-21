using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Gammashine
{
    public class EventfulAutomachine
    {
        private static readonly Dictionary<Type, Delegate> _eventfulness = new();
        private static readonly Dictionary<Type, List<object>> _buffer = new();

        private static readonly object _lock = new();

        /// <summary> Подписка на событие типа T </summary>
        public static void Subscribe<T>(Action<T> eventful) where T : class
        {
            Type type = typeof(T);
            if (_eventfulness.TryGetValue(type, out Delegate existing))
            {
                _eventfulness[type] = Delegate.Combine(existing, eventful);
            }
            else
            {
                _eventfulness[type] = eventful;
            }
        }

        /// <summary> Подписка на событие типа T (Поточно-безопасный) </summary>
        public static void SubscribeThreadsafe<T>(Action<T> handler) where T : class
        {
            lock (_lock) Subscribe(handler);
        }

        /// <summary> Отписка от события типа T </summary>
        public static void Unsubscribe<T>(Action<T> eventful) where T : class
        {
            Type type = typeof(T);
            if (_eventfulness.TryGetValue(type, out Delegate existing))
            {
                Delegate newDelegate = Delegate.Remove(existing, eventful);
                if (newDelegate == null)
                {
                    _eventfulness.Remove(type);
                }
                else
                {
                    _eventfulness[type] = newDelegate;
                }
            }
        }

        /// <summary> Отписка от события типа T (Поточно-безопасный) </summary>
        public static void UnsubscribeThreadsafe<T>(Action<T> eventful) where T : class
        {
            lock (_lock) Unsubscribe(eventful);
        }

        /// <summary> Публикация события типа T </summary>
        public static void Publish<T>(T data) where T : class
        {
            _eventfulness.TryGetValue(typeof(T), out Delegate handler);

            (handler as Action<T>)?.Invoke(data);
        }

        /// <summary> Публикация события типа T (Поточно-безопасный) </summary>
        public static void PublishThreadsafe<T>(T data) where T : class
        {
            Delegate handler;
            lock (_lock) _eventfulness.TryGetValue(typeof(T), out handler);

            (handler as Action<T>)?.Invoke(data);
        }

        /// <summary> Подписка с учетом буфера для ожидания (требуется очистка буфера) </summary>
        public static void SubscribeExpected<T>(Action<T> eventful) where T : class
        {
            Type type = typeof(T);
            if (_eventfulness.TryGetValue(type, out Delegate existing))
            {
                _eventfulness[type] = Delegate.Combine(existing, eventful);
            }
            else
            {
                _eventfulness[type] = eventful;
            }

            if (_buffer.TryGetValue(type, out List<object> pending))
            {
                foreach (T item in pending.Cast<T>())
                    eventful(item);

                _buffer.Remove(type);
            }
        }

        /// <summary> Публикация с учетом буфера для ожидания (требуется очистка буфера) </summary>
        public static void PublishExpected<T>(T data) where T : class
        {
            Type type = typeof(T);
            if (_eventfulness.TryGetValue(type, out Delegate handler))
            {
                (handler as Action<T>)?.Invoke(data);
            }
            else
            {
                if (!_buffer.TryGetValue(type, out List<object> list))
                    _buffer[type] = list = new();

                list.Add(data);
            }
        }

        /// <summary> Очистка всех подписок </summary>
        public static void AllUnsubscribe()
        {
            _eventfulness.Clear();
            _buffer.Clear();
        }

        /// <summary> Очистка всех подписок (Поточно-безопасный) </summary>
        public static void AllUnsubscribeThreadsafe()
        {
            lock (_lock)
            {
                _eventfulness.Clear();
                _buffer.Clear();
            }
        }
    }
}
