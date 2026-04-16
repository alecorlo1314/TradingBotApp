using System.Globalization;

namespace TradingBotApp.Converts;

/// <summary>
/// Retorna true si el string no está vacío — usado para mostrar StatusMessage
/// </summary>
public class StringNotEmptyConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is string s && !string.IsNullOrWhiteSpace(s);

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
