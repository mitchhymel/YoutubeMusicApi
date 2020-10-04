using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models.Search
{
    public class AlbumResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Album;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string BrowseId { get; set; }
        public string Title { get; set; }
        public string AlbumType { get; set; }
        public string Artist { get; set; }
        public string Year { get; set; }
        public string ReleaseDate { get; set; }

        public bool IsUpload { get; set; }

        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;
        private static readonly int IndexInColumnsForAlbumType = 1;
        private static readonly int IndexInColumnsForArtist = 2;
        private static readonly int IndexInColumnsForYear = 3;
        private static readonly int IndexInColumnsForReleaseDate = 4;

        public AlbumResult(Content content)
        {
            Thumbnails = ContentStaticHelpers.GetThumbnails(content);
            BrowseId = content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            IsUpload = ContentStaticHelpers.BrowseIdIndicatedUpload(BrowseId);

            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;

            AlbumType = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForAlbumType].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;

            var countFlexColumns = content.MusicResponsiveListItemRenderer.FlexColumns.Count;
            var runs = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForAlbumType].MusicResponsiveListItemFlexColumnRenderer.Text.Runs;

            if (countFlexColumns >= IndexInColumnsForArtist + 1)
            {
                Artist = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForArtist].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            }

            if (countFlexColumns >= IndexInColumnsForYear + 1)
            {
                Year = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForYear].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            }

            if (countFlexColumns >= IndexInColumnsForReleaseDate + 1)
            {
                ReleaseDate = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForReleaseDate].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            }

            if (runs.Count >= IndexInColumnsForArtist + 1)
            {
                Artist = runs[IndexInColumnsForArtist].Text;
            }

            if (runs.Count >= IndexInColumnsForReleaseDate + 1)
            {
                ReleaseDate = runs[IndexInColumnsForReleaseDate].Text;
            }
        }
    }
}
