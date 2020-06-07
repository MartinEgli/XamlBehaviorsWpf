// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Anori.WPF.Behaviors.DesignTools
{
    partial class MetadataTableProvider
    {
        private void AddAttributes(string typeIdentifier, params Attribute[] attributes)
        {
            _attributeTableBuilder.AddCustomAttributes(typeIdentifier, attributes);
        }

        private void AddAttributes(string typeIdentifier, string propertyName, params Attribute[] attributes)
        {
            _attributeTableBuilder.AddCustomAttributes(typeIdentifier, propertyName, attributes);
        }

        /// <summary>
        ///     This class contains the type names required by the new Extensibility APIs.
        /// </summary>
        private static class Targets
        {
            internal const string NameOfElementExtension = "Anori.WPF.Behaviors.NameOfElementExtension";
            internal const string EventTrigger = "Anori.WPF.Behaviors.EventTrigger";
            internal const string EventTriggerBase = "Anori.WPF.Behaviors.EventTriggerBase";
            internal const string TriggerBase = "Anori.WPF.Behaviors.TriggerBase";
            internal const string TriggerAction = "Anori.WPF.Behaviors.TriggerAction";
            internal const string TargetedTriggerAction = "Anori.WPF.Behaviors.TargetedTriggerAction";
            internal const string ChangePropertyAction = "Anori.WPF.Behaviors.Core.ChangePropertyAction";
            internal const string InvokeCommandAction = "Anori.WPF.Behaviors.InvokeCommandAction";
            internal const string LaunchUriOrFileAction = "Anori.WPF.Behaviors.Core.LaunchUriOrFileAction";
            internal const string MouseDragElementBehavior = "Anori.WPF.Behaviors.Layout.MouseDragElementBehavior";
            internal const string DataStateBehavior = "Anori.WPF.Behaviors.Core.DataStateBehavior";
            internal const string FluidMoveBehavior = "Anori.WPF.Behaviors.Layout.FluidMoveBehavior";
            internal const string FluidMoveBehaviorBase = "Anori.WPF.Behaviors.Layout.FluidMoveBehaviorBase";
            internal const string StoryboardAction = "Anori.WPF.Behaviors.Media.StoryboardAction";
            internal const string ControlStoryboardAction = "Anori.WPF.Behaviors.Media.ControlStoryboardAction";
            internal const string GoToStateAction = "Anori.WPF.Behaviors.Core.GoToStateAction";

            internal const string TranslateZoomRotateBehavior =
                "Anori.WPF.Behaviors.Input.TranslateZoomRotateBehavior";

            internal const string PlaySoundAction = "Anori.WPF.Behaviors.Media.PlaySoundAction";
            internal const string CallMethodAction = "Anori.WPF.Behaviors.Core.CallMethodAction";
        }
    }
}