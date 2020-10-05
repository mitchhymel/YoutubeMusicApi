using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models
{
    public class Artist
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("views")]
        public string Views { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("subscribers")]
        public string Subscribers { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }

        [JsonProperty("songs")]
        public Songs Songs { get; set; }

        [JsonProperty("albums")]
        public ReleaseList Albums { get; set; }

        [JsonProperty("singles")]
        public ReleaseList Singles { get; set; }

        [JsonProperty("videos")]
        public Videos Videos { get; set; }

        [JsonProperty("playlists")]
        public ArtistPlaylists Playlists { get; set; }

        [JsonProperty("relatedArtists")]
        public RelatedArtists RelatedArtists { get; set; }


        public static Artist FromBrowseResponse(BrowseResponse response)
        {
            Artist artist = new Artist();

            var header = response.Header.MusicImmersiveHeaderRenderer;
            artist.Name = header.Title.Runs[0].Text;
            artist.Thumbnails = header.Thumbnail.MusicThumbnailRenderer.Thumbnail.Thumbnails;

            // TODO:
            //artist.Description = ;
            //artist.Views = ;

            var subscribeButton = header.SubscriptionButton.SubscribeButtonRenderer;
            artist.ChannelId = subscribeButton.ChannelId;
            artist.Subscribers = subscribeButton.SubscriberCountText.Runs[0].Text;
            artist.Subscribed = subscribeButton.Subscribed;

            var contents = response.Contents.SingleColumnBrowseResultsRenderer.Tabs[0].TabRenderer.Content.SectionListRenderer.Contents;

            // first item will be songs... if the api returns any
            int indexToStart = 1;
            if (contents[0].MusicShelfRenderer != null)
            {
                artist.Songs = Songs.FromMusicShelfRenderer(contents[0].MusicShelfRenderer);
            }
            else
            {
                // if there are no songs, then the rest of our information will be moved up
                // one index
                indexToStart = 0;
            }

            for (int i = indexToStart; i < contents.Count; i++)
            {
                var content = contents[i];
                string type = content.MusicCarouselShelfRenderer.Header.MusicCarouselShelfBasicHeaderRenderer.Title.Runs[0].Text;
                switch (type)
                {
                    case "Albums":
                        artist.Albums = ReleaseList.FromMusicCarouselShelfRenderer(contents[indexToStart].MusicCarouselShelfRenderer);
                        break;
                    case "Singles":
                        artist.Singles = ReleaseList.FromMusicCarouselShelfRenderer(contents[indexToStart + 1].MusicCarouselShelfRenderer);
                        break;
                    case "Videos":
                        artist.Videos = Videos.FromMusicCarouselShelfRenderer(contents[indexToStart + 2].MusicCarouselShelfRenderer);
                        break;
                    case "Featured on":
                        artist.Playlists = ArtistPlaylists.FromMusicCarouselShelfRenderer(contents[indexToStart + 3].MusicCarouselShelfRenderer);
                        break;
                    case "Fans might also like":
                        artist.RelatedArtists = RelatedArtists.FromMusicCarouselShelfRenderer(contents[indexToStart + 4].MusicCarouselShelfRenderer);
                        break;
                    default:
                        throw new Exception($"Unsupported content type found {type}");
                }
            }

            return artist;
        }
    }

    public class ArtistSongsResult
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("artists")]
        public List<IdNamePair> Artists { get; set; } = new List<IdNamePair>();

        [JsonProperty("album")]
        public IdNamePair Album { get; set; }

        [JsonProperty("likeStatus")]
        public LikeStatus LikeStatus { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }

        public static ArtistSongsResult FromMusicResponsiveListItemRenderer(MusicResponsiveListItemRenderer renderer)
        {
            ArtistSongsResult song = new ArtistSongsResult();

            song.Thumbnails = renderer.Thumbnail.MusicThumbnailRenderer.Thumbnail.Thumbnails;

            var trackRuns = renderer.FlexColumns[0].MusicResponsiveListItemFlexColumnRenderer.Text.Runs[0];
            song.Title = trackRuns.Text;
            song.VideoId = trackRuns.NavigationEndpoint.WatchEndpoint.VideoId;

            var artistRuns = renderer.FlexColumns[1].MusicResponsiveListItemFlexColumnRenderer.Text.Runs;
            foreach (var run in artistRuns)
            {
                if (run.NavigationEndpoint != null && run.Text != ", ")
                {
                    song.Artists.Add(new IdNamePair(run.NavigationEndpoint.BrowseEndpoint.BrowseId, run.Text));
                }
            }

            var albumRuns = renderer.FlexColumns[2].MusicResponsiveListItemFlexColumnRenderer.Text.Runs;
            song.Album = new IdNamePair(albumRuns[0].NavigationEndpoint.BrowseEndpoint.BrowseId, albumRuns[0].Text);

            song.LikeStatus = (LikeStatus) Enum.Parse(typeof(LikeStatus), renderer.Menu.MenuRenderer.TopLevelButtons[0].LikeButtonRenderer.LikeStatus);

            return song;
        }
    }

    public class Songs
    {
        [JsonProperty("browseId")]
        public string BrowseId { get; set; }

        [JsonProperty("results")]
        public List<ArtistSongsResult> Results { get; set; } = new List<ArtistSongsResult>();

        public static Songs FromMusicShelfRenderer(MusicShelfRenderer renderer)
        {
            Songs songs = new Songs();

            var songsRuns = renderer.Title.Runs[0];
            if (songsRuns.NavigationEndpoint != null)
            {
                songs.BrowseId = songsRuns.NavigationEndpoint.BrowseEndpoint.BrowseId;
                // do we need params?
                // songs.Parameters = songsRuns.NavigationEndpoint.BrowseEndpoint.Params;
            }

            foreach (var content in renderer.Contents)
            {
                songs.Results.Add(ArtistSongsResult.FromMusicResponsiveListItemRenderer(content.MusicResponsiveListItemRenderer));
            }

            return songs;
        }
    }

    public class Release
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("browseId")]
        public string BrowseId { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }

        public static Release FromMusicTwoRowItemRenderer(MusicTwoRowItemRenderer renderer)
        {
            Release release = new Release();


            if (renderer.NavigationEndpoint != null)
            {
                release.BrowseId = renderer.NavigationEndpoint.BrowseEndpoint.BrowseId;
            }
            else
            {
                release.BrowseId = renderer.Title.Runs[0].NavigationEndpoint.BrowseEndpoint.BrowseId;
            }

            release.Thumbnails = renderer.ThumbnailRenderer.MusicThumbnailRenderer.Thumbnail.Thumbnails;

            release.Title = renderer.Title.Runs[0].Text;

            if (renderer.Subtitle != null && renderer.Subtitle.Runs.Count >= 3)
            {
                release.Year = renderer.Subtitle.Runs[2].Text;
            }

            return release;
        }
    }

    public class ReleaseList
    {
        [JsonProperty("browseId")]
        public string BrowseId { get; set; }

        [JsonProperty("results")]
        public List<Release> Results { get; set; } = new List<Release>();

        [JsonProperty("params")]
        public string Params { get; set; }

        public static ReleaseList FromMusicCarouselShelfRenderer(MusicCarouselShelfRenderer renderer)
        {
            ReleaseList releases = new ReleaseList();

            var browseIdRuns = renderer.Header.MusicCarouselShelfBasicHeaderRenderer.Title.Runs;
            if (browseIdRuns[0].NavigationEndpoint != null)
            {
                releases.BrowseId = browseIdRuns[0].NavigationEndpoint.BrowseEndpoint.BrowseId;
                releases.Params = browseIdRuns[0].NavigationEndpoint.BrowseEndpoint.Params;
            }

            foreach (var content in renderer.Contents)
            {
                releases.Results.Add(Release.FromMusicTwoRowItemRenderer(content.MusicTwoRowItemRenderer));
            }


            return releases;
        }
    }

    public class ArtistVideoResult
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }

        [JsonProperty("views")]
        public string Views { get; set; }

        public static ArtistVideoResult FromMusicTwoRowItemRenderer(MusicTwoRowItemRenderer renderer)
        {
            ArtistVideoResult video = new ArtistVideoResult();

            video.Title = renderer.Title.Runs[0].Text;

            video.VideoId = renderer.NavigationEndpoint.WatchEndpoint.VideoId;
            video.PlaylistId = renderer.NavigationEndpoint.WatchEndpoint.PlaylistId;

            if (renderer.Subtitle != null && renderer.Subtitle.Runs.Count >= 3)
            {
                video.Views = renderer.Subtitle.Runs[2].Text;
            }

            video.Thumbnails = renderer.ThumbnailRenderer.MusicThumbnailRenderer.Thumbnail.Thumbnails;

            return video;
        }

    }

    public class Videos
    {
        [JsonProperty("browseId")]
        public string BrowseId { get; set; }

        [JsonProperty("results")]
        public List<ArtistVideoResult> Results { get; set; } = new List<ArtistVideoResult>();

        [JsonProperty("params")]
        public string Params { get; set; }

        public static Videos FromMusicCarouselShelfRenderer(MusicCarouselShelfRenderer renderer)
        {
            Videos videos = new Videos();

            var browseIdRuns = renderer.Header.MusicCarouselShelfBasicHeaderRenderer.Title.Runs;
            if (browseIdRuns[0].NavigationEndpoint != null)
            {
                videos.BrowseId = browseIdRuns[0].NavigationEndpoint.BrowseEndpoint.BrowseId;
                videos.Params = browseIdRuns[0].NavigationEndpoint.BrowseEndpoint.Params;
            }

            foreach (var content in renderer.Contents)
            {
                videos.Results.Add(ArtistVideoResult.FromMusicTwoRowItemRenderer(content.MusicTwoRowItemRenderer));
            }

            return videos;
        }
    }

    public class RelatedArtist
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("subscribers")]
        public string Subscribers { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }

        public static RelatedArtist FromMusicTwoRowItemRenderer(MusicTwoRowItemRenderer renderer)
        {
            RelatedArtist artist = new RelatedArtist();

            artist.Thumbnails = renderer.ThumbnailRenderer.MusicThumbnailRenderer.Thumbnail.Thumbnails;

            if (renderer.Subtitle != null)
            {
                artist.Subscribers = renderer.Subtitle.Runs[0].Text;
            }

            artist.Name = renderer.Title.Runs[0].Text;
            artist.ChannelId = renderer.Title.Runs[0].NavigationEndpoint.BrowseEndpoint.BrowseId;

            return artist;
        }

    }


    public class RelatedArtists
    {
        [JsonProperty("artists")]
        public List<RelatedArtist> Artists { get; set; }  = new List<RelatedArtist>();

        public static RelatedArtists FromMusicCarouselShelfRenderer(MusicCarouselShelfRenderer renderer)
        {
            RelatedArtists relatedArtists = new RelatedArtists();

            foreach (var content in renderer.Contents)
            {
                relatedArtists.Artists.Add(RelatedArtist.FromMusicTwoRowItemRenderer(content.MusicTwoRowItemRenderer));
            }
            
            return relatedArtists;
        }
    }

    public class ArtistPlaylist
    {
        public List<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
        public string BrowseId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }


        public static ArtistPlaylist FromMusicTwoRowItemRenderer(MusicTwoRowItemRenderer renderer)
        {
            ArtistPlaylist playlist = new ArtistPlaylist();

            playlist.Thumbnails = renderer.ThumbnailRenderer.MusicThumbnailRenderer.Thumbnail.Thumbnails;

            playlist.Title = renderer.Title.Runs[0].Text;

            playlist.BrowseId = renderer.Title.Runs[0].NavigationEndpoint.BrowseEndpoint.BrowseId;

            if (renderer.Subtitle != null && renderer.Subtitle.Runs.Count >=3 )
            {
                playlist.Author = renderer.Subtitle.Runs[2].Text;
            }

            return playlist;
        }
    }

    public class ArtistPlaylists
    {
        [JsonProperty("playlists")]
        public List<ArtistPlaylist> Playlists { get; set; } = new List<ArtistPlaylist>();

        public static ArtistPlaylists FromMusicCarouselShelfRenderer(MusicCarouselShelfRenderer renderer)
        {
            ArtistPlaylists playlists = new ArtistPlaylists();

            foreach (var content in renderer.Contents)
            {
                playlists.Playlists.Add(ArtistPlaylist.FromMusicTwoRowItemRenderer(content.MusicTwoRowItemRenderer));
            }

            return playlists;
        }
    }


}
