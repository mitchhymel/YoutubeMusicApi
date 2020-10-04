using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models
{
    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("playlists")]
        public UserPlaylists Playlists { get; set; }

        public static User FromBrowseResponse(BrowseResponse result)
        {
            User user = new User();


            return user;
        }
    }

    public class UserPlaylist
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
    }

    public class UserPlaylists
    {
        [JsonProperty("browseId")]
        public string BrowseId { get; set; }

        [JsonProperty("results")]
        public List<UserPlaylist> Results { get; set; }
    }


}
