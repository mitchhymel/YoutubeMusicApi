using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace YoutubeMusicApi.Models
{
    public class AuthHeaders
    {
        [DataMember(Name = "User-Agent")]
        public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0";

        [DataMember(Name = "Accept")]
        public string Accept { get; set; } = "*/*";

        [DataMember(Name = "Accept-Language")]
        public string AcceptLanguage { get; set; } = "en-US,en;q=0.5";

        [DataMember(Name = "Content-Type")]
        public string ContentType { get; set; } = "application/json";

        [DataMember(Name = "X-Goog-AuthUser")]
        public string GoogAuthUser { get; set; } = "0";

        [DataMember(Name = "x-origin")]
        public string Origin { get; set; } = "https://music.youtube.com";

        [DataMember(Name = "Cookie")]
        public string Cookie { get; set; }
    }
}
