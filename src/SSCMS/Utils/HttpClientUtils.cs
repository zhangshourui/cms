<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
using System;
=======
﻿using System;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SSCMS.Utils
{
    public static class HttpClientUtils
    {
        public static async Task<string> GetStringAsync(string url)
        {
            return await GetStringAsync(url, Encoding.UTF8);
        }

        public static async Task<string> GetStringAsync(string url, Encoding encoding)
        {
            try
            {
                string html;

                if (encoding == Encoding.UTF8)
                {
                    using (var client = new HttpClient())
                    {
                        html = await client.GetStringAsync(url);
                    }
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        var bytes = await client.GetByteArrayAsync(url);
                        html = ConvertBytesToString(bytes, encoding);
                    }
                }

                return html;
            }
            catch (Exception ex)
            {
                throw new Exception($"页面地址“{url}”无法访问，{ex.Message}！");
            }
        }

        private static string ConvertBytesToString(byte[] bytes, Encoding encoding)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var toEncoding = Encoding.UTF8;
            var toBytes = Encoding.Convert(encoding, toEncoding, bytes);
            return toEncoding.GetString(toBytes);
        }

        public static async Task<bool> DownloadAsync(string remoteUrl, string filePath)
        {
            try
            {
                DirectoryUtils.CreateDirectoryIfNotExists(filePath);
                FileUtils.DeleteFileIfExists(filePath);

                using (var client = new HttpClient())
                {
                    using (var stream = await client.GetStreamAsync(remoteUrl))
                    {
                        using (var fs = new FileStream(filePath, FileMode.CreateNew))
                        {
                            await stream.CopyToAsync(fs);
                        }
                    }
                }

                // using var client = new WebClient();
                // client.DownloadFile(remoteUrl, filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"页面地址“{remoteUrl}”无法访问，{ex.Message}！");
            }
            return true;
        }
<<<<<<< HEAD

        public static string GetPeerIP(HttpContext context)
        {

            try
            {
                var request = context.Request;

                if (request == null)
                    return null;
                string strIPAddr = "";
                string xfr = request.Headers["X-Forwarded-For"];
                if (string.IsNullOrEmpty(xfr)
                    || xfr.IndexOf("unknown") >= 0)

                    strIPAddr = context.Connection.LocalIpAddress.MapToIPv4().ToString();
                else
                {
                    string[] arr = xfr.Split(new char[] { ',', ';' });
                    if (arr.Length >= 1)
                    {
                        strIPAddr = arr[0].Trim();
                    }
                }
                return strIPAddr;
            }
            catch (Exception)
            {

                return string.Empty;
            }


        }
=======
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }
}
