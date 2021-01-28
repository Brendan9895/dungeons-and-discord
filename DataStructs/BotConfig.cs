using System.Collections.Generic;

namespace dungeons_and_discord.DataStructs
{
    public class BotConfig
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public string GameStatus { get; set; }
        public List<ulong> BlacklistedChannels { get; set; }
    }
}