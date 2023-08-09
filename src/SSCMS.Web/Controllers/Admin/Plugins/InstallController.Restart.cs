using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Utils;
using SSCMS.Dto;

namespace SSCMS.Web.Controllers.Admin.Plugins
{
    public partial class InstallController
    {
        [HttpPost, Route(RouteActionsRestart)]
        public async Task<ActionResult<BoolResult>> Restart([FromBody] RestartRequest request)
        {
            if (!await _authManager.HasAppPermissionsAsync(MenuUtils.AppPermissions.PluginsAdd))
            {
                return Unauthorized();
            }

<<<<<<< HEAD
            _settingsManager.SaveSettings(_settingsManager.IsProtectData, _settingsManager.IsSafeMode, request.IsDisablePlugins, _settingsManager.DatabaseType, _settingsManager.DatabaseConnectionString, _settingsManager.RedisConnectionString, _settingsManager.AdminRestrictionHost, _settingsManager.AdminRestrictionAllowList, _settingsManager.AdminRestrictionBlockList);
=======
            _settingsManager.SaveSettings(_settingsManager.IsProtectData, _settingsManager.IsSafeMode, request.IsDisablePlugins, _settingsManager.DatabaseType, _settingsManager.DatabaseConnectionString, _settingsManager.RedisConnectionString, _settingsManager.AdminRestrictionHost, _settingsManager.AdminRestrictionAllowList, _settingsManager.AdminRestrictionBlockList, _settingsManager.CorsIsOrigins, _settingsManager.CorsOrigins);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            _hostApplicationLifetime.StopApplication();

            return new BoolResult
            {
                Value = true
            };
        }
    }
}
