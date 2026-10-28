using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace GridComboBox.Views.Converters
{
    public class RoleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Models.Role role)
            {
                return role.ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string roleString && Enum.TryParse(typeof(Models.Role), roleString, out var result))
            {
                return result;
            }
            return Models.Role.User;

        }
    }
}
