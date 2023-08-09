using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Utils.Office;
using SSCMS.Dto;
using SSCMS.Core.Utils;

namespace SSCMS.Web.Controllers.Admin.Settings.Users
{
    public partial class UsersController
    {
        [HttpPost, Route(RouteExport)]
        public async Task<ActionResult<StringResult>> Export()
        {
            if (!await _authManager.HasAppPermissionsAsync(MenuUtils.AppPermissions.SettingsUsers))
            {
                return Unauthorized();
            }

<<<<<<< HEAD
            const string fileName = "users.csv";
=======
            const string fileName = "用户.xlsx";
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            var filePath = _pathManager.GetTemporaryFilesPath(fileName);

            var excelObject = new ExcelObject(_databaseManager, _pathManager);
            await excelObject.CreateExcelFileForUsersAsync(filePath, null);

            var downloadUrl = _pathManager.GetRootUrlByPath(filePath);

            return new StringResult
            {
                Value = downloadUrl
            };
        }
    }
}
