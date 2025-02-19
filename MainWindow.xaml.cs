using System.Data.Common;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Drawing;

namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model modelHry = new Model();
        public MainWindow()
        {
            InitializeComponent();
            Grid.Focus();
            modelHry.Pridej(2);
        }
   
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public SolidColorBrush dostanBarvu(int value)
        {
            switch (value)
            {
                case 2: return new SolidColorBrush(Colors.LightGray);
                case 4: return new SolidColorBrush(Colors.LightBlue);
                case 8: return new SolidColorBrush(Colors.Orange);
                case 16: return new SolidColorBrush(Colors.DarkOrange);
                case 32: return new SolidColorBrush(Colors.Red);
                case 64: return new SolidColorBrush(Colors.DarkRed);
                case 128: return new SolidColorBrush(Colors.Yellow);
                case 256: return new SolidColorBrush(Colors.Gold);
                case 512: return new SolidColorBrush(Colors.LightGreen);
                case 1024: return new SolidColorBrush(Colors.Green);
                case 2048: return new SolidColorBrush(Colors.DarkGreen);
                default: return new SolidColorBrush(Colors.Black); // Default color for empty tiles
            }
        }

        private void Posunuj(object sender, KeyEventArgs e)
        {
            Key kliknuty = e.Key;

            int vektorX = 0;
            int vektorY = 0;
            // Check if the key pressed is a specific key
            if (kliknuty == Key.Up)
            {
                vektorX = 0;
                vektorY = -1;
            }
            else if (kliknuty == Key.Down)
            {
                vektorX = 0;
                vektorY = 1;
            }
            else if (kliknuty == Key.Left)
            {
                vektorX = -1;
                vektorY = 0;
            }
            else if (kliknuty == Key.Right)
            {
                vektorX = 1;
                vektorY = 0;
            }

            if (!(vektorX == 0 & vektorY == 0))
            {
                try
                {
                    modelHry.Posun(vektorY, vektorX);
                    modelHry.Pridej(2);
                }
                catch
                {
                    MessageBox.Show("No more moves available. You reached score " + modelHry.skore.ToString(), "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            Skore.Content = modelHry.skore.ToString();

            foreach (UIElement element in Grid.Children)
            {
                int radek = Grid.GetRow(element);
                int sloupec = Grid.GetColumn(element);

                int hodnotaBoxu = modelHry.pole[radek, sloupec];

                if (element is System.Windows.Shapes.Rectangle rectangle)
                {
                    rectangle.Fill = hodnotaBoxu >= 2 ? dostanBarvu(hodnotaBoxu) : new SolidColorBrush(Colors.Beige);
                }
            }
        }
    }
}