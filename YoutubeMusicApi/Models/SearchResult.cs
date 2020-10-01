using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models
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

            List<Content2> results = renderer.Contents;

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
                int defaultIndex = filter == SearchResultType.All ? 1 : 0;
                foreach (var innerContent in innerResults)
                {
                    SearchResultType type = GetSearchResultType(innerContent);

                    switch (type)
                    {
                        case SearchResultType.Album:
                            ret.Albums.Add(new AlbumResult(innerContent));
                            break;
                        case SearchResultType.Artist:
                            ret.Artists.Add(new ArtistResult(innerContent));
                            break;
                        case SearchResultType.Playlist:
                            ret.Playlists.Add(new PlaylistResult(innerContent, defaultIndex));
                            break;
                        case SearchResultType.Song:
                            ret.Songs.Add(new SongResult(innerContent, defaultIndex));
                            break;
                        case SearchResultType.Upload:
                            UploadType ut = GetUploadType(innerContent);
                            if (ut == UploadType.Song)
                            {
                                ret.UploadedSongs.Add(new UploadedSongResult(innerContent));
                            }
                            else if (ut == UploadType.Artist)
                            {
                                ret.UploadedArtists.Add(new UploadedArtistResult(innerContent));
                            }
                            else if (ut == UploadType.Album)
                            {
                                ret.UploadedAlbums.Add(new UploadedAlbumResult(innerContent));
                            }
                            else
                            {
                                throw new Exception("Unsupported upload type");
                            }
                            break;
                        case SearchResultType.Video:
                            ret.Videos.Add(new VideoResult(innerContent, defaultIndex));
                            break;
                        default:
                            throw new Exception("Unsupported type when parsing generated result");
                    }

                }
            }

            return ret;
        }

        public static UploadType GetUploadType(Content3 content)
        {
            var browseId = content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
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

        public static SearchResultType GetSearchResultType(Content3 content)
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
                // if we couldn't parse, assume it's an album since these can be multiple values like 'Single', 'EP', etc.
                type = SearchResultType.Album;
            }

            return type;
        }

        public static List<Thumbnail> GetThumbnails(Content3 content)
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

    public class ArtistResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Artist;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string BrowseId { get; set; }
        public string Artist { get; set; }

        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForArtistName = 0;

        public ArtistResult(Content3 content)
        {
            Thumbnails = SearchResult.GetThumbnails(content);
            BrowseId = content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            Artist = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForArtistName].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
        }
    }

    public class AlbumResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Album;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string BrowseId { get; set; }
        public string Title { get; set; }
        public string AlbumType { get; set; }
        public string Artist { get; set; }
        public string Year { get; set; }

        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;
        private static readonly int IndexInColumnsForAlbumType = 1;
        private static readonly int IndexInColumnsForArtist = 2;
        private static readonly int IndexInColumnsForYear = 3;

        public AlbumResult(Content3 content)
        {
            Thumbnails = SearchResult.GetThumbnails(content);
            BrowseId = content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            AlbumType = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForAlbumType].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;

            var countFlexColumns = content.MusicResponsiveListItemRenderer.FlexColumns.Count;

            if (countFlexColumns >= 3)
            {
                Artist = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForArtist].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            }

            if (countFlexColumns >= 4)
            {
                Year = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForYear].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            }
        }
    }

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

        public PlaylistResult(Content3 content, int defaultIndex)
        {
            Thumbnails = SearchResult.GetThumbnails(content);
            BrowseId = content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            Author = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForAuthor].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            ItemCount = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForItemCount].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text.Split(' ')[0];
        }
    }

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

        public SongResult(Content3 content, int defaultIndex)
        {
            Thumbnails = SearchResult.GetThumbnails(content);
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

    public class UploadedSongResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Upload;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string Title { get; set; }
        public string VideoId { get; set; }
        public string PlaylistId { get; set; }
        public IdNamePair Artist { get; set; }
        public IdNamePair Album { get; set; }
        public string Duration { get; set; }


        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;
        private static readonly int IndexInColumnsForPlaylistId= 0;
        private static readonly int IndexInColumnsForVideoId = 0;
        private static readonly int IndexInColumnsForArtist = 1;
        private static readonly int IndexInColumnsForAlbum = 2;
        private static readonly int IndexInColumnsForDuration = 3;

        public UploadedSongResult(Content3 content)
        {
            Thumbnails = SearchResult.GetThumbnails(content);
            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            VideoId = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForVideoId].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].WatchEndpoint.VideoId;
            PlaylistId = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForPlaylistId].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].WatchEndpoint.PlaylistId;

            var artist = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForArtist].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns];
            Artist = new IdNamePair(artist.NavigationEndpoint.BrowseEndpoint.BrowseId, artist.Text);

            var album = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForAlbum].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns];
            Album = new IdNamePair(album.NavigationEndpoint.BrowseEndpoint.BrowseId, album.Text);

            Duration = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForDuration].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
        }
    }

    public class UploadedArtistResult
    {
        public SearchResultType Type { get; set; } = SearchResultType.Upload;
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string Title { get; set; }
        public string BrowseId { get; set; }


        private static readonly int IndexInRuns = 0;
        private static readonly int IndexInColumnsForTitle = 0;

        public UploadedArtistResult(Content3 content)
        {
            Thumbnails = SearchResult.GetThumbnails(content);
            BrowseId = content.MusicResponsiveListItemRenderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
        }
    }

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

        public UploadedAlbumResult(Content3 content)
        {
            Thumbnails = SearchResult.GetThumbnails(content);
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

        public VideoResult(Content3 content, int defaultIndex)
        {
            Thumbnails = SearchResult.GetThumbnails(content);
            VideoId = content.MusicResponsiveListItemRenderer.Overlay.MusicItemThumbnailOverlayRenderer.Content.MusicPlayButtonRenderer.PlayNavigationEndpoint.WatchEndpoint.VideoId;
            Params = content.MusicResponsiveListItemRenderer.Overlay.MusicItemThumbnailOverlayRenderer.Content.MusicPlayButtonRenderer.PlayNavigationEndpoint.WatchEndpoint.Params;

            Title = content.MusicResponsiveListItemRenderer.FlexColumns[IndexInColumnsForTitle].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            Artist = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForArtist].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            Views = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForViews].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
            Duration = content.MusicResponsiveListItemRenderer.FlexColumns[defaultIndex + IndexInColumnsForDuration].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[IndexInRuns].Text;
        }
    }


    public class IdNamePair
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IdNamePair(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Thumbnail
    { 
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }



}
