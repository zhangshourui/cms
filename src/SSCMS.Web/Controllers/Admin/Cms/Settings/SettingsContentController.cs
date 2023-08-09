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
using SSCMS.Enums;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using SSCMS.Models;
using SSCMS.Repositories;
using SSCMS.Services;

namespace SSCMS.Web.Controllers.Admin.Cms.Settings
{
    [OpenApiIgnore]
    [Authorize(Roles = Types.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class SettingsContentController : ControllerBase
    {
        private const string Route = "cms/settings/settingsContent";

        private readonly IAuthManager _authManager;
<<<<<<< HEAD
        private readonly ICensorManager _censorManager;
        private readonly ISiteRepository _siteRepository;
        private readonly IContentRepository _contentRepository;

        public SettingsContentController(IAuthManager authManager, ICensorManager censorManager, ISiteRepository siteRepository, IContentRepository contentRepository)
        {
            _authManager = authManager;
            _censorManager = censorManager;
=======
        private readonly ISiteRepository _siteRepository;
        private readonly IContentRepository _contentRepository;

        public SettingsContentController(IAuthManager authManager, ISiteRepository siteRepository, IContentRepository contentRepository)
        {
            _authManager = authManager;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            _siteRepository = siteRepository;
            _contentRepository = contentRepository;
        }

        public class GetResult
        {
            public Site Site { get; set; }
<<<<<<< HEAD
            public bool IsCensorTextEnabled { get; set; }
=======
            public List<Select<string>> TaxisTypes { get; set; }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        }

        public class SubmitRequest : SiteRequest
        {
            public bool IsSaveImageInTextEditor { get; set; }
            public int PageSize { get; set; }
<<<<<<< HEAD
=======
            public TaxisType TaxisType { get; set; }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            public bool IsAutoPageInTextEditor { get; set; }
            public int AutoPageWordNum { get; set; }
            public bool IsContentTitleBreakLine { get; set; }
            public bool IsContentSubTitleBreakLine { get; set; }
            public int CheckContentLevel { get; set; }
            public int CheckContentDefaultLevel { get; set; }
        }
    }
}
