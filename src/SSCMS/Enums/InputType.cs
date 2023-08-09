using Datory.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SSCMS.Enums
{
    /// <summary>
    /// 表单的输入类型。
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InputType
    {
        [DataEnum(DisplayName = "文本输入框")]
        Text,
        [DataEnum(DisplayName = "数字输入框")]
        Number,
        [DataEnum(DisplayName = "多行文本输入框")]
        TextArea,
        [DataEnum(DisplayName = "富文本编辑器")]
        TextEditor,
        [DataEnum(DisplayName = "多选项")]
        CheckBox,
        [DataEnum(DisplayName = "单选项")]
        Radio,
        [DataEnum(DisplayName = "单选下拉框")]
        SelectOne,
        [DataEnum(DisplayName = "多选下拉框")]
        SelectMultiple,
        [DataEnum(DisplayName = "联动字段")]
        SelectCascading,
        [DataEnum(DisplayName = "日期选择框")]
        Date,
        [DataEnum(DisplayName = "日期及时间选择框")]
        DateTime,
        [DataEnum(DisplayName = "图片上传控件")]
        Image,
<<<<<<< HEAD
        [DataEnum(DisplayName = "音视频上传控件")]
=======
        [DataEnum(DisplayName = "视频上传控件")]
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        Video,
        [DataEnum(DisplayName = "文件上传控件")]
        File,
        [DataEnum(DisplayName = "自定义输入控件")]
        Customize,
        [DataEnum(DisplayName = "隐藏项")]
        Hidden,
    }
}