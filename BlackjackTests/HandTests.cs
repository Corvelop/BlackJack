using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blackjack;
using System;
using System.Linq;

namespace Blackjack.Tests
{
    [TestClass()]
    public class HandTests
    {
        [TestMethod()]
        public void HandValuesStringTest()
        {
            Card card = new Card();
            card.Value = 1;

            Hand hand = new Hand();
            hand.Cards.Add(card);

            Assert.AreEqual(hand.HandValues().First(), 1);
            Assert.AreEqual(hand.HandValues().Last(), 11);
        }
    }
}