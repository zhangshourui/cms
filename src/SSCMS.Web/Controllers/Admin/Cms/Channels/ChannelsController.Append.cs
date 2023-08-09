﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Core.Utils;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Channels
{
    public partial class ChannelsController
    {
        [HttpPost, Route(RouteAppend)]
        public async Task<ActionResult<List<int>>> Append([FromBody] AppendRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                    MenuUtils.SitePermissions.Channels))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return this.Error(Constants.ErrorNotFound);

            var parent = await _channelRepository.GetAsync(request.ParentId);
            if (parent == null) return this.Error("无法确定父栏目");

            var insertedChannelIdHashtable = new Hashtable { [1] = request.ParentId }; //key为栏目的级别，1为第一级栏目

            var channelTemplateId = request.ChannelTemplateId;
            var contentTemplateId = request.ContentTemplateId;
            if (request.IsParentTemplates)
            {
                channelTemplateId = parent.ChannelTemplateId;
                contentTemplateId = parent.ContentTemplateId;
            }

            var channelNames = request.Channels.Split('\n');
            IList<string> nodeIndexNameList = null;
            var expandedChannelIds = new List<int>
            {
                request.SiteId
            };
            foreach (var item in channelNames)
            {
                if (string.IsNullOrEmpty(item)) continue;

                //count为栏目的级别
                var count = StringUtils.GetStartCount('－', item) == 0 ? StringUtils.GetStartCount('-', item) : StringUtils.GetStartCount('－', item);
                var channelName = item.Substring(count, item.Length - count);
                var indexName = string.Empty;
                count++;

                if (!string.IsNullOrEmpty(channelName) && insertedChannelIdHashtable.Contains(count))
                {
                    if (request.IsIndexName)
                    {
                        indexName = channelName.Trim();
                    }

<<<<<<< HEAD
                    if (channelName.Contains('(') && channelName.Contains(')'))
=======
                    if (!channelName.StartsWith("(") && channelName.Contains('(') && channelName.Contains(')'))
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                    {
                        var length = channelName.IndexOf(')') - channelName.IndexOf('(');
                        if (length > 0)
                        {
                            indexName = channelName.Substring(channelName.IndexOf('(') + 1, length);
                            channelName = channelName.Substring(0, channelName.IndexOf('('));
                        }
                    }
<<<<<<< HEAD
                    channelName = channelName.Trim();
=======

                    channelName = channelName.Trim();
                    
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                    indexName = indexName.Trim(' ', '(', ')');
                    if (!string.IsNullOrEmpty(indexName))
                    {
                        if (nodeIndexNameList == null)
                        {
                            nodeIndexNameList = (await _channelRepository.GetIndexNamesAsync(request.SiteId)).ToList();
                        }
                        if (nodeIndexNameList.Contains(indexName))
                        {
                            indexName = string.Empty;
                        }
                        else
                        {
                            nodeIndexNameList.Add(indexName);
                        }
                    }

                    var parentId = (int)insertedChannelIdHashtable[count];

                    var insertedChannelId = await _channelRepository.InsertAsync(request.SiteId, parentId, channelName, indexName, parent.ContentModelPluginId, channelTemplateId, contentTemplateId);
                    insertedChannelIdHashtable[count + 1] = insertedChannelId;
                    expandedChannelIds.Add(insertedChannelId);

                    await _createManager.CreateChannelAsync(request.SiteId, insertedChannelId);
                }
            }

            await _channelRepository.RemoveListCacheAsync(request.SiteId);

            return expandedChannelIds;
        }
    }
}
