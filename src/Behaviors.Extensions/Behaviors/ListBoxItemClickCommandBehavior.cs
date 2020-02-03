// -----------------------------------------------------------------------
// <copyright file="ListBoxItemClickCommandBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
#pragma warning disable SA1119 // StatementMustNotUseUnnecessaryParenthesis

namespace Anori.WPF.Behaviors
{
    using System.Windows.Controls;

    /// <summary>
    ///     ListBox Item Click Command Behavior
    /// </summary>
    /// <seealso cref="UiElementClickBehavior{TUiElement}" />
    /// <inheritdoc />
    public class ListBoxItemClickCommandBehavior : UiElementClickBehavior<ListBox>
    {
        /// <summary>
        ///     Called when [clicked].
        /// </summary>
        /// <inheritdoc />
        protected override void OnClicked()
        {
            var element = this.AssociatedObject;
            if (element == null)
            {
                return;
            }

            if (!(element.SelectedItem is IClickCommand item))
            {
                return;
            }

            var command = item.ClickCommand;
            if (command == null)
            {
                return;
            }

            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
    }
}

#pragma warning restore SA1119 // StatementMustNotUseUnnecessaryParenthesis