﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Utils;
using SSCMS.Core.Utils;
using SSCMS.Configuration;

namespace SSCMS.Web.Controllers.Admin.Cms.Templates
{
    public partial class TemplatesAssetsController
    {
        [HttpPost, Route(RouteDelete)]
        public async Task<ActionResult<BoolResult>> Delete([FromBody] FileRequest request)
        {
            if (_settingsManager.IsSafeMode)
            {
                return this.Error(Constants.ErrorSafeMode);
            }
            
<<<<<<< HEAD
            if (request.FileType == "html")
            {
                if (!await _authManager.HasSitePermissionsAsync(request.SiteId, MenuUtils.SitePermissions.TemplatesIncludes))
                {
                    return Unauthorized();
                }
            }
            else
            {
                if (!await _authManager.HasSitePermissionsAsync(request.SiteId, MenuUtils.SitePermissions.TemplatesAssets))
                {
                    return Unauthorized();
                }
=======
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, MenuUtils.SitePermissions.TemplatesAssets))
            {
                return Unauthorized();
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var directoryPath = PathUtils.RemoveParentPath(request.DirectoryPath);
            var fileName = PathUtils.RemoveParentPath(request.FileName);

            FileUtils.DeleteFileIfExists(await _pathManager.GetSitePathAsync(site, directoryPath, fileName));
            await _authManager.AddSiteLogAsync(request.SiteId, "删除资源文件", $"{directoryPath}:{fileName}");

            return new BoolResult
            {
                Value = true
            };
        }
    }
}
