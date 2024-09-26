namespace Identity_Application.Common.Interfaces.Services;

public interface IUserHttpContextAccessor
{
    Guid GetUserId();
    String GetUserName();

    List<string> GetUserRoles();
}
