﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datory;
<<<<<<< HEAD
=======
using SqlKata;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using SSCMS.Core.Utils;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Utils;

namespace SSCMS.Core.Repositories
{
    public partial class ContentRepository
    {
        private string GetEntityKey(string tableName, int contentId)
        {
            return CacheUtils.GetEntityKey(tableName, contentId);
        }

        private string GetListKey(string tableName, int siteId, int channelId)
        {
            return CacheUtils.GetListKey(tableName ,siteId.ToString(), (Math.Abs(channelId)).ToString());
        }

<<<<<<< HEAD
=======
        public async Task ClearAllListCacheAsync(Site site)
        {
            var channelSummaries = await _channelRepository.GetSummariesAsync(site.Id);
            foreach (var channel in channelSummaries)
            {
                var tableName = await _channelRepository.GetTableNameAsync(site, channel.Id);
                var listKey = GetListKey(tableName, site.Id, channel.Id);

                var repository = await GetRepositoryAsync(site, channel);
                var cacheManager = await repository.GetCacheManagerAsync();

                cacheManager.Remove(listKey);
            }
        }

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        public async Task CacheAllListAndCountAsync(Site site, List<ChannelSummary> channelSummaries)
        {
            foreach (var channel in channelSummaries)
            {
                var tableName = await _channelRepository.GetTableNameAsync(site, channel.Id);
                var listKey = GetListKey(tableName, site.Id, channel.Id);

                var repository = await GetRepositoryAsync(site, channel);
                var cacheManager = await repository.GetCacheManagerAsync();

                if (!cacheManager.Exists(listKey))
                {
<<<<<<< HEAD
                    var summaries = await repository.GetAllAsync<ContentSummary>(Q
                        .Select(nameof(Content.Id), nameof(Content.ChannelId), nameof(Content.Checked))
                        .Where(nameof(Content.SiteId), site.Id)
                        .Where(nameof(Content.ChannelId), channel.Id)
                        .WhereNot(nameof(Content.SourceId), SourceManager.Preview)
                        .OrderByDesc(nameof(Content.Taxis), nameof(Content.Id))
                    );
=======
                    var query = GetSummariesQuery(site, channel);
                    var summaries = await repository.GetAllAsync<ContentSummary>(query);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

                    cacheManager.Put(listKey, summaries);
                }
            }
        }

        public async Task CacheAllEntityAsync(Site site, List<ChannelSummary> channelSummaries)
        {
            foreach (var channel in channelSummaries)
            {
                var contentSummaries = await GetSummariesAsync(site, channel);
                var summary = contentSummaries.FirstOrDefault();
                if (summary != null)
                {
                    var tableName = _channelRepository.GetTableName(site, channel);
                    var repository = await GetRepositoryAsync(tableName);
                    var cacheManager = await repository.GetCacheManagerAsync();

                    if (!cacheManager.Exists(GetEntityKey(tableName, summary.Id)))
                    {
                        var allContents = await repository.GetAllAsync(Q
                            .Where(nameof(Content.SiteId), site.Id)
                            .Where(nameof(Content.ChannelId), channel.Id)
                            .WhereNot(nameof(Content.SourceId), SourceManager.Preview)
                            .Limit(site.PageSize)
                            .OrderByDesc(nameof(Content.Taxis), nameof(Content.Id))
                        );

                        foreach (var content in allContents)
                        {
                            cacheManager.Put(GetEntityKey(tableName, content.Id), content);
                        }
                    }
                }
            }
        }

        public async Task<int> GetCountAsync(Site site, IChannelSummary channel)
        {
            var summaries = await GetSummariesAsync(site, channel);
            return summaries.Count;
        }

        private async Task<Content> GetAsync(string tableName, int contentId)
        {
            if (string.IsNullOrEmpty(tableName) || contentId <= 0) return null;

            var repository = await GetRepositoryAsync(tableName);
            return await repository.GetAsync(contentId, Q
                .CachingGet(GetEntityKey(tableName, contentId))
            );
        }

        public async Task<Content> GetAsync(int siteId, int channelId, int contentId)
        {
            var site = await _siteRepository.GetAsync(siteId);
            var tableName = await _channelRepository.GetTableNameAsync(site, channelId);
            return await GetAsync(tableName, contentId);
        }

        public async Task<Content> GetAsync(Site site, int channelId, int contentId)
        {
            var tableName = await _channelRepository.GetTableNameAsync(site, channelId);
            return await GetAsync(tableName, contentId);
        }

        public async Task<Content> GetAsync(Site site, Channel channel, int contentId)
        {
            var tableName = _channelRepository.GetTableName(site, channel);
            return await GetAsync(tableName, contentId);
        }

        public async Task<List<int>> GetContentIdsAsync(Site site, Channel channel)
        {
            var summaries = await GetSummariesAsync(site, channel, false);
            return summaries.Select(x => x.Id).ToList();
        }

        public async Task<List<int>> GetContentIdsCheckedAsync(Site site, Channel channel)
        {
            var summaries = await GetSummariesAsync(site, channel, false);
            return summaries.Where(x => x.Checked).Select(x => x.Id).ToList();
        }

        public async Task<List<int>> GetContentIdsAsync(Site site, Channel channel, bool isPeriods, string dateFrom, string dateTo, bool? checkedState)
        {
            var repository = await GetRepositoryAsync(site, channel);
            var query = Q
                .Select(nameof(Content.Id))
                .Where(nameof(Content.ChannelId), channel.Id)
                .OrderByDesc(nameof(Content.Taxis), nameof(Content.Id));

            if (isPeriods)
            {
                if (!string.IsNullOrEmpty(dateFrom))
                {
                    query.Where(nameof(Content.AddDate), ">=", DateUtils.ToString(dateFrom));
                }
                if (!string.IsNullOrEmpty(dateTo))
                {
                    var dateTime = TranslateUtils.ToDateTime(dateTo).AddDays(1);
                    query.Where(nameof(Content.AddDate), "<=", DateUtils.ToString(dateTime));
                }
            }

            if (checkedState.HasValue)
            {
                query.Where(nameof(Content.Checked), checkedState.Value);
            }

            return await repository.GetAllAsync<int>(query);
        }

        public async Task<List<int>> GetContentIdsByLinkTypeAsync(Site site, Channel channel, LinkType linkType)
        {
            var repository = await GetRepositoryAsync(site, channel);
            var query = Q
                .Select(nameof(Content.Id))
                .Where(nameof(Content.ChannelId), channel.Id)
                .Where(nameof(Content.LinkType), linkType.GetValue())
                .OrderByDesc(nameof(Content.Taxis), nameof(Content.Id));

            return await repository.GetAllAsync<int>(query);
        }

        public async Task<List<int>> GetChannelIdsCheckedByLastModifiedDateHourAsync(Site site, int hour)
        {
            var repository = await GetRepositoryAsync(site.TableName);

            var lastModifiedDate = DateTime.Now.AddHours(-hour);
            return await repository.GetAllAsync<int>(Q
                .Select(nameof(Content.ChannelId))
                .Where(nameof(Content.SiteId), site.Id)
                .WhereTrue(nameof(Content.Checked))
                .Where(nameof(Content.LastModifiedDate), ">=", DateUtils.ToString(lastModifiedDate))
            );
        }

        public async Task<List<ContentSummary>> GetSummariesAsync(Site site, Channel channel, bool isAllContents)
        {
            if (isAllContents)
            {
                var list = new List<ContentSummary>();
                var channelIds = await _channelRepository.GetChannelIdsAsync(site.Id, channel.Id, ScopeType.All);
                foreach (var channelId in channelIds)
                {
                    var child = await _channelRepository.GetAsync(channelId);
                    list.AddRange(await GetSummariesAsync(site, child));
                }

                return list;
            }

            return await GetSummariesAsync(site, channel);
        }

        public async Task<List<ContentSummary>> GetSummariesAsync(Site site, IChannelSummary channel)
        {
            if (site == null || channel == null) return new List<ContentSummary>();

            var repository = await GetRepositoryAsync(site, channel);
<<<<<<< HEAD
            var query = Q.Select(
=======
            var query = GetSummariesQuery(site, channel);
            query.CachingGet(GetListKey(repository.TableName, site.Id, channel.Id));

            return await repository.GetAllAsync<ContentSummary>(query);
        }

        private static Query GetSummariesQuery(Site site, IChannelSummary channel)
        {
          var query = Q.Select(
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
              nameof(Content.Id), 
              nameof(Content.ChannelId), 
              nameof(Content.Checked), 
              nameof(Content.CheckedLevel)
            );

<<<<<<< HEAD
            await QueryWhereAsync(query, site, channel.Id, false);

            query.OrderByDesc(nameof(Content.Taxis), nameof(Content.Id));

            query.CachingGet(GetListKey(repository.TableName, site.Id, channel.Id));

            return await repository.GetAllAsync<ContentSummary>(query);
=======
            query
              .Where(nameof(Content.SiteId), site.Id)
              .Where(nameof(Content.ChannelId), channel.Id)
              .WhereNot(nameof(Content.SourceId), SourceManager.Preview);

            if (site.TaxisType == TaxisType.OrderByTitle)
            {
                query
                  .Select(nameof(Content.Title))
                  .OrderBy(nameof(Content.Title))
                  .OrderByDesc(nameof(Content.Taxis), nameof(Content.Id));
            }
            else if (site.TaxisType == TaxisType.OrderByTitleDesc)
            {
                query
                  .Select(nameof(Content.Title))
                  .OrderByDesc(nameof(Content.Title), nameof(Content.Taxis), nameof(Content.Id));
            }
            else if (site.TaxisType == TaxisType.OrderByAddDate)
            {
                query
                  .Select(nameof(Content.AddDate))
                  .OrderBy(nameof(Content.AddDate))
                  .OrderByDesc(nameof(Content.Taxis), nameof(Content.Id));
            }
            else if (site.TaxisType == TaxisType.OrderByAddDateDesc)
            {
                query
                  .Select(nameof(Content.AddDate))
                  .OrderByDesc(nameof(Content.AddDate), nameof(Content.Taxis), nameof(Content.Id));
            }
            else
            {
                query.OrderByDesc(nameof(Content.Taxis), nameof(Content.Id));
            }

            return query;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        }
    }
}
