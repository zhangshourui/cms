using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Core.Utils;
<<<<<<< HEAD
=======
using System.Collections.Generic;
using SSCMS.Enums;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

namespace SSCMS.Web.Controllers.Admin.Cms.Settings
{
    public partial class SettingsContentController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> Get([FromQuery] SiteRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, MenuUtils.SitePermissions.SettingsContent))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
<<<<<<< HEAD
            // var isCensorTextEnabled = await _censorManager.IsCensorTextAsync();
            var isCensorTextEnabled = true;
=======

            var taxisTypes = new List<Select<string>>
            {
                new Select<string>(TaxisType.OrderByTaxisDesc),
                new Select<string>(TaxisType.OrderByAddDate),
                new Select<string>(TaxisType.OrderByAddDateDesc),
                new Select<string>(TaxisType.OrderByTitle),
                new Select<string>(TaxisType.OrderByTitleDesc),
            };
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            return new GetResult
            {
                Site = site,
<<<<<<< HEAD
                IsCensorTextEnabled = isCensorTextEnabled
=======
                TaxisTypes = taxisTypes,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            };
        }
    }
}