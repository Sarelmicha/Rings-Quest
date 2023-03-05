using System;

namespace Happyflow.Core.EventAggregator
{
    /// <summary>
    /// The Event Aggregator takes care for registering, unregistering and invoking of events loosely coupling Publishers and Subscribers.
    /// All Publishers and Subscribers of event will know only about the Event Aggregator.
    /// </summary>
    public interface IEventAggregator 
    {
        /// <summary>
        /// Subscribe to a signal.
        /// </summary>
        /// <param name="subscriber">The action to invoke when signal is fired.</param>
        /// <typeparam name="T">The type of the signal to be notify when fired by the producer.</typeparam>
        void Subscribe<T>(Action subscriber);
        
        /// <summary>
        /// Subscribe to a signal.
        /// </summary>
        /// <param name="subscriber">The action to invoke when signal is fired.</param>
        /// <typeparam name="T">The type of the signal to be notify when fired by the producer.</typeparam>
        void Subscribe<T>(Action<T> subscriber);
        
        /// <summary>
        /// Unsubscribe to a signal.
        /// </summary>
        /// <param name="subscriber">The action to remove from the invoke actions when signal is fired.</param>
        /// <typeparam name="T">The type of the signal that the consumer does not want to be notify anymore.</typeparam>
        void Unsubscribe<T>(Action subscriber);
        
        /// <summary>
        /// Unsubscribe to a signal.
        /// </summary>
        /// <param name="subscriber">The action to remove from the invoke actions when signal is fired.</param>
        /// <typeparam name="T">The type of the signal that the consumer does not want to be notify anymore.</typeparam>
        void Unsubscribe<T>(Action<T> subscriber);
        
        /// <summary>
        /// Fire a signal.
        /// </summary>
        /// <typeparam name="T">The type of the signal</typeparam>
        void Fire<T>();
        
        /// <summary>
        /// Fire a signal.
        /// </summary>
        /// <typeparam name="T">The type of the signal</typeparam>
        void Fire<T>(T message);
    }
}