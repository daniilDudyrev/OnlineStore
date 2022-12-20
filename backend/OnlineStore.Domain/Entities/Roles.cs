namespace OnlineStore.Domain.Entities;

public static class Roles
{
    public const string User = "User";
    public const string Admin = "Admin";

    public static class Defaults
    {
        public static string[] Users { get; } = { User };
    }
}