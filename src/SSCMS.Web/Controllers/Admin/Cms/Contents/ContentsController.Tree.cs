﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Utils;
using SSCMS.Configuration;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Contents
{
    public partial class ContentsController
    {
        [HttpPost, Route(RouteTree)]
        public async Task<ActionResult<TreeResult>> Tree([FromBody]TreeRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                    MenuUtils.SitePermissions.Contents))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var channel = await _channelRepository.GetAsync(request.SiteId);

            var enabledChannelIds = await _authManager.GetContentPermissionsChannelIdsAsync(site.Id);
            var visibleChannelIds = await _authManager.GetVisibleChannelIdsAsync(enabledChannelIds);

            var root = await _channelRepository.GetCascadeAsync(site, channel, async summary =>
            {
                var visible = visibleChannelIds.Contains(summary.Id);
<<<<<<< HEAD
                var disabled = !enabledChannelIds.Contains(summary.Id);
                var current = await _contentRepository.GetSummariesAsync(site, summary);

                if (!visible) return null;

=======
                if (!visible) return null;
                
                var disabled = !enabledChannelIds.Contains(summary.Id);
                var current = await _contentRepository.GetSummariesAsync(site, summary);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                return new
                {
                    current.Count,
                    Disabled = disabled
                };
            });

            if (!request.Reload)
            {
                var (cssUrls, jsUrls) = _pluginManager.GetExternalUrls();
                var siteUrl = await _pathManager.GetSiteUrlAsync(site, true);
                var groupNames = await _contentGroupRepository.GetGroupNamesAsync(request.SiteId);
                var tagNames = await _contentTagRepository.GetTagNamesAsync(request.SiteId);
                var checkedLevels = ElementUtils.GetCheckBoxes(CheckManager.GetCheckedLevels(site, true, site.CheckContentLevel, true));

                return new TreeResult
                {
                    Root = root,
                    CssUrls = cssUrls,
                    JsUrls = jsUrls,
                    SiteUrl = siteUrl,
                    GroupNames = groupNames,
                    TagNames = tagNames,
                    CheckedLevels = checkedLevels
                };
            }

            return new TreeResult
            {
                Root = root
            };
        }
    }
}
