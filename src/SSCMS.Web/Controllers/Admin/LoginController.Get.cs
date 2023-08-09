using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using SSCMS.Configuration;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

namespace SSCMS.Web.Controllers.Admin
{
    public partial class LoginController
    {
        [HttpGet, Route(Route)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetResult>> Get()
        {
            var redirectUrl = await AdminRedirectCheckAsync();
            if (!string.IsNullOrEmpty(redirectUrl))
            {
                return new GetResult
                {
                    Success = false,
                    RedirectUrl = redirectUrl
                };
            }

            var config = await _configRepository.GetAsync();
            var smsSettings = await _smsManager.GetSmsSettingsAsync();
            var isSmsAdmin = smsSettings.IsSms && smsSettings.IsSmsAdmin;
            var isSmsAdminAndDisableAccount = isSmsAdmin && smsSettings.IsSmsAdminAndDisableAccount;

            return new GetResult
            {
                Success = true,
                Version = _settingsManager.Version,
<<<<<<< HEAD
                AdminFaviconUrl = config.AdminFaviconUrl,
                AdminTitle = config.AdminTitle,
=======
                AdminFaviconUrl = config.IsCloudAdmin ? config.AdminFaviconUrl : string.Empty,
                AdminTitle = config.IsCloudAdmin ? config.AdminTitle : Constants.AdminTitle,
                IsAdminCaptchaDisabled = config.IsAdminCaptchaDisabled,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                IsSmsAdmin = isSmsAdmin,
                IsSmsAdminAndDisableAccount = isSmsAdminAndDisableAccount
            };
        }
    }
}
