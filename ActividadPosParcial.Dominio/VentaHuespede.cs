using ActividadPosParcial.Dominio.Carpeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPosParcial.Dominio
{
    public class VentaHuespede
    {
        public int Huespede { get; private set; }
        public decimal Venta { get; private set; }
        public Producto Producto { get; private set; }

        public VentaHuespede(Producto producto, int huespede, decimal venta)
        {
            Producto = producto;
            Huespede = huespede;
            Venta = venta;
        }
    }
}
