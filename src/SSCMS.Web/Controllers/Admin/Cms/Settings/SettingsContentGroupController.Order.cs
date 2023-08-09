using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Settings
{
    public partial class SettingsContentGroupController
    {
        [HttpPost, Route(RouteOrder)]
        public async Task<ActionResult<GetResult>> Order([FromBody] OrderRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                MenuUtils.SitePermissions.SettingsContentGroup))
            {
                return Unauthorized();
            }

<<<<<<< HEAD
            if (request.IsUp)
            {
                await _contentGroupRepository.UpdateTaxisUpAsync(request.SiteId, request.GroupId, request.Taxis);
            }
            else
            {
                await _contentGroupRepository.UpdateTaxisDownAsync(request.SiteId, request.GroupId, request.Taxis);
=======
            for (int i = 0; i < request.Rows; i++)
            {
                var group = await _contentGroupRepository.GetAsync(request.SiteId, request.GroupId);
                if (request.IsUp)
                {
                    await _contentGroupRepository.UpdateTaxisUpAsync(request.SiteId, group.Id, group.Taxis);
                }
                else
                {
                    await _contentGroupRepository.UpdateTaxisDownAsync(request.SiteId, group.Id, group.Taxis);
                }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }

            var groups = await _contentGroupRepository.GetContentGroupsAsync(request.SiteId);

            return new GetResult
            {
                Groups = groups
            };
        }
    }
}