using SuperMarket.Models.EntityLayer;
using SuperMarket.Models;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace SuperMarket.Models;

public class UserPageParametersConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return values.Clone();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return (object[])value;
    }
}
