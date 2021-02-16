﻿// -----------------------------------------------------------------------
// <copyright file="UITestSessionBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Testing
{
    using System;
    using System.Threading;
    using System.Timers;
    using System.Windows.Controls;
    using System.Windows.Threading;

    public abstract class UiTestSessionBase
    {
        /// <summary>
        ///     The windows application driver URL
        /// </summary>
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";

        /// <summary>
        ///     The session
        /// </summary>
        protected static TestSession Session;

        /// <summary>
        ///     The factory
        /// </summary>
        private static readonly TestHarnessFactory factory = new TestHarnessFactory(() => new TestHarness());

        /// <summary>
        ///     The window thread
        /// </summary>
        private static Thread windowThread;

        private static ITestHarness harness;

        /// <summary>
        ///     Invokes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void Invoke(Action action) => Session.Harness.Invoke(action);

        /// <summary>
        ///     Invokes the specified function.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        public static TResult Invoke<TResult>(Func<TResult> func) => Session.Harness.Invoke(func);

        /// <summary>
        ///     Sets the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public static void SetContent(Func<UserControl> content) => Session.Harness.SetContent(content);

        /// <summary>
        ///     Setups the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Setup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext context)
        {
            if (Session != null)
            {
                return;
            }

            Session = CreateSession();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(Session);
        }

        public static void Setup()
        {
            if (Session != null)
            {
                StopWatchDog();
                return;
            }

            SetupTimer();

            Session = CreateSession();
            NUnit.Framework.Assert.IsNotNull(Session);
        }

        private static System.Timers.Timer Timer;

        private static void StartWatchDog()
        {
            Timer.Start();
        }

        private static void Reset()
        {
            Timer.Stop();
            Timer.Start();
        }

        private static void StopWatchDog()
        {
            Timer.Stop();
        }

        public static void SetupTimer()
        {
            Timer = new System.Timers.Timer();
            Timer.Elapsed += OnElapsed;
            Timer.Interval = (1000 * Timeout);
        }
        private static void OnElapsed(object sender, ElapsedEventArgs e)
        {
            Timer.Stop();
            InternalTearDown();
        }
        public static int Timeout { get; set; } = 5;

        /// <summary>
        ///     Tears down.
        /// </summary>
        public static void TearDown()
        {
            StartWatchDog();
            //if (Session != null)
            //{
            //    Session.Quit();
            //    Session = null;
            //}

            //if (harness != null)
            //{
            //    harness.Invoke(() =>
            //    {
            //        harness.Close();
            //        harness = null;
            //    });
            //}

            //if (windowThread != null)
            //{
            //    windowThread.Abort();
            //}


        }


        private static void InternalTearDown()
        {
            if (Session != null)
            {
                Session.Quit();
                Session = null;
            }

            if (harness != null)
            {
                harness.Invoke(() =>
                {
                    harness.Close();
                    harness = null;
                });
            }

            if (windowThread != null)
            {
                windowThread.Abort();
            }


        }

        /// <summary>
        ///     Creates the session.
        /// </summary>
        /// <returns></returns>
        private static TestSession CreateSession()
        {
            harness = StartHarness();
            Session = harness.CreateSession(WindowsApplicationDriverUrl);

            if (Session != null)
            {
                Session.WindowsDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
            }

            return Session;
        }

        /// <summary>
        ///     Starts the harness.
        /// </summary>
        /// <returns></returns>
        private static ITestHarness StartHarness()
        {
            ITestHarness testHarness = null;
            using (var manualResetEvent = new ManualResetEvent(false))
            {
                var mre = manualResetEvent;
                windowThread = new Thread(
                    () =>
                        {
                            testHarness = factory.CreateHarness();
                            StartThreadTestHarness(testHarness, mre);
                        });

                windowThread.SetApartmentState(ApartmentState.STA);
                windowThread.IsBackground = true;
                windowThread.Start();

                manualResetEvent.WaitOne();
                manualResetEvent.Close();
            }

            return testHarness;
        }

        /// <summary>
        ///     Starts the thread test harness.
        /// </summary>
        /// <param name="harness">The harness.</param>
        /// <param name="eventWaitHandle">The eventWaitHandle.</param>
        private static void StartThreadTestHarness(ITestHarness harness, EventWaitHandle eventWaitHandle)
        {
            harness.Show();
            eventWaitHandle.Set();
            Dispatcher.Run();
        }
    }
}