using System;
using System.Collections.Generic;
using System.Linq;

namespace dungeons_and_discord.Modules.DiceRoller
{
    public class DiceRollerFunctions
    {
        public int DiceCount { get; set; } = 1;
        public int NumberOfFaces { get; set; }
        public int Multiplier { get; set; }
        public string MultiplierAsString { get; set; } = "0";

        public List<int> RollAllDice()
        {          
            
            List<int> results = new List<int>();
            for(int d = 1; d <= DiceCount; d++)
            {
                results.Add(RolledNumber());
            }

            return results;
        }

        public int TakeHighestValue(List<int> diceRolls)
        {
            return diceRolls.Max();
        }

        public int TakeLowestValue(List<int> diceRolls)
        {
            return diceRolls.Min();
        }

        public int RolledNumber()
        {
            var result = new Random();
            var diceMinimum = 1;
            return result.Next(diceMinimum, NumberOfFaces + 1);
        }

        public bool ValidateDiceFaceNumber()
        {
            var validDice = new List<int> { 
                (int)ValidDice.D4,
                (int)ValidDice.D6,
                (int)ValidDice.D8,
                (int)ValidDice.D10,
                (int)ValidDice.D12,
                (int)ValidDice.D20,
                (int)ValidDice.D100
            };

            if (!validDice.Contains(NumberOfFaces))
            {
                return false;
            }

            return true;
        }

        public int GetMultiplier()
        {
            if(!int.TryParse(MultiplierAsString, out int multiplier))
            {
                multiplier = 0;
            }

            return multiplier;
        }

        public List<int> RollAbilityScore()
        {
            DiceCount = 4;
            NumberOfFaces = 6;

            var score = RollAllDice();      
            
            return score;
        }

        public enum ValidDice
        {
            D4 = 4,
            D6 = 6,
            D8 = 8,
            D10 = 10,
            D12 = 12,
            D20 = 20,
            D100 = 100
        }

        public enum Arguments
        {
            numberOfDice = 0,
            numberOfFaces,
            multiplier
        }
    }
}
