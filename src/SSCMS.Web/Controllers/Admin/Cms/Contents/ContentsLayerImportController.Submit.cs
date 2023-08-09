﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Utils;
using SSCMS.Core.Utils.Serialization;
using SSCMS.Dto;
using SSCMS.Enums;
using SSCMS.Configuration;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Contents
{
    public partial class ContentsLayerImportController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<BoolResult>> Submit([FromBody] SubmitRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                    MenuUtils.SitePermissions.Contents) ||
                !await _authManager.HasContentPermissionsAsync(request.SiteId, request.ChannelId, MenuUtils.ContentPermissions.Add))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var channelInfo = await _channelRepository.GetAsync(request.ChannelId);
            if (channelInfo == null) return this.Error("无法确定内容对应的栏目");

            var caching = new CacheUtils(_cacheManager);
            var isChecked = request.CheckedLevel >= site.CheckContentLevel;

            var contentIdList = new List<int>();

            var adminId = _authManager.AdminId;
            if (request.ImportType == "zip")
            {
                foreach (var fileName in request.FileNames)
                {
                    var localFilePath = _pathManager.GetTemporaryFilesPath(fileName);

                    if (!FileUtils.IsFileType(FileType.Zip, PathUtils.GetExtension(localFilePath)))
                        continue;

                    var importObject = new ImportObject(_pathManager, _databaseManager, caching, site, adminId);
                    contentIdList.AddRange(await importObject.ImportContentsByZipFileAsync(channelInfo, localFilePath, request.IsOverride, isChecked, request.CheckedLevel, adminId, 0, SourceManager.Default));
                }
            }
            else if (request.ImportType == "excel")
            {
                foreach (var fileName in request.FileNames)
                {
                    var localFilePath = _pathManager.GetTemporaryFilesPath(fileName);

                    if (!FileUtils.IsFileType(FileType.Xlsx, PathUtils.GetExtension(localFilePath)))
                        continue;

                    var importObject = new ImportObject(_pathManager, _databaseManager, caching, site, adminId);
                    contentIdList.AddRange(await importObject.ImportContentsByXlsxFileAsync(channelInfo, localFilePath, request.IsOverride, isChecked, request.CheckedLevel, adminId, 0, SourceManager.Default));
                }
            }
<<<<<<< HEAD
=======
            else if (request.ImportType == "image")
            {
                for(var i = 0; i < request.FileNames.Count; i++)
                {
                  var fileName = request.FileNames[i];
                  var fileUrl = request.FileUrls[i];

                  var importObject = new ImportObject(_pathManager, _databaseManager, caching, site, adminId);
                    contentIdList.AddRange(await importObject.ImportContentsByImageFileAsync(channelInfo, fileName, fileUrl, request.IsOverride, isChecked, request.CheckedLevel, adminId, 0, SourceManager.Default));
                }
            }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            else if (request.ImportType == "txt")
            {
                foreach (var fileName in request.FileNames)
                {
                    var localFilePath = _pathManager.GetTemporaryFilesPath(fileName);
                    if (!FileUtils.IsFileType(FileType.Txt, PathUtils.GetExtension(localFilePath)))
                        continue;

                    var importObject = new ImportObject(_pathManager, _databaseManager, caching, site, adminId);
                    contentIdList.AddRange(await importObject.ImportContentsByTxtFileAsync(channelInfo, localFilePath, request.IsOverride, isChecked, request.CheckedLevel, adminId, 0, SourceManager.Default));
                }
            }

            foreach (var contentId in contentIdList)
            {
                await _createManager.CreateContentAsync(request.SiteId, channelInfo.Id, contentId);
            }
            await _createManager.CreateChannelAsync(request.SiteId, channelInfo.Id);

            await _authManager.AddSiteLogAsync(request.SiteId, request.ChannelId, 0, "导入内容", string.Empty);

<<<<<<< HEAD
=======
            var options = GetOptions(site);

            options.ImportType = request.ImportType;
            options.IsOverride = request.IsOverride;
            
            SetOptions(site, options);
            await _siteRepository.UpdateAsync(site);

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            return new BoolResult
            {
                Value = true
            };
        }
    }
}