using System;
using System.Collections.Generic;
using System.Text;
using YoutubeMusicApi.Models.Generated;

namespace YoutubeMusicApi.Models
{
    public class PlaylistList
    {
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();

        public string Continuation { get; set; }

        public static PlaylistList FromBrowseResponse(BrowseResponse response)
        {
            PlaylistList list = new PlaylistList();

            List<Content> contents;
            if (response.ContinuationContents != null)
            {
                contents = response.ContinuationContents.GridContinuation.Items;

                if (response.ContinuationContents.GridContinuation.Continuations != null)
                {
                    list.Continuation = response.ContinuationContents.GridContinuation.Continuations[0].NextContinuationData.Continuation;
                }
            }
            else
            {
                var renderer = response.Contents.SingleColumnBrowseResultsRenderer.Tabs[0].TabRenderer.Content.SectionListRenderer.Contents[1].ItemSectionRenderer.Contents[0].GridRenderer;

                contents = renderer.Items;
                list.Continuation = renderer.Continuations[0].NextContinuationData.Continuation;
            }

            foreach (var item in contents)
            {
                // the first item in the list might not be a playlist, but the "createplaylist"
                // ... not sure what this is, but we want to skip it
                if (item.MusicTwoRowItemRenderer.NavigationEndpoint.CreatePlaylistEndpoint == null)
                {
                    list.Playlists.Add(Playlist.FromMusicTwoRowItemRenderer(item.MusicTwoRowItemRenderer));
                }
            }

            return list;
        }
    }
}
