using System.Windows;
using Anori.WPF.Behaviors.Extensions;

namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.Extensions.AttachableContent{Anori.WPF.AttachedAncestorProperties.AttachedAncestorPropertyGetter}" />
    public sealed class AttachedForkGetterContent : AttachableContent<AttachedAncestorPropertyGetter>
    {
        /// <summary>
        ///     Creates a new instance of the BehaviorCollection.
        /// </summary>
        /// <returns>The new instance.</returns>
        protected override Freezable CreateInstanceCore() => new AttachedForkGetterContent();
    }
}
