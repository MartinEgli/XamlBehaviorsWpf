﻿// -----------------------------------------------------------------------
// <copyright file="BehaviorUtilities.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Xaml.Interactions.UnitTests
{
    using Anori.WPF.Behaviors;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Markup;
    using System.Windows.Shapes;
    using SysWindows = System.Windows;

    internal sealed class UniqueClass : SysWindows.DependencyObject
    {
    }

    internal static class BehaviorTestUtilities
    {
        public const string StringOperandLoremIpsum = "Lorem ipsum";

        public const string StringOperandNuncViverra = "Nunc viverra";

        public const int IntegerOperand4 = 4;

        public const int IntegerOperand5 = 5;

        public const int IntegerOperand6 = 6;

        public const float FloatOperand = 3.1415f;

        public static void TestConstraintOnAssociatedObject<T>(IAttachedObject attachedObject)
            where T : SysWindows.DependencyObject, new()
        {
            UniqueClass nonConstraintObject = new UniqueClass();
            T constraintObject = new T();

            Assert.IsNull(attachedObject.AssociatedObject, "attachedObject.AssociatedObject == null");
            attachedObject.Attach(constraintObject);
            Assert.AreEqual(
                attachedObject.AssociatedObject,
                constraintObject,
                "After attaching satisfying constraint to attachedObject, attachedObject.AssociatedObject should be that constraint");

            attachedObject.Detach();
            Assert.IsNull(
                attachedObject.AssociatedObject,
                "After detaching, attachedObject.AssociatedObject should be null");
            try
            {
                attachedObject.Attach(nonConstraintObject);
                Assert.Fail(
                    "Attaching an object that does not satisfy the constraint should have thrown an InvalidOperationException.");
            } catch (InvalidOperationException)
            {
            }
        }

        public static void TestIAttachedObject<T>(IAttachedObject attachedObject)
            where T : SysWindows.DependencyObject, new()
        {
            T generic = new T();
            Rectangle rectangle = new Rectangle();

            Assert.IsNull(attachedObject.AssociatedObject, "iAttachedObject.AssociatedObject == null");
            attachedObject.Attach(generic);
            Assert.AreEqual(
                attachedObject.AssociatedObject,
                generic,
                "After attaching generic iAttachedObject.AssociatedObject should be generic");
            try
            {
                attachedObject.Attach(generic);
            } catch
            {
                Assert.Fail("Unexpected exception thrown.");
            }

            Assert.AreEqual(attachedObject.AssociatedObject, generic, "iAttachedObject.AssociatedObject == generic");

            try
            {
                attachedObject.Attach(rectangle);
                Assert.Fail("InvalidOperationException should be thrown when attempting to attach a new object.");
            } catch (InvalidOperationException)
            {
            }

            Assert.AreEqual(attachedObject.AssociatedObject, generic, "iAttachedObject.AssociatedObject == generic");

            attachedObject.Detach();
            Assert.IsNull(attachedObject.AssociatedObject, "iAttachedObject.AssociatedObject == null");
            try
            {
                attachedObject.Detach();
            } catch
            {
                Assert.Fail("Unexpected exception thrown.");
            }

            Assert.IsNull(attachedObject.AssociatedObject, "iAttachedObject.AssociatedObject == null");

            attachedObject.Attach(rectangle);
            Assert.AreEqual(
                attachedObject.AssociatedObject,
                rectangle,
                "After attaching rectangle, AttachedObject.AssociatedObject should be rectangle");
            attachedObject.Detach();
        }
    }

    internal static class XamlFragmentParser
    {
        public static bool TryParseXaml<T>(string xamlString, out T returnObject)
        {
            try
            {
                returnObject = (T)XamlReader.Parse(xamlString);
                return true;
            } catch (XamlParseException)
            {
                returnObject = default;
            }

            return false;
        }
    }

    internal class StubWindow : SysWindows.Window, IDisposable
    {
        public StubWindow(object content)
        {
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.AllowsTransparency = true;
            this.ShowInTaskbar = false;
            this.Width = 0;
            this.Height = 0;
            this.Opacity = 0;
            this.Content = content;
            this.ShowActivated = false;
            this.Show();
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Close();
        }

        #endregion IDisposable Members
    }

    internal class DebugOutputListener : IDisposable
    {
        private readonly DebugTraceListener debugTraceListener;

        private readonly TraceListener[] storedListeners;

        private DebugOutputListener()
        {
#if !NETCOREAPP3_0
            this.storedListeners = new TraceListener[Debug.Listeners.Count];
            Debug.Listeners.CopyTo(this.storedListeners, 0);
            Debug.Listeners.Clear();

            this.debugTraceListener = new DebugTraceListener();
            Debug.Listeners.Add(this.debugTraceListener);
#endif
        }

        public List<string> Messages
        {
            get { return this.debugTraceListener.Messages; }
        }

        public static DebugOutputListener Listen()
        {
            return new DebugOutputListener();
        }

        #region IDisposable Members

        public void Dispose()
        {
#if !NETCOREAPP3_0
            Debug.Listeners.Clear();
            Debug.Listeners.AddRange(this.storedListeners);
#endif
        }

        #endregion IDisposable Members

        #region DebugTraceListener

        private class DebugTraceListener : TraceListener
        {
            public DebugTraceListener()
            {
                this.Messages = new List<string>();
            }

            public List<string> Messages
            {
                get;
            } = new List<string>();

            public override void Write(string message)
            {
                this.Messages.Add(message);
            }

            public override void WriteLine(string message)
            {
                this.Messages.Add(message);
            }

            public override void Fail(string message)
            {
                this.Messages.Add(message);
            }
        }

        #endregion DebugTraceListener
    }
}
