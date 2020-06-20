using Anori.WPF.AttachedAncestorProperties;

namespace Anori.WPF.AttachedAncestorProperties
{
    public abstract class AttachedBindingBooleanGetterExtensionBase<TOwner> :
        AttachedBindingGetterExtension<TOwner, bool>
    where TOwner : AttachedAncestorProperty<TOwner, bool>
    {
    }
}
