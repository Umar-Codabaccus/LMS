using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Api.Domain.Entities
{
    public class EnrolledModule
    {
        [Key]
        [Column("EnrolledModuleId")]
        public Guid Id { get; set; }

        [Required]
        [Column("EnrolledCourseId")]
        [ForeignKey("EnrolledCourse")]
        public Guid EnrolledCourseId { get; set; }
        public EnrolledCourse EnrolledCourse { get; set; }

        [Required]
        [Column("ModuleId")]
        [ForeignKey("Module")]
        public Guid ModuleId { get; set; }
        public Module Module { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
