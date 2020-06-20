namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Anori.WPF.AttachedAncestorProperties.AttachedBindingGetterExtension{System.Object, Anori.WPF.AttachedAncestorProperties.AttachedForkObject}" />
    public class AttachedObjectGetterExtensionBase<TOwner> : AttachedBindingGetterExtension<TOwner, object>
       where TOwner : AttachedAncestorPropertyObjectBase<TOwner>
    {
    }
}
