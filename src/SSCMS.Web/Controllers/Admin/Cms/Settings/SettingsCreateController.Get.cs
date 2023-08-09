﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Models;
using SSCMS.Core.Utils;
<<<<<<< HEAD
=======
using System;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

namespace SSCMS.Web.Controllers.Admin.Cms.Settings
{
    public partial class SettingsCreateController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<ObjectResult<Site>>> Get([FromQuery] SiteRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, MenuUtils.SitePermissions.SettingsCreate))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
<<<<<<< HEAD
=======
            if (site.CreateStaticContentAddDate == DateTime.MinValue)
            {
                site.CreateStaticContentAddDate = DateTime.Now.AddYears(-10);
            }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            return new ObjectResult<Site>
            {
                Value = site
            };
        }
    }
}