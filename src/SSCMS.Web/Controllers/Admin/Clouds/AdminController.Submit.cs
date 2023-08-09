using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;

namespace SSCMS.Web.Controllers.Admin.Clouds
{
    public partial class AdminController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<BoolResult>> Submit([FromBody] SubmitRequest request)
        {
            if (!await _authManager.IsSuperAdminAsync())
            {
                return Unauthorized();
            }

            var config = await _configRepository.GetAsync();

            config.IsCloudAdmin = request.IsCloudAdmin;
            config.AdminTitle = request.AdminTitle;
            config.AdminFaviconUrl = request.AdminFaviconUrl;
            config.AdminLogoUrl = request.AdminLogoUrl;
            config.AdminLogoLinkUrl = request.AdminLogoLinkUrl;
            config.AdminWelcomeHtml = request.AdminWelcomeHtml;
<<<<<<< HEAD
=======
            config.IsAdminUpdateDisabled = request.IsAdminUpdateDisabled;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            await _configRepository.UpdateAsync(config);

            await _authManager.AddAdminLogAsync("修改管理后台设置");

            return new BoolResult
            {
                Value = true
            };
        }
    }
}
