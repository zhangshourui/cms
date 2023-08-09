using System.Threading.Tasks;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public partial interface IUserGroupRepository
    {
        Task<bool> IsExistsAsync(string groupName);

<<<<<<< HEAD
=======
        Task<int> GetGroupIdAsync(string groupName);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task<UserGroup> GetUserGroupAsync(int groupId);

        Task<string> GetUserGroupNameAsync(int groupId);
    }
}
