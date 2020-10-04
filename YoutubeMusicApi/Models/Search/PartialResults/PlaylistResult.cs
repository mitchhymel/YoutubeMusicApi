using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models.Search
{
    public class PlaylistResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Playlist;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string BrowseId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ItemCount { get; set; }

        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;
        private static readonly int IndexInColumnsForAuthor = 1;
        private static readonly int IndexInColumnsForItemCount = 2;

        public PlaylistResult(Content content, int defaultIndex)
        {
            Thumbnails = ContentStaticHelpers.GetThumbnails(content);
            BrowseId = content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            Author = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForAuthor].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            ItemCount = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForItemCount].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text.Split(' ')[0];
        }
    }
}
