using System;
using System.Text;
using System.Threading.Tasks;
using SqlKata;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Parse;

namespace SSCMS.Services
{
    public partial interface IParseManager
    {
        ISettingsManager SettingsManager { get; }
        IPathManager PathManager { get; }
        IDatabaseManager DatabaseManager { get; }
        IFormManager FormManager { get; }
        ParsePage PageInfo { get; set; }
        ParseContext ContextInfo { get; set; }

<<<<<<< HEAD
        Task InitAsync(EditMode editMode, Site site, int pageChannelId, int pageContentId, Template template);
=======
        Task InitAsync(EditMode editMode, Site site, int pageChannelId, int pageContentId, Template template, int specialId);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

        Task ParseAsync(StringBuilder contentBuilder, string filePath, bool isDynamic);

        Task<string> GetDynamicScriptAsync(string dynamicApiUrl, Dynamic dynamic);

        Task<string> ParseDynamicAsync(Dynamic dynamic, string template);

        Task<string> AddStlErrorLogAsync(string elementName, string stlContent, Exception ex);

        Task<Channel> GetChannelAsync();

        Task<Content> GetContentAsync();
    }
}
