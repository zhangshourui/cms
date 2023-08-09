using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface IWxReplyMessageRepository : IRepository
    {
        Task<int> InsertAsync(WxReplyMessage message);

        Task UpdateAsync(WxReplyMessage message);

        Task DeleteAllAsync(int siteId, int ruleId);

<<<<<<< HEAD
=======
        Task DeleteAllAsync(int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task<List<WxReplyMessage>> GetMessagesAsync(int siteId, int ruleId);

        Task<WxReplyMessage> GetMessageAsync(int siteId, int messageId);
    }
}
