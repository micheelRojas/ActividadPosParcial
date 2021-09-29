using ActividadPosParcial.Dominio.Carpeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPosParcial.Dominio
{
    public class ProductoSimple : Producto
    {

        public decimal Precio { get; private set; }
        public decimal Utilidad { get => Cantidad * (Precio - Costo); }

        public ProductoSimple(string nombre, decimal costo, decimal precio, bool ventaDirecta) : base(nombre, costo, ventaDirecta)
        {
            Precio = precio;
        }

        public override string SalidadeProductos( int cantidad, int huespede)
        {
            if (cantidad >= 0)
            {
                DisminuirCantidadProducto(cantidad);
                _ventaHuespede.Add(new VentaHuespede(producto: this, huespede: huespede, venta: Precio * Cantidad));
                return $"Su Nueva cantidad de {Nombre} es de {Cantidad}";

            }

            return "La cantidad solicitada es incorrecta";
        }

        
    }
}
