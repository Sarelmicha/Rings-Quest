using System;
using Cysharp.Threading.Tasks;

namespace Happyflow.Core.RetryStrategy
{
    /// <summary>
    /// Class that implements a retry mechanism strategy with a maximum number of retries.
    /// </summary>
    public class MaxRetryStrategy : RetryStrategyBase
    {
        private readonly int m_MaxRetries;
        private int m_CurrentAmountOfRetires;
        
        /// <summary>
        /// Constructs a new instance of the <see cref="MaxRetryStrategy"/> class.
        /// </summary>
        /// <param name="maxRetries">The maximum amount of time to invoke the retry operation.</param>
        public MaxRetryStrategy(int maxRetries)
        {
            m_MaxRetries = maxRetries;
        }

        /// <summary>
        /// Invoke a retry mechanism on a specific operation.
        /// </summary>
        /// <param name="operation">The operation to invoke each retry.</param>
        /// <param name="predicate">The predicate the verify if the operation succeed.</param>
        /// <typeparam name="T">The type of the result from the operation.</typeparam>
        /// <returns>If succeed, return the result of the operation, otherwise return the default of T</returns>
        public override UniTask<T> TryOperationAsync<T>(Func<UniTask<T>> operation, Func<T, bool> predicate)
        {
            m_CurrentAmountOfRetires++;
            return base.TryOperationAsync(operation, predicate);
        }

        protected override bool ShouldRetry()
        {
            return m_CurrentAmountOfRetires < m_MaxRetries;
        }
    }
}