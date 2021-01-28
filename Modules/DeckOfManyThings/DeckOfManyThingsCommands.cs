using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace dungeons_and_discord.Modules.DeckOfManyThings
{
    public class DeckOfManyThingsCommands : ModuleBase
    {
        public DeckOfManyThingsHelper Helper = new DeckOfManyThingsHelper();


        [Command("drawcard")]
        public async Task DrawCardTask([Remainder] string args = "1")
        {
            if (args.Contains("help"))
            {
                await ReplyAsync(Helper.GetDrawCardCommandHelp());
            }
            else
            {
                IUser user = Context.User;
                bool useExpandedDeck = args.Contains("expanded");
                bool includeEffects = args.Contains("effects");
                int cardsToDraw;
                var exclusions = new List<string>();

                var splitArgs = args.Split(' ');

                if (!int.TryParse(splitArgs[0], out cardsToDraw))
                {
                    cardsToDraw = 1;
                }

                if (!args.Any(char.IsDigit))
                {
                    cardsToDraw = 1;
                }

                if (args.Contains("excludes"))
                {
                    exclusions = args.Substring(args.IndexOf(':') + 1).Split(',').ToList();
                }

                await ReplyAsync(Helper.DrawCardCommand(user, cardsToDraw, includeEffects, useExpandedDeck, exclusions));
            }

        }


        [Command("explaincard")]
        public async Task ExplainCardCommand([Remainder] string args = "")
        {
            if (args.Contains("help"))
            {
                await ReplyAsync(Helper.GetExplainCardCommandHelp());
            }
            else if (string.IsNullOrEmpty(args))
            {
                await ReplyAsync("No card name has been provided. Please include the name of the card to explain.");
            }
            else
            {
                await ReplyAsync(Helper.ExplainCardCommand(args));
            }            
        }

    }
}
