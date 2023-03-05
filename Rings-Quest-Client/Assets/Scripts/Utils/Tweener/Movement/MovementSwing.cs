namespace Happyflow.Utils.Movement 
{
    /// <summary>
    /// Class representing the movement swing parameters.
    /// </summary>
    public class MovementSwing
    {
        /// <summary>
        /// The burst of the movement swing.
        /// </summary>
        public float Burst { get; }
        /// <summary>
        /// The duration of the movement swing.
        /// </summary>
        public float Duration { get;}

        /// <summary>
        /// Initializes a new instance of the <see cref="MovementSwing"/> class.
        /// </summary>
        /// <param name="burst">The burst of the movement swing.</param>
        /// <param name="duration">The duration of the movement swing.</param>
        public MovementSwing(float burst, float duration)
        {
            Burst = burst;
            Duration = duration;
        }
    }
}
