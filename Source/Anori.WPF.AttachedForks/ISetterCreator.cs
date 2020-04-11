using System.Windows;

namespace Anori.WPF.AttachedForks
{
    public interface ISetterCreator
    {
        void Create(DependencyObject dependencyObject);

       
        DependencyProperty Property
        {
            get;
            set;
        }
    }
}
