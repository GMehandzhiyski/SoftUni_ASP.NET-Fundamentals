using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Common.ValidationConstants;
using static SeminarHub.Common.ValidationErrorMessages;

namespace SeminarHub.Models
{
    public class AddFormModel
    {
    

        [Required(ErrorMessage = ErrorMessageTopic)]
        [Comment("Topic")]
        [StringLength(SeminarTopicMaxLength,
                      MinimumLength = SeminarTopicMinLength,
                      ErrorMessage = ErrorMessageTopicLength)]
        public string Topic { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageLecturer)]
        [Comment("Lecturer")]
        [StringLength(SeminarLecturerMaxLength,
                MinimumLength = SeminarLecturerMinLength,
                ErrorMessage = ErrorMessageLecturerLength)]
        public string Lecturer { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageDetails)]
        [Comment("Details")]
        [StringLength(SeminarDetailsMaxLength,
                    MinimumLength = SeminarDetailsMinLength,
                    ErrorMessage = ErrorMessageDetailsLength)]
        public string Details { get; set; } = string.Empty;


       
        [Comment("OrganizerId")]
        public string OrganizerId { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageDateAndTime)]
        [Display(Name ="Date of Seminar")]
        [Comment("DateAndTime")]
        public string DateAndTime { get; set; } = string.Empty;

        [Comment("Duration")]
        [Range(SeminarDurationMinLength,
                SeminarDurationMaxLength,
                        ErrorMessage = ErrorMessageDurationLength)]
        public int Duration { get; set; }

        [Required]
        [Comment("CategoryId")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
