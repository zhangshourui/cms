using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;
using SSCMS.Repositories;
using SSCMS.Services;

namespace SSCMS.Core.Repositories
{
<<<<<<< HEAD
	public class ContentCheckRepository : IContentCheckRepository
=======
    public class ContentCheckRepository : IContentCheckRepository
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    {
        private readonly Repository<ContentCheck> _repository;

        public ContentCheckRepository(ISettingsManager settingsManager)
        {
            _repository = new Repository<ContentCheck>(settingsManager.Database, settingsManager.Redis);
        }

        public IDatabase Database => _repository.Database;

        public string TableName => _repository.TableName;

        public List<TableColumn> TableColumns => _repository.TableColumns;

        public async Task InsertAsync(ContentCheck check)
        {
            await _repository.InsertAsync(check);
        }

<<<<<<< HEAD
		public async Task<List<ContentCheck>> GetCheckListAsync(int siteId, int channelId, int contentId)
		{
=======
        public async Task DeleteAllAsync(int siteId)
        {
            await _repository.DeleteAsync(Q
                .Where(nameof(ContentCheck.SiteId), siteId)
            );
        }

        public async Task<List<ContentCheck>> GetCheckListAsync(int siteId, int channelId, int contentId)
        {
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            return await _repository.GetAllAsync(Q
                .Where(nameof(ContentCheck.SiteId), siteId)
                .Where(nameof(ContentCheck.ChannelId), channelId)
                .Where(nameof(ContentCheck.ContentId), contentId)
                .OrderByDesc(nameof(ContentCheck.Id))
            );
<<<<<<< HEAD
		}
	}
=======
        }
    }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
}