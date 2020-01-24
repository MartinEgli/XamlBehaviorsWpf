// -----------------------------------------------------------------------
// <copyright file="InvokeDependencyPropertyCommandAction.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable SA1119 // StatementMustNotUseUnnecessaryParenthesis

namespace Bfa.Common.WPF.TriggerActions
{
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;

    using Bfa.Common.WPF.Extensions;

    using Microsoft.Xaml.Behaviors;

    /// <summary>
    ///     Trigger action that executes a command when invoked.
    ///     It also maintains the Enabled state of the target control based on the CanExecute method of the command.
    /// </summary>
    public class InvokeDependencyPropertyCommandAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        ///     Dependency property identifying if the associated element should automaticaly be enabled or disabled based on the
        ///     result of the Command's CanExecute
        /// </summary>
        public static readonly DependencyProperty AutoEnableProperty = DependencyProperty.Register(
            nameof(AutoEnable),
            typeof(bool),
            typeof(InvokeDependencyPropertyCommandAction),
            new PropertyMetadata(
                true,
                (d, e) => ((InvokeDependencyPropertyCommandAction)d).OnAllowDisableChanged((bool)e.NewValue)));

        /// <summary>
        ///     Dependency property identifying the command parameter to supply on command execution.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter),
            typeof(object),
            typeof(InvokeDependencyPropertyCommandAction),
            new PropertyMetadata(null, OnCommandParameterChanged));

        /// <summary>
        ///     Dependency property identifying the command to execute when invoked.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(InvokeDependencyPropertyCommandAction),
            new PropertyMetadata(null, OnCommandChanged));

        /// <summary>
        /// The dependency property event arguments converter parameter property
        /// </summary>
        public static readonly DependencyProperty DependencyPropertyEventArgsConverterParameterProperty =
            DependencyProperty.Register(
                nameof(DependencyPropertyEventArgsConverterParameter),
                typeof(object),
                typeof(InvokeDependencyPropertyCommandAction),
                new PropertyMetadata(null, OnDependencyPropertyChangedEventArgsConverterParameterChanged));

        /// <summary>
        /// The dependency property changed event arguments converter property
        /// </summary>
        public static readonly DependencyProperty DependencyPropertyChangedEventArgsConverterProperty =
            DependencyProperty.Register(
                nameof(DependencyPropertyChangedEventArgsConverter),
                typeof(IDependencyPropertyChangedEventArgsConverter),
                typeof(InvokeDependencyPropertyCommandAction),
                new PropertyMetadata(null, OnDependencyPropertyChangedEventArgsConverterChanged));

        /// <summary>
        ///     Dependency property identifying the TriggerParameterPath to be parsed to identify the child property of the trigger
        ///     parameter to be used as the command parameter.
        /// </summary>
        public static readonly DependencyProperty TriggerParameterPathProperty = DependencyProperty.Register(
            nameof(TriggerParameterPath),
            typeof(string),
            typeof(InvokeDependencyPropertyCommandAction),
            new PropertyMetadata(null, (d, e) => { }));

        /// <summary>
        ///     The command behavior
        /// </summary>
        private Lazy<ExecutableDependencyPropertyChangedEventArgsCommandBehavior> commandBehavior;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokeDependencyPropertyCommandAction"/> class.
        /// </summary>
        public InvokeDependencyPropertyCommandAction()
        {
            this.commandBehavior = new Lazy<ExecutableDependencyPropertyChangedEventArgsCommandBehavior>(
                () => new ExecutableDependencyPropertyChangedEventArgsCommandBehavior(this.AssociatedObject));
        }

        /// <summary>
        ///     Gets or sets a value indicating whether or not the associated element will automatically be enabled or disabled
        ///     based on the result of
        ///     the commands CanExecute
        /// </summary>
        /// <value>
        ///     <c>true</c> if [automatic enable]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoEnable
        {
            get => (bool)this.GetValue(AutoEnableProperty);
            set => this.SetValue(AutoEnableProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command to execute when invoked.
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        public ICommand Command
        {
            get => this.GetValue(CommandProperty) as ICommand;
            set => this.SetValue(CommandProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command parameter to supply on command execution.
        /// </summary>
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
        ///     Gets or sets a converter used to convert the EventArgs when using
        ///     <see cref="PassEventArgsToCommand" />. If PassEventArgsToCommand is false,
        ///     this property is never used.
        /// </summary>
        /// <value>
        ///     The event arguments converter.
        /// </value>
        public IDependencyPropertyChangedEventArgsConverter DependencyPropertyChangedEventArgsConverter
        {
            get =>
                (IDependencyPropertyChangedEventArgsConverter)this.GetValue(
                    DependencyPropertyChangedEventArgsConverterProperty);
            set => this.SetValue(DependencyPropertyChangedEventArgsConverterProperty, value);
        }

        /// <summary>
        ///     Gets or sets a parameters for the converter used to convert the EventArgs when using
        ///     <see cref="PassEventArgsToCommand" />. If PassEventArgsToCommand is false,
        ///     this property is never used. This is a dependency property.
        /// </summary>
        /// <value>
        ///     The event arguments converter parameter.
        /// </value>
        public object DependencyPropertyEventArgsConverterParameter
        {
            get => this.GetValue(DependencyPropertyEventArgsConverterParameterProperty);
            set => this.SetValue(DependencyPropertyEventArgsConverterParameterProperty, value);
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
        ///     Gets or sets the TriggerParameterPath value.
        /// </summary>
        public string TriggerParameterPath
        {
            get => this.GetValue(TriggerParameterPathProperty) as string;
            set => this.SetValue(TriggerParameterPathProperty, value);
        }

        /// <summary>
        ///     Public wrapper of the Invoke method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void InvokeAction(object parameter)
        {
            this.Invoke(parameter);
        }

        /// <summary>
        ///     Executes the command
        /// </summary>
        /// <param name="parameter">
        ///     This parameter is passed to the command; the CommandParameter specified in the
        ///     CommandParameterProperty is used for command invocation if not null.
        /// </param>
        /// <inheritdoc />
        protected override void Invoke(object parameter)
        {
            object executeParameter;
            var commandParameter = this.CommandParameter;
            if (commandParameter != null)
            {
                executeParameter = commandParameter;
            }
            else if (this.PassEventArgsToCommand)
            {
                if (this.DependencyPropertyChangedEventArgsConverter != null
                    && parameter is DependencyPropertyChangedEventArgs args)
                {
                    executeParameter = this.DependencyPropertyChangedEventArgsConverter.Convert(
                        args,
                        this.CommandParameterType,
                        this.DependencyPropertyEventArgsConverterParameter,
                        this.AssociatedObject);
                }
                else if (!string.IsNullOrEmpty(this.TriggerParameterPath))
                {
                    executeParameter = this.WalkParameterPath(parameter);
                }
                else
                {
                    executeParameter = null;
                }
            }
            else
            {
                executeParameter = null;
            }

            var behavior = this.commandBehavior.Value;

            behavior?.ExecuteCommand(executeParameter);
        }

        /// <inheritdoc />
        /// <summary>
        ///     This method is called after the behavior is attached.
        ///     It updates the command behavior's Command and CommandParameter properties if necessary.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            // In case this action is attached to a target object after the Command and/or CommandParameter properties are set,
            // the command behavior would be created without a value for these properties.
            // To cover this scenario, the Command and CommandParameter properties of the behavior are updated here.
            var behavior = this.commandBehavior.Value;

            behavior.AutoEnable = this.AutoEnable;
            behavior.Command = this.Command;
            behavior.CommandParameter = this.CommandParameter;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sets the Command and CommandParameter properties to null.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.Command = null;
            this.CommandParameter = null;

            this.commandBehavior = null;
        }

        /// <summary>
        ///     Called when [command changed].
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">
        ///     The <see cref="DependencyPropertyChangedEventArgs" /> instance
        ///     containing the event data.
        /// </param>
        private static void OnCommandChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is InvokeDependencyPropertyCommandAction self))
            {
                return;
            }

            if (!(dependencyPropertyChangedEventArgs.NewValue is ICommand newValue))
            {
                return;
            }

            var behavior = self.commandBehavior.Value;
            if (behavior != null)
            {
                behavior.Command = newValue;
                self.CommandParameterType = CommandExtensions.GetCommandParameterType(newValue);
            }
        }

        /// <summary>
        ///     Called when [command parameter changed].
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">
        ///     The <see cref="DependencyPropertyChangedEventArgs" /> instance
        ///     containing the event data.
        /// </param>
        private static void OnCommandParameterChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is InvokeDependencyPropertyCommandAction self))
            {
                return;
            }

            if (!(dependencyPropertyChangedEventArgs.NewValue is IEventArgsConverter newValue))
            {
                return;
            }

            var behavior = self.commandBehavior.Value;
            if (behavior != null)
            {
                behavior.CommandParameter = newValue;
            }
        }

        /// <summary>
        ///     Called when [event arguments converter changed].
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">
        ///     The <see cref="DependencyPropertyChangedEventArgs" /> instance
        ///     containing the event data.
        /// </param>
        private static void OnDependencyPropertyChangedEventArgsConverterChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is InvokeDependencyPropertyCommandAction self))
            {
                return;
            }

            if (!(dependencyPropertyChangedEventArgs.NewValue is IDependencyPropertyChangedEventArgsConverter newValue))
            {
                return;
            }

            var behavior = self.commandBehavior.Value;
            if (behavior != null)
            {
                behavior.DependencyPropertyChangedEventArgsConverter = newValue;
            }
        }

        /// <summary>
        ///     Called when [event arguments converter parameter changed].
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">
        ///     The <see cref="DependencyPropertyChangedEventArgs" /> instance
        ///     containing the event data.
        /// </param>
        private static void OnDependencyPropertyChangedEventArgsConverterParameterChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is InvokeDependencyPropertyCommandAction self))
            {
                return;
            }

            self.commandBehavior.Value.EventArgsConverterParameter = dependencyPropertyChangedEventArgs.NewValue;
        }

        /// <summary>
        ///     Called when [allow disable changed].
        /// </summary>
        /// <param name="newValue">if set to <c>true</c> [new value].</param>
        private void OnAllowDisableChanged(bool newValue)
        {
            var behavior = this.commandBehavior.Value;
            if (behavior != null)
            {
                behavior.AutoEnable = newValue;
            }
        }

        /// <summary>
        ///     Walks the parameter path.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The path.</returns>
        private object WalkParameterPath(object parameter)
        {
            var propertyPathParts = this.TriggerParameterPath.Split('.');
            var propertyValue = parameter;
            foreach (var propertyPathPart in propertyPathParts)
            {
                var propInfo = propertyValue.GetType().GetTypeInfo().GetProperty(propertyPathPart);
                if (propInfo != null)
                {
                    propertyValue = propInfo.GetValue(propertyValue);
                }
            }

            return propertyValue;
        }
    }
}

#pragma warning restore SA1119 // StatementMustNotUseUnnecessaryParenthesis