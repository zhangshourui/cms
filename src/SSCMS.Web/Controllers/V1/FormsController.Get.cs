﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.V1
{
    public partial class FormsController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> Get([FromQuery] GetRequest request)
        {
            var form = await _formRepository.GetAsync(request.SiteId, request.FormId);
            if (form == null) return NotFound();

            var styles = await _formRepository.GetTableStylesAsync(form.Id);

            var listAttributeNames = ListUtils.GetStringList(form.ListAttributeNames);
            var allAttributeNames = _formRepository.GetAllAttributeNames(styles);
            var pageSize = _formRepository.GetPageSize(form);

<<<<<<< HEAD
            var (total, items) = await _formDataRepository.GetListAsync(form, form.IsReply, null, null, request.Word, request.Page, pageSize);
=======
            var isRepliedOnly = form.IsReply && !form.IsReplyListAll;
            var (total, items) = await _formDataRepository.GetListAsync(form, isRepliedOnly, null, null, request.Word, request.Page, pageSize);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            var columns = _formRepository.GetColumns(listAttributeNames, styles, form.IsReply);

            return new GetResult
            {
                Items = items,
                Total = total,
                PageSize = pageSize,
                Styles = styles,
                AllAttributeNames = allAttributeNames,
                ListAttributeNames = listAttributeNames,
                IsReply = form.IsReply,
                Columns = columns
            };
        }
    }
}
