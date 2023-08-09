﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Models;
using SSCMS.Utils;
using SSCMS.Core.Utils;
using System.Collections.Specialized;
using SSCMS.Configuration;

namespace SSCMS.Web.Controllers.Admin.Cms.Contents
{
    public partial class ContentsLayerAddController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<ObjectResult<List<int>>>> Submit([FromBody] SubmitRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                    MenuUtils.SitePermissions.Contents) ||
                !await _authManager.HasContentPermissionsAsync(request.SiteId, request.ChannelId, MenuUtils.ContentPermissions.Add))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var channel = await _channelRepository.GetAsync(request.ChannelId);
            if (channel == null) return this.Error("无法确定内容对应的栏目");

            var isChecked = request.CheckedLevel >= site.CheckContentLevel;

            var adminId = _authManager.AdminId;
            var contentIdList = new List<int>();

            var titles = ListUtils.GetStringListByReturnAndNewline(request.Titles);
            titles.Reverse();

            foreach (var value in titles)
            {
                if (string.IsNullOrWhiteSpace(value)) continue;

                var title = string.Empty;
<<<<<<< HEAD
                var attributes = new NameValueCollection();
=======
                var body = string.Empty;
                var attributes = new NameValueCollection();
                
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                if (value.Contains('(') && value.Contains(')'))
                {
                    var length = value.IndexOf(')') - value.IndexOf('(') - 1;
                    if (length > 0)
                    {
                        var separateString = value.Substring(value.IndexOf('(') + 1, length);
<<<<<<< HEAD
                        attributes = TranslateUtils.ToNameValueCollection(separateString);
                        title = value.Substring(0, value.IndexOf('('));
                    }
                }
=======
                        if (StringUtils.Contains(separateString, "="))
                        {
                            attributes = TranslateUtils.ToNameValueCollection(separateString);
                        }
                        else
                        {
                            body = separateString;
                        }
                        title = value.Substring(0, value.IndexOf('('));
                    }
                }
                else if (value.Contains('（') && value.Contains('）'))
                {
                    var length = value.IndexOf('）') - value.IndexOf('（') - 1;
                    if (length > 0)
                    {
                        var separateString = value.Substring(value.IndexOf('（') + 1, length);
                        if (StringUtils.Contains(separateString, "="))
                        {
                            attributes = TranslateUtils.ToNameValueCollection(separateString);
                        }
                        else
                        {
                            body = separateString;
                        }
                        title = value.Substring(0, value.IndexOf('（'));
                    }
                }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                if (string.IsNullOrWhiteSpace(title))
                {
                    title = value.Trim();
                }

                var content = new Content
                {
                    ChannelId = channel.Id,
                    SiteId = request.SiteId,
                    AdminId = adminId,
                    LastEditAdminId = adminId,
                    AddDate = DateTime.Now,
                    Checked = isChecked,
                    CheckedLevel = request.CheckedLevel,
                    Title = StringUtils.Trim(title),
                    ImageUrl = string.Empty,
<<<<<<< HEAD
                    Body = string.Empty
=======
                    Body = body
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                };
                foreach (string key in attributes.Keys)
                {
                    content.Set(key, attributes[key]);
                }

                await _contentRepository.InsertAsync(site, channel, content);
                contentIdList.Add(content.Id);
            }

            if (isChecked)
            {
                foreach (var contentId in contentIdList)
                {
                    await _createManager.CreateContentAsync(request.SiteId, channel.Id, contentId);
                }
                await _createManager.TriggerContentChangedEventAsync(request.SiteId, channel.Id);
            }

            return new ObjectResult<List<int>>
            {
                Value = contentIdList
            };
        }
    }
}