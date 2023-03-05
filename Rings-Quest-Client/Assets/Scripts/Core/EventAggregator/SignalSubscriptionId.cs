using System;

namespace Happyflow.Core.EventAggregator
{
    /// <summary>
    /// Contains the Id of the signal subscription of the <see cref="EventAggreagtor"/>
    /// </summary>
    public readonly struct SignalSubscriptionId : IEquatable<SignalSubscriptionId>
    {
        private readonly Type m_Type;
        private readonly object m_Token;

        public SignalSubscriptionId(Type type, object token)
        {
            m_Type = type;
            m_Token = token;
        }
        
        /// <summary>
        /// Get the hash code of the <see cref="SignalSubscriptionId"/>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 29 + m_Type.GetHashCode();
                hash = hash * 29 + m_Token.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Compare between <see cref="SignalSubscriptionId"/> instance and another object
        /// </summary>
        /// <param name="that"></param>
        /// <returns>True if the objects are of type <see cref="SignalSubscriptionId"/> and equals, otherwise return False.</returns>
        public override bool Equals(object that)
        {
            if (that is SignalSubscriptionId)
            {
                return Equals((SignalSubscriptionId) that);
            }

            return false;
        }

        /// <summary>
        /// Compare between <see cref="SignalSubscriptionId"/> instance and another <see cref="SignalSubscriptionId"/>
        /// </summary>
        /// <param name="that"></param>
        /// <returns>True if the <see cref="SignalSubscriptionId"/> are equals, otherwise return False.</returns>
        public bool Equals(SignalSubscriptionId that)
        {
            return m_Type == that.m_Type
                   && Equals(m_Token, that.m_Token);
        }

        public static bool operator ==(SignalSubscriptionId left, SignalSubscriptionId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SignalSubscriptionId left, SignalSubscriptionId right)
        {
            return !left.Equals(right);
        }
    }
}