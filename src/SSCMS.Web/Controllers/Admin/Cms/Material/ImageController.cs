using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Models;
using SSCMS.Repositories;
using SSCMS.Services;

namespace SSCMS.Web.Controllers.Admin.Cms.Material
{
    [OpenApiIgnore]
    [Authorize(Roles = Types.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class ImageController : ControllerBase
    {
        private const string Route = "cms/material/image";
        private const string RouteUpdate = "cms/material/image/actions/update";
        private const string RouteDelete = "cms/material/image/actions/delete";
        private const string RouteDeleteGroup = "cms/material/image/actions/deleteGroup";
        private const string RoutePull = "cms/material/image/actions/pull";
        private const string RouteDownload = "cms/material/image/actions/download";

        private readonly ISettingsManager _settingsManager;
        private readonly IAuthManager _authManager;
        private readonly IPathManager _pathManager;
        private readonly IWxManager _openManager;
<<<<<<< HEAD
=======
        private readonly IConfigRepository _configRepository;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        private readonly ISiteRepository _siteRepository;
        private readonly IMaterialGroupRepository _materialGroupRepository;
        private readonly IMaterialImageRepository _materialImageRepository;
        private readonly IWxAccountRepository _openAccountRepository;

<<<<<<< HEAD
        public ImageController(ISettingsManager settingsManager, IAuthManager authManager, IPathManager pathManager, IWxManager openManager, ISiteRepository siteRepository, IMaterialGroupRepository materialGroupRepository, IMaterialImageRepository materialImageRepository, IWxAccountRepository openAccountRepository)
=======
        public ImageController(ISettingsManager settingsManager, IAuthManager authManager, IPathManager pathManager, IWxManager openManager, IConfigRepository configRepository, ISiteRepository siteRepository, IMaterialGroupRepository materialGroupRepository, IMaterialImageRepository materialImageRepository, IWxAccountRepository openAccountRepository)
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            _settingsManager = settingsManager;
            _authManager = authManager;
            _pathManager = pathManager;
            _openManager = openManager;
<<<<<<< HEAD
=======
            _configRepository = configRepository;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            _siteRepository = siteRepository;
            _materialGroupRepository = materialGroupRepository;
            _materialImageRepository = materialImageRepository;
            _openAccountRepository = openAccountRepository;
        }

        public class QueryRequest
        {
            public int SiteId { get; set; }
            public string Keyword { get; set; }
            public int GroupId { get; set; }
            public int Page { get; set; }
            public int PerPage { get; set; }
        }

        public class QueryResult
        {
<<<<<<< HEAD
=======
            public bool IsSiteOnly { get; set; }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            public IEnumerable<MaterialGroup> Groups { get; set; }
            public int Count { get; set; }
            public IEnumerable<MaterialImage> Items { get; set; }
            public string SiteType { get; set; }
        }

        public class CreateRequest : SiteRequest
        {
            public int GroupId { get; set; }
        }

        public class UpdateRequest : SiteRequest
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int GroupId { get; set; }
        }

        public class DeleteRequest : SiteRequest
        {
            public int Id { get; set; }
        }

        public class DeleteGroupRequest : SiteRequest
        {
            public int Id { get; set; }
        }

        public class DownloadRequest : SiteRequest
        {
            public int Id { get; set; }
        }

        public class PullRequest : SiteRequest
        {
            public int GroupId { get; set; }
        }

        public class PullResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
