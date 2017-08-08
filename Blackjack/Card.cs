using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Card
    {
        public Card()
        {
            Value = new Random().Next(1, 11);
            CheckCardValue();
        }

        private int _value;

        public int Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                CheckCardValue();
            }
        }

        private void CheckCardValue()
        {
            if (_value == 1 || _value == 11)
            {
                IsAce = true;
            }
        }

        public bool IsAce { get; set; }

    }
}
