using System.Linq;
using System.Threading.Tasks;
using SSCMS.Models;

namespace SSCMS.Core.Repositories
{
    public partial class UserGroupRepository
    {
        public async Task<bool> IsExistsAsync(string groupName)
        {
            var list = await GetUserGroupsAsync();
            return list.Any(group => group.GroupName == groupName);
        }

<<<<<<< HEAD
=======
        public async Task<int> GetGroupIdAsync(string groupName)
        {
            var list = await GetUserGroupsAsync();
            var item = list.FirstOrDefault(group => group.GroupName == groupName);
            return item == null ? 0 : item.Id;
        }

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        public async Task<UserGroup> GetUserGroupAsync(int groupId)
        {
            var list = await GetUserGroupsAsync();
            return list.FirstOrDefault(group => group.Id == groupId) ?? list[0];
        }

        public async Task<string> GetUserGroupNameAsync(int groupId)
        {
            return (await GetUserGroupAsync(groupId)).GroupName;
        }
	}
}
