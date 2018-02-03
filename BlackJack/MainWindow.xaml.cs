﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        //b.Style = (Style)Application.Current.Resources["MyButtonStyle"];

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
            List<TextBox> playerNames = new List<TextBox>()
            {
                Blackjack_TextBox_Playername_1,
                Blackjack_TextBox_Playername_2,
                Blackjack_TextBox_Playername_3,
                Blackjack_TextBox_Playername_4,
                Blackjack_TextBox_Playername_5
            };
            for (int i = 0; i < Blackjack_Slider_Players.Value; i++)
            {
                if(playerNames[i].Text == null || playerNames[i].Text.ToString().Trim() == "")
                {
                    players[i].Name = "Player " + (i + 1);
                }
                else
                {
                    players[i].Name = playerNames[i].Text;
                }
                Binding b = new Binding("Player");
                b.Mode = BindingMode.OneWay;
                playerDisplay[i].DataContext = players[i];
            }
            for(int j = (int)Blackjack_Slider_Players.Value; j < 5; j++)
            {
                players[j].Playing = false;
            }
        }

        /// <summary>
        /// Starts the betting phase of play, clearing any cards, and player 1 can bet
        /// </summary>
        public void StartBettingPhase()
        {
            ClearAllHands();
            ShowAllCards();
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

        public void ClearAllHands()
        {
            
            Player1.Hand.Clear();
            Player2.Hand.Clear();
            Player3.Hand.Clear();
            Player4.Hand.Clear();
            Player5.Hand.Clear();
            Player1.SplitHand.Clear();
            Player2.SplitHand.Clear();
            Player3.SplitHand.Clear();
            Player4.SplitHand.Clear();
            Player5.SplitHand.Clear();
            
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
        public void PayoutAfterRound()
        {
            bool dealerBusted = false;
            List<Player> players = new List<Player>()
            {
                Player1,
                Player2,
                Player3,
                Player4,
                Player5
            };
            for(int i=0;i<Blackjack_Slider_Players.Value;i++)
            {
                if (players[i].FinalHandAmount > 21)
                {

                }
                else if (players[i].Hand.Count() == 5)
                {
                    players[i].Bank = players[i].Bank + (players[i].Bet * 4);
                }
                else if (players[i].FinalHandAmount > Dealer.FinalHandAmount && players[i].FinalHandAmount != 21 || (dealerBusted && players[i].FinalHandAmount != 21))
                {
                    players[i].Bank = players[i].Bank + (players[i].Bet * 2);
                }
                else if (players[i].FinalHandAmount > Dealer.FinalHandAmount && players[i].FinalHandAmount == 21 || (dealerBusted && players[i].FinalHandAmount == 21))
                {
                    players[i].Bank = players[i].Bank + (players[i].Bet * 3);
                }
                else if (players[i].FinalHandAmount == Dealer.FinalHandAmount)
                {
                    players[i].Bank = players[i].Bank + players[i].Bet;
                }
            }
        }

        public void DrawCard(Player player, bool isSplitHand)
        {
            if(!isSplitHand)
            {
                player.Hand.Add((CardEnum)Enum.Parse(typeof(CardEnum), Deck[0]));
                ShowCard(player, player.Hand.Count() - 1, false, false);
            }
            else
            {
                player.SplitHand.Add((CardEnum)Enum.Parse(typeof(CardEnum), Deck[0]));
                ShowCard(player, player.SplitHand.Count() - 1, false, true);
            }

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
            for(int i = 0; i < Blackjack_Slider_Players.Value + 1; i++)
            {
                if(players[i].Playing)
                {
                    DrawCard(players[i], false);
                    DrawCard(players[i], false);
                    ShowCard(players[i], 0, true, false);
                    ShowCard(players[i], 1, false, false);
                    DetermineHandValue(players[i]);
                }
            }
        }
        /// <summary>
        /// Calls DetermineHandValueHelper() twice, once for the players hand and once for their split hand value
        /// changing the players FinalSplitAmount and FinalHandAmount property, changes haveBusted and SlitHasBusted property
        /// returns void
        /// </summary>
        /// <param name="p"></param>
        public void DetermineHandValue(Player p)
        {
            int handValue = DetermineHandValueHelper(p.Hand);
            int splitValue = DetermineHandValueHelper(p.SplitHand);
            p.FinalSplitAmount = splitValue;
            p.FinalHandAmount = handValue;
            if (p.FinalHandAmount > 21)
            {
                p.HaveBusted = true;
            }    
            if(p.FinalSplitAmount > 21)
            {
                p.SplitHasBusted = true;
            }
        }
        /// <summary>
        /// By passing a list of CardEnums to this method it will determine the value of the cards inside the list 
        /// returns an int indicating the total value of the players hand
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int DetermineHandValueHelper(List<CardEnum> list)
        {
            int handAcesDrawnCount = 0;
            int handValue = 0;
           

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ToString().Contains("King") || list[i].ToString().Contains("Queen")
                    || list[i].ToString().Contains("Jack") || list[i].ToString().Contains("Ten"))
                {
                    handValue += 10;
                }
                else if (list[i].ToString().Contains("Ace"))
                {
                    handValue += 11;
                    handAcesDrawnCount++;

                }
                else if (list[i].ToString().Contains("Two"))
                {
                    handValue += 2;
                }
                else if (list[i].ToString().Contains("Three"))
                {
                    handValue += 3;
                }
                else if (list[i].ToString().Contains("Four"))
                {
                    handValue += 4;
                }
                else if (list[i].ToString().Contains("Five"))
                {
                    handValue += 5;
                }
                else if (list[i].ToString().Contains("Six"))
                {
                    handValue += 6;
                }
                else if (list[i].ToString().Contains("Seven"))
                {
                    handValue += 7;
                }
                else if (list[i].ToString().Contains("Eight"))
                {
                    handValue += 8;
                }
                else if (list[i].ToString().Contains("Nine"))
                {
                    handValue += 9;
                }
            }
            for (int i = 0; i < handAcesDrawnCount; i++)
            {
                if (handValue > 21)
                {
                    handValue -= 10;
                }
            }
            return handValue;
        }
        /// <summary>
        /// Takes the Dealer object and checks FinalHandAmount to make the dealer continously draw until he either busts or is above 17
        /// PayoutsAfterRound() called afterward.
        /// </summary>
        public void DealerTurn()
        {
           
            while(Dealer.FinalHandAmount < 17 && Dealer.FinalHandAmount <= 21)
            {
                DetermineHandValue(Dealer);
                DrawCard(Dealer, false);
                DetermineHandValue(Dealer);
            }
            ShowCard(Dealer, 0, false, false);
            PayoutAfterRound();
            StartBettingPhase();
        }

        /// <summary>
        /// Gets the image of the card in the player's hand at the index passed in.
        /// Gets the CardBack image if isFirstCard is true
        /// </summary>
        /// <param name="player">Player whose hand you wish to show</param>
        /// <param name="index">The index of the particular card you wish to show</param>
        /// <param name="isFirstCard">True for the first card, false for any card after</param>
        /// <returns>An image brush with the image of the card</returns>
        public void ShowCard(Player player, int index, bool isFirstCard, bool isSplitHand)
        {
            if(!isFirstCard)
            {
                if(player == Player1)
                {
                    if(!isSplitHand)
                    {
                        Blackjack_Hand_Player_1.Visibility = Visibility.Visible;
                        if (index == 0)
                        {
                            Blackjack_Hand_Player_1.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 1)
                        {
                            Blackjack_Hand_Player_1.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 2)
                        {
                            Blackjack_Hand_Player_1.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 3)
                        {
                            Blackjack_Hand_Player_1.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 4)
                        {
                            Blackjack_Hand_Player_1.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                    }
                    else
                    {
                        Blackjack_Hand_Split_Player_1.Visibility = Visibility.Visible;
                        if (index == 0)
                        {
                            Blackjack_Hand_Split_Player_1.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 1)
                        {
                            Blackjack_Hand_Split_Player_1.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 2)
                        {
                            Blackjack_Hand_Split_Player_1.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 3)
                        {
                            Blackjack_Hand_Split_Player_1.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 4)
                        {
                            Blackjack_Hand_Split_Player_1.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                    }
                }
                else if (player == Player2)
                {
                    if (!isSplitHand)
                    {
                        Blackjack_Hand_Player_2.Visibility = Visibility.Visible;
                        if (index == 0)
                        {
                            Blackjack_Hand_Player_2.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 1)
                        {
                            Blackjack_Hand_Player_2.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 2)
                        {
                            Blackjack_Hand_Player_2.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 3)
                        {
                            Blackjack_Hand_Player_2.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 4)
                        {
                            Blackjack_Hand_Player_2.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                    }
                    else
                    {
                        Blackjack_Hand_Split_Player_2.Visibility = Visibility.Visible;
                        if (index == 0)
                        {
                            Blackjack_Hand_Split_Player_2.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 1)
                        {
                            Blackjack_Hand_Split_Player_2.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 2)
                        {
                            Blackjack_Hand_Split_Player_2.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 3)
                        {
                            Blackjack_Hand_Split_Player_2.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 4)
                        {
                            Blackjack_Hand_Split_Player_2.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                    }
                }
                else if (player == Player3)
                {
                    if (!isSplitHand)
                    {
                        Blackjack_Hand_Player_3.Visibility = Visibility.Visible;
                        if (index == 0)
                        {
                            Blackjack_Hand_Player_3.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 1)
                        {
                            Blackjack_Hand_Player_3.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 2)
                        {
                            Blackjack_Hand_Player_3.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 3)
                        {
                            Blackjack_Hand_Player_3.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 4)
                        {
                            Blackjack_Hand_Player_3.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                    }
                    else
                    {
                        Blackjack_Hand_Split_Player_3.Visibility = Visibility.Visible;
                        if (index == 0)
                        {
                            Blackjack_Hand_Split_Player_3.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 1)
                        {
                            Blackjack_Hand_Split_Player_3.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 2)
                        {
                            Blackjack_Hand_Split_Player_3.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 3)
                        {
                            Blackjack_Hand_Split_Player_3.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 4)
                        {
                            Blackjack_Hand_Split_Player_3.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                    }
                }
                else if (player == Player4)
                {
                    if (!isSplitHand)
                    {
                        Blackjack_Hand_Player_4.Visibility = Visibility.Visible;
                        if (index == 0)
                        {
                            Blackjack_Hand_Player_4.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 1)
                        {
                            Blackjack_Hand_Player_4.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 2)
                        {
                            Blackjack_Hand_Player_4.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 3)
                        {
                            Blackjack_Hand_Player_4.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 4)
                        {
                            Blackjack_Hand_Player_4.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                    }
                    else
                    {
                        Blackjack_Hand_Split_Player_4.Visibility = Visibility.Visible;
                        if (index == 0)
                        {
                            Blackjack_Hand_Split_Player_4.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 1)
                        {
                            Blackjack_Hand_Split_Player_4.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 2)
                        {
                            Blackjack_Hand_Split_Player_4.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 3)
                        {
                            Blackjack_Hand_Split_Player_4.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 4)
                        {
                            Blackjack_Hand_Split_Player_4.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                    }
                }
                else if (player == Player5)
                {
                    if (!isSplitHand)
                    {
                        Blackjack_Hand_Player_5.Visibility = Visibility.Visible;
                        if (index == 0)
                        {
                            Blackjack_Hand_Player_5.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 1)
                        {
                            Blackjack_Hand_Player_5.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 2)
                        {
                            Blackjack_Hand_Player_5.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 3)
                        {
                            Blackjack_Hand_Player_5.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 4)
                        {
                            Blackjack_Hand_Player_5.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                    }
                    else
                    {
                        Blackjack_Hand_Split_Player_5.Visibility = Visibility.Visible;
                        if (index == 0)
                        {
                            Blackjack_Hand_Split_Player_5.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 1)
                        {
                            Blackjack_Hand_Split_Player_5.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 2)
                        {
                            Blackjack_Hand_Split_Player_5.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 3)
                        {
                            Blackjack_Hand_Split_Player_5.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                        else if (index == 4)
                        {
                            Blackjack_Hand_Split_Player_5.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                        }
                    }
                }
                else if (player == Dealer)
                {
                    Blackjack_Hand_Dealer.Visibility = Visibility.Visible;
                    if (index == 0)
                    {
                        Blackjack_Hand_Dealer.Slot1 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                    }
                    else if (index == 1)
                    {
                        Blackjack_Hand_Dealer.Slot2 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                    }
                    else if (index == 2)
                    {
                        Blackjack_Hand_Dealer.Slot3 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                    }
                    else if (index == 3)
                    {
                        Blackjack_Hand_Dealer.Slot4 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                    }
                    else if (index == 4)
                    {
                        Blackjack_Hand_Dealer.Slot5 = new BitmapImage(new Uri($"../Images/{player.Hand[index]}.png", UriKind.Relative));
                    }
                }
            }
            else
            {
                if(player == Player1)
                {
                    Blackjack_Hand_Player_1.Slot1 = new BitmapImage(new Uri("../Images/CardBack.png", UriKind.Relative));
                }
                else if (player == Player2)
                {
                    Blackjack_Hand_Player_2.Slot1 = new BitmapImage(new Uri("../Images/CardBack.png", UriKind.Relative));
                }
                else if (player == Player3)
                {
                    Blackjack_Hand_Player_3.Slot1 = new BitmapImage(new Uri("../Images/CardBack.png", UriKind.Relative));
                }
                else if (player == Player4)
                {
                    Blackjack_Hand_Player_4.Slot1 = new BitmapImage(new Uri("../Images/CardBack.png", UriKind.Relative));
                }
                else if (player == Player5)
                {
                    Blackjack_Hand_Player_5.Slot1 = new BitmapImage(new Uri("../Images/CardBack.png", UriKind.Relative));
                }
                else if (player == Dealer)
                {
                    Blackjack_Hand_Dealer.Slot1 = new BitmapImage(new Uri("../Images/CardBack.png", UriKind.Relative));
                }
            }
        }

        public void ShowAllCards(bool isSplitHand)
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

            if(Blackjack_StackPanel_Player_1.IsEnabled)
            {
                for(int i = 0; i < Player1.Hand.Count() - 1; i++)
                {
                    ShowCard(Player1, i, false, isSplitHand);
                }
            }
            else if (Blackjack_StackPanel_Player_2.IsEnabled)
            {
                for (int i = 0; i < Player2.Hand.Count() - 1; i++)
                {
                    ShowCard(Player2, i, false, isSplitHand);
                }
            }
            else if (Blackjack_StackPanel_Player_3.IsEnabled)
            {
                for (int i = 0; i < Player3.Hand.Count() - 1; i++)
                {
                    ShowCard(Player3, i, false, isSplitHand);
                }
            }
            else if (Blackjack_StackPanel_Player_4.IsEnabled)
            {
                for (int i = 0; i < Player4.Hand.Count() - 1; i++)
                {
                    ShowCard(Player4, i, false, isSplitHand);
                }
            }
            else if (Blackjack_StackPanel_Player_5.IsEnabled)
            {
                for (int i = 0; i < Player5.Hand.Count() - 1; i++)
                {
                    ShowCard(Player5, i, false, isSplitHand);
                }
            }
            else if (Blackjack_Grid_House.IsEnabled)
            {
                for (int i = 0; i < Dealer.Hand.Count() - 1; i++)
                {
                    ShowCard(Dealer, i, false, false);
                }
            }
        }

        /// <summary>
        /// Adds a card to players hand, makes sure it is face up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            for(int i=0; i<Blackjack_Slider_Players.Value; i++)
            {
                if (stackPanels[i].IsEnabled && userControls[i].Visibility == Visibility.Visible && userControls[i].IsEnabled)
                {
                    DrawCard(players[i], false);
                    ShowCard(players[i], players[i].Hand.Count() - 1, false, false);
                }
            }
        }

        private void Title_Screen_Click_Blackjack(object sender, RoutedEventArgs e)
        {
            Title_Screen.Visibility = Visibility.Collapsed;
            Blackjack_Options_Screen.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// When a betting option is chosen, makes it the next available player's turn to bet. If no more, starts the round.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    ResetStackPanelBackgroundsToBlack();
                    SetPanelToWheat(Blackjack_StackPanel_Player_2);
                    if(!Player2.Playing)
                    {
                        if(!Player3.Playing)
                        {
                            if(!Player4.Playing)
                            {
                                if(!Player5.Playing)
                                {
                                    Blackjack_StackPanel_Player_5.IsEnabled = false;
                                    ChangeBidVisibilites();
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
                    ResetStackPanelBackgroundsToBlack();
                    SetPanelToWheat(Blackjack_StackPanel_Player_3);
                    if (!Player3.Playing)
                    {
                        if (!Player4.Playing)
                        {
                            if (!Player5.Playing)
                            {
                                Blackjack_StackPanel_Player_5.IsEnabled = false;
                                ChangeBidVisibilites();
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
                    ResetStackPanelBackgroundsToBlack();
                    SetPanelToWheat(Blackjack_StackPanel_Player_4);
                    if (!Player4.Playing)
                    {
                        if (!Player5.Playing)
                        {
                            Blackjack_StackPanel_Player_5.IsEnabled = false;
                            ChangeBidVisibilites();
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
                    ResetStackPanelBackgroundsToBlack();
                    SetPanelToWheat(Blackjack_StackPanel_Player_5);
                    if (!Player5.Playing)
                    {
                        Blackjack_StackPanel_Player_5.IsEnabled = false;
                        ChangeBidVisibilites();
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
                    ResetStackPanelBackgroundsToBlack();
                    ChangeBidVisibilites();
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
            InitialDraw();

            Blackjack_Button_Hit.IsEnabled = true;
            Blackjack_Button_Stay.IsEnabled = true;

            DetermineHandValue(Player1);
            DetermineHandValue(Player2);
            DetermineHandValue(Player3);
            DetermineHandValue(Player4);
            DetermineHandValue(Player5);

            if (Player1.Playing && Player1.FinalHandAmount != 21)
            {
                SetPanelToWheat(Blackjack_StackPanel_Player_1);
                Blackjack_StackPanel_Player_1.IsEnabled = true;
                Blackjack_StackPanel_Player_2.IsEnabled = false;
                Blackjack_StackPanel_Player_3.IsEnabled = false;
                Blackjack_StackPanel_Player_4.IsEnabled = false;
                Blackjack_StackPanel_Player_5.IsEnabled = false;
            }
            else if (Player2.Playing && Player2.FinalHandAmount != 21)
            {
                SetPanelToWheat(Blackjack_StackPanel_Player_2);
                Blackjack_StackPanel_Player_1.IsEnabled = false;
                Blackjack_StackPanel_Player_2.IsEnabled = true;
                Blackjack_StackPanel_Player_3.IsEnabled = false;
                Blackjack_StackPanel_Player_4.IsEnabled = false;
                Blackjack_StackPanel_Player_5.IsEnabled = false;
            }
            else if (Player3.Playing && Player3.FinalHandAmount != 21)
            {
                SetPanelToWheat(Blackjack_StackPanel_Player_3);
                Blackjack_StackPanel_Player_1.IsEnabled = false;
                Blackjack_StackPanel_Player_2.IsEnabled = false;
                Blackjack_StackPanel_Player_3.IsEnabled = true;
                Blackjack_StackPanel_Player_4.IsEnabled = false;
                Blackjack_StackPanel_Player_5.IsEnabled = false;
            }
            else if (Player4.Playing && Player4.FinalHandAmount != 21)
            {
                SetPanelToWheat(Blackjack_StackPanel_Player_4);
                Blackjack_StackPanel_Player_1.IsEnabled = false;
                Blackjack_StackPanel_Player_2.IsEnabled = false;
                Blackjack_StackPanel_Player_3.IsEnabled = false;
                Blackjack_StackPanel_Player_4.IsEnabled = true;
                Blackjack_StackPanel_Player_5.IsEnabled = false;
            }
            else if (Player5.Playing && Player5.FinalHandAmount != 21)
            {
                SetPanelToWheat(Blackjack_StackPanel_Player_5);
                Blackjack_StackPanel_Player_1.IsEnabled = false;
                Blackjack_StackPanel_Player_2.IsEnabled = false;
                Blackjack_StackPanel_Player_3.IsEnabled = false;
                Blackjack_StackPanel_Player_4.IsEnabled = false;
                Blackjack_StackPanel_Player_5.IsEnabled = true;
            }
            ShowAllCards(false);
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
        /// Saves the game to a file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "blackjack files (*.blackjack)|*.blackjack";
            saveFile.FilterIndex = 1;
            saveFile.RestoreDirectory = true;
            if (saveFile.ShowDialog() == true)
            {
                IFormatter form = new BinaryFormatter();
                Stream s = new FileStream(saveFile.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                SaveInformation save = new SaveInformation(Player1, Player2, Player3, Player4, Player5, Dealer, Deck);
                form.Serialize(s, save);
                s.Close();
            }

        }

        /// <summary>
        /// Loads a game from a file (Not Finished)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "blackjack files (*.blackjack)|*.blackjack";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == true)
            {
                IFormatter form = new BinaryFormatter();
                Stream stream = new FileStream(openFile.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                SaveInformation save = new SaveInformation();
                save = (SaveInformation)form.Deserialize(stream);
                stream.Close();
                Player1 = save.Player1;
                Blackjack_Label_Player_1.Content = Player1.Name;
                Blackjack_Label_Money_1.Content = Player1.Bank;
                //Blackjack_Hand_Player_1.Content = ReLoadCards(Player1);
                Player2 = save.Player2;
                Blackjack_Label_Player_2.Content = Player2.Name;
                Blackjack_Label_Money_2.Content = Player2.Bank;
                //Blackjack_Hand_Player_2.Content = Player2.Hand;
                Player3 = save.Player3;
                Blackjack_Label_Player_3.Content = Player3.Name;
                Blackjack_Label_Money_3.Content = Player3.Bank;
                //Blackjack_Hand_Player_3.Content = Player3.Hand;
                Player4 = save.Player4;
                Blackjack_Label_Player_4.Content = Player4.Name;
                Blackjack_Label_Money_4.Content = Player4.Bank;
                //Blackjack_Hand_Player_4.Content = Player4.Hand;
                Player5 = save.Player5;
                Blackjack_Label_Player_5.Content = Player5.Name;
                Blackjack_Label_Money_5.Content = Player5.Bank;
                Blackjack_Hand_Player_5.Content = Player5.Hand;
                Dealer = save.Dealer;
                //Blackjack_Hand_Dealer.Content = Dealer.Hand;
                Deck = save.Deck;
            }

        }
        
        /// <summary>
        /// May need this method to reput the cards onto the gui 
        /// I know this doesn't work the way i originally thought
        /// </summary>
        /// <param name="player"></param>
        public void ReLoadCards(Player player)
        {
            foreach (CardEnum card in player.Hand)
            {
                for(int i = 0;  i < player.Hand.Count; i++)
                {
                    if(i == 0)
                    {
                        ShowCard(player, 0, true);
                    }
                    else
                    {
                        ShowCard(player, i, false);
                    }
                }
            }
        }

        private void Blackjack_Button_Instructions_Click(object sender, RoutedEventArgs e)
        {
            Blackjack_Game_Screen.Visibility = Visibility.Collapsed;
            Blackjack_Instructions_Screen.Visibility = Visibility.Visible;
            Blackjack_Label_Instructions.Content = "Blackjack uses one standard deck of 52 cards wheere the object is to get as close to 21 without going over.\n" +
                "Blackjack is started by players placing their bet of $1, $5, or $10. After Players are given 2 cards to start.\n" +
                "Cards are dealt face down. After the cards are dealt Players decide how to play their hand.\n" +
                "Cards 2-10 are worth face value while King, Queen, and Jack are worth 10. Ace's are worth either 1 or 11.\n" +
                "Hit - Take another card until you are as close to 21 as possible. If you exceed 21 you 'Bust'\n" +
                "Stay - elect to draw no cards, you do this if you have faith your total will beat the dealer\n" +
                "Split - if you have two cards of the same denomination, you can have a second bet equal to your first and split the pair,\n" +
                "using each card as the first card in a separate hand\n" +
                "Out - Quit playing";
        }

        private void Blackjack_Button_Instructions_Back_Click(object sender, RoutedEventArgs e)
        {
            Blackjack_Game_Screen.Visibility = Visibility.Visible;
            Blackjack_Instructions_Screen.Visibility = Visibility.Collapsed;
        }

        private void Blackjack_Slider_Players_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            switch (Blackjack_Slider_Players.Value)
            {
                case 1:
                    Blackjack_Grid_Playername_2.Visibility = Visibility.Hidden;
                    Blackjack_Grid_Playername_3.Visibility = Visibility.Hidden;
                    Blackjack_Grid_Playername_4.Visibility = Visibility.Hidden;
                    Blackjack_Grid_Playername_5.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    Blackjack_Grid_Playername_2.Visibility = Visibility.Visible;
                    Blackjack_Grid_Playername_3.Visibility = Visibility.Hidden;
                    Blackjack_Grid_Playername_4.Visibility = Visibility.Hidden;
                    Blackjack_Grid_Playername_5.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    Blackjack_Grid_Playername_2.Visibility = Visibility.Visible;
                    Blackjack_Grid_Playername_3.Visibility = Visibility.Visible;
                    Blackjack_Grid_Playername_4.Visibility = Visibility.Hidden;
                    Blackjack_Grid_Playername_5.Visibility = Visibility.Hidden;
                    break;
                case 4:
                    Blackjack_Grid_Playername_2.Visibility = Visibility.Visible;
                    Blackjack_Grid_Playername_3.Visibility = Visibility.Visible;
                    Blackjack_Grid_Playername_4.Visibility = Visibility.Visible;
                    Blackjack_Grid_Playername_5.Visibility = Visibility.Hidden;
                    break;
                case 5:
                    Blackjack_Grid_Playername_2.Visibility = Visibility.Visible;
                    Blackjack_Grid_Playername_3.Visibility = Visibility.Visible;
                    Blackjack_Grid_Playername_4.Visibility = Visibility.Visible;
                    Blackjack_Grid_Playername_5.Visibility = Visibility.Visible;
                    break;
            }
        }

        public bool PlayerHasSplit(Player p)
        {
            return (p.Hand.Count == 2) && (
                   (p.Hand[0].ToString().Contains("Ace") && p.Hand[1].ToString().Contains("Ace")) ||
                   (p.Hand[0].ToString().Contains("Two") && p.Hand[1].ToString().Contains("Two")) ||
                   (p.Hand[0].ToString().Contains("Three") && p.Hand[1].ToString().Contains("Three")) ||
                   (p.Hand[0].ToString().Contains("Four") && p.Hand[1].ToString().Contains("Four")) ||
                   (p.Hand[0].ToString().Contains("Five") && p.Hand[1].ToString().Contains("Five")) ||
                   (p.Hand[0].ToString().Contains("Six") && p.Hand[1].ToString().Contains("Six")) ||
                   (p.Hand[0].ToString().Contains("Seven") && p.Hand[1].ToString().Contains("Seven")) ||
                   (p.Hand[0].ToString().Contains("Eight") && p.Hand[1].ToString().Contains("Eight")) ||
                   (p.Hand[0].ToString().Contains("Nine") && p.Hand[1].ToString().Contains("Nine")) ||
                   (p.Hand[0].ToString().Contains("Ten") && p.Hand[1].ToString().Contains("Ten")) ||
                   (p.Hand[0].ToString().Contains("Jack") && p.Hand[1].ToString().Contains("Jack")) ||
                   (p.Hand[0].ToString().Contains("Queen") && p.Hand[1].ToString().Contains("Queen")) ||
                   (p.Hand[0].ToString().Contains("King") && p.Hand[1].ToString().Contains("King")));
        }

        private void Blackjack_Slider_Players_Loaded(object sender, RoutedEventArgs e)
        {
            Blackjack_Slider_Players.ValueChanged += Blackjack_Slider_Players_ValueChanged;
        }

        private void Blackjack_Button_Startgame_Click(object sender, RoutedEventArgs e)
        {
            Blackjack_Options_Screen.Visibility = Visibility.Collapsed;
            Blackjack_Game_Screen.Visibility = Visibility.Visible;
            BindToPlayers();
            ShuffleDeck();
            StartBettingPhase();
        }

        private void Blackjack_Button_Split_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Blackjack_Button_Stay_Click(object sender, RoutedEventArgs e)
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
                Blackjack_Hand_Split_Player_1,
                Blackjack_Hand_Player_2,
                Blackjack_Hand_Split_Player_2,
                Blackjack_Hand_Player_3,
                Blackjack_Hand_Split_Player_3,
                Blackjack_Hand_Player_4,
                Blackjack_Hand_Split_Player_4,
                Blackjack_Hand_Player_5,
                Blackjack_Hand_Split_Player_5
            };
            List<Player> players = new List<Player>()
            {
                Player1,
                Player2,
                Player3,
                Player4,
                Player5
            };
            bool switchTurn = false;
            for (int i = 0; i < Blackjack_Slider_Players.Value; i++)
            {
                if(!switchTurn)
                {
                    if (stackPanels[i].IsEnabled && (userControls[i * 2].IsEnabled || userControls[(i * 2) + 1].IsEnabled))
                    {
                        if (userControls[i * 2].IsEnabled)
                        {
                            if(players[i].SplitHand.Count == 0)
                            {
                                if (i == Blackjack_Slider_Players.Value - 1)
                                {
                                    DealerTurn();
                                }
                                else
                                {
                                    ResetStackPanelBackgroundsToBlack();
                                    
                                    if(players[i].FinalHandAmount <= 21)
                                    {
                                        ShowCard(players[i], 0, true, false);
                                    }
                                    stackPanels[i].IsEnabled = false;
                                    userControls[i * 2].IsEnabled = false;
                                    stackPanels[i + 1].IsEnabled = true;
                                    userControls[(i + 1)* 2].IsEnabled = true;
                                    switchTurn = true;
                                    //show turn progression here
                                    SetPanelToWheat(stackPanels[i + 1]);
                                    ShowAllCards(false);
                                }
                            }
                            else
                            {
                                userControls[i * 2].IsEnabled = false;
                                userControls[(i * 2) + 1].IsEnabled = true;
                                switchTurn = true;
                                //Show all cards for split here
                            }
                        }
                        else
                        {
                            if (i == Blackjack_Slider_Players.Value - 1)
                            {
                                DealerTurn();
                            }
                            else
                            {
                                ResetStackPanelBackgroundsToBlack();

                                if (players[i].FinalHandAmount <= 21)
                                {
                                    ShowCard(players[i], 0, true, false);
                                }
                                stackPanels[i].IsEnabled = false;
                                userControls[i * 2].IsEnabled = false;
                                stackPanels[i + 1].IsEnabled = true;
                                userControls[(i + 1) * 2].IsEnabled = true;
                                switchTurn = true;
                                //show turn progression here
                                SetPanelToWheat(stackPanels[i + 1]);
                                ShowAllCards(false);
                            }
                        }
                    }
                }
            }
        }

        private void SetPanelToWheat(StackPanel p)
        {
            SolidColorBrush sb = new SolidColorBrush();
            sb.Color = Colors.Wheat;
            sb.Opacity = .2;
            p.Background = sb;
        }

        private void ResetStackPanelBackgroundsToBlack()
        {
            SolidColorBrush sb = new SolidColorBrush();
            sb.Color = Colors.Black;
            sb.Opacity = .2;
            //sb.Opacity = Blackjack_StackPanel_Player_1.IsEnabled ? .2 : .6;
            Blackjack_StackPanel_Player_1.Background = sb;
            //sb.Opacity = Blackjack_StackPanel_Player_2.IsEnabled ? .2 : .6;
            Blackjack_StackPanel_Player_2.Background = sb;
            //sb.Opacity = Blackjack_StackPanel_Player_3.IsEnabled ? .2 : .6;
            Blackjack_StackPanel_Player_3.Background = sb;
            //sb.Opacity = Blackjack_StackPanel_Player_4.IsEnabled ? .2 : .6;
            Blackjack_StackPanel_Player_4.Background = sb;
            //sb.Opacity = Blackjack_StackPanel_Player_5.IsEnabled ? .2 : .6;
            Blackjack_StackPanel_Player_5.Background = sb;
        }

        private void Blackjack_Button_Hit_Click(object sender, RoutedEventArgs e)
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
                Blackjack_Hand_Split_Player_1,
                Blackjack_Hand_Player_2,
                Blackjack_Hand_Split_Player_2,
                Blackjack_Hand_Player_3,
                Blackjack_Hand_Split_Player_3,
                Blackjack_Hand_Player_4,
                Blackjack_Hand_Split_Player_4,
                Blackjack_Hand_Player_5,
                Blackjack_Hand_Split_Player_5
            };
            List<Player> players = new List<Player>()
            {
                Player1,
                Player2,
                Player3,
                Player4,
                Player5
            };
            bool playerBusted = false;
            for (int i = 0; i < Blackjack_Slider_Players.Value; i++)
            {
                if(!playerBusted)
                {
                    if (stackPanels[i].IsEnabled && (userControls[i * 2].IsEnabled || userControls[(i * 2) + 1].IsEnabled))
                    {
                        if (userControls[i * 2].IsEnabled)
                        {
                            DrawCard(players[i], false);
                            ShowCard(players[i], players[i].Hand.Count() - 1, false, false);
                            DetermineHandValue(players[i]);
                            if(players[i].HaveBusted)
                            {
                                ShowAllCards(false);
                                Blackjack_Button_Stay_Click(null, new RoutedEventArgs());
                                playerBusted = true;
                            }
                            else if(players[i].Hand.Count() == 5)
                            {
                                Blackjack_Button_Stay_Click(null, new RoutedEventArgs());
                                playerBusted = true;
                            }
                            else if(players[i].FinalHandAmount == 21)
                            {
                                Blackjack_Button_Stay_Click(null, new RoutedEventArgs());
                                playerBusted = true;
                            }
                        }
                        else
                        {
                            //DrawCard(players[i]); add to split hand
                            //ShowCard(players[i], players[i].SplitHand.Count() - 1, false);
                            if (players[i].SplitHasBusted)
                            {
                                ShowAllCards(false);
                                Blackjack_Button_Stay_Click(null, new RoutedEventArgs());
                                playerBusted = true;
                            }
                            else if (players[i].SplitHand.Count() == 5)
                            {
                                Blackjack_Button_Stay_Click(null, new RoutedEventArgs());
                                playerBusted = true;
                            }
                            else if(players[i].FinalSplitAmount == 21)
                            {
                                Blackjack_Button_Stay_Click(null, new RoutedEventArgs());
                                playerBusted = true;
                            }
                        }
                    }
                }
            }
        }
    }
}