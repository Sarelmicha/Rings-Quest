using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Happyflow.Core.RetryStrategy
{
    /// <summary>
    /// A base class for all retry strategies.
    /// This class is a generic mechanism for a retry operation.
    /// </summary>
    public abstract class RetryStrategyBase
    {
        private readonly CancellationTokenSource m_CancellationTokenSource;
        private CancellationToken Token => m_CancellationTokenSource.Token;

        protected RetryStrategyBase()
        {
            m_CancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Invoke a retry mechanism on a specific operation.
        /// </summary>
        /// <param name="operation">The operation to invoke each retry.</param>
        /// <param name="predicate">The predicate the verify if the operation succeed.</param>
        /// <typeparam name="T">The type of the result from the operation.</typeparam>
        /// <returns>If succeed, return the result of the operation, otherwise return the default of T</returns>
        public virtual async UniTask<T> TryOperationAsync<T>(Func<UniTask<T>> operation, Func<T, bool> predicate)
        {
            var result = await operation.Invoke();

            if (predicate.Invoke(result))
            {
                return result;
            }
            
            if(!Token.IsCancellationRequested && ShouldRetry())
            {
                return await TryOperationAsync(operation, predicate);
            }

            return default;
        }

        /// <summary>
        /// Stop the retry mechanism.
        /// </summary>
        public void Stop()
        {
            m_CancellationTokenSource.Cancel();
        }

        protected abstract bool ShouldRetry();
    }
}