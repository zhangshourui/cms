using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface IWxReplyKeywordRepository : IRepository
    {
        Task<int> InsertAsync(WxReplyKeyword keyword);

        Task UpdateAsync(WxReplyKeyword keyword);

        Task DeleteAllAsync(int siteId, int ruleId);

<<<<<<< HEAD
=======
        Task DeleteAllAsync(int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task<List<WxReplyKeyword>> GetKeywordsAsync(int siteId, int ruleId);

        Task<List<WxReplyKeyword>> GetKeywordsAsync(int siteId);
    }
}
