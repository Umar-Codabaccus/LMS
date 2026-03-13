using LMS.Api.Shared;

namespace LMS.Api.Application.AuthServices.Register;

/// <summary>
/// Not necessary to pass a Type field  for validation errors since the error object is mutated inside the ValidationErrors object
/// </summary>
public static class RegistrationErrors
{
    public static Error FirstnameRequired()
        => new Error()
        {
            Code = nameof(FirstnameRequired),
            Message = "Firstname is required"
        };

    public static Error FirstnameOutOfRange()
        => new Error()
        {
            Code = nameof(FirstnameOutOfRange),
            Message = "Firstname should be less than 100 characters"
        };

    public static Error LastnameRequired()
        => new Error()
        {
            Code = nameof(LastnameRequired),
            Message = "Lastname is required"
        };

    public static Error LastnameOutOfRange()
        => new Error()
        {
            Code = nameof(LastnameOutOfRange),
            Message = "Lastname should be less than 100 characters"
        };

    public static Error EmailRequired()
       => new Error()
       {
           Code = nameof(EmailRequired),
           Message = "Email is required"
       };

    public static Error InvalidEmailFormat()
        => new Error()
        {
            Code = nameof(InvalidEmailFormat),
            Message = "Invalid email format"
        };

    public static Error PasswordRequired()
       => new Error()
       {
           Code = nameof(PasswordRequired),
           Message = "Email is required"
       };

    public static Error PasswordTooShort()
        => new Error()
        {
            Code = nameof(PasswordTooShort),
            Message = "Password should be at least 8 characters long"
        };

    public static Error PasswordMissingUppercase()
        => new Error()
        {
            Code = nameof(PasswordMissingUppercase),
            Message = "Password shoud contain at least 1 uppercase character."
        };

    public static Error PasswordMissingLowercase()
        => new Error()
        {
            Code = nameof(PasswordMissingLowercase),
            Message = "Password should contain at least 1 lowercase character."
        };

    public static Error PasswordMissingNumber()
        => new Error()
        {
            Code = nameof(PasswordMissingNumber),
            Message = "Password should containat least one number"
        };
}
