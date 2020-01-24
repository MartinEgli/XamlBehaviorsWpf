// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyChangedEventToCommandBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Bfa.Common.WPF.Extensions;
    using Bfa.Common.WPF.TriggerActions;
    using Bfa.Common.WPF.Triggers;

    /// <summary>
    ///     Blend Behavior to catch a bubbling RoutedEvents and raise a ICommand
    /// </summary>
    /// <seealso cref="T:System.Windows.Interactivity.Behavior{FrameworkElement}" />
    public class DependencyPropertyChangedEventToCommandBehavior : DependencyPropertyChangedEventBehaviorBase
    {
        /// <summary>
        ///     The command parameter property
        /// </summary>
        public static readonly DependencyProperty CommandParameterPatternProperty = DependencyProperty.Register(
            nameof(CommandParameterPattern),
            typeof(ParameterPattern),
            typeof(DependencyPropertyChangedEventToCommandBehavior),
            new PropertyMetadata(ParameterPattern.None));

        /// <summary>
        ///     The command parameter property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter),
            typeof(object),
            typeof(DependencyPropertyChangedEventToCommandBehavior),
            new PropertyMetadata(null));

        /// <summary>
        ///     The command property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(DependencyPropertyChangedEventToCommandBehavior),
            new PropertyMetadata(
                null,
                (s, e) => OnCommandChanged(s as DependencyPropertyChangedEventToCommandBehavior, e)));

        /// <summary>
        ///     The routed event arguments converter parameter property
        /// </summary>
        public static readonly DependencyProperty DependencyPropertyChangedEventArgsConverterParameterProperty =
            DependencyProperty.Register(
                nameof(DependencyPropertyChangedEventArgsConverterParameter),
                typeof(IDependencyPropertyChangedEventArgsConverter),
                typeof(DependencyPropertyChangedEventToCommandBehavior),
                new PropertyMetadata(null));

        /// <summary>
        ///     The command parameter converter property
        /// </summary>
        public static readonly DependencyProperty DependencyPropertyChangedEventArgsConverterProperty =
            DependencyProperty.Register(
                nameof(DependencyPropertyChangedEventArgsConverter),
                typeof(IDependencyPropertyChangedEventArgsConverter),
                typeof(DependencyPropertyChangedEventToCommandBehavior),
                new PropertyMetadata(default(IDependencyPropertyChangedEventArgsConverter)));

        /// <summary>
        ///     The passing in property
        /// </summary>
        public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
            nameof(Property),
            typeof(string),
            typeof(DependencyPropertyChangedEventToCommandBehavior),
            new PropertyMetadata(null));

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyChangedEventToCommandBehavior" /> class.
        /// </summary>
        public DependencyPropertyChangedEventToCommandBehavior()
        {
            this.DependencyPropertyChangedEventHandler = (s, e) =>
                {
                    var command = this.Command;
                    if (command == null)
                    {
                        return;
                    }

                    var args = DependencyPropertyChangedEventHelpers.SolveEventArguments(
                        this.CommandParameter,
                        this.CommandParameterPattern,
                        this.PayloadType,
                        e,
                        this.AssociatedObject,
                        this.Property,
                        this.DependencyPropertyChangedEventArgsConverter,
                        this.DependencyPropertyChangedEventArgsConverterParameter);

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
        public DependencyPropertyChangedEventParameterPattern CommandParameterPattern
        {
            get => (DependencyPropertyChangedEventParameterPattern)this.GetValue(CommandParameterPatternProperty);
            set => this.SetValue(CommandParameterPatternProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command parameter converter.
        /// </summary>
        /// <value>
        ///     The command parameter converter.
        /// </value>
        public IDependencyPropertyChangedEventArgsConverter DependencyPropertyChangedEventArgsConverter
        {
            get =>
                (IDependencyPropertyChangedEventArgsConverter)this.GetValue(
                    DependencyPropertyChangedEventArgsConverterProperty);

            set => this.SetValue(DependencyPropertyChangedEventArgsConverterProperty, value);
        }

        /// <summary>
        ///     Gets or sets the routed event arguments converter parameter.
        /// </summary>
        /// <value>
        ///     The routed event arguments converter parameter.
        /// </value>
        public object DependencyPropertyChangedEventArgsConverterParameter
        {
            get => this.GetValue(DependencyPropertyChangedEventArgsConverterParameterProperty);
            set => this.SetValue(DependencyPropertyChangedEventArgsConverterParameterProperty, value);
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
        ///     Called when [command changed].
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnCommandChanged(
            DependencyPropertyChangedEventToCommandBehavior self,
            DependencyPropertyChangedEventArgs e)
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
        ///     with the Silverlight 3 version.
        /// </summary>
        /// <returns>
        ///     The command that must be executed when
        ///     this trigger is invoked.
        /// </returns>
        private ICommand GetCommand() => this.Command;
    }
}