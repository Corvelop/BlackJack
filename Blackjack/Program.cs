using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            int playerScore = 0, dealerScore = 0;
            bool isPlaying, playerWin;

            isPlaying = WantsToPlay();
            
            while (isPlaying)
            {
                playerWin = PlayHand();
                if(playerWin)
                {
                    playerScore++;
                }
                else
                {
                    dealerScore++;
                }

                isPlaying = WantsToPlay();

            }
            
        }

        private static bool WantsToPlay()
        {
            Console.WriteLine("Would you like to play BlackJack \nY/N");
            bool isPlaying = Console.ReadLine().ToString().Trim().ToLower() == "y";
            Console.Clear();

            return isPlaying;
        }

        private static bool PlayHand()
        {
            bool busted = false, beatDealer = false, hit = false, GameOver = false, playerWin = false;
            Hand playersHand = new Hand(), dealersHand = new Hand();

            playersHand.Cards = new List<Card>() { new Card(), new Card() };
            dealersHand.Cards = new List<Card>() { new Card(), new Card() };

            do
            {

                if (playersHand.HasBusted())
                {
                    Console.WriteLine("Looks like you have busted");
                    busted = playersHand.HasBusted();
                    break;
                }

                Console.WriteLine($"you have a hand of {playersHand.HandValuesString()} and the dealer has {dealersHand.Cards.First().Value} showing.\n would you like to hit or stay?\nY/N");
                hit = Console.ReadLine().ToString().Trim().ToLower() == "y";

                if (hit)
                {
                    playersHand.Cards.Add(new Card());
                    busted = playersHand.HasBusted();
                    if(busted)
                    {
                        GameOver = true;
                    }
                  
                }
                else
                {
                    Console.WriteLine($"Dealer flip a {dealersHand.Cards.Last().Value} and has a total of {dealersHand.HandValuesString()}");
                    do
                    {
                      
                        if (dealersHand.HandValues().First() <= 16)
                        {
                            dealersHand.Cards.Add(new Card());
                        }

                        Console.WriteLine($"Dealer Hits and get a  {dealersHand.HandValuesString()}");
                    } while (dealersHand.HandValues().First() <= 16 ); // TO FIX.

                    beatDealer = (dealersHand.HasBusted() || dealersHand.HandValues().Last() >= playersHand.HandValues().Last());
                    if (beatDealer)
                    {
                        GameOver = true;
                    }
                }

            } while (!GameOver);

            if(busted)
            {
                Console.WriteLine("You Lose! you have BUSTED!");
            }
            else if(dealersHand.HandValues().Last() >= playersHand.HandValues().Last())
            {
                Console.WriteLine("You Lose! Dealer has a higher Hand!");
            }
            else
            {
                playerWin = true;
                Console.WriteLine("You Win!");
            }

            return playerWin;
        }
    }

    public class Card
    {
        public Card()
        {
            Value = new Random().Next(1, 11);
            if (Value == 1 || Value == 11 )
            {
                IsAce = true;
            }
        }

        public int Value { get; set; }
        public bool IsAce { get; set; }
    }

    public class Hand
    {
       
     
        public List<Card> Cards { get; set; }

        public bool HasBusted ()
        {
             return HandValues().Count() == 0;
        }

        public List<int> HandValues()
        {
            List<int> possibleValues = new List<int>();
            int value = Cards.Where(x => !x.IsAce).ToList().Sum(x=>x.Value);
              
            possibleValues.Add(value);

            foreach(Card card in Cards.Where(x=> x.IsAce))
            {

                possibleValues = new List<int>()
                {
                    value++,
                    value+11
                };

                value = value++;
            }

            return possibleValues.Where(x => x <= 21).ToList();
        }

        public string HandValuesString()
        {
            var handValues = HandValues();
            if (handValues.Count == 2)
            {
                return $"{handValues.First().ToString()} or  {handValues.Last().ToString()}";
            }
            else
            {
                return handValues.First().ToString();
            }

        }
    }
}
