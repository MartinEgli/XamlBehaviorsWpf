using System.Windows;

namespace Anori.WPF.AttachedAncestorProperties
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
