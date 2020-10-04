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

        [JsonProperty("privacy")]
        public string Privacy { get; set; }

        [JsonProperty("author")]
        public IdNamePair Author { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("tracks")]
        public List<PlaylistTrack> Tracks { get; set; } = new List<PlaylistTrack>();

        [JsonProperty("continuation")]
        public string Continuation { get; set; }


        /// <summary>
        /// Note that this is what is returned when getting lists of playlists and
        /// only includes some of the fields of the playlist (like Title, PlaylistId, Thumbnails, Count).
        /// To get the full playlist information you have to call GetPlaylist
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
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

        public static Playlist FromBrowseResponse(BrowseResponse response)
        {
            Playlist playlist = new Playlist();

            var contents = response.Contents.SingleColumnBrowseResultsRenderer.Tabs[0].TabRenderer.Content.SectionListRenderer.Contents[0].MusicPlaylistShelfRenderer;

            playlist.PlaylistId = contents.PlaylistId;
            bool isUserPlaylist = response.Header.MusicEditablePlaylistDetailHeaderRenderer != null;
            Header header = null;
            if (isUserPlaylist)
            {
                header = response.Header.MusicEditablePlaylistDetailHeaderRenderer.Header;
                playlist.Privacy = header.MusicDetailHeaderRenderer.Privacy;
            }
            else
            {
                header = response.Header;// not sure if right
                playlist.Privacy = response.Header.MusicDetailHeaderRenderer.Privacy;
            }

            var authorRuns = header.MusicDetailHeaderRenderer.Subtitle.Runs;
            playlist.Author = new IdNamePair(authorRuns[2].NavigationEndpoint.BrowseEndpoint.BrowseId, authorRuns[2].Text);

            playlist.Title = header.MusicDetailHeaderRenderer.Title.Runs[0].Text;

            playlist.Thumbnails = header.MusicDetailHeaderRenderer.Thumbnail.CroppedSquareThumbnailRenderer.Thumbnail.Thumbnails;

            var secondSubtitleRuns = header.MusicDetailHeaderRenderer.SecondSubtitle.Runs;
            playlist.Count = secondSubtitleRuns[0].Text;
            if (secondSubtitleRuns.Count >= 3)
            {
                playlist.Duration = secondSubtitleRuns[2].Text;
            }

            response.Contents.SingleColumnBrowseResultsRenderer.Tabs[0].TabRenderer.Content
                .SectionListRenderer.Contents[0].MusicPlaylistShelfRenderer.Contents.ForEach(x =>
                playlist.Tracks.Add(PlaylistTrack.FromMusicResponsiveListItemRenderer(x.MusicResponsiveListItemRenderer))
            );

            // TODO continuation

            return playlist;
        }
    }

    public class PlaylistTrack
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("artists")]
        public List<IdNamePair> Artists { get; set; } = new List<IdNamePair>();

        [JsonProperty("album")]
        public IdNamePair Album { get; set; }

        [JsonProperty("likeStatus")]
        public string LikeStatus { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("setVideoId")]
        public string SetVideoId { get; set; }

        public static PlaylistTrack FromMusicResponsiveListItemRenderer(MusicResponsiveListItemRenderer renderer)
        {
            PlaylistTrack track = new PlaylistTrack();

            track.SetVideoId = renderer.PlaylistItemData.PlaylistSetVideoId;
            track.Thumbnails = renderer.Thumbnail.MusicThumbnailRenderer.Thumbnail.Thumbnails;
            track.Duration = renderer.FixedColumns[0].MusicResponsiveListItemFixedColumnRenderer.Text.Runs[0].Text;

            track.Title = renderer.FlexColumns[0].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[0].Text;
            track.VideoId = renderer.FlexColumns[0].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[0].NavigationEndpoint.WatchEndpoint.VideoId;

            var artistRuns = renderer.FlexColumns[1].MusicResponsiveListItemFlexColumnRenderer.Text.Runs;
            artistRuns.ForEach(x =>
                track.Artists.Add(new IdNamePair(x.NavigationEndpoint.BrowseEndpoint.BrowseId, x.Text))
            );

            var albumRuns = renderer.FlexColumns[2].MusicResponsiveListItemFlexColumnRenderer.Text.Runs;
            track.Album = new IdNamePair(albumRuns[0].NavigationEndpoint.BrowseEndpoint.BrowseId, albumRuns[0].Text);

            track.LikeStatus = renderer.Menu.MenuRenderer.TopLevelButtons[0].LikeButtonRenderer.LikeStatus;

            return track;
        }
    }
}
