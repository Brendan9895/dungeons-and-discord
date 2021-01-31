using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace dungeons_and_discord.Modules.DeckOfManyThings
{
    public class DeckOfManyThingsFunctions
    {
        public int CardsToDraw { get; set; } = 1;
        public int CardId { get; set; }
        public string CardName { get; set; } = string.Empty;
        public string Description { get; set; }
        public bool UseExpandedDeck { get; set; }
        public List<string> CardsToExclude { get; set; } = new List<string>();
        

        public KeyValuePair<string, string> GetCardDefinitionById()
        {
            StreamReader deckOfManyThings = new StreamReader("deckOfManyThings.json");            
            string json = deckOfManyThings.ReadToEnd();
            List<Card> Cards = JsonConvert.DeserializeObject<List<Card>>(json);

            var cardDefinition = KeyValuePair
                .Create(
                    Cards.Where(c => c.Id == CardId).Select(c => c.CardName).FirstOrDefault() ?? "No card found", 
                    Cards.Where(c => c.Id == CardId).Select(c => c.Definition).FirstOrDefault() ?? "No card description found"
                 );

            return cardDefinition;
        }

        public KeyValuePair<string, string> GetCardDefinitionByName()
        {
            StreamReader deckOfManyThings = new StreamReader("deckOfManyThings.json");
            string json = deckOfManyThings.ReadToEnd();
            List<Card> Cards = JsonConvert.DeserializeObject<List<Card>>(json);

            var cardDefinition = KeyValuePair
                .Create(
                    Cards.Where(c => c.CardName.ToLower() == CardName.ToLower()).Select(c => c.CardName).FirstOrDefault() ?? "No card found",
                    Cards.Where(c => c.CardName.ToLower() == CardName.ToLower()).Select(c => c.Definition).FirstOrDefault() ?? "No card description found"
                 );

            return cardDefinition;
        }

        public int CardNumberGenerator()
        {
            var result = new Random();
            var cardMinimum = 1;
            var cardMaximum = UseExpandedDeck ? 23 : 15;

            return result.Next(cardMinimum, cardMaximum);
        }

        public bool EnoughCardsAvailable()
        {
            int maxDrawAmount;

            if (UseExpandedDeck)
            {
                maxDrawAmount = 23 - CardsToExclude.Count;
            }
            else
            {
                maxDrawAmount = 15 - CardsToExclude.Count;
            }
            
            return CardsToDraw < maxDrawAmount;
        }

        private class Card
        {
            public int Id;
            public string CardName;
            public string Definition;
        }
    }
}
