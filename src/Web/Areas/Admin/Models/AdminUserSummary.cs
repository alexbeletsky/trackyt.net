
namespace Web.Areas.Admin.Models
{
    public class AdminUserSummary
    {
        public AdminUserSummary(int totalUsersCount, int tempUsersCount)
        {
            TotalRegisteredUsers = totalUsersCount;
            TempUsers = tempUsersCount;
        }

        public int TotalRegisteredUsers { private set; get; }
        public int TempUsers { private set; get; }
    }
}