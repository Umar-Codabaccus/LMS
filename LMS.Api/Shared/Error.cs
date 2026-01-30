namespace LMS.Api.Shared
{
    public sealed class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public static readonly Error None
            = new Error() { Code = string.Empty, Message = string.Empty };

        public static readonly Error NullValue 
            = new()
        {
            Code = "General.Null",
            Message = "Null value was provided"
        };
    }
}
