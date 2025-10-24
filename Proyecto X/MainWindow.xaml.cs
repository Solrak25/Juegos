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
        int maxMapaX = 33;
        int maxMapaY = 33;
        List<List<char>> mapa = new List<List<char>>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarMapa("Mapas/Mapa.txt");
            cargarJugador();
        }

        private void cargarMapa(string ruta)
        {
            var uri = new Uri("pack://application:,,,/" + ruta);
            var info = Application.GetResourceStream(uri);
            using (var reader = new StreamReader(info.Stream))
            {
               for (int y = 0; y < maxMapaY; y++)
                {
                    string linea = reader.ReadLine();
                    mapa.Add(new List<char>());
                    for (int x = 0; x < maxMapaX; x++)
                    {
                        char caracter;
                        if (linea != null && x <= linea.Length - 1)
                        {
                            caracter = linea[x];
                        }else
                        {
                            caracter = ' ';
                        }

                        mapa[y].Add(caracter);

                        if (y == 0)
                        {
                        
                            ColumnDefinition c = new ColumnDefinition();
                            c.Width = new GridLength(1, GridUnitType.Star);
                            GridRoot.ColumnDefinitions.Add(c);
                        }
                        Rectangle rect = asignarColor(caracter);
                        Grid.SetColumn(rect, x);
                        Grid.SetRow(rect, y);
                        GridRoot.Children.Add(rect);
                    }
                    RowDefinition r = new RowDefinition();
                    r.Height = new GridLength(1, GridUnitType.Star);
                    GridRoot.RowDefinitions.Add(r);
                }
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
            if (e.Key == Key.W && Grid.GetRow(player.aspecto) > 0 && puedesNorte())
                Grid.SetRow(player.aspecto, Grid.GetRow(player.aspecto) - 1);
            if (e.Key == Key.S && Grid.GetRow(player.aspecto) < maxMapaY && puedesSur())
                Grid.SetRow(player.aspecto, Grid.GetRow(player.aspecto) + 1); ;
            if (e.Key == Key.A && Grid.GetColumn(player.aspecto) > 0 && puedesOeste())
                Grid.SetColumn(player.aspecto, Grid.GetColumn(player.aspecto) - 1); ;
            if (e.Key == Key.D && Grid.GetColumn(player.aspecto) < maxMapaX && puedesEste())
                Grid.SetColumn(player.aspecto, Grid.GetColumn(player.aspecto) + 1); ;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Math.Abs(Width - Height) > 0.1)
            {
                double size = Math.Min(Width, Height);
                Width = Height = size;
            }
        }

        public bool puedesNorte()
        {
            int posicionX = Grid.GetColumn(player.aspecto);
            int posicionY = Grid.GetRow(player.aspecto) - 1;
            char terreno = mapa[posicionY][posicionX];
            if(terreno == 'a' || terreno == ' ')
            {
                return false;
            }
            return true;
        }
        public bool puedesSur()
        {
            int posicionX = Grid.GetColumn(player.aspecto);
            int posicionY = Grid.GetRow(player.aspecto) + 1;
            char terreno = mapa[posicionY][posicionX];
            if (terreno == 'a' || terreno == ' ')
            {
                return false;
            }
            return true;
        }

        public bool puedesOeste()
        {
            int posicionX = Grid.GetColumn(player.aspecto) - 1;
            int posicionY = Grid.GetRow(player.aspecto);
            char terreno = mapa[posicionY][posicionX];
            if (terreno == 'a' || terreno == ' ')
            {
                return false;
            }
            return true;
        }
        public bool puedesEste()
        {
            int posicionX = Grid.GetColumn(player.aspecto) + 1;
            int posicionY = Grid.GetRow(player.aspecto);
            char terreno = mapa[posicionY][posicionX];
            if (terreno == 'a' || terreno == ' ')
            {
                return false;
            }
            return true;
        }
    }
}
