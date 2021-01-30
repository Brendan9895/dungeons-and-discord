using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net;
using System.IO;

namespace dungeons_and_discord.Modules.DiceRoller
{
    public class DiceRollerCommand : ModuleBase
    {
        readonly DiceRollerHelper Helper = new DiceRollerHelper();
        
        [Command("roll")]
        public async Task RollDiceCommand([Remainder] string args)
        {  
            if (args.ToLower().Contains("help"))
            {
                await ReplyAsync(Helper.GetRollHelp());
            }
            else
            {
                IUser user = Context.User;
                 var arguments = ValidateRollCommandArguments(args);

                if (!arguments.Item2)
                {
                    await ReplyAsync($"{user.Mention} *There was an issue with your command. Make sure your arguments are in the correct format*");
                }
                else
                {
                    await ReplyAsync(Helper.RollDice(user, arguments.Item1));
                }
            }
        }

        [Command("adv")]
        public async Task RollWithAdvantageCommand([Remainder] string args = "+0")
        {            
            if (args.ToLower().Contains("help"))
            {
                await ReplyAsync(Helper.GetRollAdvantageHelp());
            }
            else
            {
                IUser user = Context.User;

                if (int.TryParse(args, out int multiplier))
                {
                    multiplier = 0;
                }

                await ReplyAsync(Helper.RollWithAdvantage(user, multiplier));
            }
        }

        [Command("dis")]
        public async Task RollWithDisadvantageCommand([Remainder] string args = "+0")
        {
            if (args.ToLower().Contains("help"))
            {
                await ReplyAsync(Helper.GetRollDisadvantageHelp());
            }
            else
            {
                IUser user = Context.User;

                if(!int.TryParse(args, out int multiplier))
                {
                    multiplier = 0;
                }

                await ReplyAsync(Helper.RollWithDisadvantage(user, multiplier));
            }
        }

        [Command("rollabilityscore")]
        public async Task RollStatsCommand([Remainder] string args = "")
        {
            if (args.ToLower().Contains("help"))
            {
                await ReplyAsync(Helper.GetRollAbilityScoresHelp());
            }
            else
            {
                IUser user = Context.User;

                await ReplyAsync(Helper.RollAbilityScores(user));
            }
        }

        // argument handling
        private (List<string>, bool) ValidateRollCommandArguments(string args)
        {
            var argumentDelimiters = new List<char> { 'd', ' ' };
            bool allArgumentsValid = true;                      

            if (args.StartsWith('d'))
            {
                args = $"1{args}";
            }

            List<string> arguments = args.Split(argumentDelimiters.ToArray()).ToList();
            arguments.Remove(string.Empty);

            if (!arguments[0].All(char.IsDigit))
            {
                allArgumentsValid = false;
            }

            if (!arguments[1].All(char.IsDigit))
            {
                allArgumentsValid = false;
            }

            if (arguments.Count > 2)
            {
                if (!int.TryParse(arguments[2], out int checkMultiplier))
                {
                    allArgumentsValid = false;
                }
            }

            return (arguments, allArgumentsValid);
        }
    }
}
