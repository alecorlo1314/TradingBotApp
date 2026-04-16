using System.Globalization;

namespace TradingBotApp.Converts;

public class BotCorriendoConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
         if (value is bool b)
        {
            return b ? "Analisis corriendo" : "Analisis desactivado";
        }
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
