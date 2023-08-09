using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Models;
using SSCMS.Repositories;
using SSCMS.Services;
using SSCMS.Utils;

namespace SSCMS.Core.Repositories
{
    public class RelatedFieldRepository : IRelatedFieldRepository
    {
        private readonly Repository<RelatedField> _repository;

        public RelatedFieldRepository(ISettingsManager settingsManager)
        {
            _repository = new Repository<RelatedField>(settingsManager.Database, settingsManager.Redis);
        }

        public IDatabase Database => _repository.Database;

        public string TableName => _repository.TableName;

        public List<TableColumn> TableColumns => _repository.TableColumns;

        public async Task<int> InsertAsync(RelatedField relatedField)
        {
            return await _repository.InsertAsync(relatedField);
        }

        public async Task<bool> UpdateAsync(RelatedField relatedField)
        {
            return await _repository.UpdateAsync(relatedField);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

<<<<<<< HEAD
=======
        public async Task DeleteAllAsync(int siteId)
        {
            await _repository.DeleteAsync(Q
                .Where(nameof(RelatedField.SiteId), siteId)
            );
        }

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        public async Task<RelatedField> GetAsync(int siteId, string title)
        {
            return await _repository.GetAsync(Q
                .Where(nameof(RelatedField.SiteId), siteId)
                .Where(nameof(RelatedField.Title), title)
            );
        }

        public async Task<RelatedField> GetAsync(int siteId, int relatedFieldId)
        {
            return await _repository.GetAsync(Q
                .Where(nameof(RelatedField.SiteId), siteId)
                .Where(nameof(RelatedField.Id), relatedFieldId)
            );
        }

        public async Task<List<RelatedField>> GetRelatedFieldsAsync(int siteId)
        {
            return await _repository.GetAllAsync(Q
                .Where(nameof(RelatedField.SiteId), siteId)
                .OrderBy(nameof(RelatedField.Id)));
        }

        public async Task<string> GetImportTitleAsync(int siteId, string relatedFieldName)
        {
            string importName;
            if (relatedFieldName.IndexOf("_", StringComparison.Ordinal) != -1)
            {
                var lastName = relatedFieldName.Substring(relatedFieldName.LastIndexOf("_", StringComparison.Ordinal) + 1);
                var firstName = relatedFieldName.Substring(0, relatedFieldName.Length - lastName.Length);
                var relatedFieldNameCount = TranslateUtils.ToInt(lastName);
                relatedFieldNameCount++;
                importName = firstName + relatedFieldNameCount;
            }
            else
            {
                importName = relatedFieldName + "_1";
            }

            var relatedField = await GetAsync(siteId, relatedFieldName);
            if (relatedField != null)
            {
                importName = await GetImportTitleAsync(siteId, importName);
            }

            return importName;
        }
	}
}