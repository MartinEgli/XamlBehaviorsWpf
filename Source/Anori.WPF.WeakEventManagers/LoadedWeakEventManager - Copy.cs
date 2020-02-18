using System;
using System.Windows;

namespace Anori.WPF.WeakEventManagers
{
    #region

    #endregion

    public class DataContextChangedObservable
    {
        private FrameworkElement frameworkElement;

        public DataContextChangedObservable(FrameworkElement frameworkElement)
        {
            this.frameworkElement = frameworkElement;
        }

        public event EventHandler<DataContextChangedObservableArgs> DataContextChanged;
    }

    public class DataContextChangedObservableArgs : EventArgs
    {
        public DataContextChangedObservableArgs(DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            this.DependencyPropertyChangedEventArgs = dependencyPropertyChangedEventArgs;
        }

        public DependencyPropertyChangedEventArgs DependencyPropertyChangedEventArgs { get; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Loaded WeakEventManager Class
    /// </summary>
    /// <seealso cref="T:System.Windows.WeakEventManager" />
    public static class DataContextChangedWEventManager
    {
        public static IDisposable Subscribe(FrameworkElement frameworkElement,
            EventHandler<DependencyPropertyChangedEventArgs> handler)
        {
            EventHandler<DataContextChangedObservableArgs> Handler = (sender, args) =>
                handler.Invoke(sender, args.DependencyPropertyChangedEventArgs);

            DataContextChangedObservable observable = new DataContextChangedObservable(frameworkElement);
            WeakReference<DataContextChangedObservable> weakObservable =
                new WeakReference<DataContextChangedObservable>(observable);
            WeakReference<EventHandler<DataContextChangedObservableArgs>> weakHandler =
                new WeakReference<EventHandler<DataContextChangedObservableArgs>>(Handler);

            WeakEventManager<DataContextChangedObservable, DataContextChangedObservableArgs>.AddHandler(observable,
                nameof(DataContextChangedObservable.DataContextChanged), Handler);

            return new Disposable(() =>
            {
                if (!weakObservable.TryGetTarget(out DataContextChangedObservable o))
                {
                    return;
                }

                if (!weakHandler.TryGetTarget(out EventHandler<DataContextChangedObservableArgs> h))
                {
                    return;
                }

                WeakEventManager<DataContextChangedObservable, DataContextChangedObservableArgs>.RemoveHandler(
                    o,
                    nameof(DataContextChangedObservable.DataContextChanged), h);
            });
        }
    }

    public class Disposable : IDisposable
    {
        private readonly Action action;

        public Disposable(Action action)
        {
            this.action = action;
        }

        public void Dispose()
        {
            this.action?.Invoke();
        }
    }
}
