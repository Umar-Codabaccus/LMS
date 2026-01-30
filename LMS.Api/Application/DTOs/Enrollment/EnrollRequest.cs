namespace LMS.Api.Application.DTOs.Enrollment;

public sealed record EnrollRequest(Guid UserId, Guid CourseId);
