// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE file in the project root for full license information. 

using System;

namespace Anori.Xaml.Behaviors.DesignTools
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
        /// This class contains the type names required by the new Extensibility APIs.
        /// </summary>
        private static class Targets
        {
            internal const string EventTrigger = "Anori.Xaml.Behaviors.EventTrigger";
            internal const string EventTriggerBase = "Anori.Xaml.Behaviors.EventTriggerBase";
            internal const string TriggerBase = "Anori.Xaml.Behaviors.TriggerBase";
            internal const string TriggerAction = "Anori.Xaml.Behaviors.TriggerAction";
            internal const string TargetedTriggerAction = "Anori.Xaml.Behaviors.TargetedTriggerAction";
            internal const string ChangePropertyAction = "Anori.Xaml.Behaviors.Core.ChangePropertyAction";
            internal const string InvokeCommandAction = "Anori.Xaml.Behaviors.InvokeCommandAction";
            internal const string LaunchUriOrFileAction = "Anori.Xaml.Behaviors.Core.LaunchUriOrFileAction";
            internal const string MouseDragElementBehavior = "Anori.Xaml.Behaviors.Layout.MouseDragElementBehavior";
            internal const string DataStateBehavior = "Anori.Xaml.Behaviors.Core.DataStateBehavior";
            internal const string FluidMoveBehavior = "Anori.Xaml.Behaviors.Layout.FluidMoveBehavior";
            internal const string FluidMoveBehaviorBase = "Anori.Xaml.Behaviors.Layout.FluidMoveBehaviorBase";
            internal const string StoryboardAction = "Anori.Xaml.Behaviors.Media.StoryboardAction";
            internal const string ControlStoryboardAction = "Anori.Xaml.Behaviors.Media.ControlStoryboardAction";
            internal const string GoToStateAction = "Anori.Xaml.Behaviors.Core.GoToStateAction";
            internal const string TranslateZoomRotateBehavior = "Anori.Xaml.Behaviors.Input.TranslateZoomRotateBehavior";
            internal const string PlaySoundAction = "Anori.Xaml.Behaviors.Media.PlaySoundAction";
            internal const string CallMethodAction = "Anori.Xaml.Behaviors.Core.CallMethodAction";
        }
    }
}
