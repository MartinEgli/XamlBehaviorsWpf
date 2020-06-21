using System;
using System.Windows.Controls;

namespace Anori.WPF.Testing
{
    public interface ITestHarness
    {
        void Show();

        string AppTopLevelWindow { get; }
        object ContentDataContext { get; }

        void SetContent(Func<UserControl> content);
        void Invoke(Action action);
        TResult Invoke<TResult>(Func<TResult> func);
        void Close();
    }
}
