namespace Library.Common
{
    public static class ValidationConstants
    {
        public static class Book
        {
            public const int MinTitleLength = 10;
            public const int MaxTitleLength = 50;
            public const int MinAuthorNameLength = 5;
            public const int MaxAuthorNameLength = 50;
            public const int MinDescriptionLength = 5;
            public const int MaxDescriptionLength = 5000;
            public const decimal MinRating = 0m;
            public const decimal MaxRating = 10.00m;
        }

        public static class Category
        {
            public const int MinNameLength = 5;
            public const int MaxNameLength = 50;
        }
    }
}
