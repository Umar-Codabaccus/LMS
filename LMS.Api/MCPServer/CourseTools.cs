using LMS.Api.Application;
using LMS.Api.Application.DTOs;
using LMS.Api.Application.DTOs.Module;
using LMS.Api.Application.McpContracts;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json;


namespace LMS.Api.MCPServer;

[McpServerToolType]
public static class CourseTools
{
    [McpServerTool, Description("Get course information for a course title")]
    public static async Task<CourseInfo> GetCourseInfo(IAppDbContext context, string title)
    {
        var course = await context.Courses
            .FirstOrDefaultAsync(c => c.Title == title);
        var instructor = await context.Users
            .FirstOrDefaultAsync(u => u.Id == course.InstructorId);
        var modules = await context.Modules
            .Where(m => m.CourseId == course.Id)
            .ToListAsync();

        List<CourseDto> instructorCourses = context.Courses
            .Where(c => c.InstructorId == instructor.Id)
            .Select(c => new CourseDto
            {
                Title = c.Title,
                Description = c.Description,
                Level = c.Level,
                ThumbnailUrl = c.ThumbnailUrl
            })
            .ToList();

        var instructorInfo = new InstructorInfo(instructorCourses);

        List<ModuleDto> moduleDtos = modules
            .Select(m => new ModuleDto
            {
                Title = m.Title,
                Description = m.Description,
                VideoUrl = m.VideoUrl
            })
            .ToList();

        var courseDto = new CourseDto
        {
            Title = course.Title,
            Description = course.Description,
            Level = course.Level,
            ThumbnailUrl = course.ThumbnailUrl
        };

        var courseInfo = new CourseInfo(
            courseDto,
            instructor.Firstname + " " + instructor.Lastname,
            instructorInfo,
            moduleDtos
            );

        return courseInfo;
    }
}
