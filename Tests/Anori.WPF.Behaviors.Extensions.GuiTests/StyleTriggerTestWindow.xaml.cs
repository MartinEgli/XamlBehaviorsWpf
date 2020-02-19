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
using System.Windows.Shapes;

namespace Anori.WPF.Behaviors.Extensions.GuiTests
{
    using Anori.WPF.Behaviors.Core;
    using Anori.WPF.Behaviors.Observables.GuiTests;

    /// <summary>
    /// Interaction logic for StyleTriggerTestWindow.xaml
    /// </summary>
    public partial class StyleTriggerTestWindow : Window
    {
        public StyleTriggerTestWindow()
        {
            InitializeComponent();
            ICommand newDataContextCommand = null;
            newDataContextCommand = new ActionCommand(
                () =>
                {
                    this.DataContext = new StyleTriggerTestViewModel(newDataContextCommand);
                });
            DataContext = new StyleTriggerTestViewModel(newDataContextCommand);
        }
    }
}
