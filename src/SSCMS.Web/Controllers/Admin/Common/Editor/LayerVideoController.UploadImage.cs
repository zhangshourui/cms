using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Enums;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Common.Editor
{
    public partial class LayerVideoController
    {
        [RequestSizeLimit(long.MaxValue)]
        [HttpPost, Route(RouteUploadImage)]
        public async Task<ActionResult<UploadResult>> UploadImage([FromQuery] SiteRequest request, [FromForm] IFormFile file)
        {
            var site = await _siteRepository.GetAsync(request.SiteId);

            if (file == null)
            {
                return this.Error(Constants.ErrorUpload);
            }

            var fileName = Path.GetFileName(file.FileName);

            var extName = PathUtils.GetExtension(fileName);
            if (!_pathManager.IsImageExtensionAllowed(site, extName))
            {
<<<<<<< HEAD
                return this.Error(Constants.ErrorVideoExtensionAllowed);
            }
            if (!_pathManager.IsImageSizeAllowed(site, file.Length))
            {
                return this.Error(Constants.ErrorVideoSizeAllowed);
=======
                return this.Error(Constants.ErrorImageExtensionAllowed);
            }
            if (!_pathManager.IsImageSizeAllowed(site, file.Length))
            {
                return this.Error(Constants.ErrorImageSizeAllowed);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }

            var localDirectoryPath = await _pathManager.GetUploadDirectoryPathAsync(site, UploadType.Image);
            var filePath = PathUtils.Combine(localDirectoryPath, _pathManager.GetUploadFileName(site, fileName));

            await _pathManager.UploadAsync(file, filePath);

            var imageUrl = await _pathManager.GetSiteUrlByPhysicalPathAsync(site, filePath, true);
            var isAutoStorage = await _storageManager.IsAutoStorageAsync(request.SiteId, SyncType.Images);
            if (isAutoStorage)
            {
                var (success, url) = await _storageManager.StorageAsync(request.SiteId, filePath);
                if (success)
                {
                    imageUrl = url;
                }
            }

            return new UploadResult
            {
                Name = fileName,
                Path = filePath,
                Url = imageUrl
            };
        }
    }
}
