﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SSCMS.Configuration;
using SSCMS.Models;
using SSCMS.Core.Utils;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.V1
{
    public partial class ContentsController
    {
        [OpenApiOperation("获取内容 API", "获取内容，使用GET发起请求，请求地址为/api/v1/contents/{siteId}/{channelId}/{id}")]
        [HttpGet, Route(RouteContent)]
        public async Task<ActionResult<Content>> Get([FromRoute]int siteId, [FromRoute] int channelId, [FromRoute] int id)
        {
            if (!await _accessTokenRepository.IsScopeAsync(_authManager.ApiToken, Constants.ScopeContents))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(siteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

<<<<<<< HEAD
            var channelInfo = await _channelRepository.GetAsync(channelId);
            if (channelInfo == null) return this.Error(Constants.ErrorNotFound);

            if (!await _authManager.HasContentPermissionsAsync(siteId, channelId, MenuUtils.ContentPermissions.View)) return Unauthorized();

            var content = await _contentRepository.GetAsync(site, channelInfo, id);
=======
            var channel = await _channelRepository.GetAsync(channelId);
            if (channel == null) return this.Error(Constants.ErrorNotFound);

            if (!await _authManager.HasContentPermissionsAsync(siteId, channelId, MenuUtils.ContentPermissions.View)) return Unauthorized();

            var content = await _contentRepository.GetAsync(site, channel, id);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            if (content == null) return this.Error(Constants.ErrorNotFound);

            return content;
        }
    }
}
