using System.Windows;
using System.Windows.Input;

namespace Anori.WPF.Behaviors.Extensions.GuiTests
{
    using Anori.WPF.Behaviors.Core;

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
