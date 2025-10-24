using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Proyecto_X
{
    class Jugador
    {
        public int Vida { get; set; } = 100;
        public Rectangle aspecto;



        public Jugador(Grid padre, int posicionX, int posicionY)
        {
            aspecto = new Rectangle();
            aspecto.Fill = new SolidColorBrush(Colors.Red);
            aspecto.Width = 50;
            aspecto.Height = 50;
            Panel.SetZIndex(aspecto, 1);
            Grid.SetColumn(aspecto, posicionX);
            Grid.SetRow(aspecto, posicionY);
            padre.Children.Add(aspecto);
        }
    }
}
