using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Player
    {
        public Player()
        {
            Name = "Player";
            Bank = 20;
            Bet = 0;
            Hand = new List<CardEnum>();
            FinalHandAmount = 0;
            Playing = true;
        }

        public string Name { get; set; }

        public int Bank { get; set; }

        public int Bet { get; set; }

        public List<CardEnum> Hand { get; set; }

        public int FinalHandAmount { get; set; }

        public bool Playing { get; set; }
    }
}
