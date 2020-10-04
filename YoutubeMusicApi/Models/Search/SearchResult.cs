using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models.Search
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SearchResultType
    {
        All,
        Album,
        Artist,
        Playlist,
        Song,
        Upload,
        Video,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum UploadType
    {
        Song,
        Artist,
        Album,
    }


    public class SearchResult
    {
        public List<AlbumResult> Albums { get; set; } = new List<AlbumResult>();
        public List<ArtistResult> Artists { get; set; } = new List<ArtistResult>();
        public List<PlaylistResult> Playlists { get; set; } = new List<PlaylistResult>();
        public List<SongResult> Songs { get; set; } = new List<SongResult>();
        public List<VideoResult> Videos { get; set; } = new List<VideoResult>();

        public static SearchResult FromBrowseResponse(BrowseResponse result, SearchResultType filter)
        {
            SearchResult ret = new SearchResult();
            if (result.Contents == null)
            {
                // no response
                return ret;
            }

            SectionListRenderer renderer = result.Contents.SectionListRenderer;
            if (renderer == null)
            {
                int indexToUse = filter == SearchResultType.Upload ? 1 : 0;

                if (result.Contents.TabbedSearchResultsRenderer != null)
                {
                    renderer = result.Contents.TabbedSearchResultsRenderer.Tabs[indexToUse].TabRenderer.Content.SectionListRenderer;
                }
                else if (result.Contents.SingleColumnBrowseResultsRenderer != null)
                {
                    renderer = result.Contents.SingleColumnBrowseResultsRenderer.Tabs[indexToUse].TabRenderer.Content.SectionListRenderer;
                }

                if (renderer == null)
                {
                    // TODO: error ? throw ? 
                    return ret; 
                }
            }

            List<Content> results = renderer.Contents;

            //if (results.Count == 1)
            //{
            //    // no results?
            //    return ret;
            //}

            foreach (var res in results)
            {
                if (res.MusicShelfRenderer == null)
                {
                    continue;
                }

                var innerResults = res.MusicShelfRenderer.Contents;
                foreach (var innerContent in innerResults)
                {
                    ParseInnerContent(ret, innerContent, filter);
                }
            }

            return ret;
        }

        private static void ParseInnerContent(SearchResult ret, Content content, SearchResultType filter)
        {
            SearchResultType type = ContentStaticHelpers.GetSearchResultType(content);
            int defaultIndex = filter == SearchResultType.All ? 1 : 0;

            switch (type)
            {
                case SearchResultType.Album:
                    ret.Albums.Add(new AlbumResult(content));
                    break;
                case SearchResultType.Artist:
                    ret.Artists.Add(new ArtistResult(content));
                    break;
                case SearchResultType.Playlist:
                    ret.Playlists.Add(new PlaylistResult(content, defaultIndex));
                    break;
                case SearchResultType.Song:
                    ret.Songs.Add(new SongResult(content, defaultIndex));
                    break;
                case SearchResultType.Video:
                    ret.Videos.Add(new VideoResult(content, defaultIndex));
                    break;
                case SearchResultType.Upload:
                    throw new Exception("We should not be handling Uploads specifically, Uploads should be handled as the type they are (Album, Artist, Song)");
                default:
                    throw new Exception($"Unsupported type when parsing generated result: {type}");
            }
        }
    }
}
