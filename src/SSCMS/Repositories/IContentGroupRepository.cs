using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public partial interface IContentGroupRepository : IRepository
    {
        Task InsertAsync(ContentGroup group);

        Task UpdateAsync(ContentGroup group);

        Task DeleteAsync(int siteId, string groupName);

<<<<<<< HEAD
        Task DeleteAsync(int siteId);
=======
        Task DeleteAllAsync(int siteId);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

        Task UpdateTaxisDownAsync(int siteId, int groupId, int taxis);

        Task UpdateTaxisUpAsync(int siteId, int groupId, int taxis);

        Task<ContentGroup> GetAsync(int siteId, int groupId);

        Task<List<ContentGroup>> GetContentGroupsAsync(int siteId);
    }
}