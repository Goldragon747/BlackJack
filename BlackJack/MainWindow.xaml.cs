using Microsoft.Win32;
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
                Blackjack_StackPanel_Player_1.IsEnabled = false;
                Player1Playing = false;
            }
            if (Player2.Bank < -50)
            {
                Blackjack_StackPanel_Player_2.IsEnabled = false;
                Player2Playing = false;
            }
            if (Player3.Bank < -50)
            {
                Blackjack_StackPanel_Player_3.IsEnabled = false;
                Player3Playing = false;
            }
            if (Player4.Bank < -50)
            {
                Blackjack_StackPanel_Player_4.IsEnabled = false;
                Player4Playing = false;
            }
            if (Player5.Bank < -50)
            {
                Blackjack_StackPanel_Player_5.IsEnabled = false;
                Player5Playing = false;
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

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void PlayerBetButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            int PlayerNumber = (int)b.Name[24];
            string BidResult = b.Name[30].ToString();

            switch (PlayerNumber)
            {
                case 1:
                    switch (BidResult)
                    {
                        case "1":
                            if(b.Name[31] == 0)
                            {
                                Player1.Bet = 10;
                                Player1.Bank -= 10;
                            }
                            else
                            {
                                Player1.Bet = 1;
                                Player1.Bank -= 1;
                            }
                            break;
                        case "5":
                            Player1.Bet = 5;
                            Player1.Bank -= 5;
                            break;
                        case "o":
                            Player1Playing = false;
                            break;
                    }
                    Blackjack_StackPanel_Player_1.IsEnabled = false;
                    Blackjack_StackPanel_Player_2.IsEnabled = false;
                    break;
                case 2:
                    switch (BidResult)
                    {
                        case "1":
                            if (b.Name[31] == 0)
                            {
                                Player2.Bet = 10;
                                Player2.Bank -= 10;
                            }
                            else
                            {
                                Player2.Bet = 1;
                                Player2.Bank -= 1;
                            }
                            break;
                        case "5":
                            Player2.Bet = 5;
                            Player2.Bank -= 5;
                            break;
                        case "o":
                            Player2Playing = false;
                            break;
                    }
                    Blackjack_StackPanel_Player_2.IsEnabled = false;
                    Blackjack_StackPanel_Player_3.IsEnabled = false;
                    break;
                case 3:
                    switch (BidResult)
                    {
                        case "1":
                            if (b.Name[31] == 0)
                            {
                                Player3.Bet = 10;
                                Player3.Bank -= 10;
                            }
                            else
                            {
                                Player3.Bet = 1;
                                Player3.Bank -= 1;
                            }
                            break;
                        case "5":
                            Player3.Bet = 5;
                            Player3.Bank -= 5;
                            break;
                        case "o":
                            Player3Playing = false;
                            break;
                    }
                    Blackjack_StackPanel_Player_3.IsEnabled = false;
                    Blackjack_StackPanel_Player_4.IsEnabled = false;
                    break;
                case 4:
                    switch (BidResult)
                    {
                        case "1":
                            if (b.Name[31] == 0)
                            {
                                Player4.Bet = 10;
                                Player4.Bank -= 10;
                            }
                            else
                            {
                                Player4.Bet = 1;
                                Player4.Bank -= 1;
                            }
                            break;
                        case "5":
                            Player4.Bet = 5;
                            Player4.Bank -= 5;
                            break;
                        case "o":
                            Player4Playing = false;
                            break;
                    }
                    Blackjack_StackPanel_Player_4.IsEnabled = false;
                    Blackjack_StackPanel_Player_5.IsEnabled = false;
                    break;
                case 5:
                    switch (BidResult)
                    {
                        case "1":
                            if (b.Name[31] == 0)
                            {
                                Player5.Bet = 10;
                                Player5.Bank -= 10;
                            }
                            else
                            {
                                Player5.Bet = 1;
                                Player5.Bank -= 1;
                            }
                            break;
                        case "5":
                            Player5.Bet = 5;
                            Player5.Bank -= 5;
                            break;
                        case "o":
                            Player5Playing = false;
                            break;
                    }
                    Blackjack_StackPanel_Player_5.IsEnabled = false;
                    //start round
                    break;
            }
        }
        public void CreateDeck()
        {
            List<String> Deck = Enum.GetNames(typeof(CardEnum)).ToList();
        }
/// <summary>
        /// Basic skeleton for saving the game. NOT DONE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "cargam files (*.cargam)|*.cargam";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true)
            {
            }
        }
        /// <summary>
        /// Basic skeleton for loading the game. NOT DONE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "cargam files (*.cargam)|*.cargam";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
            }        }

    }
}
