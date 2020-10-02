using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Models.Search
{
    public class UploadedAlbumResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Upload;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string Title { get; set; }
        public string BrowseId { get; set; }
        public string Artist { get; set; }
        public string ReleaseDate { get; set; }


        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;
        private static readonly int IndexInColumnsForArtist = 2;
        private static readonly int IndexInColumnsForReleaseDate = 4;

        public UploadedAlbumResult(Content content)
        {
            Thumbnails = ContentStaticHelpers.GetThumbnails(content);
            BrowseId = content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;

            var flexColumnCount = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs.Count;

            if (flexColumnCount >= 3)
            {
                Artist = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForArtist].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            }

            if (flexColumnCount >= 5)
            {
                ReleaseDate = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForReleaseDate].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            }

        }
    }

}
