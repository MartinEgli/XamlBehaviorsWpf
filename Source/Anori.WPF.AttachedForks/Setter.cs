using System.Windows;

namespace AttachedPropertyTests
{
    public class ValueSetter : ISetterCreator
    {
        public object Create(object o)
        {
            return Value;
        }

        public object Value
        {
            get;
            set ; 
        }

        public DependencyProperty Property
        {
            get; 
            set; 
        }
    }
}