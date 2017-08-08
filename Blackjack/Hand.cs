using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Hand
    {
        public Hand()
        {
            this.Cards = new List<Card>();
        }

        public List<Card> Cards { get; set; }

        public bool HasBusted()
        {
            return HandValues().Count() == 0;
        }

        public List<int> HandValues()
        {
            List<int> possibleValues = new List<int>();
            int value = Cards.Where(x => !x.IsAce).ToList().Sum(x => x.Value);

            possibleValues.Add(value);

            foreach (Card card in Cards.Where(x => x.IsAce))
            {
                int first = value + 1;
                int last = value + 11;
                possibleValues = new List<int>()
                {
                    first,
                    last
                };
            }

            return possibleValues.Where(x => x <= 21).ToList();
        }

        public string HandValuesString()
        {
            var handValues = HandValues();
            if (handValues.Count == 2)
            {
                return $"{handValues.First().ToString()}/{handValues.Last().ToString()}";
            }
            else
            {
                return handValues.First().ToString();
            }

        }
    }
}
