using System;
using System.Collections.Generic;
using System.Text;

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


        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;
        private static readonly int IndexInColumnsForArtists = 1;
        private static readonly int IndexInColumnsForAlbum = 2;
        private static readonly int IndexInColumnsForDuration = 3;

        public SongResult(Content content, int defaultIndex)
        {
            Thumbnails = ContentStaticHelpers.GetThumbnails(content);
            VideoId = content.MusicResponsiveListItemRenderer.Overlay.MusicItemThumbnailOverlayRenderer.Content.MusicPlayButtonRenderer.PlayNavigationEndpoint.WatchEndpoint.VideoId;
            Params = content.MusicResponsiveListItemRenderer.Overlay.MusicItemThumbnailOverlayRenderer.Content.MusicPlayButtonRenderer.PlayNavigationEndpoint.WatchEndpoint.Params;

            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForArtists].MusicResponsiveListItemFlexColumnRenderer.Text.Runs.ForEach(x => Artists.Add(new IdNamePair(x.NavigationEndpoint.BrowseEndpoint.BrowseId, x.Text)));

            // may not have an album
            if (content.MusicResponsiveListItemRenderer.FlexColumns.Count == (4 + defaultIndex))
            {
                var albumRun = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForAlbum].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns];
                Album = new IdNamePair(albumRun.NavigationEndpoint.BrowseEndpoint.BrowseId, albumRun.Text);
            }


            Duration = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForDuration].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
        }
    }
}
