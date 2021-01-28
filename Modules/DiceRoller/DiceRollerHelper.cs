using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dungeons_and_discord.Modules.DiceRoller
{
    class DiceRollerHelper
    {       
        public string RollDice(IUser user, List<string> arguments)
        {
            DiceRollerFunctions functions = new DiceRollerFunctions();
            StringBuilder message = new StringBuilder();
            int diceCount = 1;
            functions.MultiplierAsString = String.Empty;
            
            if (arguments.Count > 1)
            {
                diceCount = int.Parse(arguments[(int)DiceRollerFunctions.Arguments.numberOfDice]);
                var numberOfFaces = int.Parse(arguments[(int)DiceRollerFunctions.Arguments.numberOfFaces]);                

                functions.DiceCount = diceCount;                
                functions.NumberOfFaces = numberOfFaces;
            }
            else
            {
                functions.DiceCount = diceCount;
                functions.NumberOfFaces = int.Parse(arguments[(int)DiceRollerFunctions.Arguments.numberOfFaces]);
            }

            if (!functions.ValidateDiceFaceNumber())
            {
                message.AppendLine($"{user.Mention}, there is an invalid number of die faces. You may only roll a D4, D6, D8, D10, D12, D20 or D100.");
                return message.ToString();
            }

                        
            var result = functions.RollAllDice();
            var resultSum = result.Sum();

            if (arguments.Count == 3)
            {
                
                functions.MultiplierAsString = arguments[(int)DiceRollerFunctions.Arguments.multiplier];
                resultSum += functions.GetMultiplier();

                if (!argumentHasValidSymbol(functions.MultiplierAsString[0]))
                {
                    functions.MultiplierAsString = String.Empty;
                }

            }

            message.AppendLine($"*{user.Mention} rolled {diceCount} D{functions.NumberOfFaces} with a multiplier of {(string.IsNullOrEmpty(functions.MultiplierAsString) ? "0" : functions.MultiplierAsString)}*");
            message.AppendLine($"**{string.Join(", ", result)}**");
            message.AppendLine($"The sum of your rolled dice including multiplier is **{resultSum}**");

            return message.ToString();
        }

        public string RollWithAdvantage(IUser user, int multiplier)
        {
            DiceRollerFunctions functions = new DiceRollerFunctions();
            StringBuilder message = new StringBuilder();
            
            functions.DiceCount = 2;
            functions.NumberOfFaces = 20;

            List<int> rolledDice = functions.RollAllDice();
            int highestValue = functions.TakeHighestValue(rolledDice);            
           
            message.AppendLine($"{user.Mention} rolled with advantage.");
            message.AppendLine($"You rolled: **{string.Join(" and ", rolledDice)}** with a multiplier of **{multiplier}**");
            message.AppendLine($"The highest value including multiplier is: {highestValue + multiplier}");

            return message.ToString();
        }

        public string RollWithDisadvantage(IUser user, int multiplier)
        {
            DiceRollerFunctions functions = new DiceRollerFunctions();
            StringBuilder message = new StringBuilder();

            functions.DiceCount = 2;
            functions.NumberOfFaces = 20;

            List<int> rolledDice = functions.RollAllDice();
            int lowestValue = functions.TakeLowestValue(rolledDice);

            message.AppendLine($"{user.Mention} rolled with disadvantage.");
            message.AppendLine($"You rolled: **{string.Join(" and ", rolledDice)}** with a multiplier of **{multiplier}**");
            message.AppendLine($"The lowest value including multiplier is: {lowestValue + multiplier}");

            return message.ToString();
        }

        public string RollAbilityScores(IUser user)
        {
            DiceRollerFunctions functions = new DiceRollerFunctions();
            StringBuilder message = new StringBuilder();
            List<int> abilityScore;

            message.AppendLine($"**{user.Mention} Rolled Their Ability Scores!**");

            for (int i = 1; i <= 6; i++)
            {
                abilityScore = functions.RollAbilityScore();
                message.AppendLine($"**Roll {i}:** {string.Join(", ", abilityScore)} - Lowest Value: {abilityScore.Min()} - Score total: **{abilityScore.Sum() - abilityScore.Min()}**");
            }

            return message.ToString();
        }

        public string GetRollHelp()
        {
            var message = new StringBuilder();

            message.AppendLine(">>> __***Roll Command Help***__");
            message.AppendLine("*Roll a number of dice and return the sum of this roll plus any modifiers.");
            message.AppendLine("The first argument is number of die and faces on the die in the format of [Num]d[Num]*");
            message.AppendLine("***Example:*** *2d8*");
            message.AppendLine("*If no number is provided before the 'd', the number of die to roll will default to 1.");
            message.AppendLine("You can add your multiplier onto the result of the dice roll by adding +[Num] or -[Num] to the end of the command message.*");
            message.AppendLine("***Example:*** *2d8 +2*");
            message.AppendLine("***Note:*** *The number proceeding 'd' MUST represent a valid dice type (e.g: D4, D6, D8, D10, D12, D20 or D100).*");

            return message.ToString();
        }

        public string GetRollAdvantageHelp()
        {
            var message = new StringBuilder();

             message.AppendLine(">>> __***Roll With Advantage Command Help***__");
             message.AppendLine("*This command will roll 2 D20 die and return the value of both rolls and the highest roll value + a modifier (if specified).");
             message.AppendLine("You can provide a modifier to add on to the highest roll result by including +[Num] or -[Num] in your command message.");
             message.AppendLine("If no modifier is provided, the command will default to a modifier value of 0.*");
             message.AppendLine("***Example Commands:*** *!adv / !adv +2 / !adv -1*");

            return message.ToString();
        }

        public string GetRollDisadvantageHelp()
        {
            var message = new StringBuilder();

            message.AppendLine(">>> __***Roll With Disadvantage Command Help***__");
            message.AppendLine("*This command will roll 2 D20 die and return the value of both rolls and the lowest roll value + a modifier (if specified).");
            message.AppendLine("You can provide a modifier to add on to the lowest roll result by including +[Num] or -[Num] in your command message.");
            message.AppendLine("If no modifier is provided, the command will default to a modifier value of +0.*");
            message.AppendLine("***Example Commands:*** *!dis / !dis +2 / !dis -1*");

            return message.ToString();
        }

        public string GetRollAbilityScoresHelp()
        {
            var message = new StringBuilder();

            message.AppendLine(">>> __***Ability Score Command Help***__");
            message.AppendLine("*This command will roll 4 D6 die, sum the values of these die and then deducts the lowest dice roll from the result 6 times and return the values.");
            message.AppendLine("This command does not support any additional arguments.*");           
            
            return message.ToString();
        }

        private bool argumentHasValidSymbol(char symbol)
        {
            var allowedSymbols = new List<char> { '+', '-' };

            return allowedSymbols.Contains(symbol);
        }

    }
}
