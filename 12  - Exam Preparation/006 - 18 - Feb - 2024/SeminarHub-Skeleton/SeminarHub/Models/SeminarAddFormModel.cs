using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Common.ValidationConstants;
using static SeminarHub.Common.ValidationErrors;

namespace SeminarHub.Models
{
    public class SeminarAddFormModel
    {

        [Required(ErrorMessage = ErrorMessageTopic)]
        [Comment("Topic")]
        [StringLength(SeminarTopicMaxLenght,
                      MinimumLength = SeminarTopicMinLenght,
                      ErrorMessage = ErrorMessageTopicLength)]
        public string Topic { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageLecture)]
        [Comment("Lecturer")]
        [StringLength(SeminarLecturerMaxLenght,
                      MinimumLength = SeminarLecturerMinLenght,
                      ErrorMessage = ErrorMessageLectureLength)]
        public string Lecturer { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageDetails)]
        [Comment("Details")]
        [StringLength(SeminarDetailsMaxLenght,
                      MinimumLength = SeminarDetailsMinLenght,
                      ErrorMessage = ErrorMessageDetailsLength)]
        public string Details { get; set; } = string.Empty;

        [Comment("OrganiserId")]
        public string OrganizerId { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageDateAndTime)]
        [Comment("Data and Time")]
        public string DateAndTime { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageDuration)]
        [Comment("Duration")]
        [Range(SeminarDetailsMinLenght,
               SeminarDurationMaxLenght,
               ErrorMessage = ErrorMessageDurationLength)]
        public int Duration { get; set; }

        [Required(ErrorMessage = ErrorMessageCategoryId)]
        [Comment("CategoryId")]
        public int CategoryId { get; set; }

        public IEnumerable<SeminarCategoryViewModel> Categories { get; set; } = new List<SeminarCategoryViewModel>();   

    }
}
