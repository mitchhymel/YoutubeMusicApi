using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Models.Search
{
    public class UploadedSongResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Upload;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string Title { get; set; }
        public string VideoId { get; set; }
        public string PlaylistId { get; set; }
        public IdNamePair Artist { get; set; }
        public IdNamePair Album { get; set; }
        public string Duration { get; set; }


        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;
        private static readonly int IndexInColumnsForPlaylistId = 0;
        private static readonly int IndexInColumnsForVideoId = 0;
        private static readonly int IndexInColumnsForArtist = 1;
        private static readonly int IndexInColumnsForAlbum = 2;
        private static readonly int IndexInColumnsForDuration = 3;

        public UploadedSongResult(Content content)
        {
            Thumbnails = ContentStaticHelpers.GetThumbnails(content);
            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            VideoId = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForVideoId].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].NavigationEndpoint.WatchEndpoint.VideoId;
            PlaylistId = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForPlaylistId].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].NavigationEndpoint.WatchEndpoint.PlaylistId;

            var artist = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForArtist].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns];
            Artist = new IdNamePair(artist.NavigationEndpoint.BrowseEndpoint.BrowseId, artist.Text);

            var album = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForAlbum].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns];
            Album = new IdNamePair(album.NavigationEndpoint.BrowseEndpoint.BrowseId, album.Text);

            var flexColumnCount = content.MusicResponsiveListItemRenderer.FlexColumns.Count;
            if (flexColumnCount >= 4)
            {
                Duration = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForDuration].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            }
            else
            {
                //duration might be in fixedColumns
                if (content.MusicResponsiveListItemRenderer.FixedColumns != null
                    && content.MusicResponsiveListItemRenderer.FixedColumns.Count > 0
                    && content.MusicResponsiveListItemRenderer.FixedColumns[0].MusicResponsiveListItemFlexColumnRenderer != null)
                {
                    Duration = content.MusicResponsiveListItemRenderer.FixedColumns[0].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[0].Text;
                }
            }
        }
    }
}
