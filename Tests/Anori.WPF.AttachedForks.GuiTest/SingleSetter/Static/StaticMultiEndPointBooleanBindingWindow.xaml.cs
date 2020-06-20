using System.Windows;

namespace Anori.WPF.AttachedAncestorProperties.GuiTest
{
    using System.ComponentModel;

    /// <summary>
    /// Interaction logic for ControlBindingWindow.xaml
    /// </summary>
    public partial class StaticMultiEndPointBooleanBindingWindow : Window
    {
        public StaticMultiEndPointBooleanBindingWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var descriptor = DependencyPropertyDescriptor.FromName("Value", this.GetType(), typeof(string));

            // now you can set property value with
            if (descriptor != null)
            {
                var s = descriptor.GetValue(this);
            }

            // also, you can use the dependency property itself

var x =            DependencyProperty.Register(
                "Value",
                typeof(string),
                this.GetType(),
                new PropertyMetadata(default(object)));

var d = DependencyPropertyDescriptor.FromProperty(x, typeof(string));

        }
    }
}
