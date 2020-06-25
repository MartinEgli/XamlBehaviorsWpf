using JetBrains.Annotations;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Anori.WPF.Testing
{
    using System.Diagnostics;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TestHarness : Window, ITestHarness
    {
        private readonly TestHarnessViewModel viewModel;
        private readonly Dispatcher dispatcher;

        public TestHarness()
        {
            InitializeComponent();
            viewModel=new TestHarnessViewModel();
            this.DataContext = viewModel;
            this.dispatcher = Dispatcher;
            Closed += OnClosed;
        }
        private void OnClosed(object sender, EventArgs e)
        {
            Debug.WriteLine("Harness Closed");
        }

        ~TestHarness()
        {
            Debug.WriteLine("Harness Destroyed");
        }

        public object ContentDataContext
        {
            get
            {
                return dispatcher.Invoke(() => this.viewModel?.Content?.DataContext);
            }
        }

        public void SetContent([NotNull] Func<UserControl> content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            dispatcher.Invoke(()=> viewModel.Content = content());
        }

        public void Invoke(Action action) => dispatcher.Invoke(action);

        public TResult Invoke<TResult>(Func<TResult> func) => dispatcher.Invoke(func);

        public string AppTopLevelWindow => this.AppTopLevelWindow();
    }
}
