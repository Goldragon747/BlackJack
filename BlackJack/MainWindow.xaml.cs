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
        public Player Dealer = new Player();
        public List<String> Deck = Enum.GetNames(typeof(CardEnum)).ToList();

        public MainWindow()
        {
            InitializeComponent();
            BindToPlayers();
            StartBettingPhase();
            ShuffleDeck();
        }

        /// <summary>
        /// Binds each players display stack panel to the respective player class.
        /// </summary>
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

        /// <summary>
        /// Starts the betting phase of play, clearing any cards, and player 1 can bet
        /// </summary>
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

        /// <summary>
        /// Checks if any player has less that -$50, and takes them out if so
        /// </summary>
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

        /// <summary>
        /// Pays each player based on their hand compared to dealer, and their bet
        /// </summary>
        /// <param name="player">The player being compaered to dealer and paid</param>
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

        public void InitialDraw()
        {
            List<Player> players = new List<Player>()
            {
                Dealer,
                Player1,
                Player2,
                Player3,
                Player4,
                Player5
            };
            //Change i to be less than slider value + 1 later
            for(int i = 0; i < players.Count(); i++)
            {
                DrawCard(players[i]);
                DrawCard(players[i]);
                ShowCard(players[i], 0, true);
                ShowCard(players[i], 1, false);
            }            
        }
        /// <summary>
        /// Determines and sets the value of the player hand by passing the player object, sifting through the hand, and adding the values to the hand
        /// </summary>
        /// <param name="p"></param>
        public void DetermineHandValue(Player p)
        {
            int AcesDrawnCount = 0;
            int handValue = 0;
            for (int i = 0; i < p.Hand.Count; i++)
            {
                if (p.Hand[i].ToString().Contains("King") || p.Hand[i].ToString().Contains("Queen")
                    || p.Hand[i].ToString().Contains("Jack") || p.Hand[i].ToString().Contains("Ten"))
                {
                    handValue += 10;
                }
                else if (p.Hand[i].ToString().Contains("Ace"))
                {
                    handValue += 11;
                    AcesDrawnCount++;

                }
                else if (p.Hand[i].ToString().Contains("Two"))
                {
                    handValue += 2;
                }
                else if (p.Hand[i].ToString().Contains("Three"))
                {
                    handValue += 3;
                }
                else if (p.Hand[i].ToString().Contains("Four"))
                {
                    handValue += 4;
                }
                else if (p.Hand[i].ToString().Contains("Five"))
                {
                    handValue += 5;
                }
                else if (p.Hand[i].ToString().Contains("Six"))
                {
                    handValue += 6;
                }
                else if (p.Hand[i].ToString().Contains("Seven"))
                {
                    handValue += 7;
                }
                else if (p.Hand[i].ToString().Contains("Eight"))
                {
                    handValue += 8;
                }
                else if (p.Hand[i].ToString().Contains("Nine"))
                {
                    handValue += 9;
                }
                else
                {
                    //Hand is empty
                }
            }

            for (int i = 0; i < AcesDrawnCount; i++)
            {
                if (handValue > 21)
                {
                    handValue -= 10;
                }
            }
            p.FinalHandAmount = handValue;
        }
  
        public void DealerTurn()
        {

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

        public void HitButton_Click(object sender, RoutedEventArgs e)
        {
            List<StackPanel> stackPanels = new List<StackPanel>()
            {
                Blackjack_StackPanel_Player_1,
                Blackjack_StackPanel_Player_2,
                Blackjack_StackPanel_Player_3,
                Blackjack_StackPanel_Player_4,
                Blackjack_StackPanel_Player_5
            };
            List<UserControl> userControls = new List<UserControl>()
            {
                Blackjack_Hand_Player_1,
                Blackjack_Hand_Player_2,
                Blackjack_Hand_Player_3,
                Blackjack_Hand_Player_4,
                Blackjack_Hand_Player_5
            };
            List<Player> players = new List<Player>()
            {
                Player1,
                Player2,
                Player3,
                Player4,
                Player5
            };
            //replace 5 with how many players are playing
            for(int i=0; i<5; i++)
            {
                if (stackPanels[i].IsEnabled && userControls[i].Visibility == Visibility.Visible && userControls[i].IsEnabled)
                {
                    DrawCard(players[i]);
                    ShowCard(players[i], players[i].Hand.Count() - 1, false);
                }
            }
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
            saveFileDialog.Filter = "blackjack files (*.blackjack)|*.blackjack";
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
            openFileDialog.Filter = "blackjack files (*.blackjack)|*.blackjack";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
            }
        }

        private void Blackjack_Button_Instructions_Click(object sender, RoutedEventArgs e)
        {
            Blackjack_Game_Screen.Visibility = Visibility.Collapsed;
            Blackjack_Instructions_Screen.Visibility = Visibility.Visible;
            Blackjack_Label_Instructions.Content = " Blackjack uses one standard deck of 52 cards wheere the object is to get as close to 21 without going over. \n " +
                "Blackjack is started by players placing their bet of $1, $5, or $10. After Players are given 2 cards to start. \n" +
                "Cards are delt face down. After the cards are delt Players decide how to play their hand \n" +
                "Cards 2-10 are worth face value while King, Queen, and Jack are worth 10. Ace's are worth either 1 or 11.\n " +
                "Hit - Take another card until you are as close to 21 as possible. If you exceed 21 you 'Bust' \n" +
                "Stay - elect to draw no cards, you do this if you have faith your total will beat the dealer \n" +
                "Split - if you have two cards of the same denomination, you can have a second bet equal to your first and split the pair,\n" +
                "using each card as the first card in a separate hand \n" +
                "Out - Quit playing";
        }

        private void Blackjack_Button_Instructions_Back_Click(object sender, RoutedEventArgs e)
        {
            Blackjack_Game_Screen.Visibility = Visibility.Visible;
            Blackjack_Instructions_Screen.Visibility = Visibility.Collapsed;
        }
    }
}
