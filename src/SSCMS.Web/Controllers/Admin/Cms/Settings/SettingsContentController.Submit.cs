﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Core.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Settings
{
    public partial class SettingsContentController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<BoolResult>> Submit([FromBody] SubmitRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, MenuUtils.SitePermissions.SettingsContent))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);

            site.IsSaveImageInTextEditor = request.IsSaveImageInTextEditor;

            var isReCalculate = false;
            if (request.IsAutoPageInTextEditor)
            {
                if (site.IsAutoPageInTextEditor == false)
                {
                    isReCalculate = true;
                }
                else if (site.AutoPageWordNum != request.AutoPageWordNum)
                {
                    isReCalculate = true;
                }
            }

<<<<<<< HEAD
            site.PageSize = request.PageSize;
=======
            var isClearCache = false;
            if (site.TaxisType != request.TaxisType)
            {
                isClearCache = true;
            }

            site.PageSize = request.PageSize;
            site.TaxisType = request.TaxisType;

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            site.IsAutoPageInTextEditor = request.IsAutoPageInTextEditor;
            site.AutoPageWordNum = request.AutoPageWordNum;
            site.IsContentTitleBreakLine = request.IsContentTitleBreakLine;
            site.IsContentSubTitleBreakLine = request.IsContentSubTitleBreakLine;

            site.CheckContentLevel = request.CheckContentLevel;
            site.CheckContentDefaultLevel = request.CheckContentDefaultLevel;

            await _siteRepository.UpdateAsync(site);

            if (isReCalculate)
            {
                await _contentRepository.SetAutoPageContentToSiteAsync(site);
            }

<<<<<<< HEAD
=======
            if (isClearCache)
            {
                await _contentRepository.ClearAllListCacheAsync(site);
            }

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            await _authManager.AddSiteLogAsync(request.SiteId, "修改内容设置");

            return new BoolResult
            {
                Value = true
            };
        }
    }
}