using LMS.Api.Domain.Entities;

namespace LMS.Api.Application;

public interface ITokenProvider
{
    public string Create(User user, UserEmail email, UserAccount account);
}
