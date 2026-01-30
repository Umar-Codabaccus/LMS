using LMS.Api.Application.DTOs;
using LMS.Api.Domain.Entities;

namespace LMS.Api.Infrastructure.Interfaces
{
    public interface ICourseRepository
    {
        public Course Add(Course course);
        public bool Delete(Course course);
        public bool Update(Course course);
        public Course Get(Guid id);
        public List<Course> GetAll();
        public List<Course> GetCoursesBySearch(string searchText);
        public IEnumerable<PublishedCourse> GetCoursesForUser(Guid userId);
    }
}
