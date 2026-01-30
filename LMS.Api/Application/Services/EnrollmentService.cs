using LMS.Api.Application.DTOs.Enrollment;
using LMS.Api.Application.Errors;
using LMS.Api.Application.Services.Interfaces;
using LMS.Api.Domain.Entities;
using LMS.Api.Infrastructure.Interfaces;
using LMS.Api.Shared;

namespace LMS.Api.Application.Services
{
    public partial class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrolledCourseRepository _enrolledCourseRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IModuleRepository _moduleRepo;

        private Dictionary<EnrolledCourse, Queue<EnrolledModule>> _enrolledCourses = new();
        private Queue<Guid> _moduleIds = new();

        public Dictionary<EnrolledCourse, Queue<EnrolledModule>> EnrolledCourses => _enrolledCourses;
        public Queue<EnrolledModule> EnrolledModules { get; set; }

        public EnrollmentService(IEnrolledCourseRepository enrolledCourseRepo, ICourseRepository courseRepo, IModuleRepository moduleRepo)
        {
            _enrolledCourseRepo = enrolledCourseRepo;
            _courseRepo = courseRepo;
            _moduleRepo = moduleRepo;
            EnrolledModules = new Queue<EnrolledModule>();
        }

        public List<EnrollCourseDto> GetEnrolledCourses(Guid userId)
        {
            var enrolledCourses = _enrolledCourseRepo.GetEnrolledCourses(userId);

            if (enrolledCourses is null)
            {
                throw new InvalidOperationException();
            }

            return enrolledCourses;
        }

        public Result<EnrollResponse> Enroll(EnrollRequest request)
        {
            // check if the user is already enrolled in this course
            string resultMessage = _enrolledCourseRepo.CheckExistence(request.CourseId, request.UserId);

            // if exists
            if (Messages.EnrolledMessages.Exist() == resultMessage)
            {
                // return existence error
                return Result.Failure<EnrollResponse>(DomainErrors.EnrollmentErrors.UserAlreadyEnrolled());
            }

            // course enrollment object creation
            var enrollment = new EnrolledCourse()
            {
                Id = Guid.NewGuid(),
                CourseId = request.CourseId,
                UserId = request.UserId,
                Status = "Ongoing",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // add to repo
            var (enrolledCourse, message) = _enrolledCourseRepo.Add(enrollment);

            if (enrolledCourse is null)
            {
                // return enrollment failed error
                return Result.Failure<EnrollResponse>(DomainErrors.EnrollmentErrors.EnrollmentFailed());
            }

            // Module Enrollment
            // Get courseId, get all moduleIds and populate into the dictionary
            var (modules, operation) = GetModules(enrolledCourse.CourseId);

            if (operation == Operation.Failed || modules is null)
            {
                // Throw an error to say that the loading the modules failed, enroll is successfull but the user cannot complete the course due to an error to load the modules the specific course
                return Result.Failure<EnrollResponse>(DomainErrors.EnrollmentErrors.FetchingModulesFailed());
            }

            modules = AssigningOrders(modules);

            foreach (var module in modules)
            {
                _moduleIds.Enqueue(module.ModuleId);
            }

            // store in Queue<EnrolledModule>
            while (_moduleIds.Count > 0)
            {
                var moduleId = _moduleIds.Dequeue();

                EnrolledModule enrolledModule = new EnrolledModule()
                {
                    Id = Guid.NewGuid(),
                    EnrolledCourseId = enrolledCourse.Id,
                    ModuleId = moduleId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                EnrolledModules.Enqueue(enrolledModule);
            }

            List<EnrolledModule> enrolledModules = [];
            (enrolledModules, message) = _enrolledCourseRepo.EnrollModules(EnrolledModules);

            if (enrolledModules is null || enrolledModules.Count == 0)
            {
                // log message
                return Result.Failure<EnrollResponse>(DomainErrors.EnrollmentErrors.ModulesCouldNotBeEnrolled());
            }

            var course = _courseRepo.Get(enrolledCourse.CourseId);
            var e_modules = _moduleRepo.GetAll(enrolledCourse.CourseId);

            List<EnrolledModuleDto> dtos = enrolledModules
            .Join(
                e_modules,               // the modules with full info
                em => em.ModuleId,        // key from enrolledModules
                m => m.Id,                // key from e_modules
                (em, m) => new EnrolledModuleDto
                {
                    EnrolledModuleId = em.Id, // from enrolledModules
                    ModuleId = m.Id,          // from e_modules
                    Title = m.Title,
                    Description = m.Description,
                    VideoUrl = m.VideoUrl,
                    Order = m.ModuleOrder
                }
            )
            .OrderBy(x => x.Order)           
            .ToList();

            EnrollResponse response = new()
            {
                EnrolledCourseId = enrolledCourse.Id,
                CourseId = course.Id,
                Title = course.Title,
                Description = course.Description,
                ImageUrl = course.ThumbnailUrl,
                Status = enrolledCourse.Status,
                Modules = dtos
            };

            return Result.Success(response);
        }

        private (List<ModuleRecord>?, Operation) GetModules(Guid courseId)
        {
            var modules = _moduleRepo.GetAll(courseId)
                .Select(module => new ModuleRecord(module.Id, module.ModuleOrder))
                .ToList();

            if (modules.Count == 0)
            {
                return (null, Operation.Failed);
            }

            return (modules, Operation.Success);
        }

        private List<ModuleRecord> AssigningOrders(List<ModuleRecord> modules)
        {
            ModuleRecord[] temp = new ModuleRecord[modules.Count];

            foreach (var module in modules)
            {
                temp[module.ModuleOrder - 1] = module;
            }

            List<ModuleRecord> newModuleList = new();
            for (var i = 0; i < temp.Length; i++)
            {
                newModuleList.Add(temp[i]);
            }

            return newModuleList;
        }
    }
}
