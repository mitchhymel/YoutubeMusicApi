using System;
using System.Threading.Tasks;
using YoutubeMusicApi;

namespace ConsoleExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start");

            YoutubeMusicClient api = new YoutubeMusicClient();
            api.LoginWithCookie(Secrets.COOKIE);

            var res = await api.GetLikedPlaylists();


            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
