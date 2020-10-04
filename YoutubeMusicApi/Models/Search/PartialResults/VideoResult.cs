using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models.Search
{
    public class VideoResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Video;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string Params { get; set; }
        public string Artist { get; set; }
        public string Views { get; set; }
        public string Duration { get; set; }

        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;
        private static readonly int IndexInColumnsForArtist = 1;
        private static readonly int IndexInColumnsForViews = 2;
        private static readonly int IndexInColumnsForDuration = 3;

        public VideoResult(Content content, int defaultIndex)
        {
            Thumbnails = ContentStaticHelpers.GetThumbnails(content);
            VideoId = content.MusicResponsiveListItemRenderer.Overlay.MusicItemThumbnailOverlayRenderer.Content.MusicPlayButtonRenderer.PlayNavigationEndpoint.WatchEndpoint.VideoId;
            Params = content.MusicResponsiveListItemRenderer.Overlay.MusicItemThumbnailOverlayRenderer.Content.MusicPlayButtonRenderer.PlayNavigationEndpoint.WatchEndpoint.Params;

            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            Artist = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForArtist].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            Views = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForViews].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            Duration = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForDuration].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
        }
    }
}
