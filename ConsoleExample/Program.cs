using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YoutubeMusicApi;
using YoutubeMusicApi.Models;

namespace ConsoleExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start");

            YoutubeMusicClient api = new YoutubeMusicClient();
            api.LoginWithCookie(Secrets.COOKIE);

            var res = await api.Search("Neverkept", filter: SearchResultType.Album);
            Console.WriteLine(JsonConvert.SerializeObject(res));

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
