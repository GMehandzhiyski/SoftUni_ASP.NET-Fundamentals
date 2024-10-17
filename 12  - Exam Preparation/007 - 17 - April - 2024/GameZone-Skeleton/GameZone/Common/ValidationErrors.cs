namespace GameZone.Common
{
    public static class ValidationErrors
    {
        //Title
        public const string ErrorMessageTitle = "The {0} field is required";
        public const string ErrorMessageTitleLength = "The {0} length is between {2} and {1}";

        //Discription
        public const string ErrorMessageDescription = "The {0} field is required";
        public const string ErrorMessageDescriptionLength = "The {0} length is between {2} and {1}";

        //ReleasedOn
        public const string ErrorMessageReleasedOn = "The {0} field is required";
        public const string ErrorMessageReleasedOnLength = "The {0} length is between {2} and {1}";


        //GenreId
        public const string ErrorMessageGenreId = "The {0} field is required";
        
    }
}
