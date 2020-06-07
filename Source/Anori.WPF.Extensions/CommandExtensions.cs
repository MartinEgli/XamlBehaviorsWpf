// -----------------------------------------------------------------------
// <copyright file="CommandExtensions.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Extensions
{
    using JetBrains.Annotations;
    using System;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    ///     Command Extensions
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        ///     Tries the type of the get command parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <returns>
        ///     Gets type.
        /// </returns>
        public static bool TryGetCommandParameterType([CanBeNull] ICommand command, out Type parameterType)
        {
            if (command == null)
            {
                parameterType = null;
                return false;
            }

            Type commandType = command.GetType();
            if (!commandType.IsGenericType)
            {
                parameterType = null;
                return false;
            }

            Type[] args = commandType.GenericTypeArguments;
            if (!args.Any())
            {
                parameterType = null;
                return false;
            }

            parameterType = args.First();
            return true;
        }

        /// <summary>
        ///     Gets the type of the command parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public static Type GetCommandParameterType(ICommand command)
        {
            if (command == null)
            {
                return null;
            }

            Type commandType = command.GetType();
            if (!commandType.IsGenericType)
            {
                return null;
            }

            Type[] args = commandType.GenericTypeArguments;
            return args.FirstOrDefault();
        }
    }
}
