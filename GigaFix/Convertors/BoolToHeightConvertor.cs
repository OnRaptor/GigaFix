using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace GigaFix.Convertors;

public class BoolToHeightConvertor : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool) return null;
        var v = (bool)value;
        if (v)
            return "*";
        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}