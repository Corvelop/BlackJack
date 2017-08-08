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
            var character = Console.ReadKey().KeyChar.ToString().ToLower();
            bool isPlaying = character == "y";
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

                GameInfo(playersHand.HandValuesString(), dealersHand.Cards.First().Value.ToString());

                Console.WriteLine("Would you like to hit?\nY/N");

                hit = Console.ReadKey().KeyChar.ToString().ToLower() == "y";

                Console.Clear();
                if (hit)
                {
                    playersHand.Cards.Add(new Card());
                    busted = playersHand.HasBusted();
                    if (busted)
                    {
                        GameInfo(playersHand.HandValuesString(), dealersHand.HandValuesString());
                        GameOver = true;
                    }
                }
                else
                {
                    Console.WriteLine($"Dealer flip a {dealersHand.Cards.Last().Value} and has a total of {dealersHand.HandValuesString()}");
                    do
                    {
                        PressAnyKeyToContinue();

                        if (dealersHand.HandValues().First() <= 16)
                        {
                            dealersHand.Cards.Add(new Card());
                            GameInfo(playersHand.HandValuesString(), dealersHand.HandValuesString());
                            Console.WriteLine($"Dealer Hits and get a  {dealersHand.HandValuesString()}");
                        }

                    } while (dealersHand.HandValues().First() <= 16); // TO FIX.

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

        private static void GameInfo(string playersHand, string dealersHand)
        {
            Console.WriteLine($"Your hand:{playersHand}   Dealer hand:{dealersHand}");
        }

        private static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
