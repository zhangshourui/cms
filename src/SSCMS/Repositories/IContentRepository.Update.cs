﻿using System.Threading.Tasks;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public partial interface IContentRepository
    {
        Task UpdateHitsAsync(int siteId, int channelId, int contentId, int hits);

        Task UpdateAsync(Site site, Channel channel, Content content);

        Task UpdateAsync(Content content);

        Task SetAutoPageContentToSiteAsync(Site site);

        Task UpdateArrangeTaxisAsync(Site site, Channel channel, string attributeName, bool isDesc);

<<<<<<< HEAD
=======
        Task SetTaxisAsync(Site site, Channel channel, int contentId, bool isTop, int order);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Task<bool> SetTaxisToUpAsync(Site site, Channel channel, int contentId, bool isTop);

        Task<bool> SetTaxisToDownAsync(Site site, Channel channel, int contentId, bool isTop);

        Task AddDownloadsAsync(string tableName, int channelId, int contentId);
    }
}