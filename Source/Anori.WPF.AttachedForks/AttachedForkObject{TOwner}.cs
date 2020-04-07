namespace Anori.WPF.AttachedForks
{
    public abstract class AttachedForkObject<TOwner> : AttachedFork<object, TOwner>
        where TOwner : AttachedForkObject<TOwner>

    {
    }
}
