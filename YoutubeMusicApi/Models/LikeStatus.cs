using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace YoutubeMusicApi.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LikeStatus
    {
        [EnumMember(Value ="LIKE")]
        Like,

        [EnumMember(Value = "DISLIKE")]
        Dislike,

        [EnumMember(Value = "INDIFFERENT")]
        Indifferent,
    }
}
