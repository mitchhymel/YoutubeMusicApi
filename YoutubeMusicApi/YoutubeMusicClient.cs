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
using YoutubeMusicApi.Models;

namespace YoutubeMusicApi
{
    public class YoutubeMusicClient
    {
        public static string BaseUrl = "https://music.youtube.com/youtubei/v1";
        public static string Params = "?alt=json&key=AIzaSyC9XL3ZjWddXya6X74dJoCTL-WEYFDNX30";

        public static JObject DefaultBody = JObject.FromObject(new
        {
            context = new
            {
                capabilities = new { },
                client = new
                {
                    clientName = "WEB_REMIX",
                    clientVersion = "0.1",
                    experimentIds = new List<string>(),
                    experimentsToken = "",
                    gl = "DE",
                    hl = "en",
                    locationInfo = new
                    {
                        locationPermissionAuthorizationStatus = "LOCATION_PERMISSION_AUTHORIZATION_STATUS_UNSUPPORTED"
                    },
                    musicAppInfo = new
                    {
                        musicActivityMasterSwitch = "MUSIC_ACTIVITY_MASTER_SWITCH_INDETERMINATE",
                        musicLocationMasterSwitch = "MUSIC_LOCATION_MASTER_SWITCH_INDETERMINATE",
                        pwaInstallabilityStatus = "PWA_INSTALLABILITY_STATUS_UNKNOWN"
                    },
                    utcOffsetMinutes = 60
                },
                request = new
                {
                    internalExperimentFlags = new JArray( new JObject[]
                    {
                        JObject.FromObject(new
                        {
                            key = "force_music_enable_outertube_tastebuilder_browse",
                            value = "true"
                        }),
                        JObject.FromObject(new
                        {
                            key = "force_music_enable_outertube_playlist_detail_browse",
                            value = "true"
                        }),
                        JObject.FromObject(new
                        {
                            key = "force_music_enable_outertube_search_suggestions",
                            value = "true"
                        }),
                    }),
                    sessionIndex = new { }
                },
                user = new
                {
                    enableSafetyMode = false
                }
            }
        });

        public AuthHeaders AuthHeaders;

        public YoutubeMusicClient()
        {
            // init to default values w/o a cookie
            AuthHeaders = new AuthHeaders();
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

        #region Artist
        public async Task<JObject> GetArtist(string id)
        {
            string url = GetYTMUrl("browse");

            var data = JObject.FromObject(new
            {
                browseEndpointContextSupportedConfigs = new 
                {
                    browseEndpointContextMusicConfig = new
                    {
                        pageType = "MUSIC_PAGE_TYPE_ARTIST"
                    }
                },
                browseId = id
            });

            var resp = await Post<JObject>(url, data);
            return resp;
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

            return await AuthedPost<JObject>(url, data);
        }
        #endregion

        #region Search

        public async Task<JObject> Search(string search)
        {
            string url = GetYTMUrl("search");

            JObject data = JObject.FromObject(new 
            { 
                query = search
            });

            return await Post<JObject>(url, data);
        }
        #endregion

        #region Requests

        private async Task<T> Get<T>(string url, NameValueCollection additionalParams = null)
        {
            HttpClient client = GetHttpClient();

            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        private async Task<T> Post<T>(string url, JObject data, NameValueCollection additionalParams = null, bool authRequired = false)
        {
            HttpClient client = GetHttpClient(authRequired: authRequired);

            data.Merge(DefaultBody);
            string requestBody = data.ToString();
            HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        private async Task<T> AuthedPost<T>(string url, JObject data, NameValueCollection additionalParams = null)
        {
            if (!IsAuthed())
            {
                throw new Exception("Trying to make a request that requires authentication while not authenticated");
            }

            return await Post<T>(url, data, additionalParams, authRequired: true);
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
            return $"{BaseUrl}/{endpoint}{Params}";
        }

        #endregion

    }
}
