using System.Threading.Tasks;
using Datory;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public partial interface IRelatedFieldItemRepository : IRepository
    {
        Task<int> InsertAsync(RelatedFieldItem info);

        Task<bool> UpdateAsync(RelatedFieldItem info);

        Task DeleteAsync(int siteId, int id);

<<<<<<< HEAD
=======
        Task DeleteAllAsync(int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task UpdateTaxisToDownAsync(int siteId, int relatedFieldId, int id, int parentId);

        Task UpdateTaxisToUpAsync(int siteId, int relatedFieldId, int id, int parentId);
    }
}