using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Static
{
    using System.ComponentModel;

    /// <summary>
    /// Interaction logic for StaticMultiEntryPointsAndTwoTwoWayEndPointsBooleanView.xaml
    /// </summary>
    public partial class StaticMultiEntryPointsAndTwoTwoWayEndPointsBooleanView : UserControl
    {
        public StaticMultiEntryPointsAndTwoTwoWayEndPointsBooleanView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DependencyPropertyDescriptor descriptor =
                DependencyPropertyDescriptor.FromName("Value", this.GetType(), typeof(string));

            // now you can set property value with
            if (descriptor != null)
            {
                object s = descriptor.GetValue(this);
            }

            // also, you can use the dependency property itself

            DependencyProperty x = DependencyProperty.Register(
                "Value",
                typeof(string),
                this.GetType(),
                new PropertyMetadata(default(object)));

            DependencyPropertyDescriptor d = DependencyPropertyDescriptor.FromProperty(x, typeof(string));
        }
    }
}
