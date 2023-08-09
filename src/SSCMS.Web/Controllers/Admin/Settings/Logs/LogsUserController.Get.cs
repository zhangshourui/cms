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
    public partial class LogsUserController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<PageResult<Log>>> Get([FromBody] SearchRequest request)
        {
            if (!await _authManager.HasAppPermissionsAsync(MenuUtils.AppPermissions.SettingsLogsUser))
            {
                return Unauthorized();
            }

            var userId = 0;
            if (!string.IsNullOrEmpty(request.UserName))
            {
                var user = await _userRepository.GetByUserNameAsync(request.UserName);
<<<<<<< HEAD
                userId = user?.Id ?? 0;
=======
                if (user == null)
                {
                    return new PageResult<Log>
                    {
                        Items = new List<Log>(),
                        Count = 0,
                    };
                }
                userId = user.Id;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }

            var count = await _logRepository.GetUserLogsCountAsync(userId, request.Keyword, request.DateFrom, request.DateTo);
            var logs = await _logRepository.GetUserLogsAsync(userId, request.Keyword, request.DateFrom, request.DateTo, request.Offset, request.Limit);

            foreach (var log in logs)
            {
                var userName = await _userRepository.GetDisplayAsync(log.UserId);
                log.Set("userName", userName);
            }

            return new PageResult<Log>
            {
                Items = logs,
                Count = count
            };
        }
    }
}
