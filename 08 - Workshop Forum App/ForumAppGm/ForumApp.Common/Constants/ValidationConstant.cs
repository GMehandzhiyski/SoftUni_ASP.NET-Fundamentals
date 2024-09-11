namespace ForumApp.Common.Constants
{
    public static class ValidationConstant
    {
        public const int MinLengthTitle = 10;
        public const int MaxLengthTitle = 50;

        public const int MinLengthContent = 30;
        public const int MaxLengthContent = 1500;

        //errors
        public const string ErrorMessageTitleLength = "The {0} length is between {2} and {1}";
        public const string ErrorMessageContextLength = "The {0} is between {2} and {1}";

        public const string ErrorMessageTitle = "The {0} field is required";
        public const string ErrorMessageContext = "The {0} field is required";
    }
}
