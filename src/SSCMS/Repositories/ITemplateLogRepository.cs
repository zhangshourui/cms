using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface ITemplateLogRepository : IRepository
    {
        Task InsertAsync(TemplateLog log);

        Task<string> GetTemplateContentAsync(int logId);

        Task<List<KeyValuePair<int, string>>> GetLogIdWithNameListAsync(int siteId, int templateId);

        Task DeleteAsync(int logId);
<<<<<<< HEAD
=======

        Task DeleteAllAsync(int siteId);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }
}