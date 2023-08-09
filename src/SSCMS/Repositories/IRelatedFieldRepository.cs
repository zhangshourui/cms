using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface IRelatedFieldRepository : IRepository
    {
        Task<int> InsertAsync(RelatedField relatedField);

        Task<bool> UpdateAsync(RelatedField relatedField);

        Task DeleteAsync(int id);

<<<<<<< HEAD
=======
        Task DeleteAllAsync(int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task<RelatedField> GetAsync(int siteId, string title);

        Task<RelatedField> GetAsync(int siteId, int relatedFieldId);

        Task<List<RelatedField>> GetRelatedFieldsAsync(int siteId);

        Task<string> GetImportTitleAsync(int siteId, string relatedFieldName);
    }
}