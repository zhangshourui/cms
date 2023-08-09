﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Core.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Settings
{
    public partial class SettingsCreateController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<BoolResult>> Submit([FromBody] SubmitRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, MenuUtils.SitePermissions.SettingsCreate))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);

            site.IsCreateDoubleClick = request.IsCreateDoubleClick;
<<<<<<< HEAD
            site.IsCreateContentIfContentChanged = request.IsCreateContentIfContentChanged;
            site.IsCreateChannelIfChannelChanged = request.IsCreateChannelIfChannelChanged;
=======
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            site.IsCreateShowPageInfo = request.IsCreateShowPageInfo;
            site.IsCreateIe8Compatible = request.IsCreateIe8Compatible;
            site.IsCreateBrowserNoCache = request.IsCreateBrowserNoCache;
            site.IsCreateJsIgnoreError = request.IsCreateJsIgnoreError;
            site.IsCreateWithJQuery = request.IsCreateWithJQuery;
            site.IsCreateFilterGray = request.IsCreateFilterGray;
            site.CreateStaticMaxPage = request.CreateStaticMaxPage;
            site.IsCreateUseDefaultFileName = request.IsCreateUseDefaultFileName;
            site.CreateDefaultFileName = request.CreateDefaultFileName;
            site.IsCreateStaticContentByAddDate = request.IsCreateStaticContentByAddDate;
            site.CreateStaticContentAddDate = request.CreateStaticContentAddDate;

            await _siteRepository.UpdateAsync(site);

            await _authManager.AddSiteLogAsync(request.SiteId, "修改页面生成设置");

            return new BoolResult
            {
                Value = true
            };
        }
    }
}