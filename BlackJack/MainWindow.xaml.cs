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
            
        }

        public void PayoutAfterRound()
        {

        }
        public void CreateDeck()
        {
            List<String> Deck = Enum.GetNames(typeof(CardEnum)).ToList();
        }
    }
}
