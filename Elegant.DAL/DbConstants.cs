using Elegant.Abstraction.Settings;

namespace Elegant.DAL;

public abstract class DbConstants : ISettings
{
    public const int UserId = 0;
    public const string AdminRoleName = "Admin";
    public const string UserRoleName = "User";
    public const string ConnectionString = "online_shop";

}