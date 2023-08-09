using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface ISitePermissionsRepository : IRepository
    {
        Task InsertAsync(SitePermissions permissions);

        Task DeleteAsync(string roleName);

<<<<<<< HEAD
=======
        Task DeleteAllAsync(int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task<List<SitePermissions>> GetAllAsync(string roleName);

        Task<SitePermissions> GetAsync(string roleName, int siteId);

        Task<Dictionary<int, List<string>>> GetSitePermissionDictionaryAsync(IEnumerable<string> roles);

        Task<Dictionary<string, List<string>>> GetContentPermissionDictionaryAsync(IList<string> roles);
    }
}
