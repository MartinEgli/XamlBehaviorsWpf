// -----------------------------------------------------------------------
// <copyright file="DataContextChangedListener.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.WeakEventManagers
{
    using System;
    using System.Windows;

    public class DataContextChangedListener : IWeakEventListener
    {
        public DataContextChangedListener(EventHandler<DataContextChangedEventArgs> handler)
        {
            this.DataContextChanged = handler;
        }

        public DataContextChangedListener()
        {
        }

        private event EventHandler<DataContextChangedEventArgs> DataContextChanged;

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            this.OnDataContextChanged((DataContextChangedEventArgs)e);
            return true;
        }

        protected virtual void OnDataContextChanged(DataContextChangedEventArgs e)
        {
            this.DataContextChanged?.Invoke(this, e);
        }
    }
}
