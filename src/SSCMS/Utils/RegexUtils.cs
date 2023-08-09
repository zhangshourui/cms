﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SSCMS.Utils
{
    public static class RegexUtils
    {

        /*
         * 通用：.*?
         * 所有链接：<a\s*.*?href=(?:"(?<url>[^"]*)"|'(?<url>[^']*)'|(?<url>\S+)).*?>
         * */

        public static readonly RegexOptions Options = RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;

        public static List<string> GetOriginalImageSrcs(string html)
        {
            html = StringUtils.Replace(html, " data-src=", " src=");
            const string regex = "(img|input)[^><]*\\s+src\\s*=\\s*(?:\"(?<url>[^\"]*)\"|'(?<url>[^']*)'|(?<url>[^>\\s]*))";
            return GetContents("url", regex, html);
        }

        public static List<string> GetOriginalScriptSrcs(string html)
        {
            const string regex = "script[^><]*\\s+src\\s*=\\s*(?:\"(?<url>[^\"]*)\"|'(?<url>[^']*)'|(?<url>[^>\\s]*))";
            return GetContents("url", regex, html);
        }

        public static List<string> GetOriginalLinkHrefs(string html)
        {
            const string regex = "a[^><]*\\s+href\\s*=\\s*(?:\"(?<url>[^\"]*)\"|'(?<url>[^']*)'|(?<url>[^>\\s]*))";
            return GetContents("url", regex, html);
        }

        public static List<string> GetTagInnerContents(string tagName, string html)
        {
<<<<<<< HEAD
            string regex = $"<{tagName}\\s+[^><]*>\\s*(?<content>[\\s\\S]+?)\\s*</{tagName}>";
=======
            string regex = $"<{tagName}\\s*[^><]*>\\s*(?<content>[\\s\\S]+?)\\s*</{tagName}>";
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            return GetContents("content", regex, html);
        }

        public static string GetInnerContent(string tagName, string html)
        {
            string regex = $"<{tagName}[^><]*>(?<content>[\\s\\S]+?)</{tagName}>";
            return GetContent("content", regex, html);
        }

        public static string GetContent(string groupName, string regex, string html)
        {
            var content = string.Empty;
            if (string.IsNullOrEmpty(regex)) return content;
            if (regex.IndexOf("<" + groupName + ">", StringComparison.Ordinal) == -1)
            {
                return regex;
            }

            var reg = new Regex(regex, Options);
            var match = reg.Match(html);
            if (match.Success)
            {
                content = match.Groups[groupName].Value;
            }

            return content;
        }

        public static string Replace(string regex, string input, string replacement)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var reg = new Regex(regex, Options);
            return reg.Replace(input, replacement);
        }

        public static bool IsMatch(string regex, string input)
        {
            var reg = new Regex(regex, Options);
            return reg.IsMatch(input);
        }

        public static List<string> GetContents(string groupName, string regex, string html)
        {
            if (string.IsNullOrEmpty(regex)) return new List<string>();

            var list = new List<string>();
            var reg = new Regex(regex, Options);

            for (var match = reg.Match(html); match.Success; match = match.NextMatch())
            {
                var theValue = match.Groups[groupName].Value;
                if (!list.Contains(theValue))
                {
                    list.Add(theValue);
                }
            }
            return list;
        }
    }
}
