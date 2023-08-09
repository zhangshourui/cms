using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Utils;

namespace SSCMS.Web.Controllers.Admin.Plugins
{
    public partial class ManageController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> Get()
        {
            if (!await _authManager.HasAppPermissionsAsync(MenuUtils.AppPermissions.PluginsManagement))
            {
                return Unauthorized();
            }
<<<<<<< HEAD

            //var dict = await _pluginManager.GetPluginIdAndVersionDictAsync();
            //var list = dict.Keys.ToList();
            //var packageIds = Utilities.ToString(list);
=======
            
            // _pluginManager.Load();
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            return new GetResult
            {
                CmsVersion = _settingsManager.Version,
                AllPlugins = _pluginManager.Plugins,
                Containerized = _settingsManager.Containerized
            };
        }
    }
}
