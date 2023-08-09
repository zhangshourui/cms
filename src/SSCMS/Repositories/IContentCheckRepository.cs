using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface IContentCheckRepository : IRepository
    {

        Task InsertAsync(ContentCheck check);

<<<<<<< HEAD
=======
        Task DeleteAllAsync(int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task<List<ContentCheck>> GetCheckListAsync(int siteId, int channelId, int contentId);
    }
}