using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Enums;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface IMaterialGroupRepository : IRepository
    {
        Task<int> InsertAsync(MaterialGroup group);

        Task<bool> UpdateAsync(MaterialGroup group);

        Task<bool> DeleteAsync(MaterialType type, int groupId);

        Task<List<MaterialGroup>> GetAllAsync(MaterialType type);

<<<<<<< HEAD
=======
        Task<MaterialGroup> GetSiteGroupAsync(MaterialType type, int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task<MaterialGroup> GetAsync(int groupId);

        Task<bool> IsExistsAsync(MaterialType type, string groupName);
    }
}