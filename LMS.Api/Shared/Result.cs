namespace LMS.Api.Shared
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure { get; set; }
        public bool IsValidationError { get; set; }
        public Error Error { get; set; }
        public ValidationErrors Errors { get; set; }

        public Result(bool isSuccess, Error error)
        {
            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            IsFailure = !isSuccess;
            IsValidationError = false;
            Error = error;
        }

        public Result(bool isSuccess, ValidationErrors errors)
        {
            IsSuccess = isSuccess;
            IsFailure = !isSuccess;
            IsValidationError = IsFailure;
            Errors = errors;
            Error = Error.ValidationError;
        }

        public static Result Success()
            => new(true, Error.None);

        public static Result Failure(Error error)
            => new(false, error);

        public static Result<TValue> Success<TValue>(TValue? value)
            => new(value, true, Error.None);

        public static Result<TValue> Failure<TValue>(Error error)
            => new(default, false, error);

        public static Result Failure(ValidationErrors errors)
            => new(false, errors);
        public static Result<TValue> Failure<TValue>(ValidationErrors errors)
            => new(default, false, errors);
    }

    public class Result<TValue> : Result
    {
        public TValue? Value { get; set; }

        public Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Value = value;
        }

        public Result(TValue value, bool isSuccess, ValidationErrors errors) : base(isSuccess, errors)
        {
            Value = value;
        }

        //public static Result<TValue> Success<TValue>(TValue? value)
        //    => new(value, true, Error.None);

        //public static Result<TValue> Failure<TValue>(Error error)
        //    => new(default, false, error);

        public static implicit operator Result<TValue>(TValue? value) =>
            value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }
}
