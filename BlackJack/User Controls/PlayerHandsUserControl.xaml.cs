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
        private Image i1;
        private Image i2;
        private Image i3;
        private Image i4;
        private Image i5;

        public ImageSource Slot1
        {
            get { return i1.Source; }
            set { i1.Source = value; }
        }
        public ImageSource Slot2
        {
            get { return i2.Source; }
            set { i2.Source = value; }
        }
        public ImageSource Slot3
        {
            get { return i3.Source; }
            set { i3.Source = value; }
        }
        public ImageSource Slot4
        {
            get { return i4.Source; }
            set { i4.Source = value; }
        }
        public ImageSource Slot5
        {
            get { return i5.Source; }
            set { i5.Source = value; }
        }
        public PlayerHandsUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        //<Image x:Name="slot1" Grid.Column="1" Grid.Row="1" Grid.RowSpan="2" Grid.ColumnSpan="4" />
        public void Initialize()
        {
            MainGrid.Children.Clear();
            i1 = new Image();
            i2 = new Image();
            i3 = new Image();
            i4 = new Image();
            i5 = new Image();

            MainGrid.Children.Add(i1);
            MainGrid.Children.Add(i2);
            MainGrid.Children.Add(i3);
            MainGrid.Children.Add(i4);
            MainGrid.Children.Add(i5);

            Grid.SetColumn(i1, 0);
            Grid.SetRow(i1, 1);
            Grid.SetRowSpan(i1, 2);
            Grid.SetColumnSpan(i1, 4);

            Grid.SetColumn(i2, 1);
            Grid.SetRow(i2, 1);
            Grid.SetRowSpan(i2, 2);
            Grid.SetColumnSpan(i2, 4);

            Grid.SetColumn(i3, 2);
            Grid.SetRow(i3, 1);
            Grid.SetRowSpan(i3, 2);
            Grid.SetColumnSpan(i3, 4);

            Grid.SetColumn(i4, 3);
            Grid.SetRow(i4, 1);
            Grid.SetRowSpan(i4, 2);
            Grid.SetColumnSpan(i4, 4);

            Grid.SetColumn(i5, 4);
            Grid.SetRow(i5, 1);
            Grid.SetRowSpan(i5, 2);
            Grid.SetColumnSpan(i5, 4);
        }
    }
}
