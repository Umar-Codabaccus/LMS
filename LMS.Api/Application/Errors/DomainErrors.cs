using LMS.Api.Shared;

namespace LMS.Api.Application.Errors
{
    public static class DomainErrors
    {
        public static class AuthErrors
        {
            public static string AccountDoesNotExist()
                => nameof(AccountDoesNotExist);

            public static string WrongPassword()
                => nameof(WrongPassword);

            public static string AccountAlreadyExists()
                => nameof(AccountAlreadyExists);

            public static string RegistrationFailed()
                => nameof(RegistrationFailed);
        }
        public static class UserErrors
        {
            public static string NoUsers()
                => nameof(NoUsers);

            public static string CouldNotUpdateUser()
                => nameof(CouldNotUpdateUser);

            public static string CouldNotDeleteUser()
                => nameof(CouldNotDeleteUser);
        }
        public static class CourseErrors
        {
            public static string CourseCreationFailed()
                => nameof(CourseCreationFailed);

            public static string UpdatingCourseFailed()
                => nameof(UpdatingCourseFailed);

            public static string CourseDeletionFailed()
                => nameof(CourseDeletionFailed);

            public static string CoursePublicationFailed()
                => nameof(CoursePublicationFailed);

            public static string CourseAlreadyPublished()
                => nameof(CourseAlreadyPublished);
            public static string CourseCannotBePublishedDueToNoModulesPresent()
                => "Course cannot be published due to no modules present";

            public static string NoCourses()
                => nameof(NoCourses);
        }
        public static class ModuleErrors
        {
            public static string ModuleDoesNotExist()
                => "ERROR: Module does not exist.";
            public static string ModuleCreationFailed()
                => nameof(ModuleCreationFailed);
            public static string UpdatingModuleFailed()
                => nameof(UpdatingModuleFailed);
            public static string ModuleDeletionFailed()
                => nameof(ModuleDeletionFailed);
            public static string CannotDeleteModuleDueToCourseAlreadyPublished()
                => "Cannot delete module due to course already published";
            public static string NoModules()
                => nameof(NoModules);
            public static string CourseDoesNotExist()
                => nameof(CourseDoesNotExist);
            public static string ModuleReassignmentDuringDeletionFailed()
                => "Module reassignment during deletion failed";
        }
        public static class EnrollmentErrors
        {
            public static Error EnrollmentFailed()
                => new Error() { Code = $"Enrollment.{nameof(EnrollmentFailed)}", Message = "Enrollmnent failed" };

            public static Error UserAlreadyEnrolled()
                => new Error() { Code = $"Enrollment.{nameof(UserAlreadyEnrolled)}", Message = "User is already enrolled."};

            public static Error FetchingModulesFailed()
                => new Error() { Code = $"Enrollment.{nameof(FetchingModulesFailed)}", Message = "Modules of enrolled course could not be fetched."};

            public static Error ModulesCouldNotBeEnrolled()
                => new Error()
                {
                    Code = $"Enrollment.{nameof(ModulesCouldNotBeEnrolled)}",
                    Message = "Modules could not be enrolled."
                };
        }
    }
}
