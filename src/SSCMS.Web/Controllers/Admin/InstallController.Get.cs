﻿using System.Collections.Generic;
<<<<<<< HEAD
using System.Security.Permissions;
=======
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using System.Threading.Tasks;
using Datory;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin
{
    public partial class InstallController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> Get()
        {
            if (!await _configRepository.IsNeedInstallAsync())
            {
                return new GetResult
                {
                    Forbidden = true
                };
            }

            var rootWritable = false;
            try
            {
                var filePath = PathUtils.Combine(_settingsManager.ContentRootPath, "version.txt");
                FileUtils.WriteText(filePath, _settingsManager.Version);

<<<<<<< HEAD
                var ioPermission = new FileIOPermission(FileIOPermissionAccess.Write, _settingsManager.ContentRootPath);
                ioPermission.Demand();

=======
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                rootWritable = true;
            }
            catch
            {
                // ignored
            }

            var siteFilesWritable = false;
            try
            {
                var filePath = PathUtils.Combine(_settingsManager.WebRootPath, DirectoryUtils.SiteFiles.DirectoryName, "index.html");
                FileUtils.WriteText(filePath, Constants.Html5Empty);

<<<<<<< HEAD
                var ioPermission = new FileIOPermission(FileIOPermissionAccess.Write, PathUtils.Combine(_settingsManager.ContentRootPath, DirectoryUtils.SiteFiles.DirectoryName));
                ioPermission.Demand();

=======
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                siteFilesWritable = true;
            }
            catch
            {
                // ignored
            }

            var result = new GetResult
            {
                Version = _settingsManager.Version,
                FrameworkDescription = _settingsManager.FrameworkDescription,
                OSDescription = _settingsManager.OSDescription,
                ContentRootPath = _settingsManager.ContentRootPath,
                WebRootPath = _settingsManager.WebRootPath,
                RootWritable = rootWritable,
                SiteFilesWritable = siteFilesWritable,
                DatabaseTypes = new List<Select<string>>(),
                AdminUrl = _pathManager.GetAdminUrl(LoginController.Route),
                Containerized = _settingsManager.Containerized,
                DatabaseType = _settingsManager.DatabaseType,
                DatabaseConnectionString = _settingsManager.DatabaseConnectionString,
                RedisConnectionString = _settingsManager.RedisConnectionString
            };

            foreach (var databaseType in ListUtils.GetEnums<DatabaseType>())
            {
                result.DatabaseTypes.Add(new Select<string>(databaseType));
            }

            return result;
        }
    }
}