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
        public int CardId { get; set; }
        public string CardName { get; set; }
        public string Description { get; set; }
        public bool UseExpandedDeck { get; set; }
        

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
                    Cards.Where(c => c.CardName.ToLower() == CardName).Select(c => c.CardName).FirstOrDefault(),
                    Cards.Where(c => c.CardName.ToLower() == CardName).Select(c => c.Definition).FirstOrDefault()
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

        private class Card
        {
            public int Id;
            public string CardName;
            public string Definition;
        }
    }
}
