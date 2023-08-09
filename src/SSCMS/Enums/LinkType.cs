using Datory.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SSCMS.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LinkType
    {
        [DataEnum(DisplayName = "默认")] 
        None,

        [DataEnum(DisplayName = "链接到第一个子栏目")] 
        LinkToFirstChannel,

        [DataEnum(DisplayName = "链接到指定栏目")] 
        LinkToChannel,

<<<<<<< HEAD
=======
        [DataEnum(DisplayName = "链接到指定内容")] 
        LinkToContent,

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        [DataEnum(DisplayName = "链接到第一条内容")] 
        LinkToFirstContent,

        [DataEnum(DisplayName = "仅一条内容时链接到此内容")] 
        LinkToOnlyOneContent,

        [DataEnum(DisplayName = "不可链接")] 
        NoLink,
    }
}
