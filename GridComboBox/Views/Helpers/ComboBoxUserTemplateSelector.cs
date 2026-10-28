using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GridComboBox.Views.Helpers
{
    public class ComboBoxUserTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectedTemplate { get; set; }
        public DataTemplate DropDownTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var parent = VisualTreeHelper.GetParent(container);
            while (parent != null && !(parent is ComboBoxItem) && !(parent is ComboBox))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            // If it's inside a ComboBoxItem, use the multi-column Grid template
            if (parent is ComboBoxItem)
            {
                return DropDownTemplate;
            }

            // Otherwise, it's the main closed selection box—use the simple Name template
            return SelectedTemplate;
        }
    }
}
