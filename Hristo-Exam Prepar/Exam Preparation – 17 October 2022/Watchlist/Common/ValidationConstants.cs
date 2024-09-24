namespace Watchlist.Common;

public static class ValidationConstants
{
    public static class Genre
    {
        public const int MinNameLength = 5;
        public const int MaxNameLength = 50;
    }

    public static class Movie
    {
        public const int MinTitleLength = 10;
        public const int MaxTitleLength = 50;
        public const int MinDirectorNameLength = 5;
        public const int MaxDirectorNameLength = 50;
        public const double MinRating = 0.00;
        public const double MaxRating = 10.00;

    }
}
