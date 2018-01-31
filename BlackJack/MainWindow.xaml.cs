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
        public Player Player1 = new Player();
        public Player Player2 = new Player();
        public Player Player3 = new Player();
        public Player Player4 = new Player();
        public Player Player5 = new Player();
        public Player Dealer;
        public List<String> Deck = Enum.GetNames(typeof(CardEnum)).ToList();

        public MainWindow()
        {
            InitializeComponent();
            BindToPlayers();
            StartBettingPhase();
        }

        public void BindToPlayers()
        {
            List<Player> players = new List<Player>()
            {
                Player1,
                Player2,
                Player3,
                Player4,
                Player5
            };
            List<StackPanel> playerDisplay = new List<StackPanel>()
            {
                Blackjack_StackPanel_Player_1,
                Blackjack_StackPanel_Player_2,
                Blackjack_StackPanel_Player_3,
                Blackjack_StackPanel_Player_4,
                Blackjack_StackPanel_Player_5
            };
            //change 5 to Slider.Value later, once created
            for (int i = 0; i < 5; i++)
            {
                Binding b = new Binding("Player");
                b.Mode = BindingMode.OneWay;
                playerDisplay[i].DataContext = players[i];
            }
        }

        public void StartBettingPhase()
        {
            Blackjack_Hand_Player_1.Visibility = Visibility.Collapsed;
            Blackjack_Hand_Player_2.Visibility = Visibility.Collapsed;
            Blackjack_Hand_Player_3.Visibility = Visibility.Collapsed;
            Blackjack_Hand_Player_4.Visibility = Visibility.Collapsed;
            Blackjack_Hand_Player_5.Visibility = Visibility.Collapsed;
            Blackjack_StackPanel_Bids_1.Visibility = Visibility.Visible;
            Blackjack_StackPanel_Bids_2.Visibility = Visibility.Visible;
            Blackjack_StackPanel_Bids_3.Visibility = Visibility.Visible;
            Blackjack_StackPanel_Bids_4.Visibility = Visibility.Visible;
            Blackjack_StackPanel_Bids_5.Visibility = Visibility.Visible;
            Blackjack_StackPanel_Player_1.IsEnabled = true;
            Blackjack_StackPanel_Player_2.IsEnabled = false;
            Blackjack_StackPanel_Player_3.IsEnabled = false;
            Blackjack_StackPanel_Player_4.IsEnabled = false;
            Blackjack_StackPanel_Player_5.IsEnabled = false;
        }

        public void CheckIfBankrupt()
        {
            if (Player1.Bank < -50)
            {
                Blackjack_StackPanel_Player_1.IsEnabled = false;
                Player1.Playing = false;
            }
            if (Player2.Bank < -50)
            {
                Blackjack_StackPanel_Player_2.IsEnabled = false;
                Player2.Playing = false;
            }
            if (Player3.Bank < -50)
            {
                Blackjack_StackPanel_Player_3.IsEnabled = false;
                Player3.Playing = false;
            }
            if (Player4.Bank < -50)
            {
                Blackjack_StackPanel_Player_4.IsEnabled = false;
                Player4.Playing = false;
            }
            if (Player5.Bank < -50)
            {
                Blackjack_StackPanel_Player_5.IsEnabled = false;
                Player5.Playing = false;
            }
        }

        public void PayoutAfterRound(Player player)
        {
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

        /// <summary>
        /// Gets the image of the card in the player's hand at the index passed in.
        /// Gets the CardBack image if isFirstCard is true
        /// </summary>
        /// <param name="player">Player whose hand you wish to show</param>
        /// <param name="index">The index of the particular card you wish to show</param>
        /// <param name="isFirstCard">True for the first card, false for any card after</param>
        /// <returns>An image brush with the image of the card</returns>
        public ImageBrush ShowCard(Player player, int index, bool isFirstCard)
        {
            ImageBrush image = new ImageBrush();
            if(!isFirstCard)
            {
                image.ImageSource = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png"));
            }
            else
            {
                image.ImageSource = new BitmapImage(new Uri("../Images/CardBack.png"));
            }
            return image;
        }
        private void Title_Screen_Click_Blackjack(object sender, RoutedEventArgs e)
        {
            Title_Screen.Visibility = Visibility.Collapsed;
            //options screen
            Blackjack_Game_Screen.Visibility = Visibility.Visible;
        }

        public void PlayerBetButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            int PlayerNumber = Convert.ToInt32(b.Name[24] + "");
            string BidResult = b.Name[30].ToString(); 

            switch (PlayerNumber)
            {
                case 1:
                    switch (BidResult)
                    {
                        case "1":
                            if(b.Name.Count() == 32)
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
                            Player1.Playing = false;
                            break;
                    }
                    Blackjack_StackPanel_Player_1.IsEnabled = false;
                    if(!Player2.Playing)
                    {
                        if(!Player3.Playing)
                        {
                            if(!Player4.Playing)
                            {
                                if(!Player5.Playing)
                                {
                                    //start game
                                }
                                else
                                {
                                    Blackjack_StackPanel_Player_5.IsEnabled = true;
                                }
                            }
                            else
                            {
                                Blackjack_StackPanel_Player_4.IsEnabled = true;
                            }
                        }
                        else
                        {
                            Blackjack_StackPanel_Player_3.IsEnabled = true;
                        }
                    }
                    else
                    {
                        Blackjack_StackPanel_Player_2.IsEnabled = true;
                    }
                    break;
                case 2:
                    switch (BidResult)
                    {
                        case "1":
                            if (b.Name.Count() == 32)
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
                            Player2.Playing = false;
                            break;
                    }
                    Blackjack_StackPanel_Player_2.IsEnabled = false;
                    if (!Player3.Playing)
                    {
                        if (!Player4.Playing)
                        {
                            if (!Player5.Playing)
                            {
                                //start game
                            }
                            else
                            {
                                Blackjack_StackPanel_Player_5.IsEnabled = true;
                            }
                        }
                        else
                        {
                            Blackjack_StackPanel_Player_4.IsEnabled = true;
                        }
                    }
                    else
                    {
                        Blackjack_StackPanel_Player_3.IsEnabled = true;
                    }
                    break;
                case 3:
                    switch (BidResult)
                    {
                        case "1":
                            if (b.Name.Count() == 32)
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
                            Player3.Playing = false;
                            break;
                    }
                    Blackjack_StackPanel_Player_3.IsEnabled = false;
                    if (!Player4.Playing)
                    {
                        if (!Player5.Playing)
                        {
                            //start game
                        }
                        else
                        {
                            Blackjack_StackPanel_Player_5.IsEnabled = true;
                        }
                    }
                    else
                    {
                        Blackjack_StackPanel_Player_4.IsEnabled = true;
                    }
                    break;
                case 4:
                    switch (BidResult)
                    {
                        case "1":
                            if (b.Name.Count() == 32)
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
                            Player4.Playing = false;
                            break;
                    }
                    Blackjack_StackPanel_Player_4.IsEnabled = false;
                    if (!Player5.Playing)
                    {
                        //start game
                    }
                    else
                    {
                        Blackjack_StackPanel_Player_5.IsEnabled = true;
                    }
                    break;
                case 5:
                    switch (BidResult)
                    {
                        case "1":
                            if (b.Name.Count() == 32)
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
                            Player5.Playing = false;
                            break;
                    }
                    Blackjack_StackPanel_Player_5.IsEnabled = false;
                    ChangeBidVisibilites();
                    //start round
                    break;
            }
        }
        public void ChangeBidVisibilites()
        {
            Blackjack_StackPanel_Bids_1.Visibility = Visibility.Collapsed;
            Blackjack_StackPanel_Bids_2.Visibility = Visibility.Collapsed;
            Blackjack_StackPanel_Bids_3.Visibility = Visibility.Collapsed;
            Blackjack_StackPanel_Bids_4.Visibility = Visibility.Collapsed;
            Blackjack_StackPanel_Bids_5.Visibility = Visibility.Collapsed;

            Blackjack_Hand_Player_1.Visibility = Visibility.Visible;
            Blackjack_Hand_Split_Player_1.Visibility = Visibility.Visible;

            Blackjack_Hand_Player_2.Visibility = Visibility.Visible;
            Blackjack_Hand_Split_Player_2.Visibility = Visibility.Visible;

            Blackjack_Hand_Player_3.Visibility = Visibility.Visible;
            Blackjack_Hand_Split_Player_3.Visibility = Visibility.Visible;

            Blackjack_Hand_Player_4.Visibility = Visibility.Visible;
            Blackjack_Hand_Split_Player_4.Visibility = Visibility.Visible;

            Blackjack_Hand_Player_5.Visibility = Visibility.Visible;
            Blackjack_Hand_Split_Player_5.Visibility = Visibility.Visible;
        }
        public void ShuffleDeck()
        {
            Random rand = new Random();

            for(int i = Deck.Count() - 1; i > 0; --i)
            {
                int k = rand.Next(i + 1);
                CardEnum temp = (CardEnum)Enum.Parse(typeof(CardEnum), Deck[i]);
                Deck[i] = Deck[k];
                Deck[k] = temp.ToString();
            }
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
            }
        }

    }
}
