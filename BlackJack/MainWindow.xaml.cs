using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Player Dealer;

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
            if(Player1.Bank < -50)
            {
                //take player out of game
            }
            if (Player2.Bank < -50)
            {

            }
            if (Player3.Bank < -50)
            {

            }
            if (Player4.Bank < -50)
            {

            }
            if (Player5.Bank < -50)
            {

            }
        }

        public void PayoutAfterRound(int playerNum)
        {
            Player player = new Player();
            if(playerNum == 1)
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
            else if(player.FinalHandAmount == Dealer.FinalHandAmount)
            {
                player.Bank = player.Bank + player.Bet;
            }
        }

        public void DrawCard(Player player)
        {
            Random rand = new Random();
            //int randNum = rand.Next(deck.Count);

            //player.Hand.Add(deck[randNum]);
            //deck.Remove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void CreateDeck()
        {
            List<String> Deck = Enum.GetNames(typeof(CardEnum)).ToList();
        }​
    }
}
