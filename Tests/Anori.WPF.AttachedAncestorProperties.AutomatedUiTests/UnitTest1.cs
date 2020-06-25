﻿namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Threading;
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests;

    using NUnit.Framework;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SimpleAttachedTextWindow_Test()
        {
        }


        [Apartment(ApartmentState.STA)]
        public async Task TwoWayAttachedTextBindingWindow_Test()
        {
            var dialog = new TwoWayAttachedTextBindingWindow();
            var viewModel = new SimpleAttachedTextBindingViewModel();
            dialog.DataContext = viewModel;
            var result = dialog.EndPointTextBoxA.Text;
           // Assert.AreEqual("Attached Text", result);
            dialog.Show();
            await Task.Delay(1000);

            result = dialog.EndPointTextBoxA.Text;
            viewModel.Text = "Test Text";
            await Task.Yield();
            await Task.Delay(1000);
            
            result = dialog.EndPointTextBoxA.Text;
            dialog.EntryPointTextBox.Text = "Test Text";
            await Task.Yield();
            await Task.Delay(1000);
            result = dialog.EndPointTextBoxA.Text;

            dialog.Close();
            await Task.Yield();

        }


        public void Test_window()
        {
            var showMonitor = new ManualResetEventSlim(false);
            var showMonitor2 = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);
            TwoWayAttachedTextBindingWindow dialog = null;
            Thread th = new Thread(new ThreadStart(delegate
            {
                dialog = new TwoWayAttachedTextBindingWindow();
                var viewModel = new SimpleAttachedTextBindingViewModel();
                dialog.DataContext = viewModel;

                dialog.Show();
                Task.Delay(1000).Wait();

                var result = dialog.EndPointTextBoxA.Text;
                viewModel.Text = "Test Text";
      

                result = dialog.EndPointTextBoxA.Text;
                dialog.EntryPointTextBox.Text = "Test Text";
                Task.Delay(1000).Wait();
                result = dialog.EndPointTextBoxA.Text;

               // dialog.Close();

                showMonitor.Set();
                closeMonitor.Wait();
            }));
            
              
                //showMonitor2.Set();
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            
            showMonitor.Wait();
            Task.Delay(1000).Wait();

            dialog.Dispatcher.Invoke(() =>
            {
                var r = dialog.EndPointTextBoxA.Text;
            });
            //anything you need to test
            //t.ApartmentState = ApartmentState.STA;
            //t.Start();
            //showMonitor2.Wait();
            closeMonitor.Set();
        }
    }
}
