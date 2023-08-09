﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SSCMS.Enums;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public partial interface IContentRepository
    {
<<<<<<< HEAD
=======
        Task ClearAllListCacheAsync(Site site);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task CacheAllListAndCountAsync(Site site, List<ChannelSummary> channelSummaries);

        Task CacheAllEntityAsync(Site site, List<ChannelSummary> channelSummaries);

        Task<int> GetCountAsync(Site site, IChannelSummary channel);

        Task<Content> GetAsync(int siteId, int channelId, int contentId);

        Task<Content> GetAsync(Site site, int channelId, int contentId);

        Task<Content> GetAsync(Site site, Channel channel, int contentId);

        Task<List<int>> GetContentIdsAsync(Site site, Channel channel);

        Task<List<int>> GetContentIdsCheckedAsync(Site site, Channel channel);

        Task<List<int>> GetContentIdsAsync(Site site, Channel channel, bool isPeriods, string dateFrom, string dateTo, bool? checkedState);

        Task<List<int>> GetContentIdsByLinkTypeAsync(Site site, Channel channel, LinkType linkType);

        Task<List<int>> GetChannelIdsCheckedByLastModifiedDateHourAsync(Site site, int hour);

        Task<List<ContentSummary>> GetSummariesAsync(Site site, Channel channel, bool isAllContents);

        Task<List<ContentSummary>> GetSummariesAsync(Site site, IChannelSummary channel);
    }
}
