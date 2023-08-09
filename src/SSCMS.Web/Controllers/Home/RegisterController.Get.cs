﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Home
{
    public partial class RegisterController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> Get()
        {
            var config = await _configRepository.GetAsync();
            if (config.IsHomeClosed) return this.Error("对不起，用户中心已被禁用！");
            if (!config.IsUserRegistrationAllowed) return this.Error("对不起，系统已禁止新用户注册！");

            var userStyles = await _tableStyleRepository.GetUserStylesAsync();
            var styles = userStyles
                .Where(x => ListUtils.ContainsIgnoreCase(config.UserRegistrationAttributes, x.AttributeName))
                .Select(x => new InputStyle(x));

            var isUserVerifyMobile = false;
            var smsSettings = await _smsManager.GetSmsSettingsAsync();
            var isSmsEnabled = smsSettings.IsSms && smsSettings.IsSmsUser;
            if (isSmsEnabled && config.IsUserForceVerifyMobile)
            {
                isUserVerifyMobile = true;
            }

            var settings = new Settings
            {
                IsCloudImages = await _cloudManager.IsImagesAsync(),
            };

            return new GetResult
            {
                IsSmsEnabled = isSmsEnabled,
                IsUserVerifyMobile = isUserVerifyMobile,
                IsUserRegistrationMobile = config.IsUserRegistrationMobile,
                IsUserRegistrationEmail = config.IsUserRegistrationEmail,
                IsUserRegistrationGroup = config.IsUserRegistrationGroup,
<<<<<<< HEAD
=======
                IsUserCaptchaDisabled = config.IsUserCaptchaDisabled,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                IsHomeAgreement = config.IsHomeAgreement,
                HomeAgreementHtml = config.HomeAgreementHtml,
                Styles = styles,
                Groups = await _userGroupRepository.GetUserGroupsAsync(),
                Settings = settings
            };
        }
    }
}
