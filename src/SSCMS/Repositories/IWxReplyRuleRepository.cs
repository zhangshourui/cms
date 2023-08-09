using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface IWxReplyRuleRepository : IRepository
    {
        Task<int> InsertAsync(WxReplyRule rule);

        Task UpdateAsync(WxReplyRule rule);

        Task DeleteAsync(int ruleId);

<<<<<<< HEAD
=======
        Task DeleteAllAsync(int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task<int> GetCount(int siteId, string keyword);

        Task<List<WxReplyRule>> GetRulesAsync(int siteId, string keyword, int page, int perPage);

        Task<WxReplyRule> GetAsync(int ruleId);
    }
}
