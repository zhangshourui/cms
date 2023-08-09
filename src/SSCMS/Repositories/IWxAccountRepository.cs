using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface IWxAccountRepository : IRepository
    {
        Task SetAsync(WxAccount account);

<<<<<<< HEAD
        Task DeleteBySiteIdAsync(int siteId);
=======
        Task DeleteAllAsync(int siteId);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

        Task<WxAccount> GetBySiteIdAsync(int siteId);
    }
}