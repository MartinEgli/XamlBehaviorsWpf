// -----------------------------------------------------------------------
// <copyright file="InvokeCommandTreeViewItemClickBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.TriggerActions
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using Anori.WPF.Behaviors;
    using Anori.WPF.Extensions;

    /// <summary>
    ///     Behavior to invoke command on TreeViewItem click
    /// </summary>
    /// <seealso cref="TreeView" />
    public class InvokeCommandTreeViewItemClickBehavior : UiElementClickBehavior<TreeView>
    {
        /// <summary>
        ///     Identifies the <see cref="AlwaysInvokeCommand" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AlwaysInvokeCommandProperty = DependencyProperty.Register(
            nameof(AlwaysInvokeCommand),
            typeof(bool),
            typeof(InvokeCommandTreeViewItemClickBehavior),
            new PropertyMetadata(false));

        /// <summary>
        ///     Identifies the <see cref="CommandParameter" /> dependency property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter),
            typeof(object),
            typeof(InvokeCommandTreeViewItemClickBehavior),
            new PropertyMetadata(
                null,
                (s, e) =>
                    {
                        var sender = s as InvokeCommandTreeViewItemClickBehavior;

                        if (sender?.AssociatedObject == null)
                        {
                            return;
                        }

                        sender.EnableDisableElement();
                    }));

        /// <summary>
        ///     The command property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(object),
            typeof(InvokeCommandTreeViewItemClickBehavior),
            new PropertyMetadata(
                default(object),
                (s, e) => OnCommandChanged(s as InvokeCommandTreeViewItemClickBehavior, e)));

        /// <summary>
        ///     Identifies the <see cref="EventArgsConverterParameter" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EventArgsConverterParameterProperty = DependencyProperty.Register(
            nameof(EventArgsConverterParameter),
            typeof(object),
            typeof(InvokeCommandTreeViewItemClickBehavior),
            new PropertyMetadata(null));

        /// <summary>
        ///     Identifies the <see cref="MustToggleIsEnabled" /> dependency property
        /// </summary>
        public static readonly DependencyProperty MustToggleIsEnabledProperty = DependencyProperty.Register(
            nameof(MustToggleIsEnabled),
            typeof(bool),
            typeof(InvokeCommandTreeViewItemClickBehavior),
            new PropertyMetadata(
                false,
                (s, e) =>
                    {
                        var sender = s as InvokeCommandTreeViewItemClickBehavior;

                        if (sender?.AssociatedObject == null)
                        {
                            return;
                        }

                        sender.EnableDisableElement();
                    }));

        /// <summary>
        ///     The command parameter value
        /// </summary>
        private object commandParameterValue;

        /// <summary>
        ///     The must toggle value
        /// </summary>
        private bool? mustToggleValue;

        /// <summary>
        ///     Gets or sets a value indicating whether [always invoke command].
        ///     if the attached control is disabled. This is a dependency property.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [always invoke command]; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysInvokeCommand
        {
            get => (bool)this.GetValue(AlwaysInvokeCommandProperty);

            set => this.SetValue(AlwaysInvokeCommandProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command.
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        public object Command
        {
            get => this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        /// <summary>
        ///     Gets or sets an object that will be passed to the <see cref="Command" />
        ///     attached to this trigger. This is a DependencyProperty.
        /// </summary>
        /// <value>
        ///     The command parameter.
        /// </value>
        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);

            set => this.SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        ///     Gets or sets the type of the command parameter.
        /// </summary>
        /// <value>
        ///     The type of the command parameter.
        /// </value>
        public Type CommandParameterType { get; set; }

        /// <summary>
        ///     Gets or sets an object that will be passed to the <see cref="Command" />
        ///     attached to this trigger. This property is here for compatibility
        ///     with the Silverlight version. This is NOT a DependencyProperty.
        ///     For databinding, use the <see cref="CommandParameter" /> property.
        /// </summary>
        /// <value>
        ///     The command parameter value.
        /// </value>
        public object CommandParameterValue
        {
            get => this.commandParameterValue ?? this.CommandParameter;

            set
            {
                this.commandParameterValue = value;
                this.EnableDisableElement();
            }
        }

        /// <summary>
        ///     Gets or sets a converter used to convert the EventArgs when using
        ///     <see cref="PassEventArgsToCommand" />. If PassEventArgsToCommand is false,
        ///     this property is never used.
        /// </summary>
        /// <value>
        ///     The event arguments converter.
        /// </value>
        public IEventArgsConverter EventArgsConverter { get; set; }

        /// <summary>
        ///     Gets or sets a parameters for the converter used to convert the EventArgs when using
        ///     <see cref="PassEventArgsToCommand" />. If PassEventArgsToCommand is false,
        ///     this property is never used. This is a dependency property.
        /// </summary>
        /// <value>
        ///     The event arguments converter parameter.
        /// </value>
        public object EventArgsConverterParameter
        {
            get => this.GetValue(EventArgsConverterParameterProperty);

            set => this.SetValue(EventArgsConverterParameterProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the attached element must be
        ///     disabled when the <see cref="Command" /> property's CanExecuteChanged
        ///     event fires. If this property is true, and the command's CanExecute
        ///     method returns false, the element will be disabled. If this property
        ///     is false, the element will not be disabled when the command's
        ///     CanExecute method changes. This is a DependencyProperty.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [must toggle is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool MustToggleIsEnabled
        {
            get => (bool)this.GetValue(MustToggleIsEnabledProperty);

            set => this.SetValue(MustToggleIsEnabledProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the attached element must be
        ///     disabled when the <see cref="Command" /> property's CanExecuteChanged
        ///     event fires. If this property is true, and the command's CanExecute
        ///     method returns false, the element will be disabled. This property is here for
        ///     compatibility with the Silverlight version. This is NOT a DependencyProperty.
        ///     For databinding, use the <see cref="MustToggleIsEnabled" /> property.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [must toggle is enabled value]; otherwise, <c>false</c>.
        /// </value>
        public bool MustToggleIsEnabledValue
        {
            get => this.mustToggleValue ?? this.MustToggleIsEnabled;

            set
            {
                this.mustToggleValue = value;
                this.EnableDisableElement();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [pass event arguments to command].
        ///     Gets or sets a value specifies whether the EventArgs of the event that triggered this
        ///     action should be passed to the bound RelayCommand. If this is true,
        ///     the command should accept arguments of the corresponding
        ///     type (for example RelayCommand&lt;MouseButtonEventArgs&gt;).
        /// </summary>
        /// <value>
        ///     <c>true</c> if [pass event arguments to command]; otherwise, <c>false</c>.
        /// </value>
        public bool PassEventArgsToCommand { get; set; }

        /// <summary>
        ///     Executes the trigger.
        ///     <para>
        ///         To access the EventArgs of the fired event, use a RelayCommand&lt;EventArgs&gt;
        ///         and leave the CommandParameter and CommandParameterValue empty!
        ///     </para>
        /// </summary>
        /// <param name="parameter">The EventArgs of the fired event.</param>
        protected void Invoke(object parameter)
        {
            if (this.AssociatedElementIsDisabled() && !this.AlwaysInvokeCommand)
            {
                return;
            }

            var command = this.GetCommand();
            var commandParameter = this.CommandParameterValue;

            if (commandParameter == null && this.PassEventArgsToCommand)
            {
                commandParameter = this.EventArgsConverter == null
                                       ? parameter
                                       : this.EventArgsConverter.Convert(
                                           parameter as EventArgs,
                                           this.CommandParameterType,
                                           this.EventArgsConverterParameter,
                                           this.AssociatedObject);
            }

            if (command != null && command.CanExecute(commandParameter))
            {
                command.Execute(commandParameter);
            }
        }

        /// <summary>
        ///     Items the clicked.
        /// </summary>
        protected override void OnClicked()
        {
            this.Invoke(this.GetAssociatedObject().DataContext);
        }

        /// <summary>
        ///     Called when [command changed].
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnCommandChanged(
            InvokeCommandTreeViewItemClickBehavior element,
            DependencyPropertyChangedEventArgs e)
        {
            if (element == null)
            {
                return;
            }

            if (e.OldValue != null)
            {
                ((ICommand)e.OldValue).CanExecuteChanged -= element.OnCommandCanExecuteChanged;
                element.CommandParameterType = null;
            }

            var command = (ICommand)e.NewValue;
            element.CommandParameterType = CommandExtensions.GetCommandParameterType(command);

            if (command != null)
            {
                command.CanExecuteChanged += element.OnCommandCanExecuteChanged;
            }

            element.EnableDisableElement();
        }

        /// <summary>
        ///     Associated the element is disabled.
        /// </summary>
        /// <returns>Is disabled</returns>
        private bool AssociatedElementIsDisabled()
        {
            var element = this.GetAssociatedObject();

            if (this.AssociatedObject == null)
            {
                return true;
            }

            if (element != null && !element.IsEnabled)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Enables the disable element.
        /// </summary>
        private void EnableDisableElement()
        {
            var element = this.GetAssociatedObject();

            if (element == null)
            {
                return;
            }

            var command = this.GetCommand();

            if (this.MustToggleIsEnabledValue && command != null)
            {
                element.IsEnabled = command.CanExecute(this.CommandParameterValue);
            }
        }

        /// <summary>
        ///     This method is here for compatibility
        ///     with the Silverlight 3 version.
        /// </summary>
        /// <returns>
        ///     The command that must be executed when
        ///     this trigger is invoked.
        /// </returns>
        private ICommand GetCommand()
        {
            return (ICommand)this.Command;
        }

        /// <summary>
        ///     Called when [command can execute changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            this.EnableDisableElement();
        }
    }
}