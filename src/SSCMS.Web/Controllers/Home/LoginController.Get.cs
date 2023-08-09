<<<<<<< HEAD
﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Enums;
using SSCMS.Models;
=======
﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Home
{
    public partial class LoginController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> Get()
        {
            var config = await _configRepository.GetAsync();
            if (config.IsHomeClosed) return this.Error("对不起，用户中心已被禁用！");

            var smsSettings = await _smsManager.GetSmsSettingsAsync();
            var isSmsEnabled = smsSettings.IsSms && smsSettings.IsSmsUser;
<<<<<<< HEAD

            var user = await _authManager.GetUserAsync();
            string token = null;
            if (user != null)
            {
                token = _authManager.AuthenticateUser(user, false);

                await _userRepository.UpdateLastActivityDateAndCountOfLoginAsync(user);

                await _statRepository.AddCountAsync(StatType.UserLogin);
                await _logRepository.AddUserLogAsync(user, PageUtils.GetIpAddress(Request), Constants.ActionsLoginSuccess);

            }
         

=======
            var isUserCaptchaDisabled = config.IsUserCaptchaDisabled;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            return new GetResult
            {
                HomeTitle = config.HomeTitle,
                IsSmsEnabled = isSmsEnabled,
<<<<<<< HEAD
                User = user,
                Token = token
=======
                IsUserCaptchaDisabled = isUserCaptchaDisabled,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            };
        }
    }
}
