using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Utils;
using SSCMS.Core.Utils;
<<<<<<< HEAD
=======
using SSCMS.Enums;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

namespace SSCMS.Web.Controllers.Admin.Cms.Contents
{
    public partial class ContentsLayerImportController
    {
        [RequestSizeLimit(long.MaxValue)]
        [HttpPost, Route(RouteUpload)]
<<<<<<< HEAD
        public async Task<ActionResult<UploadResult>> Upload([FromQuery] ChannelRequest request, [FromForm] IFormFile file)
=======
        public async Task<ActionResult<UploadResult>> Upload([FromQuery] UploadRequest request, [FromForm] IFormFile file)
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                    MenuUtils.SitePermissions.Contents) ||
                !await _authManager.HasContentPermissionsAsync(request.SiteId, request.ChannelId, MenuUtils.ContentPermissions.Add))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);

            if (file == null)
            {
                return this.Error(Constants.ErrorUpload);
            }

            var fileName = Path.GetFileName(file.FileName);
<<<<<<< HEAD

            var extendName = fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal));
            if (!StringUtils.EqualsIgnoreCase(extendName, ".zip") && !StringUtils.EqualsIgnoreCase(extendName, ".xlsx") && !StringUtils.EqualsIgnoreCase(extendName, ".txt"))
            {
                return this.Error(Constants.ErrorUpload);
            }

            var filePath = _pathManager.GetTemporaryFilesPath(fileName);

            await _pathManager.UploadAsync(file, filePath);

            var url = await _pathManager.GetSiteUrlByPhysicalPathAsync(site, filePath, true);

=======
            var filePath = _pathManager.GetTemporaryFilesPath(fileName);
            var url = string.Empty;

            if (request.ImportType == "zip")
            {
                if (!FileUtils.IsFileType(FileType.Zip, PathUtils.GetExtension(fileName)))
                {
                  return this.Error(Constants.ErrorUpload);
                }
            }
            else if (request.ImportType == "excel")
            {
                if (!FileUtils.IsFileType(FileType.Xlsx, PathUtils.GetExtension(fileName)))
                {
                  return this.Error(Constants.ErrorUpload);
                }
            }
            else if (request.ImportType == "image")
            {
                if (!FileUtils.IsImage(PathUtils.GetExtension(fileName)))
                {
                  return this.Error(Constants.ErrorUpload);
                }

                (_, filePath, _) = await _pathManager.UploadImageAsync(site, file);
                url = await _pathManager.GetVirtualUrlByPhysicalPathAsync(site, filePath);
            }
            else if (request.ImportType == "txt")
            {
                if (!FileUtils.IsFileType(FileType.Txt, PathUtils.GetExtension(fileName)))
                {
                  return this.Error(Constants.ErrorUpload);
                }
            }
            
            await _pathManager.UploadAsync(file, filePath);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            return new UploadResult
            {
                Name = fileName,
                Url = url
            };
        }
    }
}