using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dungeons_and_discord.Modules.DeckOfManyThings
{
    public class DeckOfManyThingsHelper
    {
        public string DrawCardCommand(IUser user, 
                                      int cardsToDraw, 
                                      bool includeEffects, 
                                      bool useExpandedDeck,
                                      List<string> exclusions)
        {
            DeckOfManyThingsFunctions functions = new DeckOfManyThingsFunctions();
            var message = new StringBuilder();           

            functions.UseExpandedDeck = useExpandedDeck;            

            message.AppendLine($"**{user.Mention} has drawn {cardsToDraw} from the Deck of Many Things...**");
            for(int i = 1; i <= cardsToDraw; i++)
            {
                if (includeEffects)
                {
                    
                    functions.CardId = functions.CardNumberGenerator();
                    var drawnCard = functions.GetCardDefinitionById();
                    while (exclusions.Contains(drawnCard.Key))
                    {
                        functions.CardId = functions.CardNumberGenerator();
                        drawnCard = functions.GetCardDefinitionById();
                    }                    
                     message.AppendLine($"They have drawn... **{drawnCard.Key}**");
                     message.AppendLine($"*{drawnCard.Value}*");
                     exclusions.Add(drawnCard.Key);
                    
                }
                else
                {
                    functions.CardId = functions.CardNumberGenerator();
                    var drawnCard = functions.GetCardDefinitionById();
                    while (exclusions.Contains(drawnCard.Key))
                    {
                        functions.CardId = functions.CardNumberGenerator();
                        drawnCard = functions.GetCardDefinitionById();
                    }
                    message.AppendLine($"They have drawn... **{drawnCard.Key}**");
                    exclusions.Add(drawnCard.Key);
                }
            }

            return message.ToString();
        }

        public string ExplainCardCommand(string args)
        {
            DeckOfManyThingsFunctions functions = new DeckOfManyThingsFunctions();
            var message = new StringBuilder();

            functions.CardName = args;
            KeyValuePair<string, string>? definition = functions.GetCardDefinitionByName();

            if (string.IsNullOrEmpty(definition.Value.Key))
            {
                return $"There is no card called **\"{args}\"**. Please check you have typed the name correctly and try again.";
            }

            message.AppendLine($"__***{definition.Value.Key}***__");
            message.AppendLine($"*{definition.Value.Value}*");

            return message.ToString();
        }

        public string GetDrawCardCommandHelp()
        {
            var message = new StringBuilder();

            message.AppendLine(">>> __***Deck of Many Things Help***__");
            message.AppendLine("*Draw a card from the deck of Many Things.");
            message.AppendLine("You may use the !drawcard command with no arguments to draw one card from the standard deck (13 cards).");
            message.AppendLine("You may draw multiple cards by putting a number between 1 and 9 after the !drawcard command.*");
            message.AppendLine();
            message.AppendLine(" __***Argument keywords***__");
            message.AppendLine("*The !drawcard command accepts certain keywords as additional arguments. These are as follows:");
            message.AppendLine("expanded - Use the expanded deck (22 cards).");
            message.AppendLine("effects - Include a description of the effects of a drawn card.");
            message.AppendLine("excludes: - Allows you to pass in a comma separated list of card names. The cards specified in this list will not be drawn.");
            message.AppendLine("When using exclusions, the list of exclusions must be the last argument in your message.*");

            return message.ToString();            
        }

        public string GetExplainCardCommandHelp()
        {
            var message = new StringBuilder();

            message.AppendLine(">>> __***Explain Card Help***__");
            message.AppendLine("*Returns a description of the effect of a requested card name");
            message.AppendLine("The card name requested must match a card in either the standard or expanded Deck.");
            message.AppendLine("This command requires an argument.*");

            return message.ToString();
        }
    }
}
