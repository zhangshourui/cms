using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
<<<<<<< HEAD
using SSCMS.Configuration;
=======
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.V1
{
    public partial class UsersController
    {
        [OpenApiOperation("新增用户 API", "注册新用户，使用POST发起请求，请求地址为/api/v1/users")]
        [HttpPost, Route(Route)]
<<<<<<< HEAD
        public async Task<ActionResult<User>> Create([FromBody] User request)
=======
        public async Task<ActionResult<User>> Create([FromBody]User request)
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            var config = await _configRepository.GetAsync();

            if (!config.IsUserRegistrationGroup)
            {
                request.GroupId = 0;
            }
            var password = request.Password;

<<<<<<< HEAD
            var ignoreLimit = await _accessTokenRepository.IsScopeAsync(_authManager.ApiToken, Constants.ScopeUsers);
            var ip = Utils.HttpClientUtils.GetPeerIP(this.HttpContext);

            var (user, errorMessage) = await _userRepository.InsertAsync(request, password, ip, ignoreLimit);
=======
            var (user, errorMessage) = await _userRepository.InsertAsync(request, password, string.Empty);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            if (user == null)
            {
                return this.Error(errorMessage);
            }

            await _statRepository.AddCountAsync(StatType.UserRegister);

            return user;
        }
    }
}
