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
        public int Player1Bank = 20;
        public int Player2Bank = 20;
        public int Player3Bank = 20;
        public int Player4Bank = 20;
        public int Player5Bank = 20;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void CheckIfBankrupt()
        {
            if(Player1Bank < -50)
            {
                //in all of these, disable their option to go into the table, disable their play
            }
            if (Player2Bank < -50)
            {

            }
            if (Player3Bank < -50)
            {

            }
            if (Player4Bank < -50)
            {

            }
            if (Player5Bank < -50)
            {

            }
        }
    }
}
