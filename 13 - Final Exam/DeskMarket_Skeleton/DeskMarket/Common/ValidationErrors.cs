namespace DeskMarket.Common
{
    public class ValidationErrors
    {
        //ProductName
        public const string ErrorMessageProductName = "The {0} field is required";
        public const string ErrorMessageProductNameLength = "The {0} length is between {2} and {1}";

        //Price
        public const string ErrorMessagePrice = "The {0} field is required";
        public const string ErrorMessagePriceLength = "The {0} length is between {1} and {2}";

        //Description
        public const string ErrorMessageDescription = "The {0} field is required";
        public const string ErrorMessageDescriptionLength = "The {0} length is between {2} and {1}";

        //AddedOn
        //Description
        public const string ErrorMessageAddedOn = "The {0} field is required";
    }
}
