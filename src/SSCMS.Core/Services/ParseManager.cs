﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Datory;
using SSCMS.Configuration;
using SSCMS.Core.Context;
using SSCMS.Core.StlParser.StlElement;
using SSCMS.Core.Utils;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Parse;
using SSCMS.Plugins;
using SSCMS.Services;
using SSCMS.Utils;

namespace SSCMS.Core.Services
{
    public partial class ParseManager : IParseManager
    {
        public ISettingsManager SettingsManager { get; }
        public IPathManager PathManager { get; }
        public IDatabaseManager DatabaseManager { get; }
        public IFormManager FormManager { get; }
        private readonly IPluginManager _pluginManager;

        public ParseManager(ISettingsManager settingsManager, IPathManager pathManager, IDatabaseManager databaseManager, IFormManager formManager, IPluginManager pluginManager)
        {
            SettingsManager = settingsManager;
            PathManager = pathManager;
            DatabaseManager = databaseManager;
            FormManager = formManager;
            _pluginManager = pluginManager;
        }

        public ParsePage PageInfo { get; set; }
        public ParseContext ContextInfo { get; set; }

<<<<<<< HEAD
        public async Task InitAsync(EditMode editMode, Site site, int pageChannelId, int pageContentId, Template template)
        {
            var config = await DatabaseManager.ConfigRepository.GetAsync();
            PageInfo = new ParsePage(PathManager, editMode, config, pageChannelId, pageContentId, site, template, new Dictionary<string, object>());
=======
        public async Task InitAsync(EditMode editMode, Site site, int pageChannelId, int pageContentId, Template template, int specialId)
        {
            var config = await DatabaseManager.ConfigRepository.GetAsync();
            PageInfo = new ParsePage(PathManager, editMode, config, pageChannelId, pageContentId, specialId, site, template, new Dictionary<string, object>());
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            ContextInfo = new ParseContext(PageInfo);
        }

        public async Task ParseAsync(StringBuilder contentBuilder, string filePath, bool isPreview)
        {
            var context = new PluginParseContext(this);

            var startParsesAsync = _pluginManager.GetExtensions<IPluginCreateStartAsync>();
            if (startParsesAsync != null)
            {
                foreach (var extension in startParsesAsync)
                {
                    try
                    {
                        await extension.ParseAsync(context);
                    }
                    catch (Exception ex)
                    {
                        await AddStlErrorLogAsync(nameof(IPluginCreateStartAsync), string.Empty,
                            ex);
                    }
                }
            }

            var startParses = _pluginManager.GetExtensions<IPluginCreateStart>();
            if (startParses != null)
            {
                foreach (var extension in startParses)
                {
                    try
                    {
                        extension.Parse(context);
                    }
                    catch (Exception ex)
                    {
                        await AddStlErrorLogAsync(nameof(IPluginCreateStartAsync), string.Empty,
                            ex);
                    }
                }
            }

            if (contentBuilder.Length > 0)
            {
                await ParseTemplateContentAsync(contentBuilder);
            }

            var endParsesAsync = _pluginManager.GetExtensions<IPluginCreateEndAsync>();
            if (endParsesAsync != null)
            {
                foreach (var extension in endParsesAsync)
                {
                    try
                    {
                        await extension.ParseAsync(context);
                    }
                    catch (Exception ex)
                    {
                        await AddStlErrorLogAsync(nameof(IPluginCreateEndAsync), string.Empty,
                            ex);
                    }
                }
            }

            var endParses = _pluginManager.GetExtensions<IPluginCreateEnd>();
            if (endParses != null)
            {
                foreach (var extension in endParses)
                {
                    try
                    {
                        extension.Parse(context);
                    }
                    catch (Exception ex)
                    {
                        await AddStlErrorLogAsync(nameof(IPluginCreateEnd), string.Empty,
                            ex);
                    }
                }
            }

            if (FileUtils.IsHtml(PathUtils.GetExtension(filePath)))
            {
                if (isPreview)
                {
                    var pageUrl = PageUtils.AddProtocolToUrl(
                        PathManager.ParseUrl(
                            $"~/{PathUtils.GetPathDifference(SettingsManager.WebRootPath, filePath)}"));
                    string templateString = $@"
<base href=""{pageUrl}"" />";
                    StringUtils.InsertAfter(new[] { "<head>", "<HEAD>" }, contentBuilder, templateString);
                }

                PostProcess(contentBuilder, filePath);
            }
        }

        public async Task<string> GetDynamicScriptAsync(string dynamicApiUrl, Dynamic dynamic)
        {
            return await StlDynamic.GetScriptAsync(this, dynamicApiUrl, dynamic);
        }

        public async Task<string> ParseDynamicAsync(Dynamic dynamic, string template)
        {
            return await StlDynamic.ParseDynamicAsync(this, dynamic, template);
        }

        public async Task<string> AddStlErrorLogAsync(string elementName, string stlContent, Exception ex)
        {
            var summary = $@"站点名称：{PageInfo.Site.SiteName}，
模板类型：{PageInfo.Template.TemplateType.GetDisplayName()}，
模板名称：{PageInfo.Template.TemplateName}
<br />";

            summary += $@"STL标签：{StringUtils.HtmlEncode(stlContent)}";
            await DatabaseManager.ErrorLogRepository.AddErrorLogAsync(new ErrorLog
            {
                Id = 0,
                Category = LogUtils.CategoryStl,
                PluginId = string.Empty,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Summary = summary,
            });

            return $@"
<!--
{elementName}
error: {ex.Message}
stl: {stlContent}
-->";
        }

        public async Task<Channel> GetChannelAsync()
        {
            var channel = ContextInfo.Channel;
            if (channel != null) return channel;
            if (ContextInfo.ChannelId <= 0) return null;
            channel = await DatabaseManager.ChannelRepository.GetAsync(ContextInfo.ChannelId);
            ContextInfo.SetChannel(channel);
            return channel;
        }

        public async Task<Content> GetContentAsync()
        {
            var content = ContextInfo.Content;
            if (content != null) return content;
            if (ContextInfo.ContentId <= 0) return null;
            content = await DatabaseManager.ContentRepository.GetAsync(ContextInfo.Site, ContextInfo.ChannelId, ContextInfo.ContentId);
            ContextInfo.SetContent(content);
            return content;
        }
    }
}