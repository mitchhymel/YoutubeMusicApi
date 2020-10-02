using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Models.Search
{
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
        public List<UploadedSongResult> UploadedSongs { get; set; } = new List<UploadedSongResult>();
        public List<UploadedArtistResult> UploadedArtists { get; set; } = new List<UploadedArtistResult>();
        public List<UploadedAlbumResult> UploadedAlbums { get; set; } = new List<UploadedAlbumResult>();


        public static SearchResult ParseResultListFromGenerated(GeneratedSearchResult result, SearchResultType filter)
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
                renderer = result.Contents.TabbedSearchResultsRenderer.Tabs[indexToUse].TabRenderer.Content.SectionListRenderer;
                if (renderer == null)
                {
                    return ret; // no results?
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
                case SearchResultType.Upload:
                    UploadType ut = ContentStaticHelpers.GetUploadType(content);
                    if (ut == UploadType.Song)
                    {
                        ret.UploadedSongs.Add(new UploadedSongResult(content));
                    }
                    else if (ut == UploadType.Artist)
                    {
                        ret.UploadedArtists.Add(new UploadedArtistResult(content));
                    }
                    else if (ut == UploadType.Album)
                    {
                        ret.UploadedAlbums.Add(new UploadedAlbumResult(content));
                    }
                    else
                    {
                        throw new Exception("Unsupported upload type");
                    }
                    break;
                case SearchResultType.Video:
                    ret.Videos.Add(new VideoResult(content, defaultIndex));
                    break;
                default:
                    throw new Exception("Unsupported type when parsing generated result");
            }
        }
    }
}
