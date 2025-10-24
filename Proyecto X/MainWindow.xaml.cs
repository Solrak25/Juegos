using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Proyecto_X
{
    public partial class MainWindow : Window
    {
        Jugador player;
        int maxMapaX;
        int maxMapaY;
        List<List<char>> mapa = new List<List<char>>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarMapa();
            cargarJugador();
        }

        private void cargarMapa()
        {
            int y = 0;
            foreach (string linea in File.ReadLines("Mapa.txt"))
            {
                mapa.Add(new List<char>());
                int x = 0;
                foreach (char caracter in linea)
                {
                    
                    mapa[y].Add(caracter);

                    if (y == 0)
                    {
                        maxMapaX++;
                        
                        ColumnDefinition c = new ColumnDefinition();
                        c.Width = new GridLength(1, GridUnitType.Star);
                        GridRoot.ColumnDefinitions.Add(c);
                    }
                    Rectangle rect = asignarColor(caracter);
                    Grid.SetColumn(rect, x);
                    Grid.SetRow(rect, y);
                    GridRoot.Children.Add(rect);
                    x++;
                }
                y++;
                maxMapaY++;
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(1, GridUnitType.Star);
                GridRoot.RowDefinitions.Add(r);
            }


        }

        private Rectangle asignarColor( char c)
        {
            if(c == 'c')
            {
               Rectangle rectangle = new Rectangle();
                rectangle.Fill= new SolidColorBrush(Colors.LightGreen);
                return rectangle;
            }
            else if (c == 'a')
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = new SolidColorBrush(Colors.LightBlue);
                return rectangle;
            }
            else if (c == 'r')
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = new SolidColorBrush(Colors.LightGray);
                return rectangle;
            }
            else
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = new SolidColorBrush(Colors.Black);
                return rectangle;
            }
        }

        private void cargarJugador()
        {
            int medioX = maxMapaX / 2;
            int medioY = maxMapaY / 2;
            player = new Jugador(GridRoot,medioX, medioY);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W && Grid.GetRow(player.aspecto) > 0)
                Grid.SetRow(player.aspecto, Grid.GetRow(player.aspecto) - 1);
            if (e.Key == Key.S && Grid.GetRow(player.aspecto) < maxMapaY)
                Grid.SetRow(player.aspecto, Grid.GetRow(player.aspecto) + 1); ;
            if (e.Key == Key.A && Grid.GetColumn(player.aspecto) > 0)
                Grid.SetColumn(player.aspecto, Grid.GetColumn(player.aspecto) - 1); ;
            if (e.Key == Key.D && Grid.GetColumn(player.aspecto) < maxMapaX)
                Grid.SetColumn(player.aspecto, Grid.GetColumn(player.aspecto) + 1); ;
        }
    }
}
