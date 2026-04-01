using LMS.Api.Application.Abstractions.Messaging;
using LMS.Api.Application.Utilities;
using LMS.Api.Domain.Entities;
using LMS.Api.Infrastructure.Authentication;
using LMS.Api.Shared;
using Microsoft.EntityFrameworkCore;

namespace LMS.Api.Application.Registration.Learner;

public sealed class RegisterLearnerCommandHandler : ICommandHandler<RegisterLearnerCommand, string>
{
    private readonly IAppDbContext _context;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterLearnerCommandHandler(
        IAppDbContext context,
        ITokenProvider tokenProvider,
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<string>> Handle(RegisterLearnerCommand command, CancellationToken cancellationToken)
    {
        // existence check
        var userExist = await _context.UserEmails.AnyAsync(u => u.Email == command.Email, cancellationToken);

        if (userExist)
        {
            // return user already exists error
            return string.Empty;
        }

        var context = _context as DbContext;

        if (context is null)
        {
            throw new InvalidOperationException("IAppDbContext must be a DbContext in order to use transactions");
        }

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // create a user object
            var userResult = User.Create(
                command.Firstname,
                command.Lastname);

            if (userResult.IsFailure)
            {
                // return error
                return string.Empty;
            }

            var user = userResult.Value;

            // Hash Password
            var hashedPassword = _passwordHasher.HashPassword(command.Password);

            // create a user email object
            var userEmailResult = UserEmail.Create(
                user.Id,
                command.Email,
                hashedPassword);

            if (userEmailResult.IsFailure)
            {
                // return error
                return string.Empty;
            }

            var userEmail = userEmailResult.Value;

            // MOCK: organizationId -- refactor later to fetch the default org id from cache or database
            var organizationId = Guid.NewGuid();

            // create user account
            var userAccountResult = UserAccount.Create(
                organizationId,
                userEmail.Id,
                UserAccountType.DefaultUserAccount.ToString());

            if (userAccountResult.IsFailure)
            {
                // return error
                return string.Empty;
            }

            var userAccount = userAccountResult.Value;

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.UserEmails.AddAsync(userEmail, cancellationToken);
            await _context.UserAccounts.AddAsync(userAccount, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            var token = _tokenProvider.Create(user, userEmail, userAccount);

            return token;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return string.Empty;
        }
    }
}