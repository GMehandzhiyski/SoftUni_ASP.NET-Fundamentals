namespace SeminarHub.Common
{
    public class ValidationErrors
    {
        //Topic
        public const string ErrorMessageTopic = "The {0} field is required";
        public const string ErrorMessageTopicLength = "The {0} length is between {2} and {1}";

        //Lecture
        public const string ErrorMessageLecture = "The {0} field is required";
        public const string ErrorMessageLectureLength = "The {0} length is between {2} and {1}";

        //Details
        public const string ErrorMessageDetails = "The {0} field is required";
        public const string ErrorMessageDetailsLength = "The {0} length is between {2} and {1}";

        //Data and Time
        public const string ErrorMessageDateAndTime = "The {0} field is required";

        //Duration
        public const string ErrorMessageDuration = "The {0} field is required";
        public const string ErrorMessageDurationLength = "The {0} length is between {1} and {2}";

        //CategoryId
        public const string ErrorMessageCategoryId = "The {0} field is required";

    }
}
