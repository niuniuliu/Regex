using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RegexBuddy
{
    public class BooleanToVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = (bool) value;

            // if visiblity is inverted by the converter parameter, then invert our parameter
            if (IsVisibilityInverted(parameter))
                isVisible = !isVisible;

            return (isVisible ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = ((Visibility) value == Visibility.Visible);

            // if visibility is inverted by the converter parameter, then invert our value
            if (IsVisibilityInverted(parameter))
                isVisible = !isVisible;

            return isVisible;
        }

        private static bool IsVisibilityInverted(object parameter)
        {
            return (GetVisibilityMode(parameter) == Visibility.Collapsed);
        }

        private static Visibility GetVisibilityMode(object parameter)
        {
            // Default to visible
            var mode = Visibility.Visible;
            if (parameter == null) return mode;

            if (parameter is Visibility)
            {
                mode = (Visibility)parameter;
            }
            else
            {
                try
                {
                    mode = (Visibility) Enum.Parse(typeof (Visibility), parameter.ToString(), true);
                }
                catch (Exception e)
                {
                    throw new FormatException("Invalid Visiblity specified as the ConvertedParameter. Use Visible or Collapsed.", e);
                }
            }

            return mode;
        }
    }
}
