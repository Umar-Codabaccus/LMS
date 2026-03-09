namespace LMS.Api.Shared
{
    public sealed class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public ErrorType Type { get; set; }
        public string errType => Type.ToString();

        public static readonly Error None
            = new Error() { Code = string.Empty, Message = string.Empty, Type = ErrorType.None };

        public static readonly Error NullValue 
            = new()
        {
            Code = "General.Null",
            Message = "Null value was provided"
        };
    }
}
