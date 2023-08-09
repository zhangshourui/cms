﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datory;
using SqlKata;
using SSCMS.Core.Utils;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Services;
using SSCMS.Utils;

namespace SSCMS.Core.Repositories
{
    public partial class ContentRepository
    {
        private async Task<List<ContentSummary>> GetStlDataSourceCheckedAsync(IDatabaseManager databaseManager, List<int> channelIdList, string tableName, int startNum, int totalNum, string orderByString, string whereString, NameValueCollection others)
        {
            if (channelIdList == null || channelIdList.Count == 0)
            {
                return null;
            }
            var sqlWhereString = channelIdList.Count == 1 ? $"WHERE (ChannelId = {channelIdList[0]} AND Checked = true {whereString})" : $"WHERE (ChannelId IN ({TranslateUtils.ToSqlInStringWithoutQuote(channelIdList)}) AND Checked = true {whereString})";

            if (others != null && others.Count > 0)
            {
                var columnNameList = await databaseManager.GetTableColumnNameListAsync(tableName);

                foreach (var attributeName in others.AllKeys)
                {
                    if (ListUtils.ContainsIgnoreCase(columnNameList, attributeName))
                    {
                        var value = others.Get(attributeName);
                        if (!string.IsNullOrEmpty(value))
                        {
                            value = value.Trim();
                            if (StringUtils.StartsWithIgnoreCase(value, "not:"))
                            {
                                value = value.Substring("not:".Length);
<<<<<<< HEAD
                                if (value.IndexOf(',') == -1)
=======
                                if (!value.Contains(','))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                                {
                                    sqlWhereString += $" AND ({attributeName} <> '{value}')";
                                }
                                else
                                {
                                    var collection = ListUtils.GetStringList(value);
                                    foreach (var val in collection)
                                    {
                                        sqlWhereString += $" AND ({attributeName} <> '{val}')";
                                    }
                                }
                            }
                            else if (StringUtils.StartsWithIgnoreCase(value, "contains:"))
                            {
                                value = value.Substring("contains:".Length);
<<<<<<< HEAD
                                if (value.IndexOf(',') == -1)
=======
                                if (!value.Contains(','))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                                {
                                    sqlWhereString += $" AND ({attributeName} LIKE '%{value}%')";
                                }
                                else
                                {
                                    var builder = new StringBuilder(" AND (");
                                    var collection = ListUtils.GetStringList(value);
                                    foreach (var val in collection)
                                    {
                                        builder.Append($" {attributeName} LIKE '%{val}%' OR ");
                                    }
                                    builder.Length -= 3;

<<<<<<< HEAD
                                    builder.Append(")");
=======
                                    builder.Append(')');
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

                                    sqlWhereString += builder.ToString();
                                }
                            }
                            else if (StringUtils.StartsWithIgnoreCase(value, "start:"))
                            {
                                value = value.Substring("start:".Length);
<<<<<<< HEAD
                                if (value.IndexOf(',') == -1)
=======
                                if (!value.Contains(','))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                                {
                                    sqlWhereString += $" AND ({attributeName} LIKE '{value}%')";
                                }
                                else
                                {
                                    var builder = new StringBuilder(" AND (");
                                    var collection = ListUtils.GetStringList(value);
                                    foreach (var val in collection)
                                    {
                                        builder.Append($" {attributeName} LIKE '{val}%' OR ");
                                    }
                                    builder.Length -= 3;

<<<<<<< HEAD
                                    builder.Append(")");
=======
                                    builder.Append(')');
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

                                    sqlWhereString += builder.ToString();
                                }
                            }
                            else if (StringUtils.StartsWithIgnoreCase(value, "end:"))
                            {
                                value = value.Substring("end:".Length);
<<<<<<< HEAD
                                if (value.IndexOf(',') == -1)
=======
                                if (!value.Contains(','))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                                {
                                    sqlWhereString += $" AND ({attributeName} LIKE '%{value}')";
                                }
                                else
                                {
                                    var builder = new StringBuilder(" AND (");
                                    var collection = ListUtils.GetStringList(value);
                                    foreach (var val in collection)
                                    {
                                        builder.Append($" {attributeName} LIKE '%{val}' OR ");
                                    }
                                    builder.Length -= 3;

<<<<<<< HEAD
                                    builder.Append(")");
=======
                                    builder.Append(')');
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

                                    sqlWhereString += builder.ToString();
                                }
                            }
                            else
                            {
<<<<<<< HEAD
                                if (value.IndexOf(',') == -1)
=======
                                if (!value.Contains(','))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                                {
                                    sqlWhereString += $" AND ({attributeName} = '{value}')";
                                }
                                else
                                {
                                    var builder = new StringBuilder(" AND (");
                                    var collection = ListUtils.GetStringList(value);
                                    foreach (var val in collection)
                                    {
                                        builder.Append($" {attributeName} = '{val}' OR ");
                                    }
                                    builder.Length -= 3;

                                    builder.Append(")");

                                    sqlWhereString += builder.ToString();
                                }
                            }
                        }
                    }
                }
            }

            return startNum <= 1 ? GetStlDataSourceByContentNumAndWhereString(tableName, totalNum, sqlWhereString, orderByString) : GetStlDataSourceByStartNum(tableName, startNum, totalNum, sqlWhereString, orderByString);
        }

        private async Task<List<ContentSummary>> GetContentsDataSourceAsync(IDatabaseManager databaseManager, Site site, int channelId, int contentId, string groupContent, string groupContentNot, string tags, bool isImageExists, bool isImage, bool isVideoExists, bool isVideo, bool isFileExists, bool isFile, bool isRelatedContents, int startNum, int totalNum, string orderByString, bool isTopExists, bool isTop, bool isRecommendExists, bool isRecommend, bool isHotExists, bool isHot, bool isColorExists, bool isColor, ScopeType scopeType, string groupChannel, string groupChannelNot, NameValueCollection others)
        {
            if (!await _channelRepository.IsExistsAsync(channelId)) return null;

            var channel = await _channelRepository.GetAsync(channelId);
            var tableName = _channelRepository.GetTableName(site, channel);

            var where = string.Empty;
            if (isRelatedContents && contentId > 0)
            {
                where = $"ID <> {contentId}";
            }

            var sqlWhereString = GetStlWhereString(site.Id, groupContent,
                    groupContentNot, tags, isImageExists, isImage, isVideoExists, isVideo, isFileExists, isFile,
                    isTopExists, isTop, isRecommendExists, isRecommend, isHotExists, isHot, isColorExists, isColor,
                    where);

            var channelIdList = await _channelRepository.GetChannelIdsAsync(channel, scopeType, groupChannel, groupChannelNot, string.Empty);
            return await GetStlDataSourceCheckedAsync(databaseManager, channelIdList, tableName, startNum, totalNum, orderByString, sqlWhereString, others);
        }

        public async Task<List<ContentSummary>> GetSummariesAsync(IDatabaseManager databaseManager, Site site, int channelId, int contentId, string groupContent, string groupContentNot, string tags, bool isImageExists, bool isImage, bool isVideoExists, bool isVideo, bool isFileExists, bool isFile, bool isRelatedContents, int startNum, int totalNum, string orderByString, bool isTopExists, bool isTop, bool isRecommendExists, bool isRecommend, bool isHotExists, bool isHot, bool isColorExists, bool isColor, ScopeType scopeType, string groupChannel, string groupChannelNot, NameValueCollection others)
        {
            var dataSource = await GetContentsDataSourceAsync(databaseManager, site, channelId, contentId, groupContent, groupContentNot, tags,
                isImageExists, isImage, isVideoExists, isVideo, isFileExists, isFile, isRelatedContents, startNum,
                totalNum, orderByString, isTopExists, isTop, isRecommendExists, isRecommend, isHotExists, isHot,
                isColorExists, isColor, scopeType, groupChannel, groupChannelNot, others);

            return dataSource;

            //var list = new List<ContentSummary>();

            //foreach (DataRow dataItem in dataSource.Tables[0].Rows)
            //{
            //    var minContentInfo = new ContentSummary
            //    {
            //        Id = (int)dataItem[ContentAttribute.Id],
            //        ChannelId = (int)dataItem[ContentAttribute.ChannelId]
            //    };
            //    list.Add(minContentInfo);
            //}

            //return list;
        }

<<<<<<< HEAD
        public async Task<List<KeyValuePair<int, Content>>> ParserGetContentsDataSourceAsync(Site site, int channelId, int contentId, string groupContent, string groupContentNot, string tags, bool isImageExists, bool isImage, bool isVideoExists, bool isVideo, bool isFileExists, bool isFile, string since, bool isRelatedContents, int startNum, int totalNum, TaxisType taxisType, bool isTopExists, bool isTop, bool isRecommendExists, bool isRecommend, bool isHotExists, bool isHot, bool isColorExists, bool isColor, ScopeType scopeType, string groupChannel, string groupChannelNot, NameValueCollection others, Query query)
=======
        public async Task<List<KeyValuePair<int, Content>>> ParserGetContentsDataSourceAsync(Site site, int channelId, int contentId, string groupContent, string groupContentNot, string tags, bool isImageExists, bool isImage, bool isVideoExists, bool isVideo, bool isFileExists, bool isFile, string since, bool isRelatedContents, int startNum, int totalNum, TaxisType taxisType, bool isTopExists, bool isTop, bool isRecommendExists, bool isRecommend, bool isHotExists, bool isHot, bool isColorExists, bool isColor, ScopeType scopeType, string groupChannel, string groupChannelNot, string where, NameValueCollection others, Query query)
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            if (!await _channelRepository.IsExistsAsync(channelId)) return null;

            var channel = await _channelRepository.GetAsync(channelId);
            var repository = await GetRepositoryAsync(site, channel);

            query = GetQuery(query, site.Id)
                .Select(
                  nameof(ContentSummary.Id),
                  nameof(ContentSummary.ChannelId),
                  nameof(ContentSummary.Checked),
                  nameof(ContentSummary.CheckedLevel)
                )
                .WhereTrue(nameof(Content.Checked));

            if (!string.IsNullOrEmpty(since))
            {
                var sinceDate = DateTime.Now.AddHours(-DateUtils.GetSinceHours(since));
                query.WhereBetween(nameof(Content.AddDate), sinceDate, DateTime.Now);
            }

            if (isRelatedContents && contentId > 0)
            {
                query.WhereNot(nameof(Content.Id), contentId);
            }

            if (isTopExists)
            {
                query = isTop ? query.WhereTrue(nameof(Content.Top)) : query.WhereNullOrFalse(nameof(Content.Top));
            }

            if (isRecommendExists)
            {
                query = isRecommend
                    ? query.WhereTrue(nameof(Content.Recommend))
                    : query.WhereNullOrFalse(nameof(Content.Recommend));
            }

            if (isHotExists)
            {
                query = isHot ? query.WhereTrue(nameof(Content.Hot)) : query.WhereNullOrFalse(nameof(Content.Hot));
            }

            if (isColorExists)
            {
                query = isColor ? query.WhereTrue(nameof(Content.Color)) : query.WhereNullOrFalse(nameof(Content.Color));
            }

            if (!string.IsNullOrEmpty(groupContent))
            {
<<<<<<< HEAD
                query.Where(q => q
                    .Where(nameof(Content.GroupNames), groupContent)
                    .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{groupContent}")
                    .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{groupContent},")
                    .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $"{groupContent},")
                );
=======
                query.Where(q =>
                {
                    foreach (var group in ListUtils.GetStringList(groupContent))
                    {
                        q
                        .OrWhere(nameof(Content.GroupNames), group)
                        .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{group}")
                        .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{group},")
                        .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $"{group},");
                    }
                    return q;
                });
                // query.Where(q => q
                //     .Where(nameof(Content.GroupNames), groupContent)
                //     .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{groupContent}")
                //     .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{groupContent},")
                //     .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $"{groupContent},")
                // );
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }

            if (!string.IsNullOrEmpty(groupContentNot))
            {
<<<<<<< HEAD
                query
                    .WhereNot(nameof(Content.GroupNames), groupContentNot)
                    .WhereNotInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{groupContentNot}")
                    .WhereNotInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{groupContentNot},")
                    .WhereNotInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $"{groupContentNot},");
=======
                foreach (var groupNot in ListUtils.GetStringList(groupContentNot))
                {
                    query
                      .WhereNot(nameof(Content.GroupNames), groupNot)
                      .WhereNotInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{groupNot}")
                      .WhereNotInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{groupNot},")
                      .WhereNotInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $"{groupNot},");
                }

                // query
                //     .WhereNot(nameof(Content.GroupNames), groupContentNot)
                //     .WhereNotInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{groupContentNot}")
                //     .WhereNotInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $",{groupContentNot},")
                //     .WhereNotInStr(repository.Database.DatabaseType, nameof(Content.GroupNames), $"{groupContentNot},");
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            }

            if (!string.IsNullOrEmpty(tags))
            {
                var tagNames = ListUtils.GetStringList(tags);
                query.Where(q =>
                {
                    foreach (var tagName in tagNames)
                    {
                        q
                        .OrWhere(nameof(Content.TagNames), tagName)
                        .OrWhereLike(nameof(Content.TagNames), $"%,{tagName}", true)
                        .OrWhereLike(nameof(Content.TagNames), $"%,{tagName},%", true)
                        .OrWhereLike(nameof(Content.TagNames), $"{tagName},%", true);
                    }
                    return q;
                });
                // foreach (var tagName in tagNames)
                // {
                //     query.Where(q => q
                //         .Where(nameof(Content.TagNames), tagName)
                //         .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.TagNames), $",{tagName}")
                //         .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.TagNames), $",{tagName},")
                //         .OrWhereInStr(repository.Database.DatabaseType, nameof(Content.TagNames), $"{tagName},"));
                // }
            }

            if (isImageExists)
            {
                if (isImage)
                {
                    query
                        .WhereNot(nameof(Content.ImageUrl), string.Empty)
                        .WhereNotNull(nameof(Content.ImageUrl));
                }
                else
                {
                    query.WhereNullOrEmpty(nameof(Content.ImageUrl));
                }
            }

            if (isVideoExists)
            {
                if (isVideo)
                {
                    query
                        .WhereNot(nameof(Content.VideoUrl), string.Empty)
                        .WhereNotNull(nameof(Content.VideoUrl));
                }
                else
                {
                    query.WhereNullOrEmpty(nameof(Content.VideoUrl));
                }
            }

            if (isFileExists)
            {
                if (isFile)
                {
                    query
                        .WhereNot(nameof(Content.FileUrl), string.Empty)
                        .WhereNotNull(nameof(Content.FileUrl));
                }
                else
                {
                    query.WhereNullOrEmpty(nameof(Content.FileUrl));
                }
            }

            var channelIdList = await _channelRepository.GetChannelIdsAsync(channel, scopeType, groupChannel, groupChannelNot, string.Empty);

            if (channelIdList == null || channelIdList.Count == 0)
            {
                return null;
            }

            if (channelIdList.Count == 1)
            {
                query.Where(nameof(Content.ChannelId), channelIdList[0]);
            }
            else
            {
                query.WhereIn(nameof(Content.ChannelId), channelIdList);
            }

<<<<<<< HEAD
=======
            if (!string.IsNullOrEmpty(where))
            {
                query.WhereRaw(where);
            }

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            if (others != null && others.Count > 0)
            {
                foreach (var attributeName in others.AllKeys)
                {
                    if (repository.TableColumns.Exists(x =>
                        StringUtils.EqualsIgnoreCase(x.AttributeName, attributeName)))
                    {
                        var value = others.Get(attributeName);
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (StringUtils.StartsWithIgnoreCase(value, "not:"))
                            {
                                value = value.Substring("not:".Length);
<<<<<<< HEAD
                                if (value.IndexOf(',') == -1)
=======
                                if (!value.Contains(','))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                                {
                                    query.WhereNot(attributeName, value);
                                }
                                else
                                {
                                    var collection = ListUtils.GetStringList(value);
                                    foreach (var val in collection)
                                    {
                                        query.WhereNot(attributeName, val);
                                    }
                                }
                            }
                            else if (StringUtils.StartsWithIgnoreCase(value, "contains:"))
                            {
                                value = value.Substring("contains:".Length);
<<<<<<< HEAD
                                if (value.IndexOf(',') == -1)
=======
                                if (!value.Contains(','))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                                {
                                    query.WhereLike(attributeName, $"%{value}%");
                                }
                                else
                                {
                                    var collection = ListUtils.GetStringList(value);
                                    query.Where(q =>
                                    {
                                        foreach (var val in collection)
                                        {
                                            q.OrWhereLike(attributeName, $"%{val}%");
                                        }
                                        return q;
                                    });
                                }
                            }
                            else if (StringUtils.StartsWithIgnoreCase(value, "start:"))
                            {
                                value = value.Substring("start:".Length);
<<<<<<< HEAD
                                if (value.IndexOf(',') == -1)
=======
                                if (!value.Contains(','))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                                {
                                    query.WhereStarts(attributeName, value);
                                }
                                else
                                {
                                    var collection = ListUtils.GetStringList(value);
                                    query.Where(q =>
                                    {
                                        foreach (var val in collection)
                                        {
                                            q.OrWhereStarts(attributeName, val);
                                        }
                                        return q;
                                    });
                                }
                            }
                            else if (StringUtils.StartsWithIgnoreCase(value, "end:"))
                            {
                                value = value.Substring("end:".Length);
<<<<<<< HEAD
                                if (value.IndexOf(',') == -1)
=======
                                if (!value.Contains(','))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                                {
                                    query.WhereEnds(attributeName, value);
                                }
                                else
                                {
                                    var collection = ListUtils.GetStringList(value);
                                    query.Where(q =>
                                    {
                                        foreach (var val in collection)
                                        {
                                            q.OrWhereEnds(attributeName, val);
                                        }
                                        return q;
                                    });
                                }
                            }
                            else
                            {
<<<<<<< HEAD
                                if (value.IndexOf(',') == -1)
=======
                                if (!value.Contains(','))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                                {
                                    query.Where(attributeName, value);
                                }
                                else
                                {
                                    var collection = ListUtils.GetStringList(value);
                                    query.Where(q =>
                                    {
                                        foreach (var val in collection)
                                        {
                                            q.OrWhere(attributeName, val);
                                        }
                                        return q;
                                    });
                                }
                            }
                        }
                    }
                }
            }

            ParserOrderQuery(repository.Database.DatabaseType, query, taxisType);

            //if (!string.IsNullOrEmpty(where))
            //{
            //    query.WhereRaw(where);
            //}

            var summaries = await repository.GetAllAsync<ContentSummary>(query);

            if (startNum > 1 && totalNum > 0)
            {
                summaries = summaries.Skip(startNum - 1).Take(totalNum).ToList();
            }
            else if (startNum > 1)
            {
                summaries = summaries.Skip(startNum - 1).ToList();
            }
            else if (totalNum > 0)
            {
                summaries = summaries.Take(totalNum).ToList();
            }

            var list = new List<KeyValuePair<int, Content>>();
            var i = 0;
            foreach (var summary in summaries)
            {
                var content = await GetAsync(site, summary.ChannelId, summary.Id);
                list.Add(new KeyValuePair<int, Content>(i++, content));
            }

            return list;
        }

<<<<<<< HEAD
        private void ParserOrderQuery(DatabaseType databaseType, Query query, TaxisType taxisType)
=======
        private static void ParserOrderQuery(DatabaseType databaseType, Query query, TaxisType taxisType)
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            if (taxisType == TaxisType.OrderById)
            {
                query.OrderByDesc(nameof(Content.Top)).OrderBy(nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByIdDesc)
            {
                query.OrderByDesc(nameof(Content.Top), nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByChannelId)
            {
                query.OrderByDesc(nameof(Content.Top))
                    .OrderBy(nameof(Content.ChannelId))
                    .OrderByDesc(nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByChannelIdDesc)
            {
                query.OrderByDesc(nameof(Content.Top), nameof(Content.ChannelId), nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByAddDate)
            {
                query.OrderByDesc(nameof(Content.Top))
                    .OrderBy(nameof(Content.AddDate))
                    .OrderByDesc(nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByAddDateDesc)
            {
                query.OrderByDesc(nameof(Content.Top), nameof(Content.AddDate), nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByLastModifiedDate)
            {
                query.OrderByDesc(nameof(Content.Top))
                    .OrderBy(nameof(Content.LastModifiedDate))
                    .OrderByDesc(nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByLastModifiedDateDesc)
            {
                query.OrderByDesc(nameof(Content.Top), nameof(Content.LastModifiedDate), nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByTaxis)
            {
                query.OrderByDesc(nameof(Content.Top))
                    .OrderBy(nameof(Content.Taxis))
                    .OrderByDesc(nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByTaxisDesc)
            {
                query.OrderByDesc(nameof(Content.Top), nameof(Content.Taxis), nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByHits)
            {
                query.OrderByDesc(nameof(Content.Hits), nameof(Content.Id));
            }
            else if (taxisType == TaxisType.OrderByRandom)
            {
                if (databaseType == DatabaseType.SQLite)
                {
                    query.OrderByRaw("RANDOM()");
                }
                else if (databaseType == DatabaseType.MySql)
                {
                    query.OrderByRaw("RAND()");
                }
                else if (databaseType == DatabaseType.PostgreSql)
                {
                    query.OrderByRaw("random()");
                }
                else if (databaseType == DatabaseType.SqlServer)
                {
                    query.OrderByRaw("NEWID()");
                }
            }
        }
    }
}
