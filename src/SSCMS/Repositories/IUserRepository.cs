using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Enums;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public partial interface IUserRepository : IRepository
    {
<<<<<<< HEAD
        /// <summary>
        /// 注册一个一个新的用户
        /// </summary>
        /// <param name="user">用户对象 <see cref="SSCMS.Models.User"/>  </param>
        /// <param name="password">密码</param>
        /// <param name="ipAddress">注册IP</param>
        /// <param name="ignoreConfigLimit">是否忽略配置限制，true为忽略限制。默认false，按配置限制。</param>
        /// <returns></returns>
        Task<(User user, string errorMessage)> InsertAsync(User user, string password, string ipAddress, bool ignoreConfigLimit = false);
=======
        Task<(User user, string errorMessage)> InsertAsync(User user, string password, string ipAddress);

        Task<int> InsertWithoutValidationAsync(User user, string password);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

        Task<(bool success, string errorMessage)> UpdateAsync(User user);

        Task UpdateLastActivityDateAndCountOfLoginAsync(User user);

        Task UpdateLastActivityDateAsync(User user);

        Task<(bool success, string errorMessage)> ChangePasswordAsync(int userId, string password);

        Task<(bool success, string errorMessage)> IsPasswordCorrectAsync(string password);

        Task CheckAsync(IList<int> userIds);

        Task LockAsync(IList<int> userIds);

        Task UnLockAsync(IList<int> userIds);

        Task<bool> IsUserNameExistsAsync(string userName);

        Task<bool> IsEmailExistsAsync(string email);

        Task<bool> IsMobileExistsAsync(string mobile);

        Task<List<int>> GetUserIdsAsync(bool isChecked);

        bool CheckPassword(string password, bool isPasswordMd5, string dbPassword, PasswordFormat passwordFormat,
            string passwordSalt);

        Task<(User user, string userName, string errorMessage)> ValidateAsync(string account, string password,
            bool isPasswordMd5);

        Task<(bool success, string errorMessage)> ValidateStateAsync(User user);

        Dictionary<DateTime, int> GetTrackingDictionary(DateTime dateFrom, DateTime dateTo, string xType);

        Task<int> GetCountAsync();

        Task<int> GetCountAsync(bool? state, int groupId, int dayOfLastActivity, string keyword);

        Task<List<User>> GetUsersAsync(bool? state, int groupId, int dayOfLastActivity, string keyword, string order,
            int offset, int limit);

        Task<bool> IsExistsAsync(int id);

        Task<User> DeleteAsync(int userId);
    }
}
