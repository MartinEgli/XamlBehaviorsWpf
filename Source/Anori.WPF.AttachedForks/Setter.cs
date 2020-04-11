using System.Windows;

namespace Anori.WPF.AttachedForks
{
    public class ValueSetter : ISetterCreator
    {
        private object value;

        public void Create(DependencyObject o)
        {
            o.SetValue(Property, this.Value);
        }

        public object Value

        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        public DependencyProperty Property
        {
            get; 
            set; 
        }
    }

    public class AttachedBindingStringGetterExtensionSetterCreator : ISetterCreator
    {
        private DependencyProperty property;

        public void Create(DependencyObject o)
        {
            new AttachedForkSetter<AttachedForkString,string>(o as DependencyObject).Create(o as DependencyObject, this.property );
        }

        public DependencyProperty Property
        {
            get
            {
                return this.property;
            }
            set
            {
                this.property = value;
            }
        }
    }
}
