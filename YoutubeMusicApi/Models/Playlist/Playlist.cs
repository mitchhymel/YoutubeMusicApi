using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models
{
    public class Playlist
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }

        [JsonProperty("count")]
        public string Count { get; set; }
        
        
        public static Playlist FromContent(MusicTwoRowItemRenderer renderer)
        {
            Playlist playlist = new Playlist();

            playlist.PlaylistId = renderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            playlist.Thumbnails = renderer.ThumbnailRenderer.MusicThumbnailRenderer.Thumbnail.Thumbnails;
            playlist.Title = renderer.Title.Runs[0].Text;
            
            if (renderer.Subtitle.Runs.Count >= 3)
            {
                playlist.Count = renderer.Subtitle.Runs[2].Text;
            }

            return playlist;
        }
    }
}
