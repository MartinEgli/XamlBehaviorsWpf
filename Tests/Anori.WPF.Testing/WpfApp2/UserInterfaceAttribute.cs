// -----------------------------------------------------------------------
// <copyright file="UserInterfaceAttribute.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Testing
{
    using System;

    using NUnit.Framework;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class UserInterfaceAttribute : CategoryAttribute
    {
        public UserInterfaceAttribute()
            : base("UserInterface")
        {
        }
    }

    public class UiAttribute : UserInterfaceAttribute
    {
      
    }
}
