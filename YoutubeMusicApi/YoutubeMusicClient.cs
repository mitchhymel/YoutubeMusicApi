using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using YoutubeMusicApi.Logging;
using YoutubeMusicApi.Models;
using YoutubeMusicApi.Models.Search;

namespace YoutubeMusicApi
{
    public class YoutubeMusicClient
    {

        public AuthHeaders AuthHeaders { get; private set; }
        public ILogger Logger { get; set; }

        public YoutubeMusicClient(ILogger logger = null)
        {
            // init to default values w/o a cookie
            AuthHeaders = new AuthHeaders();
            Logger = logger;
        }

        #region Authentication

        public async Task<bool> LoginWithAuthJsonFile(string filePath)
        {
            using (StreamReader reader = File.OpenText(filePath))
            {
                string contents = await reader.ReadToEndAsync();
                return  LoginWithAuthHeaderString(contents);
            }
        }

        public bool LoginWithAuthHeaderString(string headersJson)
        {
            AuthHeaders = JsonConvert.DeserializeObject<AuthHeaders>(headersJson);

            return !string.IsNullOrEmpty(AuthHeaders.Cookie);
        }

        public bool LoginWithCookie(string cookie)
        {
            AuthHeaders = new AuthHeaders
            {
                Cookie = cookie
            };

            return true;
        }

        public bool IsAuthed()
        {
            return AuthHeaders != null && !string.IsNullOrEmpty(AuthHeaders.Cookie);
        }

        #endregion

        #region Search

        public async Task<SearchResult> Search(string search, SearchResultType filter = SearchResultType.All)
        {
            if (filter == SearchResultType.Upload)
            {
                throw new Exception("To search uploads, use SearchUploads() method. Uploads will not be returned as a result from Search()");
            }

            string url = GetYTMUrl("search");

            JObject data = JObject.FromObject(new
            {
                query = search
            });

            if (filter != SearchResultType.All)
            {
                string parameters = GetSearchParamStringFromFilter(filter);
                data.Add("params", parameters);
            }

            GeneratedSearchResult result = await Post<GeneratedSearchResult>(url, data);

            SearchResult results = SearchResult.ParseResultListFromGenerated(result, filter);

            return results;
        }

        /// <summary>
        /// Search uploaded songs, artists, albums
        /// 
        /// The YTM api does not return results with mixes of uploads and non uploaded tracks, so you
        /// have to search for uploads specifically
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<SearchResult> SearchUploads(string search)
        {
            string url = GetYTMUrl("search");

            JObject data = JObject.FromObject(new
            {
                query = search
            });

            string parameters = GetSearchParamStringFromFilter(SearchResultType.Upload);
            data.Add("params", parameters);

            GeneratedSearchResult result = await Post<GeneratedSearchResult>(url, data, authRequired: true);

            SearchResult results = SearchResult.ParseResultListFromGenerated(result, SearchResultType.Upload);

            return results;
        }

        private string GetSearchParamStringFromFilter(SearchResultType filter)
        {
            string param1 = "Eg-KAQwIA";
            string param3 = "MABqChAEEAMQCRAFEAo%3D";
            string parameters = "";

            if (filter == SearchResultType.Upload)
            {
                parameters = "agIYAw%3D%3D";
            }
            else
            {
                string param2 = "";
                switch (filter)
                {
                    case SearchResultType.Album:
                        param2 = "BAAGAEgACgA";
                        break;
                    case SearchResultType.Artist:
                        param2 = "BAAGAAgASgA";
                        break;
                    case SearchResultType.Playlist:
                        param2 = "BAAGAAgACgB";
                        break;
                    case SearchResultType.Song:
                        param2 = "RAAGAAgACgA";
                        break;
                    case SearchResultType.Video:
                        param2 = "BABGAAgACgA";
                        break;
                    case SearchResultType.Upload:
                        param2 = "RABGAEgASgB"; // not sure if this is right, uploads should never get here due to above if clause, but the python code has this
                        break;
                    default:
                        throw new Exception($"Unsupported search filter type: {filter}");
                }

                parameters = param1 + param2 + param3;
            }

            return parameters;
        }
        #endregion

        #region Browsing

        public async Task<JObject> GetArtist(string id)
        {
            string url = GetYTMUrl("browse");

            var data = PrepareBrowse("ARTIST", id);

            return await Post<JObject>(url, data);
        }

        public async Task<JObject> GetArtistAlbums(string channelId, string parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetUser(string channelId)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetUserPlaylists(string channelId, string parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetAlbum(string browseId)
        {
            throw new NotImplementedException();
        }

        public async Task<Song> GetSong(string videoId)
        {
            string url = $"https://www.youtube.com/get_video_info?video_id={videoId}&el=detailpage&hl=en";

            var result = await GetFromQueryStringEndpoint(url);

            return Song.FromNameValueCollection(result);
        }

        #endregion

        #region Library

        public async Task<JObject> GetLibraryPlaylists(int limit = 25)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetLibrarySongs(int limit = 25)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetLibraryAlbums(int limit = 25)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetLibrarySubscriptions(int limit = 25)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetLikedSongs(int limit = 100)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetHistory()
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> RemoveHistoryItems(List<string> feedbackTokens)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> RateSong(string videoId, string rating)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> RatePlaylist(string playlistId, string rating)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> SubscribeArtists(List<string> channelIds)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> UnsubscribeArtists(List<string> channelIds)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Playlists

        public async Task<JObject> GetLikedPlaylists()
        {
            string url = GetYTMUrl("browse");
            var data = JObject.FromObject(new
            {
                browseId = "FEmusic_liked_playlists"
            });

            return await Post<JObject>(url, data, authRequired: true);
        }

        public async Task<JObject> GetPlaylist(string id)
        {
            string url = GetYTMUrl("browse");
            var data = PrepareBrowse("PLAYLIST", id);
            return await Post<JObject>(url, data);
        }

        public async Task<JObject> CreatePlaylist(string title, string description, string privacyStatus, List<string> videoIds = null, string sourcePlaylist = null)
        {
            string url = GetYTMUrl("playlist/create");
            var data = JObject.FromObject(new
            {
                title = title,
                description = description,
                privacyStatus = privacyStatus,
                // videoIds = videoIds,
                // sourcePlaylist = sourcePlaylist
            });
            return await Post<JObject>(url, data, authRequired: true);
        }

        public async Task<JObject> EditPlaylist(string playlistId, string title = null, string description = null, string privacyStatus = null, Tuple<string, string> moveItem = null, string addPlaylistId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> DeletePlaylist(string playlistId)
        {
            string url = GetYTMUrl("playlist/delete");
            var data = JObject.FromObject(new
            {
                playlistId = playlistId
            });
            return await Post<JObject>(url, data, authRequired: true);
        }

        public async Task<JObject> AddPlaylistItems(string playlistId, List<string> videoIds, string sourcePlaylist=null, bool duplicates=false)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> RemovePlaylistItems(string playlistId,  List<object> videos)
        {
            throw new NotImplementedException();
            //string url = GetYTMUrl("browse/edit_playlist");

            //List<JObject> actionsList = new List<JObject>();

            //var data = JObject.FromObject(new
            //{

            //});
            //return await Post<JObject>(url, data, authRequired: true);
        }

        #endregion

        #region Uploads
        public async Task<JObject> GetUploadedSongs(int limit=25)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetUploadedAlbums(int limit=25)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetUploadedArtists(int limit=25)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetUploadedArtist(string artistBrowseId)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> GetUploadedAlbum(string albumBrowseId)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> UploadSong(string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> DeleteUpload(string uploadId)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Requests

        private async Task<NameValueCollection> GetFromQueryStringEndpoint(string url, bool authRequired = false)
        {
            if (authRequired && !IsAuthed())
            {
                throw new Exception("Tried to make a GET request that requires auth but was not authed");
            }

            HttpClient client = GetHttpClient(authRequired: authRequired);

            Log($"GET: {url}");
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            Log($"\tRESPONSE: {responseString}");
            return HttpUtility.ParseQueryString(responseString);
        }

        private async Task<T> Get<T>(string url, bool authRequired = false)
        {
            HttpClient client = GetHttpClient(authRequired: authRequired);

            Log($"GET: {url}");
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            Log($"\tRESPONSE: {responseString}");
            T result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        private async Task<T> Post<T>(string url, JObject data,  bool authRequired = false)
        {
            if (authRequired && !IsAuthed())
            {
                throw new Exception("Tried to make a POST request that requires auth but was not authed");
            }

            HttpClient client = GetHttpClient(authRequired: authRequired);

            data.Merge(YoutubeMusicClientConstants.DefaultBody);
            string requestBody = data.ToString();
            HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

            Log($"POST: {url}");
            Log($"\tBODY: {requestBody}");
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            Log($"\tRESPONSE: {responseString}");
            T result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        private HttpClient GetHttpClient(bool authRequired = false)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", AuthHeaders.UserAgent);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(AuthHeaders.Accept));
            client.DefaultRequestHeaders.Add("Accept-Language", AuthHeaders.AcceptLanguage);
            client.DefaultRequestHeaders.Add("X-Goog-AuthUser", AuthHeaders.GoogAuthUser);
            client.DefaultRequestHeaders.Add("x-origin", AuthHeaders.Origin);
            client.DefaultRequestHeaders.Add("Cookie", AuthHeaders.Cookie);

            if (authRequired)
            {
                string auth = GetAuthorization(AuthHeaders.Cookie, AuthHeaders.Origin);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("SAPISIDHASH", auth);
            }

            return client;
        }

        #endregion

        #region Utils

        private string GetAuthorization(string cookie, string origin)
        {
            string sapisid = GetSapsidFromCookie(cookie);
            string timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string authToEncode = $"{timestamp} {sapisid} {origin}";
            byte[] bytes = Encoding.UTF8.GetBytes(authToEncode);
            var sha = SHA1.Create();
            byte[] digest = sha.ComputeHash(bytes);
            string decoded = String.Concat(Array.ConvertAll(digest, x => x.ToString("X2").ToLower()));
            string auth = $"{timestamp}_{decoded}";
            return auth;
        }

        private string GetSapsidFromCookie(string cookie)
        {
            string pattern = "SAPISID=(?<sapisid>[^;]+);";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(cookie);
            string sapisid = match.Groups["sapisid"].Value;
            return sapisid;
        }

        private string GetYTMUrl(string endpoint)
        {
            return $"{YoutubeMusicClientConstants.BaseUrl}/{endpoint}{YoutubeMusicClientConstants.Params}";
        }

        private JObject PrepareBrowse(string endpoint, string id)
        {
            return JObject.FromObject(new
            {
                browseEndpointContextSupportedConfigs = new
                {
                    browseEndpointContextMusicConfig = new
                    {
                        pageType = $"MUSIC_PAGE_TYPE_{endpoint}"
                    }
                },
                browseId = id
            });
        }

        private void Log(string str)
        {
            if (Logger != null)
            {
                Logger.Log(str);
            }
        }

        #endregion

    }
}
