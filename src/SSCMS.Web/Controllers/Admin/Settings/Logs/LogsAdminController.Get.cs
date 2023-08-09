﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Models;
using SSCMS.Core.Utils;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

namespace SSCMS.Web.Controllers.Admin.Settings.Logs
{
    public partial class LogsAdminController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<PageResult<Log>>> Get([FromBody] SearchRequest request)
        {
            if (!await _authManager.HasAppPermissionsAsync(MenuUtils.AppPermissions.SettingsLogsAdmin))
            {
                return Unauthorized();
            }

<<<<<<< HEAD
            var admin = await _administratorRepository.GetByUserNameAsync(request.UserName);
            var adminId = admin?.Id ?? 0;
=======
            var adminId = 0;
            if (!string.IsNullOrEmpty(request.UserName))
            {
                var admin = await _administratorRepository.GetByUserNameAsync(request.UserName);
                if (admin == null)
                {
                    return new PageResult<Log>
                    {
                        Items = new List<Log>(),
                        Count = 0,
                    };
                }
                adminId = admin.Id;
            }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            var count = await _logRepository.GetAdminLogsCountAsync(adminId, request.Keyword, request.DateFrom, request.DateTo);
            var logs = await _logRepository.GetAdminLogsAsync(adminId, request.Keyword, request.DateFrom, request.DateTo, request.Offset, request.Limit);

            foreach (var log in logs)
            {
                var adminName = await _administratorRepository.GetDisplayAsync(log.AdminId);
                log.Set("adminName", adminName);
            }

            return new PageResult<Log>
            {
                Items = logs,
                Count = count
            };
        }
    }
}
