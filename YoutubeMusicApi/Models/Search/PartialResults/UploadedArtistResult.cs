using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Models.Search
{
    public class UploadedArtistResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Upload;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string Title { get; set; }
        public string BrowseId { get; set; }


        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;

        public UploadedArtistResult(Content content)
        {
            Thumbnails = ContentStaticHelpers.GetThumbnails(content);
            BrowseId = content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
        }
    }
}
