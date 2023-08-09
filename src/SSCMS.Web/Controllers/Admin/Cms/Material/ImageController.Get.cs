using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Enums;
using SSCMS.Core.Utils;
<<<<<<< HEAD
=======
using SSCMS.Models;
using System.Collections.Generic;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

namespace SSCMS.Web.Controllers.Admin.Cms.Material
{
    public partial class ImageController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<QueryResult>> Get([FromQuery] QueryRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                MenuUtils.SitePermissions.MaterialImage))
            {
                return Unauthorized();
            }

<<<<<<< HEAD
            var site = await _siteRepository.GetAsync(request.SiteId);

            var groups = await _materialGroupRepository.GetAllAsync(MaterialType.Image);
            var count = await _materialImageRepository.GetCountAsync(request.GroupId, request.Keyword);
            var items = await _materialImageRepository.GetAllAsync(request.GroupId, request.Keyword, request.Page, request.PerPage);

            return new QueryResult
            {
=======
            IEnumerable<MaterialGroup> groups;
            int count;
            IEnumerable<MaterialImage> items;

            var site = await _siteRepository.GetAsync(request.SiteId);
            var config = await _configRepository.GetAsync();
            if (config.IsMaterialSiteOnly)
            {
                var group = await _materialGroupRepository.GetSiteGroupAsync(MaterialType.Image, request.SiteId);
                groups = new List<MaterialGroup>
                {
                    group
                };
                count = await _materialImageRepository.GetCountAsync(group.Id, request.Keyword);
                items = await _materialImageRepository.GetAllAsync(group.Id, request.Keyword, request.Page, request.PerPage);
            }
            else
            {
              groups = await _materialGroupRepository.GetAllAsync(MaterialType.Image);
              count = await _materialImageRepository.GetCountAsync(request.GroupId, request.Keyword);
              items = await _materialImageRepository.GetAllAsync(request.GroupId, request.Keyword, request.Page, request.PerPage);
            }

            return new QueryResult
            {
                IsSiteOnly = config.IsMaterialSiteOnly,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                Groups = groups,
                Count = count,
                Items = items,
                SiteType = site.SiteType
            };
        }
    }
}
