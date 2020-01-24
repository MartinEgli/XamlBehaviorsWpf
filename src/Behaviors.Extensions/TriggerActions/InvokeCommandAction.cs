// -----------------------------------------------------------------------
// <copyright file="InvokeCommandAction.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.TriggerActions
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Bfa.Common.WPF.Extensions;

    using Microsoft.Xaml.Behaviors;

    /// <summary>
    ///     This <see cref="T:System.Windows.Interactivity.TriggerAction`1" /> can be
    ///     used to bind any event on any FrameworkElement to an <see cref="ICommand" />.
    ///     Typically, this element is used in XAML to connect the attached element
    ///     to a command located in a ViewModel. This trigger can only be attached
    ///     to a FrameworkElement or a class deriving from FrameworkElement.
    ///     <para>
    ///         To access the EventArgs of the fired event, use a RelayCommand&lt;EventArgs&gt;
    ///         and leave the CommandParameter and CommandParameterValue empty!
    ///     </para>
    /// </summary>
    public class InvokeCommandAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        ///     Identifies the <see cref="AlwaysInvokeCommand" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AlwaysInvokeCommandProperty = DependencyProperty.Register(
            nameof(AlwaysInvokeCommand),
            typeof(bool),
            typeof(InvokeCommandAction),
            new PropertyMetadata(false));

        /// <summary>
        ///     Identifies the <see cref="CommandParameter" /> dependency property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter),
            typeof(object),
            typeof(InvokeCommandAction),
            new PropertyMetadata(null, OnCommandParameterPropertyChangedCallback));

        /// <summary>
        ///     Identifies the <see cref="Command" /> dependency property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(InvokeCommandAction),
            new PropertyMetadata(null, (s, e) => OnCommandChanged(s as InvokeCommandAction, e)));

        /// <summary>
        ///     Identifies the <see cref="EventArgsConverterParameter" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EventArgsConverterParameterProperty = DependencyProperty.Register(
            nameof(EventArgsConverterParameter),
            typeof(object),
            typeof(InvokeCommandAction),
            new PropertyMetadata(null));

        /// <summary>
        ///     The command parameter value
        /// </summary>
        private object commandParameterValue;

        /// <summary>
        ///     Gets or sets a value indicating whether [always invoke command].
        ///     Gets or sets a value indicating if the command should be invoked even
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
        ///     Gets or sets the ICommand that this trigger is bound to. This
        ///     is a DependencyProperty.
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
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
        ///     Gets or sets a value indicating whether [pass event arguments to command].
        ///     Specifies whether the EventArgs of the event that triggered this
        ///     action should be passed to the bound RelayCommand. If this is true,
        ///     the command should accept arguments of the corresponding
        ///     type (for example RelayCommand&lt;MouseButtonEventArgs&gt;).
        /// </summary>
        /// <value>
        ///     <c>true</c> if [pass event arguments to command]; otherwise, <c>false</c>.
        /// </value>
        public bool PassEventArgsToCommand { get; set; }

        /// <summary>
        ///     Provides a simple way to invoke this trigger programatically
        ///     without any EventArgs.
        /// </summary>
        public void Invoke()
        {
            this.Invoke(null);
        }

        /// <summary>
        ///     Called when [command can execute changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            this.EnableDisableElement();
        }

        /// <summary>
        ///     Enables the disable element.
        /// </summary>
        protected void EnableDisableElement()
        {
            var element = this.GetAssociatedObject();

            if (element == null)
            {
                return;
            }

            var command = this.GetCommand();

            if (command != null)
            {
                element.IsEnabled = command.CanExecute(this.CommandParameterValue);
            }
        }

        /// <summary>
        ///     Executes the trigger.
        ///     <para>
        ///         To access the EventArgs of the fired event, use a RelayCommand&lt;EventArgs&gt;
        ///         and leave the CommandParameter and CommandParameterValue empty!
        ///     </para>
        /// </summary>
        /// <param name="parameter">The EventArgs of the fired event.</param>
        protected override void Invoke(object parameter)
        {
            if (this.AssociatedElementIsDisabled() && !this.AlwaysInvokeCommand)
            {
                return;
            }

            var command = this.GetCommand();
            var commandParameter = this.CommandParameterValue;

            if (commandParameter == null && this.PassEventArgsToCommand && parameter is EventArgs args)
            {
                commandParameter = this.EventArgsConverter == null
                                       ? parameter
                                       : this.EventArgsConverter.Convert(
                                           args,
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
        ///     Called when this trigger is attached to a FrameworkElement.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.EnableDisableElement();
        }

        /// <summary>
        ///     Called when [command changed].
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnCommandChanged(InvokeCommandAction element, DependencyPropertyChangedEventArgs e)
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
        ///     Called when [command parameter property changed callback].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnCommandParameterPropertyChangedCallback(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var sender = d as InvokeCommandAction;

            if (sender?.AssociatedObject == null)
            {
                return;
            }

            sender.EnableDisableElement();
        }

        /// <summary>
        ///     Associated the element is disabled.
        /// </summary>
        /// <returns>The result.</returns>
        private bool AssociatedElementIsDisabled()
        {
            var element = this.GetAssociatedObject();

            return this.AssociatedObject == null || ((element != null) && (!element.IsEnabled));
        }

        /// <summary>
        ///     This method is here for compatibility
        ///     with the Silverlight version.
        /// </summary>
        /// <returns>
        ///     The FrameworkElement to which this trigger
        ///     is attached.
        /// </returns>
        private FrameworkElement GetAssociatedObject()
        {
            return this.AssociatedObject as FrameworkElement;
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
            return this.Command;
        }
    }
}