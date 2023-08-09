﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Services;
using SSCMS.Models;
using SSCMS.Utils;
using SSCMS.Configuration;

namespace SSCMS.Web.Controllers.Admin.Common
{
    public partial class AdminLayerViewController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> Get([FromQuery] GetRequest request)
        {
            Administrator admin = null;
            if (request.AdminId > 0)
            {
                admin = await _administratorRepository.GetByUserIdAsync(request.AdminId);
            }
            else if (!string.IsNullOrEmpty(request.UserName))
            {
                admin = await _administratorRepository.GetByUserNameAsync(request.UserName);
            }

            if (admin == null) return this.Error(Constants.ErrorNotFound);

            var permissions = new AuthManager(_context, _antiforgery, _cacheManager, _settingsManager, _databaseManager);
            await permissions.InitAsync(admin);
            var level = await permissions.GetAdminLevelAsync();
            var siteNames = new List<string>();
            var siteIdListWithPermissions = await permissions.GetSiteIdsAsync();
            foreach (var siteId in siteIdListWithPermissions)
            {
                var site = await _siteRepository.GetAsync(siteId);
<<<<<<< HEAD
                siteNames.Add(site.SiteName);
=======
                if (site != null)
                {
                    siteNames.Add(site.SiteName);
                }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }
            var roleNames = await _administratorRepository.GetRolesAsync(admin.UserName);

            return new GetResult
            {
                Administrator = admin,
                Level = level,
                SiteNames = ListUtils.ToString(siteNames, "<br />"),
                RoleNames = roleNames
            };
        }
    }
}
