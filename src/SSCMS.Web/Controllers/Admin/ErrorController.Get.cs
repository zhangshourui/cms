using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using SSCMS.Utils;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

namespace SSCMS.Web.Controllers.Admin
{
    public partial class ErrorController
    {
        [HttpGet, Route(Route)]
        public async Task<GetResult> Get([FromQuery] int logId)
        {
<<<<<<< HEAD
            return new GetResult
            {
                Error = await _errorLogRepository.GetErrorLogAsync(logId)
=======
            var error = await _errorLogRepository.GetErrorLogAsync(logId);

            return new GetResult
            {
                Message = StringUtils.Trim(error.Message),
                StackTrace = StringUtils.Trim(error.StackTrace),
                Summary = StringUtils.Trim(error.Summary),
                CreatedDate = error.CreatedDate,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            };
        }
    }
}
