﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Utils;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Configuration;
using SSCMS.Utils;
using SSCMS.Dto;
using SSCMS.Services;

namespace SSCMS.Web.Controllers.Admin.Cms.Editor
{
    public partial class EditorController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> Get([FromQuery] GetRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                    MenuUtils.SitePermissions.Contents) ||
                !await _authManager.HasContentPermissionsAsync(request.SiteId, request.ChannelId,
                    MenuUtils.ContentPermissions.Add, MenuUtils.ContentPermissions.Edit))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var channel = await _channelRepository.GetAsync(request.ChannelId);
            if (channel == null) return this.Error(Constants.ErrorNotFound);

            var groupNames = await _contentGroupRepository.GetGroupNamesAsync(site.Id);
            var tagNames = await _contentTagRepository.GetTagNamesAsync(site.Id);

            var allStyles = await _tableStyleRepository.GetContentStylesAsync(site, channel);
            var styles = allStyles
                .Where(style =>
                    !string.IsNullOrEmpty(style.DisplayName) &&
                    !ListUtils.ContainsIgnoreCase(ColumnsManager.MetadataAttributes.Value, style.AttributeName)).ToList();
            var templates =
                await _templateRepository.GetTemplatesByTypeAsync(request.SiteId, TemplateType.ContentTemplate);

            var (userIsChecked, userCheckedLevel) = await CheckManager.GetUserCheckLevelAsync(_authManager, site, request.ChannelId);
            var checkedLevels = CheckManager.GetCheckedLevelOptions(site, userIsChecked, userCheckedLevel, true);

            Content content;
            if (request.ContentId > 0)
            {
                content = await _pathManager.DecodeContentAsync(site, channel, request.ContentId);
            }
            else
            {
                content = new Content
                {
                    Id = 0,
                    SiteId = site.Id,
                    ChannelId = channel.Id,
                    AddDate = DateTime.Now,
                    CheckedLevel = site.CheckContentDefaultLevel
                };
            }

            var relatedFields = new Dictionary<int, List<Cascade<int>>>();

            foreach (var style in styles)
            {
                if (style.InputType == InputType.CheckBox || style.InputType == InputType.SelectMultiple)
                {
                    if (request.ContentId == 0)
                    {
                        var value = style.Items != null
                            ? style.Items.Where(x => x.Selected).Select(x => x.Value).ToList()
                            : new List<string>();
                        content.Set(style.AttributeName, value);
                    }
                    else
                    {
                        var value = content.Get(style.AttributeName);
                        content.Set(style.AttributeName, ListUtils.ToList(value));
                    }
                }
                else if (style.InputType == InputType.Radio || style.InputType == InputType.SelectOne)
                {
                    if (request.ContentId == 0)
                    {
                        var item = style.Items?.FirstOrDefault(x => x.Selected);
                        var value = item != null ? item.Value : string.Empty;
                        content.Set(style.AttributeName, value);
                    }
                    else
                    {
                        var value = content.Get(style.AttributeName);
                        content.Set(style.AttributeName, StringUtils.ToString(value));
                    }
                }
                else if (style.InputType == InputType.Text || style.InputType == InputType.TextArea || style.InputType == InputType.TextEditor)
                {
                    if (request.ContentId == 0)
                    {
                        content.Set(style.AttributeName, string.Empty);
                    }
                }
                else if (style.InputType == InputType.SelectCascading)
                {
                    if (style.RelatedFieldId > 0)
                    {
                        var items = await _relatedFieldItemRepository.GetCascadesAsync(request.SiteId, style.RelatedFieldId, 0);
                        relatedFields[style.RelatedFieldId] = items;
                    }
                }
<<<<<<< HEAD
=======
                else if (style.InputType == InputType.Date || style.InputType == InputType.DateTime)
                {
                    var date = TranslateUtils.ToDateTime(content.Get<string>(style.AttributeName), DateTime.Now);
                    content.Set(style.AttributeName, date);
                }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }

            var siteUrl = await _pathManager.GetSiteUrlAsync(site, true);

            var linkTypes = _pathManager.GetLinkTypeSelects(false);
<<<<<<< HEAD
            var root = await _channelRepository.GetCascadeAsync(site, await _channelRepository.GetAsync(request.SiteId));
            if (content.LinkType == LinkType.LinkToChannel)
            {
                var channelIds = ListUtils.GetIntList(content.LinkUrl);
                if (channelIds.Count > 0 && channelIds[channelIds.Count - 1] > 0)
                {
                    var targetChannelId = channelIds[channelIds.Count - 1];
                    var name = await _channelRepository.GetChannelNameNavigationAsync(request.SiteId, targetChannelId);
                    content.Set("LinkToChannel", name);
=======
            var root = await _channelRepository.GetCascadeAsync(site, await _channelRepository.GetAsync(request.SiteId), async summary =>
            {
                var count = await _contentRepository.GetCountAsync(site, summary);

                return new
                {
                    Count = count
                };
            });

            var linkTo = new LinkTo
            {
                ChannelIds = new List<int> {
                  request.SiteId,
                },
                ContentId = 0,
                ContentTitle = string.Empty
            };
            if (content.LinkType == Enums.LinkType.LinkToChannel)
            {
                linkTo.ChannelIds = ListUtils.GetIntList(content.LinkUrl);
            }
            else if (content.LinkType == Enums.LinkType.LinkToContent)
            {
                if (!string.IsNullOrEmpty(content.LinkUrl) && content.LinkUrl.IndexOf('_') != -1)
                {
                    var arr = content.LinkUrl.Split('_');
                    if (arr.Length == 2)
                    {
                        var channelIds = ListUtils.GetIntList(arr[0]);
                        var contentId = TranslateUtils.ToInt(arr[1]);
                        var channelId = channelIds.Count > 0 ? channelIds[channelIds.Count - 1] : 0;
                        var linkToContent = await _contentRepository.GetAsync(site.Id, channelId, contentId);
                        if (linkToContent != null)
                        {
                            linkTo.ChannelIds = channelIds;
                            linkTo.ContentId = contentId;
                            linkTo.ContentTitle = linkToContent.Title;
                        }
                    }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                }
            }

            var settings = new Settings
            {
                IsCloudCensor = _censorManager is ICloudManager,
                CensorSettings = await _censorManager.GetCensorSettingsAsync(),
                IsCloudSpell = _spellManager is ICloudManager,
                SpellSettings = await _spellManager.GetSpellSettingsAsync(),
                IsCloudImages = await _cloudManager.IsImagesAsync(),
                CloudType = await _cloudManager.GetCloudTypeAsync(),
            };

<<<<<<< HEAD
=======
            var breadcrumbItems = new List<Select<int>>();
            if (channel.ParentsPath != null && channel.ParentsPath.Count > 0)
            {
                foreach (var channelId in channel.ParentsPath)
                {
                    var channelName = await _channelRepository.GetChannelNameAsync(request.SiteId, channelId);
                    if (string.IsNullOrEmpty(channelName)) continue;

                    breadcrumbItems.Add(new Select<int>
                    {
                        Value = channelId,
                        Label = channelName,
                    });
                }
            }
            breadcrumbItems.Add(new Select<int>
            {
                Value = channel.Id,
                Label = channel.ChannelName,
            });

            var isScheduled = false;
            DateTime? scheduledDate = DateTime.Now.AddDays(1);
            if (!content.Checked && content.CheckedLevel == CheckManager.LevelInt.ScheduledPublish)
            {
                var task = await _scheduledTaskRepository.GetPublishAsync(content.SiteId, content.ChannelId, content.Id);
                if (task != null)
                {
                    isScheduled = true;
                    scheduledDate = task.ScheduledDate;
                }
            }

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            return new GetResult
            {
                CSRFToken = _authManager.GetCSRFToken(),
                Content = content,
                Site = site,
                SiteUrl = StringUtils.TrimEndSlash(siteUrl),
                Channel = channel,
                GroupNames = groupNames,
                TagNames = tagNames,
                Styles = styles,
                RelatedFields = relatedFields,
                Templates = templates,
                CheckedLevels = checkedLevels,
                CheckedLevel = userCheckedLevel,
                LinkTypes = linkTypes,
<<<<<<< HEAD
                Root = root,
                Settings = settings,
=======
                LinkTo = linkTo,
                Root = root,
                Settings = settings,
                BreadcrumbItems = breadcrumbItems,
                IsScheduled = isScheduled,
                ScheduledDate = scheduledDate,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            };
        }
    }
}
