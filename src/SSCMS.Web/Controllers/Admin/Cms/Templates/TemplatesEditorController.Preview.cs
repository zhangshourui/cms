﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Enums;
using SSCMS.Core.Utils;
using SSCMS.Configuration;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Templates
{
    public partial class TemplatesEditorController
	{
        [HttpPost, Route(RoutePreview)]
        public async Task<ActionResult<PreviewResult>> ChangeMode([FromBody] PreviewRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, MenuUtils.SitePermissions.Templates))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var template = await _templateRepository.GetAsync(request.TemplateId);
<<<<<<< HEAD
            await _parseManager.InitAsync(EditMode.Preview, site, request.ChannelId, request.ContentId, template);
=======
            await _parseManager.InitAsync(EditMode.Preview, site, request.ChannelId, request.ContentId, template, 0);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            var parsedContent = await _parseManager.ParseTemplateWithCodesHtmlAsync(request.Content);

            var baseUrl = string.Empty;
            if (template.TemplateType == TemplateType.IndexPageTemplate)
            {
                baseUrl = await _pathManager.GetIndexPageUrlAsync(site, false);
            }
            else if (template.TemplateType == TemplateType.ChannelTemplate)
            {
                var channel = await _channelRepository.GetAsync(request.ChannelId);
                baseUrl = await _pathManager.GetChannelUrlAsync(site, channel, false);
            }
            else if (template.TemplateType == TemplateType.ContentTemplate)
            {
                var content = await _contentRepository.GetAsync(site, request.ChannelId, request.ContentId);
                baseUrl = await _pathManager.GetContentUrlByIdAsync(site, content, false);
            }
            else if (template.TemplateType == TemplateType.FileTemplate)
            {
                baseUrl = await _pathManager.GetFileUrlAsync(site, template.Id, false);
            }

            return new PreviewResult
            {
                BaseUrl = baseUrl,
                Html = parsedContent
            };
        }
    }
}
