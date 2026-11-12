using GridComboBox.Views.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GridComboBox.Controls
{
    /// <summary>
    /// Interaction logic for SearchableComboBoxControl.xaml
    /// </summary>
    public partial class SearchableComboBoxControl : UserControl
    {
        public SearchableComboBoxControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ItemsSource Property
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                nameof(ItemsSource), 
                typeof(IEnumerable<object>), 
                typeof(SearchableComboBoxControl), 
                new PropertyMetadata(null)
            );

        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        /// <summary>
        /// DropDownTemplate Property
        /// </summary>
        public static readonly DependencyProperty DropDownTemplateProperty =
            DependencyProperty.Register(
                nameof(DropDownTemplate), 
                typeof(DataTemplate), 
                typeof(SearchableComboBoxControl), 
                new PropertyMetadata(null, OnDropDownTemplateChanged)
            );

        public DataTemplate DropDownTemplate
        {
            get { return (DataTemplate)GetValue(DropDownTemplateProperty); }
            set { SetValue(DropDownTemplateProperty, value); }
        }

        private static void OnDropDownTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SearchableComboBoxControl control &&
                control.Resources["ComboBoxTemplateSelector"] is ComboBoxUserTemplateSelector selector)
            {
                // Safely assign the template directly to the selector instance
                selector.DropDownTemplate = e.NewValue as DataTemplate;
            }
        }
    }
}
