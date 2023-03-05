using System;
using System.Collections.Generic;
using UnityEngine;

namespace Happyflow.Core.ServiceLocator
{
    /// <summary>
    /// service locator for <see cref="IGameService"/> instances.
    /// </summary>
    public class ServiceLocator
    {
        /// <summary>
        /// currently registered services.
        /// </summary>
        private readonly Dictionary<string, IGameService> m_Services;

        /// <summary>
        /// Gets the currently active service locator instance.
        /// </summary>
        public static ServiceLocator Instance => m_Instance ??= new ServiceLocator();
        
        private static ServiceLocator m_Instance;
        
        private ServiceLocator()
        {
            m_Services = new Dictionary<string, IGameService>();
        }
        
        /// <summary>
        /// Gets the service instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>The service instance.</returns>
        public T Get<T>() where T : IGameService
        {
            string key = typeof(T).Name;
            if (!m_Services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)m_Services[key];
        }

        /// <summary>
        /// Registers the service with the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance.</param>
        public void Register<T>(T service) where T : IGameService
        {
            string key = typeof(T).Name;
            if (m_Services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }

            m_Services.Add(key, service);
        }

        /// <summary>
        /// Unregisters the service from the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        public void Unregister<T>() where T : IGameService
        {
            string key = typeof(T).Name;
            if (!m_Services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }

            m_Services.Remove(key);
        }
    }
}