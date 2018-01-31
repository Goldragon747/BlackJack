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

namespace BlackJack.User_Controls
{
    /// <summary>
    /// Interaction logic for PlayerHandsUserControl.xaml
    /// </summary>
    public partial class PlayerHandsUserControl : UserControl
    {
        public ImageSource Slot1
        {
            get { return slot1.Source; }
            set { slot1.Source = value; }
        }
        public ImageSource Slot2
        {
            get { return slot2.Source; }
            set { slot2.Source = value; }
        }
        public ImageSource Slot3
        {
            get { return slot3.Source; }
            set { slot3.Source = value; }
        }
        public ImageSource Slot4
        {
            get { return slot4.Source; }
            set { slot4.Source = value; }
        }
        public ImageSource Slot5
        {
            get { return slot5.Source; }
            set { slot5.Source = value; }
        }
        public PlayerHandsUserControl()
        {
            InitializeComponent();
        }
    }
}
