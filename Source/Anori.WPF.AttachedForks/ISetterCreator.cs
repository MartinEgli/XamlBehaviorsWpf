using System.Windows;

namespace AttachedPropertyTests
{
    public interface ISetterCreator
    {
        object Create(object o);

       
        DependencyProperty Property
        {
            get;
            set;
        }
    }
}