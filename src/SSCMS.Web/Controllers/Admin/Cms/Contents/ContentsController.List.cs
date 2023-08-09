﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Core.Utils;
<<<<<<< HEAD
=======
using SSCMS.Dto;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using SSCMS.Models;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Contents
{
    public partial class ContentsController
    {
        [HttpPost, Route(RouteList)]
        public async Task<ActionResult<ListResult>> List([FromBody] ListRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                    MenuUtils.SitePermissions.Contents) ||
                !await _authManager.HasContentPermissionsAsync(request.SiteId, request.ChannelId,
                    MenuUtils.ContentPermissions.View,
                    MenuUtils.ContentPermissions.Add,
                    MenuUtils.ContentPermissions.Edit,
                    MenuUtils.ContentPermissions.Delete,
                    MenuUtils.ContentPermissions.Translate,
                    MenuUtils.ContentPermissions.Arrange,
                    MenuUtils.ContentPermissions.CheckLevel1,
                    MenuUtils.ContentPermissions.CheckLevel2,
                    MenuUtils.ContentPermissions.CheckLevel3,
                    MenuUtils.ContentPermissions.CheckLevel4,
                    MenuUtils.ContentPermissions.CheckLevel5))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var channel = await _channelRepository.GetAsync(request.ChannelId);
<<<<<<< HEAD
            if (channel == null) return this.Error("无法确定内容对应的栏目");
=======
            if (channel == null) return this.Error(Constants.ErrorNotFound);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            if (channel.IsPreviewContentsExists)
            {
                await _contentRepository.DeletePreviewAsync(site, channel);
            }

            var columnsManager = new ColumnsManager(_databaseManager, _pathManager);
            var columns = await columnsManager.GetContentListColumnsAsync(site, channel, ColumnsManager.PageType.Contents);

            var pageContents = new List<Content>();
            List<ContentSummary> summaries;
            if (!string.IsNullOrEmpty(request.SearchType) &&
                !string.IsNullOrEmpty(request.SearchText) ||
                request.IsAdvanced)
            {
                summaries = await _contentRepository.SearchAsync(site, channel, channel.IsAllContents, request.SearchType, request.SearchText, request.IsAdvanced, request.CheckedLevels, request.IsTop, request.IsRecommend, request.IsHot, request.IsColor, request.GroupNames, request.TagNames);
            }
            else
            {
                summaries = await _contentRepository.GetSummariesAsync(site, channel, channel.IsAllContents);
            }
            var total = summaries.Count;

            var channelPlugins = _pluginManager.GetPlugins(request.SiteId, request.ChannelId);
            var contentMenus = new List<Menu>();
            var contentsMenus = new List<Menu>();
            foreach (var plugin in channelPlugins)
            {
                var pluginMenus = plugin.GetMenus()
                    .Where(x => ListUtils.ContainsIgnoreCase(x.Type, Types.MenuTypes.Content)).ToList();
                if (pluginMenus.Count > 0)
                {
                    contentMenus.AddRange(pluginMenus);
                }
                pluginMenus = plugin.GetMenus()
                    .Where(x => ListUtils.ContainsIgnoreCase(x.Type, Types.MenuTypes.Contents)).ToList();
                if (pluginMenus.Count > 0)
                {
                    contentsMenus.AddRange(pluginMenus);
                }
            }

            if (total > 0)
            {
                var offset = site.PageSize * (request.Page - 1);
                var pageSummaries = summaries.Skip(offset).Take(site.PageSize).ToList();

                var sequence = offset + 1;
                foreach (var summary in pageSummaries)
                {
                    var content = await _contentRepository.GetAsync(site, summary.ChannelId, summary.Id);
                    if (content == null) continue;

                    var pageContent =
                        await columnsManager.CalculateContentListAsync(sequence++, site, request.ChannelId, content, columns);

                    pageContents.Add(pageContent);
                }
            }

            var (isChecked, checkedLevel) = await CheckManager.GetUserCheckLevelAsync(_authManager, site, request.ChannelId);
            var checkedLevels = ElementUtils.GetCheckBoxes(CheckManager.GetCheckedLevels(site, isChecked, checkedLevel, true));

            var permissions = new Permissions
            {
                IsAdd = await _authManager.HasContentPermissionsAsync(site.Id, channel.Id, MenuUtils.ContentPermissions.Add),
                IsDelete = await _authManager.HasContentPermissionsAsync(site.Id, channel.Id, MenuUtils.ContentPermissions.Delete),
                IsEdit = await _authManager.HasContentPermissionsAsync(site.Id, channel.Id, MenuUtils.ContentPermissions.Edit),
                IsArrange = await _authManager.HasContentPermissionsAsync(site.Id, channel.Id, MenuUtils.ContentPermissions.Arrange),
                IsTranslate = await _authManager.HasContentPermissionsAsync(site.Id, channel.Id, MenuUtils.ContentPermissions.Translate),
                IsCheck = await _authManager.HasContentPermissionsAsync(site.Id, channel.Id, MenuUtils.ContentPermissions.CheckLevel1),
                IsCreate = await _authManager.HasSitePermissionsAsync(site.Id, MenuUtils.SitePermissions.CreateContents) || await _authManager.HasContentPermissionsAsync(site.Id, channel.Id, MenuUtils.ContentPermissions.Create),
            };

            var titleColumn =
                columns.FirstOrDefault(x => StringUtils.EqualsIgnoreCase(x.AttributeName, nameof(Models.Content.Title)));
            columns.Remove(titleColumn);

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

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            return new ListResult
            {
                PageContents = pageContents,
                Total = total,
                PageSize = site.PageSize,
                TitleColumn = titleColumn,
                Columns = columns,
<<<<<<< HEAD
                IsAllContents = channel.IsAllContents,
                CheckedLevels = checkedLevels,
                Permissions = permissions,
                ContentMenus = contentMenus,
                ContentsMenus = contentsMenus
=======
                CheckedLevels = checkedLevels,
                Permissions = permissions,
                ContentMenus = contentMenus,
                ContentsMenus = contentsMenus,
                BreadcrumbItems = breadcrumbItems,
                IsAllContents = channel.IsAllContents,
                IsChangeBanned = channel.IsChangeBanned,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            };
        }
    }
}
