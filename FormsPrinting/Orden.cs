using System;
using System.Linq;
using System.Collections.Generic;

namespace FormsPrinting
{
    public class Orden
    {
        public int IdOrden { get; set; }
        public string Cliente { get; set; }
        public List<Producto> Productos { get; set; }
        public double Total => Productos.Sum(p => p.Total);
    }

    public class Producto
    {
        public string Nombre { get; set; }
        public double Total { get; set; }
    }
}
