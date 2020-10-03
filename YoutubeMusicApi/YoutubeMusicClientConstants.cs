using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi
{
    public class YoutubeMusicClientConstants
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
                    internalExperimentFlags = new JArray(new JObject[]
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
    }
}
