using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models.Search
{
    public class ContentStaticHelpers
    {
        public static SearchResultType GetSearchResultType(Content content)
        {
            int indexOfType = 1;
            var typeRuns = content.MusicResponsiveListItemRenderer.FlexColumns[indexOfType].MusicResponsiveListItemFlexColumnRenderer.Text.Runs;
            if (typeRuns == null || typeRuns.Count == 0)
            {
                // if it doesn't provide the type there, it's an Upload
                UploadType ut = GetUploadType(content);
                switch (ut)
                {
                    case UploadType.Album:
                        return SearchResultType.Album;
                    case UploadType.Artist:
                        return SearchResultType.Artist;
                    case UploadType.Song:
                        return SearchResultType.Song;
                    default:
                        throw new Exception($"Unsupported UploadType when trying to get SearchResultType: {ut}");
                }
            }

            string typeStr = typeRuns[0].Text;

            // assume it's an album since these can be multiple values like 'Single', 'EP', etc.
            SearchResultType type = SearchResultType.Album;
            if (!Enum.TryParse<SearchResultType>(typeStr, out type))
            {
                // if we couldn't parse, it could be a song (uploaded or otherwise),
                if (typeRuns[0].NavigationEndpoint != null
                    && typeRuns[0].NavigationEndpoint.BrowseEndpoint.BrowseId != null)
                {
                    type = SearchResultType.Song;
                }
            }

            // sometimes the string-to-enum mistakenly results in All... assume album
            if (type == SearchResultType.All)
            {
                type = SearchResultType.Album;
            }

            return type;
        }

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

        public static List<Thumbnail> GetThumbnails(Content content)
        {
            return content.MusicResponsiveListItemRenderer.Thumbnail.MusicThumbnailRenderer.Thumbnail.Thumbnails;
        }

        public static bool BrowseIdIndicatedUpload(string browseId)
        {
            return browseId.Contains("privately_owned");
        }
    }
}
