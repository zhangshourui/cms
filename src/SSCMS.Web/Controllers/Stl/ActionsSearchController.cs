﻿using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SSCMS.Configuration;
using SSCMS.Core.StlParser.Models;
using SSCMS.Repositories;
using SSCMS.Services;

namespace SSCMS.Web.Controllers.Stl
{
    [OpenApiIgnore]
    [Route(Constants.ApiPrefix + Constants.ApiStlPrefix)]
    public partial class ActionsSearchController : ControllerBase
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IAuthManager _authManager;
        private readonly IParseManager _parseManager;
        private readonly IDatabaseManager _databaseManager;
        private readonly ISiteRepository _siteRepository;
        private readonly IContentRepository _contentRepository;

        public ActionsSearchController(ISettingsManager settingsManager, IAuthManager authManager, IParseManager parseManager, IDatabaseManager databaseManager, ISiteRepository siteRepository, IContentRepository contentRepository)
        {
            _settingsManager = settingsManager;
            _authManager = authManager;
            _parseManager = parseManager;
            _databaseManager = databaseManager;
            _siteRepository = siteRepository;
            _contentRepository = contentRepository;
        }

<<<<<<< HEAD
        private NameValueCollection GetPostCollection(StlSearchRequest request)
=======
        private static NameValueCollection GetPostCollection(StlSearchRequest request)
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            var formCollection = new NameValueCollection();
            if (request != null)
            {
                foreach (var key in request.GetKeys())
                {
                    var value = request.Get(key);
                    if (value != null)
                    {
                        formCollection[key] = request.Get(key).ToString();
                    }
                }
            }

            return formCollection;
        }
    }
}
