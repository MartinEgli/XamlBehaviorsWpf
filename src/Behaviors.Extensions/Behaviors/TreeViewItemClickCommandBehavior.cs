// -----------------------------------------------------------------------
// <copyright file="TreeViewItemClickCommandBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
#pragma warning disable SA1119 // StatementMustNotUseUnnecessaryParenthesis

namespace Bfa.Common.WPF.Behaviors
{
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// TreeView Item Click Command Behavior
    /// </summary>
    /// <seealso cref="UiElementClickBehavior{TreeView}" />
    /// <inheritdoc />
    public class TreeViewItemClickCommandBehavior : UiElementClickBehavior<TreeView>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Called when [clicked].
        /// </summary>
        protected override void OnClicked()
        {
            var element = this.AssociatedObject;
            if (element == null)
            {
                return;
            }

            var command = this.ExtractCommand(element.SelectedItem);

            if (command == null)
            {
                return;
            }

            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        /// <summary>
        ///     Extracts the command.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The command</returns>
        protected virtual ICommand ExtractCommand(object viewModel)
        {
            if (!(viewModel is IClickCommand item))
            {
                return null;
            }

            return item.ClickCommand;
        }
    }
}

#pragma warning restore SA1119 // StatementMustNotUseUnnecessaryParenthesis