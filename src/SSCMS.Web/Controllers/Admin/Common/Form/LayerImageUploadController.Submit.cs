﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Common.Form
{
    public partial class LayerImageUploadController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<List<SubmitResult>>> Submit([FromBody] SubmitRequest request)
        {
            var result = new List<SubmitResult>();

            if (request.SiteId > 0)
            {
                var site = await _siteRepository.GetAsync(request.SiteId);
                if (site == null) return this.Error("无法确定内容对应的站点");

                var isAutoStorage = await _storageManager.IsAutoStorageAsync(request.SiteId, SyncType.Images);

                foreach (var filePath in request.FilePaths)
                {
                    if (string.IsNullOrEmpty(filePath)) continue;

                    var fileName = PathUtils.GetFileName(filePath);

                    var fileExtName = StringUtils.ToLower(PathUtils.GetExtension(filePath));
                    var localDirectoryPath = await _pathManager.GetUploadDirectoryPathAsync(site, fileExtName);

                    var virtualUrl = await _pathManager.GetVirtualUrlByPhysicalPathAsync(site, filePath);
                    var imageUrl = await _pathManager.ParseSiteUrlAsync(site, virtualUrl, true);
                    if (isAutoStorage)
                    {
                        var (success, url) = await _storageManager.StorageAsync(request.SiteId, filePath);
                        if (success)
                        {
                            virtualUrl = imageUrl = url;
                        }
                    }

                    if (request.IsMaterial)
                    {
                        var materialFileName = PathUtils.GetMaterialFileName(fileName);
                        var virtualDirectoryPath = PathUtils.GetMaterialVirtualDirectoryPath(UploadType.Image);

                        var directoryPath = _pathManager.ParsePath(virtualDirectoryPath);
                        var materialFilePath = PathUtils.Combine(directoryPath, materialFileName);
                        DirectoryUtils.CreateDirectoryIfNotExists(materialFilePath);

                        FileUtils.CopyFile(filePath, materialFilePath, true);

                        var image = new MaterialImage
                        {
                            GroupId = -request.SiteId,
                            Title = fileName,
                            Url = PageUtils.Combine(virtualDirectoryPath, materialFileName)
                        };

                        await _materialImageRepository.InsertAsync(image);
                    }

                    if (request.IsThumb)
                    {
                        var localSmallFileName = Constants.SmallImageAppendix + fileName;
                        var localSmallFilePath = PathUtils.Combine(localDirectoryPath, localSmallFileName);

                        var thumbnailVirtualUrl = await _pathManager.GetVirtualUrlByPhysicalPathAsync(site, localSmallFilePath);
                        var thumbnailUrl = await _pathManager.ParseSiteUrlAsync(site, thumbnailVirtualUrl, true);
                        _pathManager.ResizeImageByMax(filePath, localSmallFilePath, request.ThumbWidth, request.ThumbHeight);
                        
                        if (isAutoStorage)
                        {
                            var (success, url) = await _storageManager.StorageAsync(request.SiteId, localSmallFilePath);
                            if (success)
                            {
                                thumbnailVirtualUrl = thumbnailUrl = url;
                            }
                        }

                        if (request.IsLinkToOriginal)
                        {
                            result.Add(new SubmitResult
                            {
                                ImageUrl = thumbnailUrl,
                                ImageVirtualUrl = thumbnailVirtualUrl,
                                PreviewUrl = imageUrl,
                                PreviewVirtualUrl = virtualUrl
                            });
                        }
                        else
                        {
                            FileUtils.DeleteFileIfExists(filePath);
                            result.Add(new SubmitResult
                            {
                                ImageUrl = thumbnailUrl,
                                ImageVirtualUrl = thumbnailVirtualUrl
                            });
                        }
                    }
                    else
                    {
                        result.Add(new SubmitResult
                        {
                            ImageUrl = imageUrl,
                            ImageVirtualUrl = virtualUrl
                        });
                    }
                }

<<<<<<< HEAD
                var options = TranslateUtils.JsonDeserialize(site.Get<string>(nameof(LayerImageUploadController)), new Options
                {
                    IsEditor = true,
                    IsMaterial = true,
                    IsThumb = false,
                    ThumbWidth = 1024,
                    ThumbHeight = 1024,
                    IsLinkToOriginal = true,
                });
=======
                var options = GetOptions(site);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

                options.IsEditor = request.IsEditor;
                options.IsMaterial = request.IsMaterial;
                options.IsThumb = request.IsThumb;
                options.ThumbWidth = request.ThumbWidth;
                options.ThumbHeight = request.ThumbHeight;
                options.IsLinkToOriginal = request.IsLinkToOriginal;
<<<<<<< HEAD
                site.Set(nameof(LayerImageUploadController), TranslateUtils.JsonSerialize(options));

=======

                SetOptions(site, options);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                await _siteRepository.UpdateAsync(site);
            }
            else if (request.UserId > 0)
            {
                foreach (var filePath in request.FilePaths)
                {
                    if (string.IsNullOrEmpty(filePath)) continue;

                    var fileName = PathUtils.GetFileName(filePath);
                    var userFilePath = _pathManager.GetUserUploadPath(request.UserId, fileName);
                    FileUtils.CopyFile(filePath, userFilePath, true);

                    var imageUrl = _pathManager.GetUserUploadUrl(request.UserId, fileName);
                    result.Add(new SubmitResult
                    {
                        ImageUrl = imageUrl,
                        ImageVirtualUrl = imageUrl
                    });
                }
            }

            return result;
        }
    }
}