// -----------------------------------------------------------------------
// <copyright file="RoutedEventToCommandBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Bfa.Common.WPF.Extensions;
    using Bfa.Common.WPF.Triggers;

    /// <summary>
    ///     Blend Behavior to catch a bubbling RoutedEvents and raise a ICommand
    /// </summary>
    /// <seealso cref="T:System.Windows.Interactivity.Behavior{FrameworkElement}" />
    public class RoutedEventToCommandBehavior : Microsoft.Xaml.Behaviors.Behavior<FrameworkElement>
    {
        /// <summary>
        ///     The command parameter property
        /// </summary>
        public static readonly DependencyProperty CommandParameterPatternProperty = DependencyProperty.Register(
            nameof(CommandParameterPattern),
            typeof(ParameterPattern),
            typeof(RoutedEventToCommandBehavior),
            new PropertyMetadata(ParameterPattern.None));

        /// <summary>
        ///     The command parameter property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter),
            typeof(object),
            typeof(RoutedEventToCommandBehavior),
            new PropertyMetadata(null));

        /// <summary>
        ///     The command property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(RoutedEventToCommandBehavior),
            new PropertyMetadata(null, (s, e) => OnCommandChanged(s as RoutedEventToCommandBehavior, e)));

        /// <summary>
        ///     The passing in property
        /// </summary>
        public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
            nameof(Property),
            typeof(string),
            typeof(RoutedEventToCommandBehavior),
            new PropertyMetadata(null));

        /// <summary>
        ///     The routed event arguments converter parameter property
        /// </summary>
        public static readonly DependencyProperty RoutedEventArgsConverterParameterProperty =
            DependencyProperty.Register(
                nameof(RoutedEventArgsConverterParameter),
                typeof(IRoutedEventArgsConverter),
                typeof(RoutedEventToCommandBehavior),
                new PropertyMetadata(null));

        /// <summary>
        ///     The command parameter converter property
        /// </summary>
        public static readonly DependencyProperty RoutedEventArgsConverterProperty = DependencyProperty.Register(
            nameof(RoutedEventArgsConverter),
            typeof(IRoutedEventArgsConverter),
            typeof(RoutedEventToCommandBehavior),
            new PropertyMetadata(default(IRoutedEventArgsConverter)));

        /// <summary>
        ///     The routed event property
        /// </summary>
        public static readonly DependencyProperty RoutedEventProperty = DependencyProperty.Register(
            nameof(RoutedEvent),
            typeof(RoutedEvent),
            typeof(RoutedEventToCommandBehavior),
            new PropertyMetadata(null));

        /// <summary>
        ///     The routed event handler
        /// </summary>
        private readonly RoutedEventHandler routedEventHandler;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoutedEventToCommandBehavior" /> class.
        /// </summary>
        public RoutedEventToCommandBehavior()
        {
            this.routedEventHandler = (s, e) =>
                {
                    var command = this.Command;
                    if (command == null)
                    {
                        return;
                    }

                    var args = RoutedEventHelpers.SolveEventArguments(
                        this.CommandParameter,
                        this.CommandParameterPattern,
                        this.PayloadType,
                        e,
                        this.AssociatedObject,
                        this.Property,
                        this.RoutedEventArgsConverter,
                        this.RoutedEventArgsConverterParameter);

                    if (command.CanExecute(args))
                    {
                        command.Execute(args);
                    }
                };
        }

        /// <summary>
        ///     Gets or sets the command.
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
        ///     Gets or sets the command parameter.
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
        ///     Gets or sets the command parameter.
        /// </summary>
        /// <value>
        ///     The command parameter.
        /// </value>
        public ParameterPattern CommandParameterPattern
        {
            get => (ParameterPattern)this.GetValue(CommandParameterPatternProperty);
            set => this.SetValue(CommandParameterPatternProperty, value);
        }

        /// <summary>
        ///     Gets or sets the passing in.
        /// </summary>
        /// <value>
        ///     The passing in.
        /// </value>
        public string Property
        {
            get => (string)this.GetValue(PropertyProperty);
            set => this.SetValue(PropertyProperty, value);
        }

        /// <summary>
        ///     Gets or sets the routed event.
        /// </summary>
        /// <value>
        ///     The routed event.
        /// </value>
        public RoutedEvent RoutedEvent
        {
            get => (RoutedEvent)this.GetValue(RoutedEventProperty);
            set => this.SetValue(RoutedEventProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command parameter converter.
        /// </summary>
        /// <value>
        ///     The command parameter converter.
        /// </value>
        public IRoutedEventArgsConverter RoutedEventArgsConverter
        {
            get => (IRoutedEventArgsConverter)this.GetValue(RoutedEventArgsConverterProperty);
            set => this.SetValue(RoutedEventArgsConverterProperty, value);
        }

        /// <summary>
        ///     Gets or sets the routed event arguments converter parameter.
        /// </summary>
        /// <value>
        ///     The routed event arguments converter parameter.
        /// </value>
        public object RoutedEventArgsConverterParameter
        {
            get => this.GetValue(RoutedEventArgsConverterParameterProperty);
            set => this.SetValue(RoutedEventArgsConverterParameterProperty, value);
        }

        /// <summary>
        ///     Gets or sets the type of the payload.
        /// </summary>
        /// <value>
        ///     The type of the payload.
        /// </value>
        private Type PayloadType { get; set; }

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
                element.IsEnabled = command.CanExecute(this.CommandParameter);
            }
        }

        /// <summary>
        ///     Called when [attached].
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject?.AddHandler(this.RoutedEvent, this.routedEventHandler);
        }

        /// <summary>
        ///     Called when [detaching].
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject?.RemoveHandler(this.RoutedEvent, this.routedEventHandler);
        }

        /// <summary>
        ///     Called when [command changed].
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnCommandChanged(RoutedEventToCommandBehavior self, DependencyPropertyChangedEventArgs e)
        {
            if (self == null)
            {
                return;
            }

            if (e.OldValue != null)
            {
                ((ICommand)e.OldValue).CanExecuteChanged -= self.OnCommandCanExecuteChanged;
                self.PayloadType = null;
            }

            var command = (ICommand)e.NewValue;
            self.PayloadType = CommandExtensions.GetCommandParameterType(command);

            if (command != null)
            {
                command.CanExecuteChanged += self.OnCommandCanExecuteChanged;
            }

            self.EnableDisableElement();
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
            return this.AssociatedObject;
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