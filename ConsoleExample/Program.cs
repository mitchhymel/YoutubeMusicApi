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
            Console.WriteLine("Start");

            YoutubeMusicClient api = new YoutubeMusicClient();
            api.LoginWithCookie(Secrets.COOKIE);

            var res = await api.Search("The New Age", SearchResultType.Upload);
            Console.WriteLine(JsonConvert.SerializeObject(res));

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
