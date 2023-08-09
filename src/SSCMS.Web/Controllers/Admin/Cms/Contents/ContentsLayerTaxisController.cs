<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Authorization;
=======
﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SSCMS.Configuration;
using SSCMS.Dto;
<<<<<<< HEAD
=======
using SSCMS.Models;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using SSCMS.Repositories;
using SSCMS.Services;

namespace SSCMS.Web.Controllers.Admin.Cms.Contents
{
    [OpenApiIgnore]
    [Authorize(Roles = Types.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class ContentsLayerTaxisController : ControllerBase
    {
        private const string Route = "cms/contents/contentsLayerTaxis";

        private readonly IAuthManager _authManager;
        private readonly ICreateManager _createManager;
        private readonly ISiteRepository _siteRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly IContentRepository _contentRepository;

        public ContentsLayerTaxisController(IAuthManager authManager, ICreateManager createManager, ISiteRepository siteRepository, IChannelRepository channelRepository, IContentRepository contentRepository)
        {
            _authManager = authManager;
            _createManager = createManager;
            _siteRepository = siteRepository;
            _channelRepository = channelRepository;
            _contentRepository = contentRepository;
        }

<<<<<<< HEAD
        public class SubmitRequest : ChannelRequest
        {
            public string ChannelContentIds { get; set; }
            public bool IsUp { get; set; }
            public int Taxis { get; set; }
=======
        public class GetRequest : ChannelRequest
        {
            public string ChannelContentIds { get; set; }
        }

        public class GetResult
        {
            public IEnumerable<Content> Contents { get; set; }
            public int TotalCount { get; set; }
        }

        public class SubmitRequest : ChannelRequest
        {
            public string ChannelContentIds { get; set; }
            public string Type { get; set; }
            public int Value { get; set; }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        }
    }
}
