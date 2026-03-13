using LMS.Api.Shared;
using System.Net.Mail;

namespace LMS.Api.Application.AuthServices.Register;

public static class RegisterUserValidator
{
    public static ValidationErrors Validate(RegisterUserRequest request)
    {
        ValidationErrors errors = new();

        // FIRSTNAME
        if (string.IsNullOrWhiteSpace(request.Firstname))
            errors.Add(RegistrationErrors.FirstnameRequired());

        if (!string.IsNullOrWhiteSpace(request.Firstname) && request.Firstname.Length > 100)
            errors.Add(RegistrationErrors.FirstnameOutOfRange());


        // LASTNAME
        if (string.IsNullOrWhiteSpace(request.Lastname))
            errors.Add(RegistrationErrors.LastnameRequired());

        if (!string.IsNullOrWhiteSpace(request.Lastname) && request.Lastname.Length > 100)
            errors.Add(RegistrationErrors.LastnameOutOfRange());


        // EMAIL
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors.Add(RegistrationErrors.EmailRequired());
        }
        else
        {
            try
            {
                var mailAddress = new MailAddress(request.Email);

                if (mailAddress.Address != request.Email)
                    errors.Add(RegistrationErrors.InvalidEmailFormat());
            }
            catch
            {
                errors.Add(RegistrationErrors.InvalidEmailFormat());
            }
        }


        // PASSWORD
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            errors.Add(RegistrationErrors.PasswordRequired());
        }
        else
        {
            if (request.Password.Length < 8)
                errors.Add(RegistrationErrors.PasswordTooShort());

            if (!request.Password.Any(char.IsUpper))
                errors.Add(RegistrationErrors.PasswordMissingUppercase());

            if (!request.Password.Any(char.IsLower))
                errors.Add(RegistrationErrors.PasswordMissingLowercase());

            if (!request.Password.Any(char.IsDigit))
                errors.Add(RegistrationErrors.PasswordMissingNumber());
        }

        return errors;
    }
}
