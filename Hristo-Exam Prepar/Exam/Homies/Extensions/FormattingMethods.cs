namespace Homies.Extensions;

using static Common.DateTimeParseFormats;
public static class FormattingMethods
{
    public static string FormatDateTime(DateTime inputDatetime)
    {
        return inputDatetime.ToString(DefaultTimeFormat);
    }
}
