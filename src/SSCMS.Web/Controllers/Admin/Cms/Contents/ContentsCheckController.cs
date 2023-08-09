﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Models;
using SSCMS.Repositories;
using SSCMS.Services;

namespace SSCMS.Web.Controllers.Admin.Cms.Contents
{
    [OpenApiIgnore]
    [Authorize(Roles = Types.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class ContentsCheckController : ControllerBase
    {
        private const string RouteList = "cms/contents/contentsCheck/actions/list";
        private const string RouteTree = "cms/contents/contentsCheck/actions/tree";
        private const string RouteColumns = "cms/contents/contentsCheck/actions/columns";

        private readonly IAuthManager _authManager;
        private readonly IPathManager _pathManager;
        private readonly IDatabaseManager _databaseManager;
        private readonly ISiteRepository _siteRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IContentGroupRepository _contentGroupRepository;
        private readonly IContentTagRepository _contentTagRepository;

        public ContentsCheckController(IAuthManager authManager, IPathManager pathManager, IDatabaseManager databaseManager, ISiteRepository siteRepository, IChannelRepository channelRepository, IContentRepository contentRepository, IContentGroupRepository contentGroupRepository, IContentTagRepository contentTagRepository)
        {
            _authManager = authManager;
            _pathManager = pathManager;
            _databaseManager = databaseManager;
            _siteRepository = siteRepository;
            _channelRepository = channelRepository;
            _contentRepository = contentRepository;
            _contentGroupRepository = contentGroupRepository;
            _contentTagRepository = contentTagRepository;
        }

        public class ColumnsRequest : SiteRequest
        {
            public List<string> AttributeNames { get; set; }
        }

        public class ListRequest : SiteRequest
        {
<<<<<<< HEAD
            public int? ChannelId { get; set; }
=======
            public List<int> ChannelIds { get; set; }
            public bool IsAllContents { get; set; }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public IEnumerable<KeyValuePair<string, string>> Items { get; set; }
            public int Page { get; set; }
            public List<int> CheckedLevels { get; set; }
            public bool IsTop { get; set; }
            public bool IsRecommend { get; set; }
            public bool IsHot { get; set; }
            public bool IsColor { get; set; }
            public List<string> GroupNames { get; set; }
            public List<string> TagNames { get; set; }
        }

        public class ListResult
        {
            public List<Content> PageContents { get; set; }
            public int Total { get; set; }
            public int PageSize { get; set; }
        }

<<<<<<< HEAD
=======
        public class Permissions
        {
            public bool IsAdd { get; set; }
            public bool IsDelete { get; set; }
            public bool IsEdit { get; set; }
            public bool IsArrange { get; set; }
            public bool IsTranslate { get; set; }
            public bool IsCheck { get; set; }
            public bool IsCreate { get; set; }
        }

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        public class TreeResult
        {
            public Cascade<int> Root { get; set; }
            public string SiteUrl { get; set; }
            public IEnumerable<string> GroupNames { get; set; }
            public IEnumerable<string> TagNames { get; set; }
            public IEnumerable<CheckBox<int>> CheckedLevels { get; set; }
<<<<<<< HEAD
            public List<ContentColumn> Columns { get; set; }
=======
            public ContentColumn TitleColumn { get; set; }
            public ContentColumn BodyColumn { get; set; }
            public List<ContentColumn> Columns { get; set; }
            public Permissions Permissions { get; set; }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        }
    }
}