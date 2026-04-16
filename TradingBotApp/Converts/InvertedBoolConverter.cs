using System.Globalization;

namespace TradingBotApp.Converts;

/// <summary>
/// Invierte un bool — usado para IsVisible cuando BotCorriendo = false
/// </summary>
public class InvertedBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool b && !b;

    //Lo anterior es equivalente a:
    //if( value is bool)
    //  {
    //     bool b = (bool)value;
    //  }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool b && !b;
}
