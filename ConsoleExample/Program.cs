using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YoutubeMusicApi;
using YoutubeMusicApi.Models;
using YoutubeMusicApi.Logging;
using YoutubeMusicApi.Models.Search;

namespace ConsoleExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start\n\n");

            YoutubeMusicClient api = new YoutubeMusicClient();
            api.LoginWithCookie(Secrets.COOKIE);

            var res = await api.GetLibraryPlaylists(10);
            Console.WriteLine(JsonConvert.SerializeObject(res));

            Console.WriteLine("\n\nDone");
            Console.ReadLine();
        }
    }
}
