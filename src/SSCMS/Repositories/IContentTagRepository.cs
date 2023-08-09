using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface IContentTagRepository : IRepository
    {
        Task InsertAsync(int siteId, string tagName);

        Task InsertAsync(ContentTag tag);

        Task DeleteAsync(int siteId, string tagName);

<<<<<<< HEAD
        Task DeleteAsync(int siteId);
=======
        Task DeleteAllAsync(int siteId);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

        Task UpdateTagsAsync(List<string> previousTags, List<string> nowTags, int siteId, int contentId);

        Task<List<string>> GetTagNamesAsync(int siteId);

        Task<List<ContentTag>> GetTagsAsync(int siteId);

        List<ContentTag> GetTagsByLevel(List<ContentTag> tagInfoList, int totalNum, int tagLevel);
    }
}