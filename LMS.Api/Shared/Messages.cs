namespace LMS.Api.Shared
{
    public static class Messages
    {
        public static class EnrolledMessages
        {
            public static string Exist()
                => nameof(Exist);

            public static string DoNotExist()
                => nameof(DoNotExist);
        }

        public static class Failures
        {
            public static string DatabaseFailure(Exception e, string location)
                => $"ERROR: {e}, {location}";
        }
    }
}
