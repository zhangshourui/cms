﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Datory;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Menu;
using SSCMS.Core.Utils;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Utils;

namespace SSCMS.Core.Services
{
    public partial class WxManager
    {
        public async Task PullMaterialAsync(string accessTokenOrAppId, MaterialType materialType, int groupId)
        {
            var count = await MediaApi.GetMediaCountAsync(accessTokenOrAppId);
            if (materialType == MaterialType.Message)
            {
                if (count.news_count > 0)
                {
                    var newsList = await MediaApi.GetNewsMediaListAsync(accessTokenOrAppId, 0, count.news_count);
                    newsList.item.Reverse();

                    foreach (var message in newsList.item)
                    {
                        if (await _materialMessageRepository.IsExistsAsync(message.media_id)) continue;

                        //var news = await MediaApi.GetForeverNewsAsync(accessTokenOrAppId, message.media_id);
                        var messageItems = new List<MaterialMessageItem>();
                        foreach (var item in message.content.news_item)
                        {
                            var imageUrl = string.Empty;
                            if (!string.IsNullOrEmpty(item.thumb_media_id) && !string.IsNullOrEmpty(item.thumb_url))
                            {
                                await using var ms = new MemoryStream();
                                await MediaApi.GetForeverMediaAsync(accessTokenOrAppId, item.thumb_media_id, ms);
                                ms.Seek(0, SeekOrigin.Begin);

                                var extName = "png";
                                if (StringUtils.Contains(item.thumb_url, "wx_fmt="))
                                {
                                    extName = item.thumb_url.Substring(item.thumb_url.LastIndexOf("=", StringComparison.Ordinal) + 1);
                                }

                                var materialFileName = PathUtils.GetMaterialFileNameByExtName(extName);
                                var virtualDirectoryPath = PathUtils.GetMaterialVirtualDirectoryPath(UploadType.Image);

                                var directoryPath = PathUtils.Combine(_settingsManager.WebRootPath, virtualDirectoryPath);
                                var filePath = PathUtils.Combine(directoryPath, materialFileName);

                                await FileUtils.WriteStreamAsync(filePath, ms);

                                imageUrl = PageUtils.Combine(virtualDirectoryPath, materialFileName);
                            }
                            else if (!string.IsNullOrEmpty(item.thumb_url))
                            {
                                var extName = "png";
                                if (StringUtils.Contains(item.thumb_url, "wx_fmt="))
                                {
                                    extName = item.thumb_url.Substring(item.thumb_url.LastIndexOf("=", StringComparison.Ordinal) + 1);
                                }

                                var materialFileName = PathUtils.GetMaterialFileNameByExtName(extName);
                                var virtualDirectoryPath = PathUtils.GetMaterialVirtualDirectoryPath(UploadType.Image);

                                var directoryPath = PathUtils.Combine(_settingsManager.WebRootPath, virtualDirectoryPath);
                                var filePath = PathUtils.Combine(directoryPath, materialFileName);

                                await HttpClientUtils.DownloadAsync(item.thumb_url, filePath);

                                imageUrl = PageUtils.Combine(virtualDirectoryPath, materialFileName);
                            }

                            var commentType = CommentType.Block;
                            if (item.need_open_comment == 1)
                            {
                                commentType = item.only_fans_can_comment == 1 ? CommentType.OnlyFans : CommentType.Everyone;
                            }

                            messageItems.Add(new MaterialMessageItem
                            {
                                MessageId = 0,
                                MaterialType = MaterialType.Article,
                                MaterialId = 0,
                                Taxis = 0,
                                ThumbMediaId = item.thumb_media_id,
                                Author = item.author,
                                Title = item.title,
                                ContentSourceUrl = item.content_source_url,
                                Content = await SaveImagesAsync(item.content),
                                Digest = item.digest,
                                ShowCoverPic = item.show_cover_pic == "1",
                                ThumbUrl = imageUrl,
                                Url = item.url,
                                CommentType = commentType
                            });
                        }

                        await _materialMessageRepository.InsertAsync(groupId, message.media_id, messageItems);
                    }
                }
            }
            else if (materialType == MaterialType.Image)
            {
                if (count.image_count > 0)
                {
                    var list = await MediaApi.GetOthersMediaListAsync(accessTokenOrAppId, UploadMediaFileType.image, 0, count.image_count);

                    foreach (var image in list.item)
                    {
                        if (await _materialImageRepository.IsExistsAsync(image.media_id)) continue;

                        await using var ms = new MemoryStream();
                        await MediaApi.GetForeverMediaAsync(accessTokenOrAppId, image.media_id, ms);
                        ms.Seek(0, SeekOrigin.Begin);

                        var extName = image.url.Substring(image.url.LastIndexOf("=", StringComparison.Ordinal) + 1);

                        var materialFileName = PathUtils.GetMaterialFileNameByExtName(extName);
                        var virtualDirectoryPath = PathUtils.GetMaterialVirtualDirectoryPath(UploadType.Image);

                        var directoryPath = PathUtils.Combine(_settingsManager.WebRootPath, virtualDirectoryPath);
                        var filePath = PathUtils.Combine(directoryPath, materialFileName);

                        await FileUtils.WriteStreamAsync(filePath, ms);

                        var material = new MaterialImage
                        {
                            GroupId = groupId,
                            Title = image.name,
                            Url = PageUtils.Combine(virtualDirectoryPath, materialFileName),
                            MediaId = image.media_id
                        };

                        await _materialImageRepository.InsertAsync(material);
                    }
                }
            }
            else if (materialType == MaterialType.Audio)
            {
                if (count.voice_count > 0)
                {
                    var list = await MediaApi.GetOthersMediaListAsync(accessTokenOrAppId, UploadMediaFileType.voice, 0, count.voice_count);

                    foreach (var voice in list.item)
                    {
                        if (await _materialAudioRepository.IsExistsAsync(voice.media_id)) continue;

                        await using var ms = new MemoryStream();
                        await MediaApi.GetForeverMediaAsync(accessTokenOrAppId, voice.media_id, ms);
                        ms.Seek(0, SeekOrigin.Begin);

                        var extName = voice.url.Substring(voice.url.LastIndexOf("=", StringComparison.Ordinal) + 1);

                        var materialFileName = PathUtils.GetMaterialFileNameByExtName(extName);
                        var virtualDirectoryPath = PathUtils.GetMaterialVirtualDirectoryPath(UploadType.Audio);

                        var directoryPath = PathUtils.Combine(_settingsManager.WebRootPath, virtualDirectoryPath);
                        var filePath = PathUtils.Combine(directoryPath, materialFileName);

                        await FileUtils.WriteStreamAsync(filePath, ms);

                        var audio = new MaterialAudio
                        {
                            GroupId = groupId,
                            Title = voice.name,
                            FileType = extName.ToUpper().Replace(".", string.Empty),
                            Url = PageUtils.Combine(virtualDirectoryPath, materialFileName),
                            MediaId = voice.media_id
                        };

                        await _materialAudioRepository.InsertAsync(audio);
                    }
                }
            }
            else if (materialType == MaterialType.Video)
            {
                if (count.video_count > 0)
                {
                    var list = await MediaApi.GetOthersMediaListAsync(accessTokenOrAppId, UploadMediaFileType.video, 0, count.video_count);

                    foreach (var video in list.item)
                    {
                        if (await _materialVideoRepository.IsExistsAsync(video.media_id)) continue;

                        await using var ms = new MemoryStream();
                        await MediaApi.GetForeverMediaAsync(accessTokenOrAppId, video.media_id, ms);
                        ms.Seek(0, SeekOrigin.Begin);

                        var extName = "mp4";

                        if (!string.IsNullOrEmpty(video.url))
                        {
                            extName = video.url.Substring(video.url.LastIndexOf("=", StringComparison.Ordinal) + 1);
                        }

                        var materialFileName = PathUtils.GetMaterialFileNameByExtName(extName);
                        var virtualDirectoryPath = PathUtils.GetMaterialVirtualDirectoryPath(UploadType.Video);

                        var directoryPath = PathUtils.Combine(_settingsManager.WebRootPath, virtualDirectoryPath);
                        var filePath = PathUtils.Combine(directoryPath, materialFileName);

                        await FileUtils.WriteStreamAsync(filePath, ms);

                        var material = new MaterialVideo
                        {
                            GroupId = groupId,
                            Title = video.name,
                            FileType = extName.ToUpper().Replace(".", string.Empty),
                            Url = PageUtils.Combine(virtualDirectoryPath, materialFileName),
                            MediaId = video.media_id
                        };

                        await _materialVideoRepository.InsertAsync(material);
                    }
                }
            }
        }

        public async Task<string> PushMaterialAsync(string accessTokenOrAppId, MaterialType materialType, int materialId)
        {
            string mediaId = null;

            if (materialType == MaterialType.Message)
            {
                var message = await _materialMessageRepository.GetAsync(materialId);

                var newsList = new List<NewsModel>();
                foreach (var item in message.Items)
                {
                    var news = new NewsModel
                    {
                        thumb_media_id = item.ThumbMediaId,
                        author = item.Author,
                        title = item.Title,
                        content_source_url = item.ContentSourceUrl,
                        content = item.Content,
                        digest = item.Digest,
                        show_cover_pic = item.ShowCoverPic ? "1" : "0",
                        thumb_url = item.ThumbUrl,
                        need_open_comment = item.CommentType == CommentType.Block ? 0 : 1,
                        only_fans_can_comment = item.CommentType == CommentType.OnlyFans ? 1 : 0
                    };
                    newsList.Add(news);
                }

                mediaId = message.MediaId;
                if (string.IsNullOrEmpty(mediaId))
                {
                    var result = await MediaApi.UploadNewsAsync(accessTokenOrAppId, 10000, newsList.ToArray());
                    mediaId = result.media_id;
                    await _materialMessageRepository.UpdateMediaIdAsync(materialId, mediaId);
                }
                else
                {
                    var index = 0;
                    foreach (var news in newsList)
                    {
                        await MediaApi.UpdateForeverNewsAsync(accessTokenOrAppId, message.MediaId, index++, news);
                    }
                }

                // sync article url
                var media = await MediaApi.GetForeverNewsAsync(accessTokenOrAppId, mediaId);
                for (var i = 0; i < message.Items.Count; i++)
                {
                    var item = media.news_item[i];
                    await _materialArticleRepository.UpdateUrlAsync(message.Items[i].MaterialId, item.url);
                }
            }
            else if (materialType == MaterialType.Image)
            {
                var image = await _materialImageRepository.GetAsync(materialId);
                mediaId = image.MediaId;
                if (string.IsNullOrEmpty(mediaId))
                {
                    var filePath = _pathManager.ParsePath(image.Url);
                    if (FileUtils.IsFileExists(filePath))
                    {
                        var result = await MediaApi.UploadForeverMediaAsync(accessTokenOrAppId, filePath, UploadForeverMediaType.image);
                        mediaId = result.media_id;
                        await _materialImageRepository.UpdateMediaIdAsync(materialId, mediaId);
                    }
                }
            }
            else if (materialType == MaterialType.Audio)
            {
                var audio = await _materialAudioRepository.GetAsync(materialId);
                mediaId = audio.MediaId;
                if (string.IsNullOrEmpty(mediaId))
                {
                    var filePath = _pathManager.ParsePath(audio.Url);
                    if (FileUtils.IsFileExists(filePath))
                    {
                        var result = await MediaApi.UploadForeverMediaAsync(accessTokenOrAppId, filePath, UploadForeverMediaType.voice);
                        mediaId = result.media_id;
                        await _materialAudioRepository.UpdateMediaIdAsync(materialId, mediaId);
                    }
                }
            }
            else if (materialType == MaterialType.Video)
            {
                var video = await _materialVideoRepository.GetAsync(materialId);
                mediaId = video.MediaId;
                if (string.IsNullOrEmpty(mediaId))
                {
                    var filePath = _pathManager.ParsePath(video.Url);
                    if (FileUtils.IsFileExists(filePath))
                    {
                        var result = await MediaApi.UploadForeverMediaAsync(accessTokenOrAppId, filePath, UploadForeverMediaType.voice);
                        mediaId = result.media_id;
                        await _materialVideoRepository.UpdateMediaIdAsync(materialId, mediaId);
                    }
                }
            }

            return mediaId;
        }

        public async Task PullMenuAsync(string accessTokenOrAppId, int siteId)
        {
            var result = CommonApi.GetMenu(accessTokenOrAppId);

<<<<<<< HEAD
            if (result == null) return;
=======
            if (result == null || result.menu == null || result.menu.button == null) return;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            await _wxMenuRepository.DeleteAllAsync(siteId);

            var json = result.menu.button.ToJson();
            var buttons = TranslateUtils.JsonDeserialize<List<MenuFull_RootButton>>(json);

            var firstTaxis = 1;
            foreach (var button in buttons)
            {

                var first = new WxMenu
                {
                    SiteId = siteId,
                    ParentId = 0,
                    Taxis = firstTaxis++,
                    Text = button.name,
                    MenuType = TranslateUtils.ToEnum(button.type, WxMenuType.View),
                    Key = button.key,
                    Url = button.url,
                    AppId = button.appid,
                    PagePath = button.pagepath,
                    MediaId = button.media_id
                };
                var menuId = await _wxMenuRepository.InsertAsync(first);
                if (button.sub_button != null && button.sub_button.Count > 0)
                {
                    var childTaxis = 1;
                    foreach (var sub in button.sub_button)
                    {
                        var child = new WxMenu
                        {
                            SiteId = siteId,
                            ParentId = menuId,
                            Taxis = childTaxis++,
                            Text = sub.name,
                            MenuType = TranslateUtils.ToEnum(sub.type, WxMenuType.View),
                            Key = sub.key,
                            Url = sub.url,
                            AppId = sub.appid,
                            PagePath = sub.pagepath,
                            MediaId = sub.media_id
                        };
                        await _wxMenuRepository.InsertAsync(child);
                    }
                }
            }
        }

        public async Task PushMenuAsync(string accessTokenOrAppId, int siteId)
        {
            var resultFull = new GetMenuResultFull
            {
                menu = new MenuFull_ButtonGroup
                {
                    button = new List<MenuFull_RootButton>()
                }
            };

            var openMenus = await _wxMenuRepository.GetMenusAsync(siteId);

            foreach (var firstMenu in openMenus.Where(x => x.ParentId == 0))
            {
                var root = new MenuFull_RootButton
                {
                    name = firstMenu.Text,
                    type = firstMenu.MenuType.GetValue(),
                    url = firstMenu.Url,
                    key = firstMenu.Key,
                    sub_button = new List<MenuFull_RootButton>()
                };
                foreach (var child in openMenus.Where(x => x.ParentId == firstMenu.Id))
                {
                    root.sub_button.Add(new MenuFull_RootButton
                    {
                        name = child.Text,
                        type = child.MenuType.GetValue(),
                        url = child.Url,
                        key = child.Key
                    });
                }

                resultFull.menu.button.Add(root);
            }

            var buttonGroup = CommonApi.GetMenuFromJsonResult(resultFull, new ButtonGroup()).menu;
            var result = CommonApi.CreateMenu(accessTokenOrAppId, buttonGroup);

            if (result.errmsg != "ok")
            {
                throw new Exception(result.errmsg);
            }
        }
    }
}
