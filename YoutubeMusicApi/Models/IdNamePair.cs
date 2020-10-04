using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Models
{
    public class IdNamePair
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public IdNamePair(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
