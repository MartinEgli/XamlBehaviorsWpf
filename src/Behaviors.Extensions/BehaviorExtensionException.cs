using System;

namespace Behaviors.Extensions
{
    /// <summary>
    /// BehaviorExtensionException
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class BehaviorExtensionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviorExtensionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BehaviorExtensionException(string message) : base(message)
        {
        }
    }
}
