using LMS.Api.Application.Abstractions.Messaging;

namespace LMS.Api.Application.Registration.Learner;

public sealed record RegisterLearnerCommand(string Firstname, string Lastname, string Email, string Password) : ICommand<string>;
