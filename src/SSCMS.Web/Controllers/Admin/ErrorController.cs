<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
=======
﻿using System;
using Microsoft.AspNetCore.Mvc;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using NSwag.Annotations;
using SSCMS.Configuration;
using SSCMS.Models;
using SSCMS.Repositories;

namespace SSCMS.Web.Controllers.Admin
{
<<<<<<< HEAD
    [OpenApiIgnore]
    [Route(Constants.ApiAdminPrefix)]
    public partial class ErrorController : ControllerBase
    {
        public const string Route = "error";

        private readonly IErrorLogRepository _errorLogRepository;

        public ErrorController(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public class GetResult
        {
            public ErrorLog Error { get; set; }
        }
    }
=======
  [OpenApiIgnore]
  [Route(Constants.ApiAdminPrefix)]
  public partial class ErrorController : ControllerBase
  {
    public const string Route = "error";

    private readonly IErrorLogRepository _errorLogRepository;

    public ErrorController(IErrorLogRepository errorLogRepository)
    {
      _errorLogRepository = errorLogRepository;
    }

    public class GetResult
    {
      public string Message { get; set; }
      public string StackTrace { get; set; }
      public string Summary { get; set; }
      public DateTime? CreatedDate { get; set; }
    }
  }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
}
