using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Models;
using SSCMS.Repositories;
using SSCMS.Services;

namespace SSCMS.Web.Controllers.Home.Common.Material
{
    [OpenApiIgnore]
    [Authorize(Roles = Types.Roles.User)]
    [Route(Constants.ApiHomePrefix)]
    public partial class LayerImageSelectController : ControllerBase
    {
        private const string Route = "common/material/layerImageSelect";
        private const string RouteSelect = "common/material/layerImageSelect/actions/select";

        private readonly ISettingsManager _settingsManager;
        private readonly IPathManager _pathManager;
<<<<<<< HEAD
=======
        private readonly IConfigRepository _configRepository;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        private readonly IMaterialGroupRepository _materialGroupRepository;
        private readonly IMaterialImageRepository _materialImageRepository;
        private readonly ISiteRepository _siteRepository;

<<<<<<< HEAD
        public LayerImageSelectController(ISettingsManager settingsManager, IPathManager pathManager, IMaterialGroupRepository materialGroupRepository, IMaterialImageRepository materialImageRepository, ISiteRepository siteRepository)
        {
            _settingsManager = settingsManager;
            _pathManager = pathManager;
=======
        public LayerImageSelectController(ISettingsManager settingsManager, IPathManager pathManager, IConfigRepository configRepository, IMaterialGroupRepository materialGroupRepository, IMaterialImageRepository materialImageRepository, ISiteRepository siteRepository)
        {
            _settingsManager = settingsManager;
            _pathManager = pathManager;
            _configRepository = configRepository;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            _materialGroupRepository = materialGroupRepository;
            _materialImageRepository = materialImageRepository;
            _siteRepository = siteRepository;
        }

<<<<<<< HEAD
        public class QueryRequest
=======
        public class QueryRequest : SiteRequest
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
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
        }

        public class SelectRequest : SiteRequest
        {
            public int MaterialId { get; set; }
        }

        public class SelectResult
        {
            public string VirtualUrl { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
