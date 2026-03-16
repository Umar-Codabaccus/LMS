using LMS.Api.Application.DTOs;
using LMS.Api.Application.DTOs.Module;

namespace LMS.Api.Application.McpContracts;

public sealed record InstructorInfo(List<CourseDto> Courses);
public sealed record CourseInfo(CourseDto Course, string Instructor, InstructorInfo InstructorInfo, List<ModuleDto> Modules);
