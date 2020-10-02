using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Models.Search
{
    public class ContentStaticHelpers
    {
        public static UploadType GetUploadType(Content content)
        {
            string browseId = null;
            if (HasTopLevelNavigationEndpoint(content))
            {
                browseId = content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            }
            else if (HasBrowseIdInRuns(content))
            {
                browseId = content.MusicResponsiveListItemRenderer.FlexColumns[0].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[0].NavigationEndpoint.BrowseEndpoint.BrowseId;
            }

            if (browseId == null)
            {
                return UploadType.Song;
            }

            if (browseId.Contains("artist"))
            {
                return UploadType.Artist;
            }

            // else assume album
            return UploadType.Album;
        }

        public static bool HasTopLevelNavigationEndpoint(Content content)
        {
            return content.MusicResponsiveListItemRenderer.NavigationEndpoint != null
                && content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint != null;
        }

        public static bool HasBrowseIdInRuns(Content content)
        {
            var flexColumns = content.MusicResponsiveListItemRenderer.FlexColumns;
            if (flexColumns.Count <= 0)
            {
                return false;
            }

            var renderer = flexColumns[0];
            if (renderer == null)
            {
                return false;
            }

            var runs = renderer.MusicResponsiveListItemFlexColumnRenderer.Text.Runs;
            if (runs.Count <= 0)
            {
                return false;
            }

            var navEndpoint = runs[0].NavigationEndpoint;
            if (navEndpoint == null)
            {
                return false;
            }

            return navEndpoint.BrowseEndpoint != null;
        }

        public static SearchResultType GetSearchResultType(Content content)
        {
            int indexOfType = 1;
            var typeRuns = content.MusicResponsiveListItemRenderer.FlexColumns[indexOfType].MusicResponsiveListItemFlexColumnRenderer.Text.Runs;
            if (typeRuns == null || typeRuns.Count == 0)
            {
                // if it doesn't provide the type there, it's an Upload
                return SearchResultType.Upload;
            }

            string typeStr = typeRuns[0].Text;

            SearchResultType type;
            if (!Enum.TryParse<SearchResultType>(typeStr, out type))
            {
                // if we couldn't parse, it could be an upload,
                if (typeRuns[0].NavigationEndpoint != null
                    && typeRuns[0].NavigationEndpoint.BrowseEndpoint.BrowseId != null
                    && typeRuns[0].NavigationEndpoint.BrowseEndpoint.BrowseId.Contains("privately_owned"))
                {
                    return SearchResultType.Upload;
                }

                // if we couldn't parse and it wasn't an upload, assume it's an album since these can be multiple values like 'Single', 'EP', etc.
                type = SearchResultType.Album;
            }

            return type;
        }

        public static List<Thumbnail> GetThumbnails(Content content)
        {
            List<Thumbnail> thumbnails = new List<Thumbnail>();
            content.MusicResponsiveListItemRenderer.Thumbnail.MusicThumbnailRenderer.Thumbnail.Thumbnails.ForEach(x => thumbnails.Add(new Thumbnail()
            {
                Height = x.Height,
                Url = x.Url,
                Width = x.Width
            }));

            return thumbnails;
        }
    }
}
