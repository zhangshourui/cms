﻿using System.Threading.Tasks;
using Datory;
using SSCMS.Core.Utils;
using SSCMS.Enums;
using SSCMS.Models;

namespace SSCMS.Core.Repositories
{
    public partial class ContentRepository
    {
        private async Task<int> SyncTaxisAsync(Site site, Channel channel, Content content)
        {
            var taxis = content.Taxis;
            var updateHigher = false;

            var repository = await GetRepositoryAsync(site, channel);

            var query = Q
                .Where(nameof(Content.ChannelId), channel.Id)
                .WhereNot(nameof(Content.SourceId), SourceManager.Preview)
                .Where(nameof(Content.Taxis), content.Top ? ">=" : "<", TaxisIsTopStartValue);

            if (channel.DefaultTaxisType == TaxisType.OrderByAddDate)
            {
                if (content.AddDate.HasValue)
                {
                    taxis = (await repository.MaxAsync(nameof(Content.Taxis), query.Clone()
                                 .Where(nameof(Content.AddDate), ">", DateUtils.ToString(content.AddDate))
                             ) ?? 0) + 1;
                    updateHigher = true;
                }
            }
            else if (channel.DefaultTaxisType == TaxisType.OrderByAddDateDesc)
            {
                if (content.AddDate.HasValue)
                {
                    taxis = (await repository.MaxAsync(nameof(Content.Taxis), query.Clone()
                                 .Where(nameof(Content.AddDate), "<", DateUtils.ToString(content.AddDate))
                             ) ?? 0) + 1;
                    updateHigher = true;
                }
            }
            else if (channel.DefaultTaxisType == TaxisType.OrderByTaxis)
            {
                taxis = 1;
                updateHigher = true;
            }
            else
            {
                if (content.Top == false && content.Taxis >= TaxisIsTopStartValue)
                {
                    taxis = await GetMaxTaxisAsync(site, channel, false) + 1;
                }
                else if (content.Top && content.Taxis < TaxisIsTopStartValue)
                {
                    taxis = await GetMaxTaxisAsync(site, channel, true) + 1;
                }

                if (taxis == 0)
                {
                    taxis = (await repository.MaxAsync(nameof(Content.Taxis), query.Clone()) ?? 0) + 1;
                }
            }

            if (content.Top && taxis < TaxisIsTopStartValue)
            {
                taxis += TaxisIsTopStartValue;
            }

            if (updateHigher)
            {
                await repository.IncrementAsync(nameof(Content.Taxis), query
                    .Where(nameof(Content.Taxis), ">=", taxis)
                );
            }

            return taxis;
        }

        public async Task<int> InsertAsync(Site site, Channel channel, Content content)
        {
<<<<<<< HEAD
=======
            if (channel.IsChangeBanned) return 0;

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            var taxis = 0;
            if (content.SourceId == SourceManager.Preview)
            {
                channel.IsPreviewContentsExists = true;
                await _channelRepository.UpdateAsync(channel);
            }
            else
            {
                taxis = await SyncTaxisAsync(site, channel, content);
            }
            return await InsertWithTaxisAsync(site, channel, content, taxis);
        }

        public async Task<int> InsertPreviewAsync(Site site, Channel channel, Content content)
        {
<<<<<<< HEAD
=======
             if (channel.IsChangeBanned) return 0;
             
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            channel.IsPreviewContentsExists = true;
            await _channelRepository.UpdateAsync(channel);

            content.SourceId = SourceManager.Preview;
            return await InsertWithTaxisAsync(site, channel, content, 0);
        }

        public async Task<int> InsertWithTaxisAsync(Site site, Channel channel, Content content, int taxis)
        {
            if (site.IsAutoPageInTextEditor)
            {
                content.Body = ContentUtility.GetAutoPageBody(content.Body, site.AutoPageWordNum);
            }

            content.Taxis = taxis;

            var tableName = _channelRepository.GetTableName(site, channel);
            if (string.IsNullOrEmpty(tableName)) return 0;

            var repository = await GetRepositoryAsync(tableName);
            if (content.SourceId == SourceManager.Preview)
            {
                await repository.InsertAsync(content);
            }
            else
            {
                await repository.InsertAsync(content, Q.CachingRemove(GetListKey(tableName, content.SiteId, content.ChannelId)));

                await _statRepository.AddCountAsync(StatType.ContentAdd, content.SiteId);
                await _statRepository.AddCountAsync(StatType.ContentAdd, content.SiteId,
                    content.AdminId);
            }

            return content.Id;
        }
    }
}
