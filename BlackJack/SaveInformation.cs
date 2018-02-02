using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    [Serializable]
    public class SaveInformation
    {
        public Player Player1;
        public Player Player2;
        public Player Player3;
        public Player Player4;
        public Player Player5;
        public Player Dealer;
        public List<String> Deck;

        public SaveInformation()
        {

        }

        public SaveInformation(Player player1, Player player2, Player player3, Player player4, Player player5, Player dealer, List<string> deck)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            this.Player3 = player3;
            this.Player4 = player4;
            this.Player5 = player5;
            this.Dealer = dealer;
            this.Deck = deck;
        }
    }

}
