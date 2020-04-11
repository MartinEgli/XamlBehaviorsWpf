namespace Anori.WPF.AttachedForks
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Anori.WPF.AttachedForks.AttachedBindingGetterExtension{System.Object, Anori.WPF.AttachedForks.AttachedForkObject}" />
    public class AttachedObjectGetterExtensionBase<TOwner> : AttachedBindingGetterExtension<TOwner, object>
       where TOwner : AttachedForkObjectBase<TOwner>
    {
    }
}
