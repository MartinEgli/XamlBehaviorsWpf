using System;
using System.Threading;
using System.Threading.Tasks;
using Anori.WPF.AttachedAncestorProperties.GuiTest;
using NUnit.Framework;

namespace Anori.WPF.AttachedForks.UnitTest
{
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
            var gui = new StaticEndPointTextBindingWindow();
            var result =gui.TextBox1.Text;
            Assert.AreEqual("Attached Text", result);
            gui.Show();
            result = gui.TextBox1.Text;
            Assert.AreEqual("Attached Text", result);
            gui.Close();
        }


        [Test]
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


        [Test]
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
