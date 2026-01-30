using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Api.Domain.Entities
{
    [Table("EnrolledCourses")]
    public class EnrolledCourse
    {
        [Key]
        [Column("EnrolledCourseId")]
        public Guid Id { get; set; }

        [Column("Status")]
        public string Status { get; set; } = string.Empty; // Either Complete or Ongoing

        [Required]
        [Column("CourseId")]
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        [Column("UserId")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IReadOnlyCollection<EnrolledModule> EnrolledModules => _enrolledModules;
        private readonly List<EnrolledModule> _enrolledModules = [];

    }
}