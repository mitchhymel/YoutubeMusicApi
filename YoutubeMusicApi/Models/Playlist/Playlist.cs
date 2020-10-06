using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        [JsonProperty("author")]
        public IdNamePair Author { get; set; }

        [JsonProperty("count")]
        public string Count { get; set; }

        [JsonProperty("privacy")]
        public PrivacyStatus Privacy { get; set; }

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
        public static Playlist FromMusicTwoRowItemRenderer(MusicTwoRowItemRenderer renderer)
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
            }
            else
            {
                header = response.Header;// not sure if right
            }

            if (header.MusicDetailHeaderRenderer != null && header.MusicDetailHeaderRenderer.Privacy != null)
            {
                playlist.Privacy = (PrivacyStatus)Enum.Parse(typeof(PrivacyStatus), header.MusicDetailHeaderRenderer.Privacy, true);
            }
            else
            {
                playlist.Privacy = PrivacyStatus.Public;
            }

            var authorRuns = header.MusicDetailHeaderRenderer.Subtitle.Runs;
            if (authorRuns[2].NavigationEndpoint == null)
            {
                // sometimes the author is "YouTube Music"
                playlist.Author = new IdNamePair(authorRuns[2].Text, authorRuns[2].Text);
            }
            else
            {
                playlist.Author = new IdNamePair(authorRuns[2].NavigationEndpoint.BrowseEndpoint.BrowseId, authorRuns[2].Text);
            }

            playlist.Title = header.MusicDetailHeaderRenderer.Title.Runs[0].Text;

            playlist.Thumbnails = header.MusicDetailHeaderRenderer.Thumbnail.CroppedSquareThumbnailRenderer.Thumbnail.Thumbnails;

            var secondSubtitleRuns = header.MusicDetailHeaderRenderer.SecondSubtitle.Runs;
            playlist.Count = secondSubtitleRuns[0].Text;
            if (secondSubtitleRuns.Count >= 3)
            {
                playlist.Duration = secondSubtitleRuns[2].Text;
            }

            if (contents.Contents != null)
            {
                contents.Contents.ForEach(x =>
                    playlist.Tracks.Add(PlaylistTrack.FromMusicResponsiveListItemRenderer(x.MusicResponsiveListItemRenderer))
                );
            }

            if (contents.Continuations != null)
            {
                playlist.Continuation = contents.Continuations[0].NextContinuationData.Continuation;
            }

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
        public LikeStatus LikeStatus { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("setVideoId")]
        public string SetVideoId { get; set; }

        public static PlaylistTrack FromMusicResponsiveListItemRenderer(MusicResponsiveListItemRenderer renderer)
        {
            PlaylistTrack track = new PlaylistTrack();

            // if this is not your playlist, it won't have setvideoids for tracks because you can't edit it
            if (renderer.PlaylistItemData != null)
            {
                track.SetVideoId = renderer.PlaylistItemData.PlaylistSetVideoId;
            }

            track.Thumbnails = renderer.Thumbnail.MusicThumbnailRenderer.Thumbnail.Thumbnails;
            track.Duration = renderer.FixedColumns[0].MusicResponsiveListItemFixedColumnRenderer.Text.Runs[0].Text;

            track.Title = renderer.FlexColumns[0].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[0].Text;
            track.VideoId = renderer.FlexColumns[0].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[0].NavigationEndpoint.WatchEndpoint.VideoId;

            var artistRuns = renderer.FlexColumns[1].MusicResponsiveListItemFlexColumnRenderer.Text.Runs;
            foreach (var run in artistRuns)
            {
                if (run.NavigationEndpoint != null && run.Text != ", ")
                {
                    track.Artists.Add(new IdNamePair(run.NavigationEndpoint.BrowseEndpoint.BrowseId, run.Text));
                }
            }

            // sometimes album is not included
            var albumRuns = renderer.FlexColumns[2].MusicResponsiveListItemFlexColumnRenderer.Text.Runs;
            if (albumRuns != null && albumRuns[0].Text != null && albumRuns[0].NavigationEndpoint != null)
            {
                track.Album = new IdNamePair(albumRuns[0].NavigationEndpoint.BrowseEndpoint.BrowseId, albumRuns[0].Text);
            }

            track.LikeStatus = (LikeStatus) Enum.Parse(typeof(LikeStatus), renderer.Menu.MenuRenderer.TopLevelButtons[0].LikeButtonRenderer.LikeStatus, true);

            return track;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PrivacyStatus
    { 
        Public,
        Private,
        Unlisted,
    }

}
