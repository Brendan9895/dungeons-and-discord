using System.Threading.Tasks;
using dungeons_and_discord.Services;

namespace dungeons_and_discord
{       
    class Program
    {        
        /* Keep This File Super Simple. (This Method Requires C# 7.2 or Higher!) */
        private static Task Main()
           => new DiscordService().InitializeAsync();        
    }
}