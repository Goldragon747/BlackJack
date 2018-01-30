using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Player Player1;
        public Player Player2;
        public Player Player3;
        public Player Player4;
        public Player Player5;
        public bool Player1Playing;
        public bool Player2Playing;
        public bool Player3Playing;
        public bool Player4Playing;
        public bool Player5Playing;
        public Player Dealer;
        public List<String> Deck = Enum.GetNames(typeof(CardEnum)).ToList();

        public MainWindow()
        {
            InitializeComponent();
        }

        //Creates each player, which sets their bank to 20, their hand to a new list of CardEnum, and bet to 0 (to be changed at beginning of round)
        public void PlayerCreation()
        {
            Player1 = new Player();
            Player2 = new Player();
            Player3 = new Player();
            Player4 = new Player();
            Player5 = new Player();
        }

        public void CheckIfBankrupt()
        {
            if (Player1.Bank < -50)
            {
                Blackjack_Grid_Player_1.IsEnabled = false;
            }
            if (Player2.Bank < -50)
            {
                Blackjack_Grid_Player_2.IsEnabled = false;
            }
            if (Player3.Bank < -50)
            {
                Blackjack_Grid_Player_3.IsEnabled = false;
            }
            if (Player4.Bank < -50)
            {
                Blackjack_Grid_Player_4.IsEnabled = false;
            }
            if (Player5.Bank < -50)
            {
                Blackjack_Grid_Player_5.IsEnabled = false;
            }
        }

        public void PayoutAfterRound(int playerNum)
        {
            Player player = new Player();
            if (playerNum == 1)
            {
                player = Player1;
            }
            else if (playerNum == 2)
            {
                player = Player2;
            }
            else if (playerNum == 3)
            {
                player = Player3;
            }
            else if (playerNum == 4)
            {
                player = Player4;
            }
            else if (playerNum == 5)
            {
                player = Player5;
            }

            if (player.Hand.Count() == 5)
            {
                player.Bank = player.Bank + (player.Bet * 4);
            }
            else if (player.FinalHandAmount > Dealer.FinalHandAmount && player.FinalHandAmount != 21)
            {
                player.Bank = player.Bank + (player.Bet * 2);
            }
            else if (player.FinalHandAmount > Dealer.FinalHandAmount && player.FinalHandAmount == 21)
            {
                player.Bank = player.Bank + (player.Bet * 3);
            }
            else if (player.FinalHandAmount == Dealer.FinalHandAmount)
            {
                player.Bank = player.Bank + player.Bet;
            }
        }

        public void DrawCard(Player player)
        {
            player.Hand.Add((CardEnum)Enum.Parse(typeof(CardEnum), Deck[0]));
            Deck.RemoveAt(0);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void PlayerBetButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Regex Player1Check = new Regex(@"Player_1");
            Regex Player2Check = new Regex(@"Player_2");
            Regex Player3Check = new Regex(@"Player_3");
            Regex Player4Check = new Regex(@"Player_4");
            Regex Player5Check = new Regex(@"Player_5");
            Regex Bid1Check = new Regex(@"Bid_1");
            Regex Bid5Check = new Regex(@"Bid_5");
            Regex Bid10Check = new Regex(@"Bid_10");
            Regex BidOutCheck = new Regex(@"Bid_out");

            if(BidOutCheck.IsMatch(b.Name))
            {
                if (Player1Check.IsMatch(b.Name))
                {
                    Blackjack_Grid_Player_1.IsEnabled = false;
                    Player1Playing = false;
                    if (Player2Playing)
                    {
                        Blackjack_Grid_Player_2.IsEnabled = true;
                    }
                }
                else if (Player2Check.IsMatch(b.Name))
                {
                    Blackjack_Grid_Player_2.IsEnabled = false;
                    Player2Playing = false;
                    if (Player3Playing)
                    {
                        Blackjack_Grid_Player_3.IsEnabled = true;
                    }
                }
                else if (Player3Check.IsMatch(b.Name))
                {
                    Blackjack_Grid_Player_3.IsEnabled = false;
                    Player3Playing = false;
                    if (Player4Playing)
                    {
                        Blackjack_Grid_Player_4.IsEnabled = true;
                    }
                }
                else if (Player4Check.IsMatch(b.Name))
                {
                    Blackjack_Grid_Player_4.IsEnabled = false;
                    Player4Playing = false;
                    if (Player5Playing)
                    {
                        Blackjack_Grid_Player_5.IsEnabled = true;
                    }
                }
                else if (Player5Check.IsMatch(b.Name))
                {
                    Blackjack_Grid_Player_5.IsEnabled = false;
                    Player5Playing = false;
                    //start round
                }
            }
        }
        public void ShuffleDeck()
        {
            Random rand = new Random();

            for (int i = Deck.Count() - 1; i > 0; --i)
            {
                int k = rand.Next(i + 1);
                CardEnum card = (CardEnum)Enum.Parse(typeof(CardEnum),Deck[i]);
                Deck[i] = Deck[k];
                Deck[k] = card.ToString();
            }
        }

        /// <summary>
        /// Checks to see if Dealer's hand total is  greater then or equal to 17
        /// This helps determine if Dealer needs to Hit or Bust
        /// </summary>
        /// <param name="handtotal"></param>
        /// <returns>True or false depending if Dealer can draw more cards or not</returns>
        public bool DealerHit(int handtotal)
        {
            if (handtotal < 17)
            {
                return true;
            }
            else
            {
                
                return false;
            }
        }
        /// <summary>
        /// I'll come back to write this
        /// </summary>
        public void DealerDraw()
        {

        }
    }
}
