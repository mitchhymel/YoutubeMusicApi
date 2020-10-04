using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models.Search
{
    public class SongResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Song;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string Params { get; set; }
        public List<IdNamePair> Artists { get; set; } = new List<IdNamePair>();
        public IdNamePair Album { get; set; }
        public string Duration { get; set; }
        public bool IsUpload { get; set; }
        public string PlaylistId { get; set; }


        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;
        private static readonly int IndexInColumnsForPlaylistId = 0;
        private static readonly int IndexInColumnsForVideoId = 0;
        private static readonly int IndexInColumnsForArtists = 1;
        private static readonly int IndexInColumnsForAlbum = 2;
        private static readonly int IndexInColumnsForDuration = 3;

        public SongResult(Content content, int defaultIndex)
        {
            Thumbnails = ContentStaticHelpers.GetThumbnails(content);
            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;

            // Get artists first, so we can know if this is uploaded or not
            content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForArtists].MusicResponsiveListItemFlexColumnRenderer.Text.Runs.ForEach(x => Artists.Add(new IdNamePair(x.NavigationEndpoint.BrowseEndpoint.BrowseId, x.Text)));
            IsUpload = ContentStaticHelpers.BrowseIdIndicatedUpload(Artists[0].Id);
            if (IsUpload)
            {
                VideoId = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForVideoId].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].NavigationEndpoint.WatchEndpoint.VideoId;
                PlaylistId = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForPlaylistId].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].NavigationEndpoint.WatchEndpoint.PlaylistId;
            }
            else
            {
                VideoId = content.MusicResponsiveListItemRenderer.Overlay.MusicItemThumbnailOverlayRenderer.Content.MusicPlayButtonRenderer.PlayNavigationEndpoint.WatchEndpoint.VideoId;
                Params = content.MusicResponsiveListItemRenderer.Overlay.MusicItemThumbnailOverlayRenderer.Content.MusicPlayButtonRenderer.PlayNavigationEndpoint.WatchEndpoint.Params;
            }

            var flexColumnCount = content.MusicResponsiveListItemRenderer.FlexColumns.Count;
            
            // may not have an album
            if (flexColumnCount >= (defaultIndex + IndexInColumnsForAlbum + 1))
            {
                var albumRun = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForAlbum].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns];
                Album = new IdNamePair(albumRun.NavigationEndpoint.BrowseEndpoint.BrowseId, albumRun.Text);
            }

            if (flexColumnCount >= (defaultIndex + IndexInColumnsForDuration + 1))
            {
                Duration = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForDuration].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            }
            else
            {
                //duration might be in fixedColumns
                if (content.MusicResponsiveListItemRenderer.FixedColumns != null
                    && content.MusicResponsiveListItemRenderer.FixedColumns.Count > 0
                    && content.MusicResponsiveListItemRenderer.FixedColumns[0].MusicResponsiveListItemFixedColumnRenderer != null)
                {
                    Duration = content.MusicResponsiveListItemRenderer.FixedColumns[0].MusicResponsiveListItemFixedColumnRenderer.Text.Runs[0].Text;
                }
            }
        }
    }
}
