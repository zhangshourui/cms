﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Utils;
using SSCMS.Dto;
using SSCMS.Configuration;
using SSCMS.Utils;
<<<<<<< HEAD

namespace SSCMS.Web.Controllers.Admin.Cms.Contents
{
    public partial class ContentsLayerDeleteController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<BoolResult>> Submit([FromBody] SubmitRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                    MenuUtils.SitePermissions.Contents) ||
                !await _authManager.HasContentPermissionsAsync(request.SiteId, request.ChannelId, MenuUtils.ContentPermissions.Delete))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var summaries = ContentUtility.ParseSummaries(request.ChannelContentIds);

            if (!request.IsRetainFiles)
            {
                foreach (var summary in summaries)
                {
                    await _createManager.DeleteContentAsync(site, summary.ChannelId, summary.Id);
                }
            }

            if (summaries.Count == 1)
            {
                var summary = summaries[0];

                var content = await _contentRepository.GetAsync(site, summary.ChannelId, summary.Id);
                if (content != null)
                {
                    await _authManager.AddSiteLogAsync(request.SiteId, summary.ChannelId, summary.Id, "删除内容",
                        $"栏目：{await _channelRepository.GetChannelNameNavigationAsync(request.SiteId, summary.ChannelId)}，内容标题：{content.Title}");
                }
            }
            else
            {
                await _authManager.AddSiteLogAsync(request.SiteId, "批量删除内容",
                    $"栏目：{await _channelRepository.GetChannelNameNavigationAsync(request.SiteId, request.ChannelId)}，内容条数：{summaries.Count}");
            }

            var adminId = _authManager.AdminId;
            foreach (var distinctChannelId in summaries.Select(x => x.ChannelId).Distinct())
            {
                var distinctChannel = await _channelRepository.GetAsync(distinctChannelId);
                var contentIdList = summaries.Where(x => x.ChannelId == distinctChannelId)
                    .Select(x => x.Id).ToList();
                await _contentRepository.TrashContentsAsync(site, distinctChannel, contentIdList, adminId);

                await _createManager.TriggerContentChangedEventAsync(request.SiteId, distinctChannelId);
            }

            return new BoolResult
            {
                Value = true
            };
        }
    }
=======
using SSCMS.Models;
using System.Collections.Generic;

namespace SSCMS.Web.Controllers.Admin.Cms.Contents
{
  public partial class ContentsLayerDeleteController
  {
    [HttpPost, Route(Route)]
    public async Task<ActionResult<BoolResult>> Submit([FromBody] SubmitRequest request)
    {
      if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
              MenuUtils.SitePermissions.Contents) ||
          !await _authManager.HasContentPermissionsAsync(request.SiteId, request.ChannelId, MenuUtils.ContentPermissions.Delete))
      {
        return Unauthorized();
      }

      var site = await _siteRepository.GetAsync(request.SiteId);
      if (site == null) return this.Error(Constants.ErrorNotFound);

      var originalSummaries = ContentUtility.ParseSummaries(request.ChannelContentIds);
      var summaries = new List<ContentSummary>();
      foreach (var summary in originalSummaries)
      {
        var channel = await _channelRepository.GetAsync(summary.ChannelId);
        if (!channel.IsChangeBanned)
        {
          summaries.Add(summary);
        }
      }

      if (!request.IsRetainFiles)
      {
        foreach (var summary in summaries)
        {
          await _createManager.DeleteContentAsync(site, summary.ChannelId, summary.Id);
        }
      }

      if (summaries.Count == 1)
      {
        var summary = summaries[0];

        var content = await _contentRepository.GetAsync(site, summary.ChannelId, summary.Id);
        if (content != null)
        {
          await _authManager.AddSiteLogAsync(request.SiteId, summary.ChannelId, summary.Id, "删除内容",
              $"栏目：{await _channelRepository.GetChannelNameNavigationAsync(request.SiteId, summary.ChannelId)}，内容标题：{content.Title}");
        }
      }
      else
      {
        await _authManager.AddSiteLogAsync(request.SiteId, "批量删除内容",
            $"栏目：{await _channelRepository.GetChannelNameNavigationAsync(request.SiteId, request.ChannelId)}，内容条数：{summaries.Count}");
      }

      var adminId = _authManager.AdminId;
      foreach (var distinctChannelId in summaries.Select(x => x.ChannelId).Distinct())
      {
        var distinctChannel = await _channelRepository.GetAsync(distinctChannelId);
        var contentIdList = summaries.Where(x => x.ChannelId == distinctChannelId)
            .Select(x => x.Id).ToList();
        await _contentRepository.TrashContentsAsync(site, distinctChannel, contentIdList, adminId);

        await _createManager.TriggerContentChangedEventAsync(request.SiteId, distinctChannelId);
      }

      return new BoolResult
      {
        Value = true
      };
    }
  }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
}