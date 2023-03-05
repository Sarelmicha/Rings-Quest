using System;
using System.Collections.Generic;
using UnityEngine;

namespace Happyflow.Core.EventAggregator
{
    /// <summary>
    /// The Event Aggregator takes care for registering, unregistering and invoking of events loosely coupling Publishers and Subscribers.
    /// All Publishers and Subscribers of event will know only about the Event Aggregator.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, Dictionary<SignalSubscriptionId, Action<object>>> m_SubscribersMap;
        
        public EventAggregator()
        {
            m_SubscribersMap = new Dictionary<Type, Dictionary<SignalSubscriptionId, Action<object>>>();
        }
        
        /// <summary>
        /// Subscribe to a signal.
        /// </summary>
        /// <param name="subscriber">The action to invoke when signal is fired.</param>
        /// <typeparam name="T">The type of the signal to be notify when fired by the producer.</typeparam>
        public void Subscribe<T>(Action subscriber)
        {
            void WrapperCallback(object args) => subscriber?.Invoke();
            SubscribeInternal(typeof(T), subscriber, WrapperCallback);
        }

        /// <summary>
        /// Subscribe to a signal.
        /// </summary>
        /// <param name="subscriber">The action to invoke when signal is fired.</param>
        /// <typeparam name="T">The type of the signal to be notify when fired by the producer.</typeparam>
        public void Subscribe<T>(Action<T> subscriber)
        {
            void WrapperCallback(object args) => subscriber?.Invoke((T) args);
            SubscribeInternal(typeof(T), subscriber, WrapperCallback);
        }
        
        /// <summary>
        /// Unsubscribe to a signal.
        /// </summary>
        /// <param name="subscriber">The action to remove from the invoke actions when signal is fired.</param>
        /// <typeparam name="T">The type of the signal that the consumer does not want to be notify anymore.</typeparam>
        public void Unsubscribe<T>(Action subscriber)
        {
            UnsubscribeInternal(typeof(T), subscriber);
        }
        
        /// <summary>
        /// Unsubscribe to a signal.
        /// </summary>
        /// <param name="subscriber">The action to remove from the invoke actions when signal is fired.</param>
        /// <typeparam name="T">The type of the signal that the consumer does not want to be notify anymore.</typeparam>
        public void Unsubscribe<T>(Action<T> subscriber)
        {
            UnsubscribeInternal(typeof(T), subscriber);
        }
        
        /// <summary>
        /// Fire a signal.
        /// </summary>
        /// <typeparam name="T">The type of the signal</typeparam>
        public void Fire<T>()
        {
            FireInternal<T>(typeof(T));
        }
        
        /// <summary>
        /// Fire a signal.
        /// </summary>
        /// <typeparam name="T">The type of the signal</typeparam>
        public void Fire<T>(T message)
        {
            FireInternal(typeof(T), message);
        }
        
        private void SubscribeInternal(Type type, object token, Action<object> callback)
        {
            if (!m_SubscribersMap.ContainsKey(type) || m_SubscribersMap[type] == null)
            {
                m_SubscribersMap[type] = new Dictionary<SignalSubscriptionId, Action<object>>();
            }

            SignalSubscriptionId signalSubscriptionId = new SignalSubscriptionId(type, token);
            m_SubscribersMap[type][signalSubscriptionId] = callback;
        }
        
        private void UnsubscribeInternal(Type type, object token)
        {
            if (!m_SubscribersMap.TryGetValue(type, out var subscribers))
            {
                Debug.Log($"Failed to unsubscribe because {type} is not exists.");
                return;
            }
            
            SignalSubscriptionId signalSubscriptionId = new SignalSubscriptionId(type, token);

            if (!subscribers.ContainsKey(signalSubscriptionId))
            {
                Debug.Log($"Failed to unsubscribe because {signalSubscriptionId} is not exists.");
                return;
            }

            subscribers.Remove(signalSubscriptionId);

            if (m_SubscribersMap.Count <= 0)
            {
                m_SubscribersMap.Remove(type);
            }
        }

        private void FireInternal<T>(Type type, T message = default)
        {
            if (!m_SubscribersMap.TryGetValue(type, out var subscribers))
            {
                Debug.Log($"Failed to fire signal because {type} is not exists.");
                return;
            }

            foreach (var subscriber in subscribers.Values)
            {
                subscriber?.Invoke(message);
            }
        }
    }
}