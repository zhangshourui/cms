﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using SSCMS.Configuration;
using SSCMS.Core.StlParser.Attributes;
using SSCMS.Core.Utils;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Services;
using SSCMS.Utils;

namespace SSCMS.Core.StlParser.StlElement
{
    [StlElement(Title = "获取站点值", Description = "通过 stl:site 标签在模板中显示站点值")]
    public static class StlSite
    {
        public const string ElementName = "stl:site";

        [StlAttribute(Title = "站点名称")]
        private const string SiteName = nameof(SiteName);

        [StlAttribute(Title = "站点文件夹")]
        private const string SiteDir = nameof(SiteDir);

        [StlAttribute(Title = "类型")]
        private const string Type = nameof(Type);

        [StlAttribute(Title = "显示的格式")]
        private const string Format = nameof(Format);

        [StlAttribute(Title = "显示的格式")]
        private const string FormatString = nameof(FormatString);

        [StlAttribute(Title = "显示第几项")]
        private const string No = nameof(No);

        [StlAttribute(Title = "显示多项时的分割字符串")]
        private const string Separator = nameof(Separator);

        [StlAttribute(Title = "字符开始位置")]
        private const string StartIndex = nameof(StartIndex);

        [StlAttribute(Title = "指定字符长度")]
        private const string Length = nameof(Length);

        [StlAttribute(Title = "显示字符的数目")]
        private const string WordNum = nameof(WordNum);

        [StlAttribute(Title = "文字超出部分显示的文字")]
        private const string Ellipsis = nameof(Ellipsis);

        [StlAttribute(Title = "需要替换的文字，可以是正则表达式")]
        private const string Replace = nameof(Replace);

        [StlAttribute(Title = "替换replace的文字信息")]
        private const string To = nameof(To);

        [StlAttribute(Title = "是否清除HTML标签")]
        private const string IsClearTags = nameof(IsClearTags);

        [StlAttribute(Title = "是否清除空格")]
        private const string IsClearBlank = nameof(IsClearBlank);

        [StlAttribute(Title = "是否将回车替换为HTML换行标签")]
        private const string IsReturnToBr = nameof(IsReturnToBr);

        [StlAttribute(Title = "是否转换为小写")]
        private const string IsLower = nameof(IsLower);

        [StlAttribute(Title = "是否转换为大写")]
        private const string IsUpper = nameof(IsUpper);

        private const string TypeId = nameof(Site.Id);
        private const string TypeSiteName = nameof(Site.SiteName);
        private const string TypeImageUrl = nameof(Site.ImageUrl);
        private const string TypeKeywords = nameof(Site.Keywords);
        private const string TypeDescription = nameof(Site.Description);
        private const string TypeSiteUrl = "SiteUrl";

        public static SortedList<string, string> TypeList => new SortedList<string, string>
        {
            {TypeId, "站点Id"},
            {TypeSiteName, "站点名称"},
            {TypeImageUrl, "站点图片/LOGO"},
            {TypeKeywords, "站点关键字"},
            {TypeDescription, "站点描述"},
            {TypeSiteUrl, "站点的域名地址"}
        };

        internal static async Task<object> ParseAsync(IParseManager parseManager)
        {
            var siteName = string.Empty;
            var siteDir = string.Empty;

            var type = string.Empty;
            var format = string.Empty;
            var no = "0";
            string separator = null;
            var startIndex = 0;
            var length = 0;
            var wordNum = 0;
            var ellipsis = Constants.Ellipsis;
            var replace = string.Empty;
            var to = string.Empty;
            var isClearTags = false;
            var isClearBlank = false;
<<<<<<< HEAD
            var isReturnToBr = false;
=======
            var isReturnToBrStr = string.Empty;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            var isLower = false;
            var isUpper = false;
            var attributes = new NameValueCollection();

            foreach (var name in parseManager.ContextInfo.Attributes.AllKeys)
            {
                var value = parseManager.ContextInfo.Attributes[name];

                if (StringUtils.EqualsIgnoreCase(name, SiteName))
                {
                    siteName = await parseManager.ReplaceStlEntitiesForAttributeValueAsync(value);
                }
                else if (StringUtils.EqualsIgnoreCase(name, SiteDir))
                {
                    siteDir = await parseManager.ReplaceStlEntitiesForAttributeValueAsync(value);
                }
                else if (StringUtils.EqualsIgnoreCase(name, Type))
                {
                    type = value;
                }
                else if (StringUtils.EqualsIgnoreCase(name, Format) || StringUtils.EqualsIgnoreCase(name, FormatString))
                {
                    format = value;
                }
                else if (StringUtils.EqualsIgnoreCase(name, No))
                {
                    no = value;
                }
                else if (StringUtils.EqualsIgnoreCase(name, Separator))
                {
                    separator = value;
                }
                else if (StringUtils.EqualsIgnoreCase(name, StartIndex))
                {
                    startIndex = TranslateUtils.ToInt(value);
                }
                else if (StringUtils.EqualsIgnoreCase(name, Length))
                {
                    length = TranslateUtils.ToInt(value);
                }
                else if (StringUtils.EqualsIgnoreCase(name, WordNum))
                {
                    wordNum = TranslateUtils.ToInt(value);
                }
                else if (StringUtils.EqualsIgnoreCase(name, Ellipsis))
                {
                    ellipsis = value;
                }
                else if (StringUtils.EqualsIgnoreCase(name, Replace))
                {
                    replace = value;
                }
                else if (StringUtils.EqualsIgnoreCase(name, To))
                {
                    to = value;
                }
                else if (StringUtils.EqualsIgnoreCase(name, IsClearTags))
                {
                    isClearTags = TranslateUtils.ToBool(value, false);
                }
                else if (StringUtils.EqualsIgnoreCase(name, IsClearBlank))
                {
                    isClearBlank = TranslateUtils.ToBool(value, false);
                }
                else if (StringUtils.EqualsIgnoreCase(name, IsReturnToBr))
                {
<<<<<<< HEAD
                    isReturnToBr = TranslateUtils.ToBool(value, false);
=======
                    isReturnToBrStr = value;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                }
                else if (StringUtils.EqualsIgnoreCase(name, IsLower))
                {
                    isLower = TranslateUtils.ToBool(value, true);
                }
                else if (StringUtils.EqualsIgnoreCase(name, IsUpper))
                {
                    isUpper = TranslateUtils.ToBool(value, true);
                }
                else
                {
                    attributes[name] = value;
                }
            }

            var site = parseManager.ContextInfo.Site;

            if (!string.IsNullOrEmpty(siteName))
            {
                site = await parseManager.DatabaseManager.SiteRepository.GetSiteBySiteNameAsync(siteName);
            }
            else if (!string.IsNullOrEmpty(siteDir))
            {
                site = await parseManager.DatabaseManager.SiteRepository.GetSiteByDirectoryAsync(siteDir);
            }

            if (parseManager.ContextInfo.IsStlEntity && string.IsNullOrEmpty(type))
            {
                return site;
            }

<<<<<<< HEAD
            return await ParseAsync(parseManager, site, type, format, no, separator, startIndex, length, wordNum, ellipsis, replace, to, isClearTags, isClearBlank, isReturnToBr, isLower, isUpper, attributes);
        }

        private static async Task<string> ParseAsync(IParseManager parseManager, Site site, string type, string format, string no, string separator, int startIndex, int length, int wordNum, string ellipsis, string replace, string to, bool isClearTags, bool isClearBlank, bool isReturnToBr, bool isLower, bool isUpper, NameValueCollection attributes)
=======
            return await ParseAsync(parseManager, site, type, format, no, separator, startIndex, length, wordNum, ellipsis, replace, to, isClearTags, isClearBlank, isReturnToBrStr, isLower, isUpper, attributes);
        }

        private static async Task<string> ParseAsync(IParseManager parseManager, Site site, string type, string format, string no, string separator, int startIndex, int length, int wordNum, string ellipsis, string replace, string to, bool isClearTags, bool isClearBlank, string isReturnToBrStr, bool isLower, bool isUpper, NameValueCollection attributes)
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            var databaseManager = parseManager.DatabaseManager;
            var pageInfo = parseManager.PageInfo;
            var contextInfo = parseManager.ContextInfo;

            if (site == null) return string.Empty;

            var parsedContent = string.Empty;

            if (!string.IsNullOrEmpty(contextInfo.InnerHtml))
            {
                var preSite = pageInfo.Site;
                var prePageChannelId = pageInfo.PageChannelId;
                var prePageContentId = pageInfo.PageContentId;

                pageInfo.ChangeSite(site, site.Id, 0, contextInfo);

                var innerBuilder = new StringBuilder(contextInfo.InnerHtml);
                await parseManager.ParseInnerContentAsync(innerBuilder);
                parsedContent = innerBuilder.ToString();

                pageInfo.ChangeSite(preSite, prePageChannelId, prePageContentId, contextInfo);

                return parsedContent;
            }

            var inputType = InputType.Text;

            if (StringUtils.EqualsIgnoreCase(type, TypeSiteName))
            {
                parsedContent = site.SiteName;
            }
            else if (StringUtils.EqualsIgnoreCase(type, TypeId))
            {
                parsedContent = site.Id.ToString();
            }
            else if (StringUtils.EqualsIgnoreCase(type, TypeImageUrl))
            {
                var inputParser = new InputParserManager(parseManager.PathManager, parseManager.DatabaseManager.RelatedFieldItemRepository);

                if (no == "all")
                {
                    var sbParsedContent = new StringBuilder();
                    //第一条
                    sbParsedContent.Append(contextInfo.IsStlEntity
                        ? await parseManager.PathManager.ParseSiteUrlAsync(site, site.ImageUrl, pageInfo.IsLocal)
                        : await inputParser.GetImageOrFlashHtmlAsync(site, site.ImageUrl, attributes, false));

                    //第n条
                    var countName = ColumnsManager.GetCountName(nameof(Content.ImageUrl));
                    var count = site.Get<int>(countName);
                    for (var i = 1; i <= count; i++)
                    {
                        var extendName = ColumnsManager.GetExtendName(nameof(Content.ImageUrl), i);
                        var extend = site.Get<string>(extendName);

                        sbParsedContent.Append(contextInfo.IsStlEntity
                            ? await parseManager.PathManager.ParseSiteUrlAsync(pageInfo.Site, extend, pageInfo.IsLocal)
                            : await inputParser.GetImageOrFlashHtmlAsync(pageInfo.Site, extend, attributes, false));
                    }

                    parsedContent = sbParsedContent.ToString();
                }
                else
                {
                    var num = TranslateUtils.ToInt(no);
                    if (num <= 1)
                    {
                        parsedContent = contextInfo.IsStlEntity
                            ? await parseManager.PathManager.ParseSiteUrlAsync(site, site.ImageUrl, pageInfo.IsLocal)
                            : await inputParser.GetImageOrFlashHtmlAsync(site, site.ImageUrl, attributes, false);
                    }
                    else
                    {
                        var extendName = ColumnsManager.GetExtendName(nameof(Site.ImageUrl), num - 1);
                        var extend = site.Get<string>(extendName);
                        if (!string.IsNullOrEmpty(extend))
                        {
                            parsedContent = contextInfo.IsStlEntity
                                ? await parseManager.PathManager.ParseSiteUrlAsync(pageInfo.Site, extend,
                                    pageInfo.IsLocal)
                                : await inputParser.GetImageOrFlashHtmlAsync(pageInfo.Site, extend, attributes, false);
                        }
                    }
                }
            }
            else if (StringUtils.EqualsIgnoreCase(type, TypeKeywords))
            {
                parsedContent = site.Keywords;
            }
            else if (StringUtils.EqualsIgnoreCase(type, TypeDescription))
            {
                parsedContent = site.Description;
            }
            else if (StringUtils.EqualsIgnoreCase(type, TypeSiteUrl))
            {
                parsedContent = await parseManager.PathManager.GetWebUrlAsync(site);
            }
            else if (pageInfo.Site.Get<string>(type) != null)
            {
<<<<<<< HEAD
                

=======
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                var num = TranslateUtils.ToInt(no);
                if (num <= 1)
                {
                    parsedContent = site.Get<string>(type);
                }
                else
                {
                    var extendName = ColumnsManager.GetExtendName(type, num - 1);
                    parsedContent = site.Get<string>(extendName);
                }

                if (!string.IsNullOrEmpty(parsedContent))
                {
                    var styleInfo = await databaseManager.TableStyleRepository.GetTableStyleAsync(databaseManager.SiteRepository.TableName, type, databaseManager.TableStyleRepository.GetRelatedIdentities(pageInfo.SiteId));

                    if (styleInfo.Id > 0)
                    {
                        if (isClearTags && InputTypeUtils.EqualsAny(styleInfo.InputType, InputType.Image, InputType.Video, InputType.File))
                        {
                            parsedContent = await parseManager.PathManager.ParseSiteUrlAsync(pageInfo.Site, parsedContent, pageInfo.IsLocal);
                        }
                        else
                        {
                            var inputParser = new InputParserManager(parseManager.PathManager, parseManager.DatabaseManager.RelatedFieldItemRepository);

                            parsedContent = await inputParser.GetContentByTableStyleAsync(parsedContent, separator, pageInfo.Site, styleInfo, format, attributes, contextInfo.InnerHtml, contextInfo.IsStlEntity);

                            inputType = styleInfo.InputType;
                        }
                    }
                    else
                    {
                        // 如果字段已经被删除或不再显示了，则此字段的值为空。有时虚拟字段值不会清空
                        parsedContent = string.Empty;
                    }
                }
            }

            if (string.IsNullOrEmpty(parsedContent)) return string.Empty;

<<<<<<< HEAD
=======
            var isReturnToBr = false;
            if (string.IsNullOrEmpty(isReturnToBrStr))
            {
                if (inputType == InputType.TextArea)
                {
                    isReturnToBr = true;
                }
            }
            else
            {
                isReturnToBr = TranslateUtils.ToBool(isReturnToBrStr, true);
            }

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            return InputTypeUtils.ParseString(inputType, parsedContent, replace, to, startIndex, length, wordNum, ellipsis, isClearTags, isClearBlank, isReturnToBr, isLower, isUpper, format);
        }
    }
}
