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
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();

        public static User FromBrowseResponse(BrowseResponse response)
        {
            User user = new User();

            user.Name = response.Header.MusicVisualHeaderRenderer.Title.Runs[0].Text;

            var contents = response.Contents.SingleColumnBrowseResultsRenderer.Tabs[0].TabRenderer.Content.SectionListRenderer.Contents[0].MusicCarouselShelfRenderer.Contents;
            foreach (var content in contents)
            {
                user.Playlists.Add(Playlist.FromMusicTwoRowItemRenderer(content.MusicTwoRowItemRenderer));
            }

            return user;
        }
    }


}
