namespace Watchlist.Extensions;

public static class FormattingMethods
{
    public static decimal DecimalToGlobalStandard(string inputNumber)
    {
        if (inputNumber.Contains(','))
        {
            inputNumber = inputNumber.Replace(',', '.');
        }

        return decimal.Parse(inputNumber);
    }
}
