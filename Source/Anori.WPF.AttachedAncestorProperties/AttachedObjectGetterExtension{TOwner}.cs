namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <seealso cref="Anori.WPF.AttachedAncestorProperties.AttachedBindingGetterExtension{TOwner, System.Object}" />
    public class AttachedObjectGetterExtensionBase<TOwner> : AttachedBindingGetterExtension<TOwner, object>
       where TOwner : AttachedAncestorPropertyObjectBase<TOwner>
    {
    }
}
