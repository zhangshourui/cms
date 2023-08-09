﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Core.Utils;
using SSCMS.Utils;
<<<<<<< HEAD
=======
using System.Linq;
using SSCMS.Enums;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

namespace SSCMS.Web.Controllers.Admin.Cms.Contents
{
    public partial class ContentsRecycleController
    {
        [HttpPost, Route(RouteTree)]
        public async Task<ActionResult<TreeResult>> Tree([FromBody]SiteRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                    MenuUtils.SitePermissions.ContentsRecycle))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var channel = await _channelRepository.GetAsync(request.SiteId);
            var root = await _channelRepository.GetCascadeAsync(site, channel);

            var siteUrl = await _pathManager.GetSiteUrlAsync(site, true);
            var groupNames = await _contentGroupRepository.GetGroupNamesAsync(request.SiteId);
            var tagNames = await _contentTagRepository.GetTagNamesAsync(request.SiteId);
            var checkedLevels = ElementUtils.GetCheckBoxes(CheckManager.GetCheckedLevels(site, true, site.CheckContentLevel, true));

            var columnsManager = new ColumnsManager(_databaseManager, _pathManager);
            var columns = await columnsManager.GetContentListColumnsAsync(site, channel, ColumnsManager.PageType.RecycleContents);

<<<<<<< HEAD
=======
            var titleColumn =
                columns.FirstOrDefault(x => StringUtils.EqualsIgnoreCase(x.AttributeName, nameof(Models.Content.Title)));
            var bodyColumn = new ContentColumn
            {
                AttributeName = nameof(Models.Content.Body),
                DisplayName = "内容正文",
                InputType = InputType.TextEditor,
                IsSearchable = true,
            };

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            return new TreeResult
            {
                Root = root,
                SiteUrl = siteUrl,
                GroupNames = groupNames,
                TagNames = tagNames,
                CheckedLevels = checkedLevels,
<<<<<<< HEAD
                Columns = columns
            };
        }

        public class TreeResult
        {
            public Cascade<int> Root { get; set; }
            public string SiteUrl { get; set; }
            public IEnumerable<string> GroupNames { get; set; }
            public IEnumerable<string> TagNames { get; set; }
            public IEnumerable<CheckBox<int>> CheckedLevels { get; set; }
            public List<ContentColumn> Columns { get; set; }
        }
=======
                Columns = columns,
                TitleColumn = titleColumn,
                BodyColumn = bodyColumn,
            };
        }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }
}
