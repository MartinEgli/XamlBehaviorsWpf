// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WPF.AttachedAncestorProperties.ManualUiTests;
using System.Windows;

namespace AttachedPropertyTests
{
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter;
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.Element;
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.ItemsControl;
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.Static;
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter;
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Static;

    using StaticChangeEntryPointsTreeAndBindableEndPointWindow2 = Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.Static.StaticChangeEntryPointsTreeAndBindableEndPointWindow2;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SwitchContentWindow();
            dialog.ShowDialog();
        }

        private void OneToMultiButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OneToMultibleWindow();
            dialog.ShowDialog();
        }

        private void OneToMultiCachedButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OneToMultibleCachedWindow();
            dialog.ShowDialog();
        }

        private void OneToMultiStyleButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OneToMultibleStyleWindow();
            dialog.ShowDialog();
        }

        private void SimpleBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new BoundEntryPointAndTwoWayEndPointTextMVVMWindow { DataContext = new SimpleAttachedTextBindingViewModel()};
            dialog.ShowDialog();
        }

        private void SingleStaticUpdateableText_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticEntryPointAndOneWayEndPointTextWindow();
            dialog.ShowDialog();
        }

        private void TwoWayTextBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TwoWayAttachedTextBindingWindow();
            dialog.DataContext = new SimpleAttachedTextBindingViewModel();

            dialog.ShowDialog();
        }
        private void OneWayTextBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OneWayAttachedTextBindingWindow();
            dialog.ShowDialog();
        }

        private void ControlBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ControlBindingWindow();
            dialog.ShowDialog();
        }

        private void ChangeHost_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ChangeAttachedAncestorPropertyInTreeWindow();
            dialog.ShowDialog();
        }

        private void ChangeHostAndSetter_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ChangeAttachedAncestorPropertyAndSetterBindingWindow();
            dialog.ShowDialog();
        }

        private void TreeChangeHost_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticChangeEntryPointsTreeAndOneWayEndPointtWindow();
            dialog.ShowDialog();
        }

        private void ItemsControlBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ItemsControlBindingWindow();
            dialog.ShowDialog();
        }

        private void MuliAttachedBoolControlBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MultiAttachedBoolControlBindingWindow();
            dialog.ShowDialog();
        }

        private void MuliAttachedBoolControlWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticMultiEntryPointsAndOneWayEndPointsBoolControlWindow();
            dialog.ShowDialog();
        }

        private void DynamicMultiEntryMultiEndPointBooleanBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DynamicMultiEntryMultiEndPointBooleanBindingWindow();
            dialog.ShowDialog();
        }

        private void StaticMultiEndPointBooleanBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticMultiEndPointBooleanBindingWindow();
            dialog.ShowDialog();
        }

        private void SimpleGetterBinding_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Anori.WPF.AttachedAncestorProperties.ManualUiTests.SimpleGetterBindingWindow();
            dialog.ShowDialog();
        }

        private void MultiStaticBoundText_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticEntryPointAndTwoWayEndPointTextWindow2();
            dialog.ShowDialog();
        }

        private void SingleStaticBoundText_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticEntryPointAndTwoWayEndPointTextWindow();
            dialog.ShowDialog();
        }

        private void MultiStaticUpdateableText_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticEntryPointAndOneWayEndPointTextWindow2();
            dialog.ShowDialog();
        }

        private void TwoWayTextBindingWindow2_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new BoundEntryPointAndTwoWayEndPointTextControlWindow2();
            dialog.ShowDialog();
        }

        private void TreeChangeHost2_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticChangeEntryPointsTreeAndOneWayEndPointtWindow2();
            dialog.ShowDialog();

        }

        private void TreeChangeHost_Bindable2_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticChangeEntryPointsTreeAndBindableEndPointWindow2();
            dialog.ShowDialog();

        }

        private void Switch2_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticEntryPointAndOneWayEndPointSwitchContentWindow();
            dialog.ShowDialog();
        }


        private void Switch3_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticEntryPointAndTwoWayEndPointSwitchContentWindow();
            dialog.ShowDialog();
        }

        private void MultiStaticEntryEndItemsBindingMvvm_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticEntryPointAndEndPointItemsBindingMvvmWindow();
            dialog.ShowDialog();
        }
    }
}
