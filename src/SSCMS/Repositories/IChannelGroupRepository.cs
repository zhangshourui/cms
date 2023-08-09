using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public partial interface IChannelGroupRepository : IRepository
    {
        Task InsertAsync(ChannelGroup group);

        Task UpdateAsync(ChannelGroup group);

        Task DeleteAsync(int siteId, string groupName);

<<<<<<< HEAD
=======
        Task DeleteAllAsync(int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task UpdateTaxisDownAsync(int siteId, int groupId, int taxis);

        Task UpdateTaxisUpAsync(int siteId, int groupId, int taxis);

        Task<ChannelGroup> GetAsync(int siteId, int groupId);

        Task<List<ChannelGroup>> GetChannelGroupsAsync(int siteId);
    }
}