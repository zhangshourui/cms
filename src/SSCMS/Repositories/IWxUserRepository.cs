using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface IWxUserRepository : IRepository
    {
        Task<int> InsertAsync(int siteId, WxUser user);

        Task UpdateAllAsync(int siteId, List<WxUser> users);

        Task DeleteAllAsync(int siteId, List<string> openIds);

<<<<<<< HEAD
=======
        Task DeleteAllAsync(int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task<(int Total, int Count, List<string> Results)> GetPageOpenIds(int siteId, int tagId, string keyword, int page, int perPage);

        Task<List<string>> GetAllOpenIds(int siteId);
    }
}