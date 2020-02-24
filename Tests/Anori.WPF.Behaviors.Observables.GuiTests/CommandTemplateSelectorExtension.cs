namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class CommandTemplateSelectorExtension : MarkupExtension
    {
        public static readonly DependencyProperty TemplateDictionaryProperty = DependencyProperty.Register("TemplateDictionary", typeof(Dictionary<Type, DataTemplate>), typeof(CommandTemplateSelectorExtension), new PropertyMetadata(default(Dictionary<Type, DataTemplate>)));

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var multiBinding = new MultiBinding();
            multiBinding.Bindings.Add(this.TemplateDictionaryBinding);
            multiBinding.Converter = new CommandTemplateSelectorConverter();
            return new CommandTemplateSelector();
        }

        public Binding TemplateDictionaryBinding
        {
            get;
            set;
        }
    }

    public class CommandTemplateSelectorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new CommandTemplateSelector();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
