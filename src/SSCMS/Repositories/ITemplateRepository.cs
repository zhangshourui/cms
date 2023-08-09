using System.Threading.Tasks;
using Datory;
using SSCMS.Models;
using SSCMS.Services;

namespace SSCMS.Repositories
{
    public partial interface ITemplateRepository : IRepository
    {
        Task<int> InsertAsync(Template template);

        Task UpdateAsync(Template template);

        Task SetDefaultAsync(int templateId);

        Task DeleteAsync(IPathManager pathManager, Site site, int templateId);

<<<<<<< HEAD
=======
        Task DeleteAllAsync(int siteId);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task CreateDefaultTemplateAsync(int siteId);
    }
}