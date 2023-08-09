using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Utils.Office;
using SSCMS.Dto;
using SSCMS.Core.Utils;

namespace SSCMS.Web.Controllers.Admin.Settings.Administrators
{
    public partial class AdministratorsController
    {
        [HttpPost, Route(RouteExport)]
        public async Task<ActionResult<StringResult>> Export()
        {
            if (!await _authManager.HasAppPermissionsAsync(MenuUtils.AppPermissions.SettingsAdministrators))
            {
                return Unauthorized();
            }

<<<<<<< HEAD
            const string fileName = "administrators.csv";
=======
            const string fileName = "管理员.xlsx";
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            var filePath = _pathManager.GetTemporaryFilesPath(fileName);

            var excelObject = new ExcelObject(_databaseManager, _pathManager);
            await excelObject.CreateExcelFileForAdministratorsAsync(filePath);
            var downloadUrl = _pathManager.GetRootUrlByPath(filePath);

            return new StringResult
            {
                Value = downloadUrl
            };
        }
    }
}
