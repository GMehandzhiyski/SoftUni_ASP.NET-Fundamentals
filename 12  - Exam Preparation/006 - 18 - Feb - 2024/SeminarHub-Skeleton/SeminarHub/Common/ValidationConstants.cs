using Newtonsoft.Json.Serialization;

namespace SeminarHub.Common
{
    public static class ValidationConstants
    {
        //Seminar
        public const int SeminarTopicMinLenght = 3;
        public const int SeminarTopicMaxLenght = 100;

        //Lecturer
        public const int SeminarLecturerMinLenght = 5;
        public const int SeminarLecturerMaxLenght = 60;

        //Details
        public const int SeminarDetailsMinLenght = 10;
        public const int SeminarDetailsMaxLenght = 500;

        //Duration
        public const int SeminarDurationMinLenght = 30;
        public const int SeminarDurationMaxLenght = 180;

        //Category
        public const int CategoryNameMinLenght = 3;
        public const int CategoryNameMaxLenght = 50;
    }
}
