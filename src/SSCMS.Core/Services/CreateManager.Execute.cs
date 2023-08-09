﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSCMS.Configuration;
using SSCMS.Core.StlParser.StlElement;
using SSCMS.Core.StlParser.Utility;
using SSCMS.Core.Utils;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Parse;
using SSCMS.Utils;

namespace SSCMS.Core.Services
{
    public partial class CreateManager
    {
        public async Task ExecuteAsync(int siteId, CreateType createType, int channelId = 0, int contentId = 0,
            int fileTemplateId = 0, int specialId = 0)
        {
            if (createType == CreateType.Index)
            {
                await ExecuteChannelAsync(siteId, siteId);
            }
            else if (createType == CreateType.Channel)
            {
                await ExecuteChannelAsync(siteId, channelId);
            }
            else if (createType == CreateType.Content)
            {
                var site = await _siteRepository.GetAsync(siteId);
<<<<<<< HEAD
                var channelInfo = await _channelRepository.GetAsync(channelId);
                await ExecuteContentAsync(site, channelInfo, contentId);
=======
                var channel = await _channelRepository.GetAsync(channelId);
                await ExecuteContentAsync(site, channel, contentId);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }
            else if (createType == CreateType.AllContent)
            {
                await ExecuteContentsAsync(siteId, channelId);
            }
            else if (createType == CreateType.File)
            {
                await ExecuteFileAsync(siteId, fileTemplateId);
            }
            else if (createType == CreateType.Special)
            {
                await ExecuteSpecialAsync(siteId, specialId);
            }
            else if (createType == CreateType.All)
            {
                var channelIdList = await _channelRepository.GetChannelIdsAsync(siteId);
                foreach (var theChannelId in channelIdList)
                {
                    await ExecuteChannelAsync(siteId, theChannelId);
                }

                foreach (var theChannelId in channelIdList)
                {
                    await ExecuteContentsAsync(siteId, theChannelId);
                }

                foreach (var theSpecialId in await _specialRepository.GetAllSpecialIdsAsync(siteId))
                {
                    await ExecuteSpecialAsync(siteId, theSpecialId);
                }

                foreach (var theFileTemplateId in await _templateRepository.GetAllFileTemplateIdsAsync(siteId))
                {
                    await ExecuteFileAsync(siteId, theFileTemplateId);
                }
            }
        }

        private async Task ExecuteContentsAsync(int siteId, int channelId)
        {
            var site = await _siteRepository.GetAsync(siteId);
<<<<<<< HEAD
            var channelInfo = await _channelRepository.GetAsync(channelId);

            var contentIdList = await _contentRepository.GetContentIdsCheckedAsync(site, channelInfo);

            foreach (var contentId in contentIdList)
            {
                await ExecuteContentAsync(site, channelInfo, contentId);
=======
            var channel = await _channelRepository.GetAsync(channelId);
            if (channel.IsCreateBanned) return;

            var contentIdList = await _contentRepository.GetContentIdsCheckedAsync(site, channel);

            foreach (var contentId in contentIdList)
            {
                await ExecuteContentAsync(site, channel, contentId);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }
        }

        private async Task ExecuteChannelAsync(int siteId, int channelId)
        {
            var site = await _siteRepository.GetAsync(siteId);
<<<<<<< HEAD
            var channelInfo = await _channelRepository.GetAsync(channelId);

            var count = await _contentRepository.GetCountAsync(site, channelInfo);
            if (!_channelRepository.IsCreatable(site, channelInfo, count)) return;

            var template = channelId == siteId
                ? await _templateRepository.GetIndexPageTemplateAsync(siteId)
                : await _templateRepository.GetChannelTemplateAsync(siteId, channelInfo);
            var filePath = await _pathManager.GetChannelPageFilePathAsync(site, channelId);

            await _parseManager.InitAsync(EditMode.Default, site, channelId, 0, template);
=======
            var channel = await _channelRepository.GetAsync(channelId);

            var count = await _contentRepository.GetCountAsync(site, channel);
            if (!_channelRepository.IsCreatable(site, channel, count)) return;

            var template = channelId == siteId
                ? await _templateRepository.GetIndexPageTemplateAsync(siteId)
                : await _templateRepository.GetChannelTemplateAsync(siteId, channel);
            var filePath = await _pathManager.GetChannelPageFilePathAsync(site, channelId);

            await _parseManager.InitAsync(EditMode.Default, site, channelId, 0, template, 0);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            _parseManager.ContextInfo.ContextType = ParseType.Channel;

            await ExecuteAsync(site, template, filePath);
        }

        private async Task ExecuteAsync(Site site, Template template, string filePathWithoutPage)
        {
            var contentBuilder = new StringBuilder(await _pathManager.GetTemplateContentAsync(site, template));

            var stlLabelList = ParseUtils.GetStlLabels(contentBuilder.ToString());

            //如果标签中存在<stl:channel type="PageContent"></stl:channel>
            if (StlParserUtility.IsStlChannelElementWithTypePageContent(stlLabelList)) //内容存在
            {
                var stlElement = StlParserUtility.GetStlChannelElementWithTypePageContent(stlLabelList);
                var stlElementTranslated = _parseManager.StlEncrypt(stlElement);
                contentBuilder.Replace(stlElement, stlElementTranslated);

                var innerBuilder = new StringBuilder(stlElement);
                await _parseManager.ParseInnerContentAsync(innerBuilder);
                var pageContentHtml = innerBuilder.ToString();
                var pageCount = StringUtils.GetCount(Constants.PagePlaceHolder, pageContentHtml) + 1; //一共需要的页数

                await _parseManager.ParseAsync(contentBuilder, filePathWithoutPage, false);

                for (var currentPageIndex = 0; currentPageIndex < pageCount; currentPageIndex++)
                {
                    var page = _parseManager.PageInfo;
                    _parseManager.PageInfo = _parseManager.PageInfo.Clone();

                    var index = pageContentHtml.IndexOf(Constants.PagePlaceHolder, StringComparison.Ordinal);
                    var length = index == -1 ? pageContentHtml.Length : index;

                    var pageHtml = pageContentHtml.Substring(0, length);
                    var pagedBuilder =
                        new StringBuilder(contentBuilder.ToString().Replace(stlElementTranslated, pageHtml));
                    await _parseManager.ReplacePageElementsInChannelPageAsync(pagedBuilder, stlLabelList, currentPageIndex, pageCount, 0);

                    var filePath = _pathManager.GetPageFilePathAsync(filePathWithoutPage, currentPageIndex);
                    await GenerateFileAsync(filePath, pagedBuilder);

                    if (index != -1)
                    {
                        pageContentHtml = pageContentHtml.Substring(length + Constants.PagePlaceHolder.Length);
                    }

                    _parseManager.PageInfo = page;
                }
            }
            //如果标签中存在<stl:pageContents>
            else if (ParseUtils.IsStlElementExists(StlPageContents.ElementName, stlLabelList))
            {
                var stlElement = ParseUtils.GetStlElement(StlPageContents.ElementName, stlLabelList);
                var stlElementTranslated = _parseManager.StlEncrypt(stlElement);

                var pageContentsElementParser = await StlPageContents.GetAsync(stlElement, _parseManager);
                var (pageCount, totalNum) = pageContentsElementParser.GetPageCount();

                await _parseManager.PageInfo.AddPageHeadCodeIfNotExistsAsync(ParsePage.Const.Jquery);
                await _parseManager.ParseAsync(contentBuilder, filePathWithoutPage, false);

                for (var currentPageIndex = 0; currentPageIndex < pageCount; currentPageIndex++)
                {
                    var page = _parseManager.PageInfo;
                    _parseManager.PageInfo = _parseManager.PageInfo.Clone();

                    var pageHtml = await pageContentsElementParser.ParseAsync(totalNum, currentPageIndex, pageCount, true);
                    var pagedBuilder =
                        new StringBuilder(contentBuilder.ToString().Replace(stlElementTranslated, pageHtml));

                    await _parseManager.ReplacePageElementsInChannelPageAsync(pagedBuilder, stlLabelList, currentPageIndex, pageCount, totalNum);

                    var filePath = _pathManager.GetPageFilePathAsync(filePathWithoutPage, currentPageIndex);

                    await GenerateFileAsync(filePath, pagedBuilder);

                    _parseManager.PageInfo = page;
                }
            }
            //如果标签中存在<stl:pageChannels>
            else if (ParseUtils.IsStlElementExists(StlPageChannels.ElementName, stlLabelList))
            {
                var stlElement = ParseUtils.GetStlElement(StlPageChannels.ElementName, stlLabelList);
                var stlElementTranslated = _parseManager.StlEncrypt(stlElement);

                var pageChannelsElementParser = await StlPageChannels.GetAsync(stlElement, _parseManager);
                var pageCount = pageChannelsElementParser.GetPageCount(out var totalNum);

                await _parseManager.PageInfo.AddPageHeadCodeIfNotExistsAsync(ParsePage.Const.Jquery);
                await _parseManager.ParseAsync(contentBuilder, filePathWithoutPage, false);

                for (var currentPageIndex = 0; currentPageIndex < pageCount; currentPageIndex++)
                {
                    var page = _parseManager.PageInfo;
                    _parseManager.PageInfo = _parseManager.PageInfo.Clone();

                    var pageHtml = await pageChannelsElementParser.ParseAsync(currentPageIndex, pageCount);
                    var pagedBuilder =
                        new StringBuilder(contentBuilder.ToString().Replace(stlElementTranslated, pageHtml));

                    await _parseManager.ReplacePageElementsInChannelPageAsync(pagedBuilder, stlLabelList, currentPageIndex, pageCount, totalNum);

                    var filePath = _pathManager.GetPageFilePathAsync(filePathWithoutPage, currentPageIndex);
                    await GenerateFileAsync(filePath, pagedBuilder);

                    _parseManager.PageInfo = page;
                }
            }
            //如果标签中存在<stl:pageSqlContents>
            else if (ParseUtils.IsStlElementExists(StlPageSqlContents.ElementName, stlLabelList))
            {
                var stlElement = ParseUtils.GetStlElement(StlPageSqlContents.ElementName, stlLabelList);
                var stlElementTranslated = _parseManager.StlEncrypt(stlElement);

                var pageSqlContentsElementParser = await StlPageSqlContents.GetAsync(stlElement, _parseManager);
                var pageCount = pageSqlContentsElementParser.GetPageCount(out var totalNum);

                await _parseManager.PageInfo.AddPageHeadCodeIfNotExistsAsync(ParsePage.Const.Jquery);
                await _parseManager.ParseAsync(contentBuilder, filePathWithoutPage, false);

                for (var currentPageIndex = 0; currentPageIndex < pageCount; currentPageIndex++)
                {
                    var page = _parseManager.PageInfo;
                    _parseManager.PageInfo = _parseManager.PageInfo.Clone();

                    var pageHtml = await pageSqlContentsElementParser.ParseAsync(totalNum, currentPageIndex, pageCount);
                    var pagedBuilder =
                        new StringBuilder(contentBuilder.ToString().Replace(stlElementTranslated, pageHtml));

                    await _parseManager.ReplacePageElementsInChannelPageAsync(pagedBuilder, stlLabelList, currentPageIndex, pageCount, totalNum);

                    var filePath = _pathManager.GetPageFilePathAsync(filePathWithoutPage, currentPageIndex);
                    await GenerateFileAsync(filePath, pagedBuilder);

                    _parseManager.PageInfo = page;
                }
            }
            else
            {
                await _parseManager.ParseAsync(contentBuilder, filePathWithoutPage, false);
                await GenerateFileAsync(filePathWithoutPage, contentBuilder);
            }
        }

        private async Task ExecuteContentAsync(Site site, Channel channel, int contentId)
        {
<<<<<<< HEAD
=======
            if (channel.IsCreateBanned) return;

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            var contentInfo = await _contentRepository.GetAsync(site, channel, contentId);

            if (contentInfo == null)
            {
                return;
            }

            if (!contentInfo.Checked)
            {
                await DeleteContentAsync(site, channel.Id, contentId);
                return;
            }

            if (!ContentUtility.IsCreatable(channel, contentInfo)) return;

            if (site.IsCreateStaticContentByAddDate &&
                contentInfo.AddDate < site.CreateStaticContentAddDate)
            {
                return;
            }

            var template = await _templateRepository.GetContentTemplateAsync(site.Id, channel, contentInfo.TemplateId);
<<<<<<< HEAD
            await _parseManager.InitAsync(EditMode.Default, site, channel.Id, contentId, template);
=======
            await _parseManager.InitAsync(EditMode.Default, site, channel.Id, contentId, template, 0);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            _parseManager.ContextInfo.ContextType = ParseType.Content;
            _parseManager.ContextInfo.SetContent(contentInfo);

            var filePath = await _pathManager.GetContentPageFilePathAsync(site, _parseManager.PageInfo.PageChannelId, contentInfo, 0);
            var contentBuilder = new StringBuilder(await _pathManager.GetTemplateContentAsync(site, template));

            var stlLabelList = ParseUtils.GetStlLabels(contentBuilder.ToString());

            //如果标签中存在<stl:content type="PageContent"></stl:content>
            if (StlParserUtility.IsStlContentElementWithTypePageContent(stlLabelList)) //内容存在
            {
                var stlElement = StlParserUtility.GetStlContentElementWithTypePageContent(stlLabelList);
                var stlElementTranslated = _parseManager.StlEncrypt(stlElement);
                contentBuilder.Replace(stlElement, stlElementTranslated);

                var innerBuilder = new StringBuilder(stlElement);
                await _parseManager.ParseInnerContentAsync(innerBuilder);
                var pageContentHtml = innerBuilder.ToString();
                var pageCount = StringUtils.GetCount(Constants.PagePlaceHolder, pageContentHtml) + 1; //一共需要的页数

                await _parseManager.ParseAsync(contentBuilder, filePath, false);

                for (var currentPageIndex = 0; currentPageIndex < pageCount; currentPageIndex++)
                {
                    var page = _parseManager.PageInfo;
                    _parseManager.PageInfo = _parseManager.PageInfo.Clone();

                    var index = pageContentHtml.IndexOf(Constants.PagePlaceHolder, StringComparison.Ordinal);
                    var length = index == -1 ? pageContentHtml.Length : index;

                    var pageHtml = pageContentHtml.Substring(0, length);
                    var pagedBuilder =
                        new StringBuilder(contentBuilder.ToString().Replace(stlElementTranslated, pageHtml));
                    await _parseManager.ReplacePageElementsInContentPageAsync(pagedBuilder, stlLabelList, currentPageIndex, pageCount);

                    filePath = await _pathManager.GetContentPageFilePathAsync(site, page.PageChannelId, contentInfo, currentPageIndex);
                    await GenerateFileAsync(filePath, pagedBuilder);

                    if (index != -1)
                    {
                        pageContentHtml = pageContentHtml.Substring(length + Constants.PagePlaceHolder.Length);
                    }

                    _parseManager.PageInfo = page;
                }
            }
            //如果标签中存在<stl:channel type="PageContent"></stl:channel>
            else if (StlParserUtility.IsStlChannelElementWithTypePageContent(stlLabelList)) //内容存在
            {
                var stlElement = StlParserUtility.GetStlChannelElementWithTypePageContent(stlLabelList);
                var stlElementTranslated = _parseManager.StlEncrypt(stlElement);
                contentBuilder.Replace(stlElement, stlElementTranslated);

                var innerBuilder = new StringBuilder(stlElement);
                await _parseManager.ParseInnerContentAsync(innerBuilder);
                var pageContentHtml = innerBuilder.ToString();
                var pageCount = StringUtils.GetCount(Constants.PagePlaceHolder, pageContentHtml) + 1; //一共需要的页数

                await _parseManager.ParseAsync(contentBuilder, filePath, false);

                for (var currentPageIndex = 0; currentPageIndex < pageCount; currentPageIndex++)
                {
                    var page = _parseManager.PageInfo;
                    _parseManager.PageInfo = _parseManager.PageInfo.Clone();

                    var index = pageContentHtml.IndexOf(Constants.PagePlaceHolder, StringComparison.Ordinal);
                    var length = index == -1 ? pageContentHtml.Length : index;

                    var pageHtml = pageContentHtml.Substring(0, length);
                    var pagedBuilder =
                        new StringBuilder(contentBuilder.ToString().Replace(stlElementTranslated, pageHtml));
                    await _parseManager.ReplacePageElementsInContentPageAsync(pagedBuilder, stlLabelList, currentPageIndex, pageCount);

                    filePath = await _pathManager.GetContentPageFilePathAsync(site, page.PageChannelId, contentInfo, currentPageIndex);
                    await GenerateFileAsync(filePath, pagedBuilder);

                    if (index != -1)
                    {
                        pageContentHtml = pageContentHtml.Substring(length + Constants.PagePlaceHolder.Length);
                    }

                    _parseManager.PageInfo = page;
                }
            }
            //如果标签中存在<stl:pageContents>
            else if (ParseUtils.IsStlElementExists(StlPageContents.ElementName, stlLabelList))
            {
                var stlElement = ParseUtils.GetStlElement(StlPageContents.ElementName, stlLabelList);
                var stlElementTranslated = _parseManager.StlEncrypt(stlElement);

                var pageContentsElementParser = await StlPageContents.GetAsync(stlElement, _parseManager);
                var (pageCount, totalNum) = pageContentsElementParser.GetPageCount();

                await _parseManager.ParseAsync(contentBuilder, filePath, false);

                for (var currentPageIndex = 0; currentPageIndex < pageCount; currentPageIndex++)
                {
                    var page = _parseManager.PageInfo;
                    _parseManager.PageInfo = _parseManager.PageInfo.Clone();

                    var pageHtml = await pageContentsElementParser.ParseAsync(totalNum, currentPageIndex, pageCount, true);
                    var pagedBuilder =
                        new StringBuilder(contentBuilder.ToString().Replace(stlElementTranslated, pageHtml));

                    await _parseManager.ReplacePageElementsInContentPageAsync(pagedBuilder, stlLabelList, currentPageIndex, pageCount);

                    filePath = await _pathManager.GetContentPageFilePathAsync(site, page.PageChannelId, contentInfo, currentPageIndex);
                    await GenerateFileAsync(filePath, pagedBuilder);

                    _parseManager.PageInfo = page;
                }
            }
            //如果标签中存在<stl:pageChannels>
            else if (ParseUtils.IsStlElementExists(StlPageChannels.ElementName, stlLabelList))
            {
                var stlElement = ParseUtils.GetStlElement(StlPageChannels.ElementName, stlLabelList);
                var stlElementTranslated = _parseManager.StlEncrypt(stlElement);

                var pageChannelsElementParser = await StlPageChannels.GetAsync(stlElement, _parseManager);
                var pageCount = pageChannelsElementParser.GetPageCount(out _);

                await _parseManager.ParseAsync(contentBuilder, filePath, false);

                for (var currentPageIndex = 0; currentPageIndex < pageCount; currentPageIndex++)
                {
                    var page = _parseManager.PageInfo;
                    _parseManager.PageInfo = _parseManager.PageInfo.Clone();

                    var pageHtml = await pageChannelsElementParser.ParseAsync(currentPageIndex, pageCount);
                    var pagedBuilder =
                        new StringBuilder(contentBuilder.ToString().Replace(stlElementTranslated, pageHtml));

                    await _parseManager.ReplacePageElementsInContentPageAsync(pagedBuilder, stlLabelList, currentPageIndex, pageCount);

                    filePath = await _pathManager.GetContentPageFilePathAsync(site, page.PageChannelId, contentInfo, currentPageIndex);
                    await GenerateFileAsync(filePath, pagedBuilder);

                    _parseManager.PageInfo = page;
                }
            }
            //如果标签中存在<stl:pageSqlContents>
            else if (ParseUtils.IsStlElementExists(StlPageSqlContents.ElementName, stlLabelList))
            {
                var stlElement = ParseUtils.GetStlElement(StlPageSqlContents.ElementName, stlLabelList);
                var stlElementTranslated = _parseManager.StlEncrypt(stlElement);

                var pageSqlContentsElementParser = await StlPageSqlContents.GetAsync(stlElement, _parseManager);
                var pageCount = pageSqlContentsElementParser.GetPageCount(out var totalNum);

                await _parseManager.ParseAsync(contentBuilder, filePath, false);

                for (var currentPageIndex = 0; currentPageIndex < pageCount; currentPageIndex++)
                {
                    var page = _parseManager.PageInfo;
                    _parseManager.PageInfo = _parseManager.PageInfo.Clone();

                    var pageHtml = await pageSqlContentsElementParser.ParseAsync(totalNum, currentPageIndex, pageCount);
                    var pagedBuilder =
                        new StringBuilder(contentBuilder.ToString().Replace(stlElementTranslated, pageHtml));

                    await _parseManager.ReplacePageElementsInContentPageAsync(pagedBuilder, stlLabelList, currentPageIndex, pageCount);

                    filePath = await _pathManager.GetContentPageFilePathAsync(site, page.PageChannelId, contentInfo, currentPageIndex);
                    await GenerateFileAsync(filePath, pagedBuilder);

                    _parseManager.PageInfo = page;
                }
            }
            else //无翻页
            {
                await _parseManager.ParseAsync(contentBuilder, filePath, false);
                await GenerateFileAsync(filePath, contentBuilder);
            }
        }

        private async Task ExecuteFileAsync(int siteId, int fileTemplateId)
        {
            var site = await _siteRepository.GetAsync(siteId);
            var template = await _templateRepository.GetAsync(fileTemplateId);
            if (template == null || template.TemplateType != TemplateType.FileTemplate)
            {
                return;
            }

<<<<<<< HEAD
            await _parseManager.InitAsync(EditMode.Default, site, siteId, 0, template);
=======
            await _parseManager.InitAsync(EditMode.Default, site, siteId, 0, template, 0);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            var filePath = await _pathManager.ParseSitePathAsync(site, template.CreatedFileFullName);

            await ExecuteAsync(site, template, filePath);

            //var contentBuilder = new StringBuilder(await _pathManager.GetTemplateContentAsync(site, template));
            //await _parseManager.ParseAsync(contentBuilder, filePath, false);
            //await GenerateFileAsync(filePath, contentBuilder);
        }

        private async Task ExecuteSpecialAsync(int siteId, int specialId)
        {
            var site = await _siteRepository.GetAsync(siteId);
            var templates = await _pathManager.GetSpecialTemplateListAsync(site, specialId);

            foreach (var template in templates)
            {
<<<<<<< HEAD
                await _parseManager.InitAsync(EditMode.Default, site, siteId, 0, template);
=======
                await _parseManager.InitAsync(EditMode.Default, site, siteId, 0, template, specialId);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

                var filePath = await _pathManager.ParseSitePathAsync(site, template.CreatedFileFullName);

                var contentBuilder = new StringBuilder(template.Content);
                await _parseManager.ParseAsync(contentBuilder, filePath, false);
                await GenerateFileAsync(filePath, contentBuilder);
            }
        }

        private async Task GenerateFileAsync(string filePath, StringBuilder contentBuilder)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            try
            {
                await ReplaceCDNAsync(contentBuilder);
                await FileUtils.WriteTextAsync(filePath, contentBuilder.ToString());
            }
            catch
            {
                FileUtils.RemoveReadOnlyAndHiddenIfExists(filePath);
                await FileUtils.WriteTextAsync(filePath, contentBuilder.ToString());
            }
        }

        private async Task ReplaceCDNAsync(StringBuilder contentBuilder)
        {
            var isCloudCdnImage = false;
            var isCloudCdnFiles = false;
            var pageInfo = _parseManager.PageInfo;
            if (pageInfo.Config.CloudUserId > 0 && pageInfo.Config.IsCloudCdn)
            {
                isCloudCdnImage = pageInfo.Config.IsCloudCdnImages;
                isCloudCdnFiles = pageInfo.Config.IsCloudCdnFiles;
            }

            if (!isCloudCdnImage && !isCloudCdnFiles) return;

            var webUrl = pageInfo.Site.IsSeparatedWeb ? PageUtils.Combine(pageInfo.Site.SeparatedWebUrl, "/") : "/";
            var siteDirs = await _siteRepository.GetSiteDirCascadingAsync(pageInfo.SiteId);

            var storageFiles = await _storageFileRepository.GetStorageFileListAsync();
            var siteFiles = storageFiles.Where(x => !FileUtils.IsHtml(x.FileType));
            if (!string.IsNullOrEmpty(siteDirs))
            {
                siteFiles = siteFiles.Where(x => StringUtils.StartsWithIgnoreCase(x.Key, siteDirs));
            }

            foreach (var file in siteFiles)
            {
                if (FileUtils.IsImage(file.FileType))
                {
                    if (isCloudCdnImage)
                    {
                        contentBuilder.Replace($"{webUrl}{file.Key}", $"{CloudManager.DomainDns}/{pageInfo.Config.CloudUserId}/{file.Key}");
                    }
                }
                else
                {
                    if (isCloudCdnFiles)
                    {
                        contentBuilder.Replace($"{webUrl}{file.Key}", $"{CloudManager.DomainDns}/{pageInfo.Config.CloudUserId}/{file.Key}");
                    }
                }
            }
        }
    }
}