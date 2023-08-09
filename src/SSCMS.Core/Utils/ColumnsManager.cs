﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using SSCMS.Configuration;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Services;
using SSCMS.Utils;

namespace SSCMS.Core.Utils
{
<<<<<<< HEAD
    public class ColumnsManager
    {
        private readonly IDatabaseManager _databaseManager;
        private readonly IPathManager _pathManager;

        public ColumnsManager(IDatabaseManager databaseManager, IPathManager pathManager)
        {
            _databaseManager = databaseManager;
            _pathManager = pathManager;
        }

        public const string PageContent = nameof(PageContent);
        public const string NavigationUrl = nameof(NavigationUrl);
        public const string CheckState = nameof(CheckState);
        public const string CheckAdminId = nameof(CheckAdminId);                    //审核人
        public const string CheckDate = nameof(CheckDate);                          //审核时间
        public const string CheckReasons = nameof(CheckReasons);                    //审核原因

        private const string Sequence = nameof(Sequence);                            //序号
        private const string ChannelName = nameof(ChannelName);
        private const string AdminName = nameof(AdminName);
        private const string LastEditAdminName = nameof(LastEditAdminName);
        private const string UserName = nameof(UserName);
        private const string CheckAdminName = nameof(CheckAdminName);
        private const string SourceName = nameof(SourceName);
        private const string TemplateName = nameof(TemplateName);
        private const string State = nameof(State);

        public static string GetFormatStringAttributeName(string attributeName)
        {
            return attributeName + "FormatString";
        }

        public static string GetCountName(string attributeName)
        {
            return $"{attributeName}Count";
        }

        public static string GetExtendName(string attributeName, int n)
        {
            return n > 0 ? $"{attributeName}{n}" : attributeName;
        }

        public static string GetColumnWidthName(string attributeName)
        {
            return $"{attributeName}ColumnWidth";
        }

        public static readonly Lazy<List<string>> MetadataAttributes = new Lazy<List<string>>(() => new List<string>
=======
  public class ColumnsManager
  {
    private readonly IDatabaseManager _databaseManager;
    private readonly IPathManager _pathManager;

    public ColumnsManager(IDatabaseManager databaseManager, IPathManager pathManager)
    {
      _databaseManager = databaseManager;
      _pathManager = pathManager;
    }

    public const string PageContent = nameof(PageContent);
    public const string NavigationUrl = nameof(NavigationUrl);
    public const string CheckState = nameof(CheckState);
    public const string CheckAdminId = nameof(CheckAdminId);                    //审核人
    public const string CheckDate = nameof(CheckDate);                          //审核时间
    public const string CheckReasons = nameof(CheckReasons);                    //审核原因

    public const string Sequence = nameof(Sequence);                            //序号
    private const string ChannelName = nameof(ChannelName);
    private const string AdminName = nameof(AdminName);
    private const string LastEditAdminName = nameof(LastEditAdminName);
    private const string UserName = nameof(UserName);
    private const string CheckAdminName = nameof(CheckAdminName);
    private const string SourceName = nameof(SourceName);
    private const string TemplateName = nameof(TemplateName);
    private const string State = nameof(State);

    public static string GetFormatStringAttributeName(string attributeName)
    {
      return attributeName + "FormatString";
    }

    public static string GetCountName(string attributeName)
    {
      return $"{attributeName}Count";
    }

    public static string GetExtendName(string attributeName, int n)
    {
      return n > 0 ? $"{attributeName}{n}" : attributeName;
    }

    public static string GetColumnWidthName(string attributeName)
    {
      return $"{attributeName}ColumnWidth";
    }

    public static readonly Lazy<List<string>> MetadataAttributes = new Lazy<List<string>>(() => new List<string>
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            nameof(Content.Id),
            nameof(Content.Guid),
            nameof(Content.CreatedDate),
            nameof(Content.LastModifiedDate),
            nameof(Content.ChannelId),
            nameof(Content.SiteId),
            nameof(Content.AdminId),
            nameof(Content.LastEditAdminId),
            nameof(Content.UserId),
            nameof(Content.Taxis),
            nameof(Content.GroupNames),
            nameof(Content.TagNames),
            nameof(Content.SourceId),
            nameof(Content.TemplateId),
            nameof(Content.ReferenceId),
            nameof(Content.Checked),
            nameof(Content.CheckedLevel),
            nameof(Content.Hits),
            nameof(Content.Downloads),
            nameof(Content.Top),
            nameof(Content.Recommend),
            nameof(Content.Hot),
            nameof(Content.Color),
            nameof(Content.AddDate),
            nameof(Content.LinkType),
            nameof(Content.LinkUrl),
            "HitsByDay",
            "HitsByWeek",
            "HitsByMonth",
            "LastHitsDate",
            "ExtendValues",
        });


<<<<<<< HEAD
        public static readonly Lazy<List<string>> DropAttributes = new Lazy<List<string>>(() => new List<string>
=======
    public static readonly Lazy<List<string>> DropAttributes = new Lazy<List<string>>(() => new List<string>
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            "WritingUserName",
            "ConsumePoint",
            "Comments",
            "Reply",
            "CheckTaskDate",
            "UnCheckTaskDate",
            "Photos",
            "Teleplays",
            "MemberName",
            "GroupNameCollection",
            "Tags",
            "IsChecked",
            "SettingsXml",
            "IsTop",
            "IsRecommend",
            "IsHot",
            "IsColor",
            "AddUserName",
            "LastEditUserName",
            "Content",
            "LastEditDate",
            "HitsByDay",
            "HitsByWeek",
            "HitsByMonth",
            "LastHitsDate"
        });

<<<<<<< HEAD
        private static readonly List<string> CalculatedAttributes = new List<string>
=======
    private static readonly List<string> CalculatedAttributes = new List<string>
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            Sequence,
            nameof(Content.ChannelId),
            nameof(Content.SourceId),
            nameof(Content.TemplateId),
            nameof(Content.AdminId),
            nameof(Content.LastEditAdminId),
            nameof(Content.UserId),
            CheckAdminId
        };

<<<<<<< HEAD
        private static readonly List<string> UnSearchableAttributes = new List<string>
=======
    private static readonly List<string> UnSearchableAttributes = new List<string>
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            Sequence,
            nameof(Content.ChannelId),
            nameof(Content.AddDate),
            nameof(Content.GroupNames),
            nameof(Content.TagNames),
            nameof(Content.SourceId),
            nameof(Content.TemplateId),
            CheckAdminId,
            CheckDate,
            CheckReasons,
        };

<<<<<<< HEAD
        public static List<TableStyle> GetContentListStyles(List<TableStyle> tableStyleList)
        {
            var taxis = 1;
            var list = new List<TableStyle>
            {
                new TableStyle
                {
                    AttributeName = nameof(Content.Id),
                    DisplayName = "内容Id",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.Guid),
                    DisplayName = "识别码",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.Title),
                    DisplayName = "标题",
                    Taxis = taxis++
                }
            };

            if (tableStyleList != null)
            {
                foreach (var tableStyle in tableStyleList)
                {
                    if (!list.Exists(t => t.AttributeName == tableStyle.AttributeName))
                    {
                        list.Add(new TableStyle
                        {
                            AttributeName = tableStyle.AttributeName,
                            DisplayName = tableStyle.DisplayName,
                            InputType = tableStyle.InputType,
                            Taxis = taxis++
                        });
                    }
                }
            }

            list.AddRange(new List<TableStyle>
=======
    public static List<TableStyle> GetContentListStyles(List<TableStyle> tableStyleList)
    {
      var taxis = 1;
      var list = new List<TableStyle>
      {
          new TableStyle
          {
              AttributeName = nameof(Content.Id),
              DisplayName = "内容Id",
              Taxis = taxis++
          },
          new TableStyle
          {
              AttributeName = nameof(Content.Guid),
              DisplayName = "识别码",
              Taxis = taxis++
          },
          new TableStyle
          {
              AttributeName = nameof(Content.Title),
              DisplayName = "标题",
              Taxis = taxis++
          }
      };

      if (tableStyleList != null)
      {
        foreach (var tableStyle in tableStyleList)
        {
          if (!list.Exists(t => t.AttributeName == tableStyle.AttributeName))
          {
            list.Add(new TableStyle
            {
              AttributeName = tableStyle.AttributeName,
              DisplayName = tableStyle.DisplayName,
              InputType = tableStyle.InputType,
              Taxis = taxis++
            });
          }
        }
      }

      list.AddRange(new List<TableStyle>
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            {
                new TableStyle
                {
                    AttributeName = nameof(Content.LinkType),
                    DisplayName = "链接类型",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.LinkUrl),
                    DisplayName = "外部链接",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.ChannelId),
                    DisplayName = "所属栏目",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.AddDate),
                    DisplayName = "添加时间",
<<<<<<< HEAD
=======
                    InputType = InputType.DateTime,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.LastModifiedDate),
                    DisplayName = "最后修改时间",
<<<<<<< HEAD
=======
                    InputType = InputType.DateTime,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.GroupNames),
                    DisplayName = "内容组",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.TagNames),
                    DisplayName = "标签",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.AdminId),
                    DisplayName = "添加人",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.LastEditAdminId),
                    DisplayName = "最后修改人",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.UserId),
                    DisplayName = "投稿用户",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.SourceId),
                    DisplayName = "来源标识",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.TemplateId),
                    DisplayName = "内容模板",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.Hits),
                    DisplayName = "点击量",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Content.Downloads),
                    DisplayName = "下载量",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = CheckAdminId,
                    DisplayName = "审核人",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = CheckDate,
                    DisplayName = "审核时间",
<<<<<<< HEAD
=======
                    InputType = InputType.DateTime,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = CheckReasons,
                    DisplayName = "审核原因",
                    Taxis = taxis
                },
            });

<<<<<<< HEAD
            return list.OrderBy(styleInfo => styleInfo.Taxis == 0 ? int.MaxValue : styleInfo.Taxis).ToList();
        }

        public async Task<Content> CalculateContentListAsync(int sequence, Site site, int currentChannelId, Content source, List<ContentColumn> columns)
        {
            if (source == null) return null;

            if (source.ReferenceId > 0 && source.SourceId > 0)
            {
                var contentId = source.Id;
                var channelId = source.ChannelId;
                var isChecked = source.Checked;
                var checkedLevel = source.CheckedLevel;
                var title = source.Title;

                var referenceSiteId = await _databaseManager.ChannelRepository.GetSiteIdAsync(source.SourceId);
                if (referenceSiteId == 0) return null;

                var referenceSite = await _databaseManager.SiteRepository.GetAsync(referenceSiteId);
                if (referenceSiteId == 0) return null;

                var reference =
                    await _databaseManager.ContentRepository.GetAsync(referenceSite, source.SourceId, source.ReferenceId);
                if (reference == null) return null;

                source.LoadDict(reference.ToDictionary());
                source.Id = contentId;
                source.ChannelId = channelId;
                source.Checked = isChecked;
                source.CheckedLevel = checkedLevel;
                source.Title = title;

                source.SourceId = reference.ChannelId;
                source.ReferenceId = reference.Id;
            }

            var channel = await _databaseManager.ChannelRepository.GetAsync(source.ChannelId);
            var content = await _pathManager.DecodeContentAsync(site, channel, source);

            content.Set(State, CheckManager.GetCheckState(site, content));

            foreach (var column in columns)
            {
                if (!ListUtils.ContainsIgnoreCase(CalculatedAttributes, column.AttributeName)) continue;

                if (column.InputType == InputType.TextEditor)
                {
                    content.Set(column.AttributeName, string.Empty);
                }
                else if (StringUtils.EqualsIgnoreCase(column.AttributeName, Sequence))
                {
                    content.Set(Sequence, sequence);
                }
                else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.ChannelId)))
                {
                    var channelName = await _databaseManager.ChannelRepository.GetChannelNameNavigationAsync(source.SiteId, currentChannelId, source.ChannelId);
                    content.Set(ChannelName, channelName);
                }
                else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.AdminId)))
                {
                    var adminName = await _databaseManager.AdministratorRepository.GetDisplayAsync(source.AdminId);
                    content.Set(AdminName, adminName);
                }
                else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.LastEditAdminId)))
                {
                    var lastEditAdminName =
                        await _databaseManager.AdministratorRepository.GetDisplayAsync(source.LastEditAdminId);
                    content.Set(LastEditAdminName, lastEditAdminName);
                }
                else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.UserId)))
                {
                    var userName = await _databaseManager.UserRepository.GetDisplayAsync(source.UserId);
                    content.Set(UserName, userName);
                }
                else if (StringUtils.EqualsIgnoreCase(column.AttributeName, CheckAdminId))
                {
                    var checkedId = source.Get<int?>(CheckAdminId);
                    if (checkedId.HasValue)
                    {
                        var checkAdminName =
                            await _databaseManager.AdministratorRepository.GetDisplayAsync(checkedId.Value);
                        content.Set(CheckAdminName, checkAdminName);
                    }
                }
                else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.SourceId)))
                {
                    var sourceName = await SourceManager.GetSourceNameAsync(_databaseManager, source);
                    content.Set(SourceName, sourceName);
                }
                else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.TemplateId)))
                {
                    if (content.TemplateId > 0)
                    {
                        var template = await _databaseManager.TemplateRepository.GetAsync(content.TemplateId);
                        if (template != null && template.TemplateType == TemplateType.ContentTemplate)
                        {
                            content.Set(TemplateName, template.TemplateName);
                        }
                    }
                }
            }

            //if (pluginColumns != null)
            //{
            //    foreach (var pluginId in pluginColumns.Keys)
            //    {
            //        var contentColumns = pluginColumns[pluginId];
            //        if (contentColumns == null || contentColumns.Count == 0) continue;

            //        foreach (var columnName in contentColumns.Keys)
            //        {
            //            var attributeName = $"{pluginId}:{columnName}";
            //            if (columns.All(x => x.AttributeName != attributeName)) continue;

            //            try
            //            {
            //                var func = contentColumns[columnName];
            //                var value = func(new ContentContextImpl
            //                {
            //                    SiteId = source.SiteId,
            //                    ChannelId = source.ChannelId,
            //                    ContentId = source.Id
            //                });

            //                content.Set(attributeName, value);
            //            }
            //            catch (Exception ex)
            //            {
            //                await _databaseManager.ErrorLogRepository.AddErrorLogAsync(pluginId, ex);
            //            }
            //        }
            //    }
            //}

            return content;
        }

        public enum PageType
        {
            Contents,
            SearchContents,
            CheckContents,
            RecycleContents
        }

        public async Task<List<ContentColumn>> GetContentListColumnsAsync(Site site, Channel channel, PageType pageType)
        {
            var columns = new List<ContentColumn>();
            var listColumns = new List<string>();

            if (pageType == PageType.Contents)
            {
                listColumns = ListUtils.GetStringList(channel.ListColumns);
                if (listColumns.Count == 0)
                {
                    listColumns.Add(nameof(Content.Title));
                    listColumns.Add(nameof(Content.AddDate));
                }
            }
            else if (pageType == PageType.SearchContents)
            {
                listColumns = ListUtils.GetStringList(site.SearchListColumns);
                if (listColumns.Count == 0)
                {
                    listColumns.Add(nameof(Content.Title));
                    listColumns.Add(nameof(Content.AddDate));
                }
            }
            else if (pageType == PageType.CheckContents)
            {
                listColumns = ListUtils.GetStringList(site.CheckListColumns);
                if (listColumns.Count == 0)
                {
                    listColumns.Add(nameof(Content.Title));
                    listColumns.Add(nameof(Content.ChannelId));
                    listColumns.Add(nameof(Content.AdminId));
                    listColumns.Add(nameof(Content.AddDate));
                }
            }
            else if (pageType == PageType.RecycleContents)
            {
                listColumns = ListUtils.GetStringList(site.RecycleListColumns);
                if (listColumns.Count == 0)
                {
                    listColumns.Add(nameof(Content.Title));
                }
            }

            //var pluginIds = _pluginManager.GetContentPluginIds(channel);
            //var pluginColumns = _pluginManager.GetContentColumns(pluginIds);

            var styles = GetContentListStyles(await _databaseManager.TableStyleRepository.GetContentStylesAsync(site, channel));

            styles.Insert(0, new TableStyle
            {
                AttributeName = Sequence,
                DisplayName = "序号"
            });

            foreach (var style in styles)
            {
                if (string.IsNullOrEmpty(style.DisplayName) || style.InputType == InputType.TextEditor) continue;

                var column = new ContentColumn
                {
                    AttributeName = style.AttributeName,
                    DisplayName = style.DisplayName,
                    InputType = style.InputType,
                    Width = GetColumnWidth(style.AttributeName, channel)
                };
                if (style.AttributeName == nameof(Content.Title))
                {
                    column.IsList = true;
                }
                else
                {
                    if (ListUtils.ContainsIgnoreCase(listColumns, style.AttributeName))
                    {
                        column.IsList = true;
                    }
                }

                if (!ListUtils.ContainsIgnoreCase(UnSearchableAttributes, style.AttributeName))
                {
                    column.IsSearchable = true;
                }

                columns.Add(column);
            }

            //if (pluginColumns != null)
            //{
            //    foreach (var pluginId in pluginColumns.Keys)
            //    {
            //        var contentColumns = pluginColumns[pluginId];
            //        if (contentColumns == null || contentColumns.Count == 0) continue;

            //        foreach (var columnName in contentColumns.Keys)
            //        {
            //            var attributeName = $"{pluginId}:{columnName}";
            //            var column = new ContentColumn
            //            {
            //                AttributeName = attributeName,
            //                DisplayName = $"{columnName}({pluginId})",
            //                InputType = InputType.Text
            //            };

            //            if (ListUtils.ContainsIgnoreCase(listColumns, attributeName))
            //            {
            //                column.IsList = true;
            //            }

            //            columns.Add(column);
            //        }
            //    }
            //}

            return columns;
        }

        private static int GetColumnWidth(string attributeName, Channel channel)
        {
            var width = channel.Get(GetColumnWidthName(attributeName), 0);
            if (width > 0) return width;

            switch (attributeName)
            {
                case nameof(Sequence):
                case nameof(Content.Id):
                case nameof(Content.Hits):
                case nameof(Content.Downloads):
                    return 70;
                case nameof(Content.ImageUrl):
                    return 100;
                case nameof(Content.Guid):
                case nameof(Content.SourceId):
                    return 310;
                default:
                    return 160;
            }
        }

        private static List<TableStyle> GetChannelListStyles(List<TableStyle> tableStyleList)
        {
            var taxis = 1;
            var list = new List<TableStyle>
=======
      return list.OrderBy(styleInfo => styleInfo.Taxis == 0 ? int.MaxValue : styleInfo.Taxis).ToList();
    }

    public async Task<Content> CalculateContentListAsync(int sequence, Site site, int currentChannelId, Content source, List<ContentColumn> columns)
    {
      if (source == null) return null;

      if (source.ReferenceId > 0 && source.SourceId > 0)
      {
        var contentId = source.Id;
        var channelId = source.ChannelId;
        var isChecked = source.Checked;
        var checkedLevel = source.CheckedLevel;
        var title = source.Title;

        var referenceSiteId = await _databaseManager.ChannelRepository.GetSiteIdAsync(source.SourceId);
        if (referenceSiteId == 0) return null;

        var referenceSite = await _databaseManager.SiteRepository.GetAsync(referenceSiteId);
        if (referenceSiteId == 0) return null;

        var reference =
            await _databaseManager.ContentRepository.GetAsync(referenceSite, source.SourceId, source.ReferenceId);
        if (reference == null) return null;

        source.LoadDict(reference.ToDictionary());
        source.Id = contentId;
        source.ChannelId = channelId;
        source.Checked = isChecked;
        source.CheckedLevel = checkedLevel;
        source.Title = title;

        source.SourceId = reference.ChannelId;
        source.ReferenceId = reference.Id;
      }

      var channel = await _databaseManager.ChannelRepository.GetAsync(source.ChannelId);
      var content = await _pathManager.DecodeContentAsync(site, channel, source);

      content.Set(State, CheckManager.GetCheckState(site, content));

      foreach (var column in columns)
      {
        if (!ListUtils.ContainsIgnoreCase(CalculatedAttributes, column.AttributeName)) continue;

        if (column.InputType == InputType.TextEditor)
        {
          content.Set(column.AttributeName, string.Empty);
        }
        else if (StringUtils.EqualsIgnoreCase(column.AttributeName, Sequence))
        {
          content.Set(Sequence, sequence);
        }
        else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.ChannelId)))
        {
          var channelName = await _databaseManager.ChannelRepository.GetChannelNameNavigationAsync(source.SiteId, currentChannelId, source.ChannelId);
          content.Set(ChannelName, channelName);
        }
        else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.AdminId)))
        {
          var adminName = await _databaseManager.AdministratorRepository.GetDisplayAsync(source.AdminId);
          content.Set(AdminName, adminName);
        }
        else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.LastEditAdminId)))
        {
          var lastEditAdminName =
              await _databaseManager.AdministratorRepository.GetDisplayAsync(source.LastEditAdminId);
          content.Set(LastEditAdminName, lastEditAdminName);
        }
        else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.UserId)))
        {
          var userName = await _databaseManager.UserRepository.GetDisplayAsync(source.UserId);
          content.Set(UserName, userName);
        }
        else if (StringUtils.EqualsIgnoreCase(column.AttributeName, CheckAdminId))
        {
          var checkedId = source.Get<int>(CheckAdminId);
          if (checkedId > 0)
          {
            var checkAdminName =
                await _databaseManager.AdministratorRepository.GetDisplayAsync(checkedId);
            content.Set(CheckAdminName, checkAdminName);
          }
        }
        else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.SourceId)))
        {
          var sourceName = await SourceManager.GetSourceNameAsync(_databaseManager, source);
          content.Set(SourceName, sourceName);
        }
        else if (StringUtils.EqualsIgnoreCase(column.AttributeName, nameof(Content.TemplateId)))
        {
          if (content.TemplateId > 0)
          {
            var template = await _databaseManager.TemplateRepository.GetAsync(content.TemplateId);
            if (template != null && template.TemplateType == TemplateType.ContentTemplate)
            {
              content.Set(TemplateName, template.TemplateName);
            }
          }
        }
      }

      //if (pluginColumns != null)
      //{
      //    foreach (var pluginId in pluginColumns.Keys)
      //    {
      //        var contentColumns = pluginColumns[pluginId];
      //        if (contentColumns == null || contentColumns.Count == 0) continue;

      //        foreach (var columnName in contentColumns.Keys)
      //        {
      //            var attributeName = $"{pluginId}:{columnName}";
      //            if (columns.All(x => x.AttributeName != attributeName)) continue;

      //            try
      //            {
      //                var func = contentColumns[columnName];
      //                var value = func(new ContentContextImpl
      //                {
      //                    SiteId = source.SiteId,
      //                    ChannelId = source.ChannelId,
      //                    ContentId = source.Id
      //                });

      //                content.Set(attributeName, value);
      //            }
      //            catch (Exception ex)
      //            {
      //                await _databaseManager.ErrorLogRepository.AddErrorLogAsync(pluginId, ex);
      //            }
      //        }
      //    }
      //}

      return content;
    }

    public enum PageType
    {
      Contents,
      SearchContents,
      CheckContents,
      RecycleContents
    }

    public async Task<List<ContentColumn>> GetContentListColumnsAsync(Site site, Channel channel, PageType pageType)
    {
      var columns = new List<ContentColumn>();
      var listColumns = new List<string>();

      if (pageType == PageType.Contents)
      {
        listColumns = await _databaseManager.ChannelRepository.GetListColumnsRecursiveAsync(channel);
      }
      else if (pageType == PageType.SearchContents)
      {
        listColumns = ListUtils.GetStringList(site.SearchListColumns);
        if (listColumns.Count == 0)
        {
          listColumns.Add(nameof(Content.Title));
          listColumns.Add(nameof(Content.AddDate));
        }
      }
      else if (pageType == PageType.CheckContents)
      {
        listColumns = ListUtils.GetStringList(site.CheckListColumns);
        if (listColumns.Count == 0)
        {
          listColumns.Add(nameof(Content.Title));
          listColumns.Add(nameof(Content.ChannelId));
          listColumns.Add(nameof(Content.AdminId));
          listColumns.Add(nameof(Content.AddDate));
        }
      }
      else if (pageType == PageType.RecycleContents)
      {
        listColumns = ListUtils.GetStringList(site.RecycleListColumns);
        if (listColumns.Count == 0)
        {
          listColumns.Add(nameof(Content.Title));
        }
      }

      //var pluginIds = _pluginManager.GetContentPluginIds(channel);
      //var pluginColumns = _pluginManager.GetContentColumns(pluginIds);

      var styles = GetContentListStyles(await _databaseManager.TableStyleRepository.GetContentStylesAsync(site, channel));

      styles.Insert(0, new TableStyle
      {
        AttributeName = Sequence,
        DisplayName = "序号",
      });

      foreach (var style in styles)
      {
        if (string.IsNullOrEmpty(style.DisplayName) || style.InputType == InputType.TextEditor) continue;

        var column = new ContentColumn
        {
          AttributeName = style.AttributeName,
          DisplayName = style.DisplayName,
          InputType = style.InputType,
          Width = GetColumnWidth(style.AttributeName, channel)
        };
        if (style.AttributeName == nameof(Content.Title))
        {
          column.IsList = true;
        }
        else
        {
          if (ListUtils.ContainsIgnoreCase(listColumns, style.AttributeName))
          {
            column.IsList = true;
          }
        }

        if (!ListUtils.ContainsIgnoreCase(UnSearchableAttributes, style.AttributeName))
        {
          column.IsSearchable = true;
        }

        columns.Add(column);
      }

      //if (pluginColumns != null)
      //{
      //    foreach (var pluginId in pluginColumns.Keys)
      //    {
      //        var contentColumns = pluginColumns[pluginId];
      //        if (contentColumns == null || contentColumns.Count == 0) continue;

      //        foreach (var columnName in contentColumns.Keys)
      //        {
      //            var attributeName = $"{pluginId}:{columnName}";
      //            var column = new ContentColumn
      //            {
      //                AttributeName = attributeName,
      //                DisplayName = $"{columnName}({pluginId})",
      //                InputType = InputType.Text
      //            };

      //            if (ListUtils.ContainsIgnoreCase(listColumns, attributeName))
      //            {
      //                column.IsList = true;
      //            }

      //            columns.Add(column);
      //        }
      //    }
      //}

      return columns;
    }

    private static int GetColumnWidth(string attributeName, Channel channel)
    {
      var width = channel.Get(GetColumnWidthName(attributeName), 0);
      if (width > 0) return width;

      switch (attributeName)
      {
        case nameof(Sequence):
        case nameof(Content.Id):
        case nameof(Content.Hits):
        case nameof(Content.Downloads):
          return 70;
        case nameof(Content.ImageUrl):
          return 100;
        case nameof(Content.Guid):
        case nameof(Content.SourceId):
          return 310;
        default:
          return 160;
      }
    }

    private static List<TableStyle> GetChannelListStyles(List<TableStyle> tableStyleList)
    {
      var taxis = 1;
      var list = new List<TableStyle>
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            {
                new TableStyle
                {
                    AttributeName = nameof(Channel.ChannelName),
                    DisplayName = "栏目名称",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Channel.Id),
                    DisplayName = "栏目Id",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Channel.IndexName),
                    DisplayName = "栏目索引",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Channel.GroupNames),
                    DisplayName = "栏目组",
                    Taxis = taxis++
                }
        };

<<<<<<< HEAD
            if (tableStyleList != null)
            {
                foreach (var tableStyle in tableStyleList)
                {
                    if (!list.Exists(t => t.AttributeName == tableStyle.AttributeName))
                    {
                        list.Add(new TableStyle
                        {
                            AttributeName = tableStyle.AttributeName,
                            DisplayName = tableStyle.DisplayName,
                            InputType = tableStyle.InputType,
                            Taxis = taxis++
                        });
                    }
                }
            }

            list.AddRange(new List<TableStyle>
=======
      if (tableStyleList != null)
      {
        foreach (var tableStyle in tableStyleList)
        {
          if (!list.Exists(t => t.AttributeName == tableStyle.AttributeName))
          {
            list.Add(new TableStyle
            {
              AttributeName = tableStyle.AttributeName,
              DisplayName = tableStyle.DisplayName,
              InputType = tableStyle.InputType,
              Taxis = taxis++
            });
          }
        }
      }

      list.AddRange(new List<TableStyle>
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            {
                new TableStyle
                {
                    AttributeName = nameof(Channel.ChannelTemplateId),
                    DisplayName = "栏目模板",
                    Taxis = taxis++
                },
                new TableStyle
                {
                    AttributeName = nameof(Channel.ContentTemplateId),
                    DisplayName = "内容模板",
                    Taxis = taxis
                }
            });

<<<<<<< HEAD
            return list.OrderBy(styleInfo => styleInfo.Taxis == 0 ? int.MaxValue : styleInfo.Taxis).ToList();
        }

        public async Task<List<ContentColumn>> GetChannelListColumnsAsync(Site site)
        {
            var columns = new List<ContentColumn>();
            var listColumns = ListUtils.GetStringList(site.ChannelListColumns);
            if (listColumns.Count == 0)
            {
                listColumns.Add(nameof(Channel.ChannelName));
                listColumns.Add(nameof(Channel.IndexName));
                listColumns.Add(nameof(Channel.GroupNames));
            }

            var styles = GetChannelListStyles(await _databaseManager.TableStyleRepository.GetChannelStylesAsync(await _databaseManager.ChannelRepository.GetAsync(site.Id)));

            foreach (var style in styles)
            {
                if (string.IsNullOrEmpty(style.DisplayName) || style.InputType == InputType.TextEditor) continue;

                var column = new ContentColumn
                {
                    AttributeName = style.AttributeName,
                    DisplayName = style.DisplayName,
                    InputType = style.InputType
                };
                if (style.AttributeName == nameof(Channel.ChannelName))
                {
                    column.IsList = true;
                }
                else
                {
                    if (ListUtils.ContainsIgnoreCase(listColumns, style.AttributeName))
                    {
                        column.IsList = true;
                    }
                }

                if (!ListUtils.ContainsIgnoreCase(UnSearchableAttributes, style.AttributeName))
                {
                    column.IsSearchable = true;
                }

                columns.Add(column);
            }

            return columns;
        }

        public static async Task<Dictionary<string, object>> SaveAttributesAsync(IPathManager pathManager, Site site, List<TableStyle> styleList, NameValueCollection formCollection, List<string> dontAddAttributes)
        {
            var dict = new Dictionary<string, object>();

            if (dontAddAttributes == null)
            {
                dontAddAttributes = new List<string>();
            }

            foreach (var style in styleList)
            {
                if (ListUtils.ContainsIgnoreCase(dontAddAttributes, style.AttributeName)) continue;
                //var theValue = GetValueByForm(style, site, formCollection);

                var theValue = formCollection[style.AttributeName] ?? string.Empty;
                var inputType = style.InputType;
                if (inputType == InputType.TextEditor)
                {
                    theValue = await pathManager.EncodeTextEditorAsync(site, theValue);
                    theValue = UEditorUtils.TranslateToStlElement(theValue);
                }

                if (inputType != InputType.TextEditor && inputType != InputType.Image && inputType != InputType.File && inputType != InputType.Video && !StringUtils.EqualsIgnoreCase(style.AttributeName, nameof(Content.LinkUrl)))
                {
                    theValue = StringUtils.FilterXss(theValue);
                }

                dict[style.AttributeName] = theValue;

                if (style.IsFormatString)
                {
                    var formatString = TranslateUtils.ToBool(formCollection[style.AttributeName + "_formatStrong"]);
                    var formatEm = TranslateUtils.ToBool(formCollection[style.AttributeName + "_formatEM"]);
                    var formatU = TranslateUtils.ToBool(formCollection[style.AttributeName + "_formatU"]);
                    var formatColor = formCollection[style.AttributeName + "_formatColor"];
                    var theFormatString = ContentUtility.GetTitleFormatString(formatString, formatEm, formatU, formatColor);

                    dict[GetFormatStringAttributeName(style.AttributeName)] = theFormatString;
                }

                //if (inputType == InputType.Image || inputType == InputType.File || inputType == InputType.Video)
                //{
                //    var attributeName = GetExtendAttributeName(style.AttributeName);
                //    dict[attributeName] = formCollection[attributeName];
                //}
            }

            return dict;
        }
    }
=======
      return list.OrderBy(styleInfo => styleInfo.Taxis == 0 ? int.MaxValue : styleInfo.Taxis).ToList();
    }

    public async Task<List<ContentColumn>> GetChannelListColumnsAsync(Site site)
    {
      var columns = new List<ContentColumn>();
      var listColumns = ListUtils.GetStringList(site.ChannelListColumns);
      if (listColumns.Count == 0)
      {
        listColumns.Add(nameof(Channel.ChannelName));
        listColumns.Add(nameof(Channel.IndexName));
        listColumns.Add(nameof(Channel.GroupNames));
      }

      var styles = GetChannelListStyles(await _databaseManager.TableStyleRepository.GetChannelStylesAsync(await _databaseManager.ChannelRepository.GetAsync(site.Id)));

      foreach (var style in styles)
      {
        if (string.IsNullOrEmpty(style.DisplayName) || style.InputType == InputType.TextEditor) continue;

        var column = new ContentColumn
        {
          AttributeName = style.AttributeName,
          DisplayName = style.DisplayName,
          InputType = style.InputType
        };
        if (style.AttributeName == nameof(Channel.ChannelName))
        {
          column.IsList = true;
        }
        else
        {
          if (ListUtils.ContainsIgnoreCase(listColumns, style.AttributeName))
          {
            column.IsList = true;
          }
        }

        if (!ListUtils.ContainsIgnoreCase(UnSearchableAttributes, style.AttributeName))
        {
          column.IsSearchable = true;
        }

        columns.Add(column);
      }

      return columns;
    }

    public static async Task<Dictionary<string, object>> SaveAttributesAsync(IPathManager pathManager, Site site, List<TableStyle> styleList, NameValueCollection formCollection, List<string> dontAddAttributes)
    {
      var dict = new Dictionary<string, object>();

      if (dontAddAttributes == null)
      {
        dontAddAttributes = new List<string>();
      }

      foreach (var style in styleList)
      {
        if (ListUtils.ContainsIgnoreCase(dontAddAttributes, style.AttributeName)) continue;
        //var theValue = GetValueByForm(style, site, formCollection);

        var theValue = formCollection[style.AttributeName] ?? string.Empty;
        var inputType = style.InputType;
        if (inputType == InputType.TextEditor)
        {
          theValue = await pathManager.EncodeTextEditorAsync(site, theValue);
          theValue = UEditorUtils.TranslateToStlElement(theValue);
        }

        if (inputType != InputType.TextEditor && inputType != InputType.Image && inputType != InputType.File && inputType != InputType.Video && !StringUtils.EqualsIgnoreCase(style.AttributeName, nameof(Content.LinkUrl)))
        {
          theValue = StringUtils.FilterXss(theValue);
        }

        dict[style.AttributeName] = theValue;

        if (style.IsFormatString)
        {
          var formatString = TranslateUtils.ToBool(formCollection[style.AttributeName + "_formatStrong"]);
          var formatEm = TranslateUtils.ToBool(formCollection[style.AttributeName + "_formatEM"]);
          var formatU = TranslateUtils.ToBool(formCollection[style.AttributeName + "_formatU"]);
          var formatColor = formCollection[style.AttributeName + "_formatColor"];
          var theFormatString = ContentUtility.GetTitleFormatString(formatString, formatEm, formatU, formatColor);

          dict[GetFormatStringAttributeName(style.AttributeName)] = theFormatString;
        }

        //if (inputType == InputType.Image || inputType == InputType.File || inputType == InputType.Video)
        //{
        //    var attributeName = GetExtendAttributeName(style.AttributeName);
        //    dict[attributeName] = formCollection[attributeName];
        //}
      }

      return dict;
    }
  }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
}
