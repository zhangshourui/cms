﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Common.Form
{
    public partial class LayerImageUploadController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<Options>> Get([FromQuery] SiteRequest request)
        {
            var options = new Options();
            if (request.SiteId > 0)
            {
                var site = await _siteRepository.GetAsync(request.SiteId);
                if (site == null) return this.Error("无法确定内容对应的站点");

<<<<<<< HEAD
                options = TranslateUtils.JsonDeserialize(site.Get<string>(nameof(LayerImageUploadController)), new Options
                {
                    IsEditor = true,
                    IsMaterial = true,
                    IsThumb = false,
                    ThumbWidth = 1024,
                    ThumbHeight = 1024,
                    IsLinkToOriginal = true
                });
=======
                options = GetOptions(site);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }

            return options;
        }
    }
}