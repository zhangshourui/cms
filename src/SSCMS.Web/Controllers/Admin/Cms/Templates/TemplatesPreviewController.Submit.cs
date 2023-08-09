﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Utils;
using SSCMS.Core.Utils;
using SSCMS.Configuration;

namespace SSCMS.Web.Controllers.Admin.Cms.Templates
{
    public partial class TemplatesPreviewController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<StringResult>> Submit([FromBody] SubmitRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, MenuUtils.SitePermissions.TemplatesPreview))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var contentId = 0;
            if (request.TemplateType == TemplateType.ContentTemplate)
            {
                var channel = await _channelRepository.GetAsync(request.ChannelId);
                var count = await _contentRepository.GetCountAsync(site, channel);
                if (count > 0)
                {
                    contentId = await _contentRepository.GetFirstContentIdAsync(site, channel);
                }

                if (contentId == 0)
                {
                    return this.Error("所选栏目下无内容，请选择有内容的栏目");
                }
            }

            _cacheManager.AddOrUpdateSliding(CacheKey, request.Content, 60);

            //var cacheItem = new CacheItem<string>(CacheKey, request.Content, ExpirationMode.Sliding, TimeSpan.FromHours(1));
            //_cacheManager.AddOrUpdate(cacheItem, _ => request.Content);

<<<<<<< HEAD
            var templateInfo = new Template
=======
            var template = new Template
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            {
                TemplateType = request.TemplateType
            };

<<<<<<< HEAD
            await _parseManager.InitAsync(EditMode.Preview, site, request.ChannelId, contentId, templateInfo);
=======
            await _parseManager.InitAsync(EditMode.Preview, site, request.ChannelId, contentId, template, 0);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            var parsedContent = await _parseManager.ParseTemplateWithCodesHtmlAsync(request.Content);

            return new StringResult
            {
                Value = parsedContent
            };
        }
    }
}
