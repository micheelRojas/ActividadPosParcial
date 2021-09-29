using ActividadPosParcial.Dominio.Carpeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPosParcial.Dominio
{
    public class ProductoCompuesto : Producto
    {

        public decimal Precio { get; private set; }
        public decimal Utilidad { get => Cantidad * (Precio - Costo); }
        public List<Ingrediente> Ingredientes { get; private set; }

        public ProductoCompuesto(string nombre, decimal precio, List<Ingrediente> ingredientes) : base(nombre, calcularCostos(ingredientes), false)
        {
            Ingredientes = ingredientes;
            Precio = precio;
        }
        private static decimal calcularCostos(List<Ingrediente> ingredientes)
        {
            decimal sumaCostos = 0;
            for (int i = 0; i < Inventario.productos.LongCount(); i++)
            {
                for (int j = 0; j < ingredientes.LongCount(); j++)
                {
                    if (Inventario.productos.ToList()[i].Nombre.Equals(ingredientes[j].Nombre))
                    {
                        sumaCostos = sumaCostos + Inventario.productos.ToList()[i].Costo;
                    }
                }

            }
            return sumaCostos;
        }



        public string SalidadeProductosCompuesto( int cantidad, int huespede)
        {
            if (cantidad >= 0)
            {
                var validacion = ValidarExistencia(this, cantidad);
                if (validacion)
                {
                    _ventaHuespede.Add(new VentaHuespede(producto: this, huespede: huespede, venta: Precio * cantidad));
                    EstablecerCantidadProductoCompuesto(cantidad);
                    return $"La utilidad de {Nombre} es de: {Utilidad}";
                }
                if (!validacion)
                {
                    return "No existe la Cantidad de productos suficientes para la venta";
                }

            }
            throw new NotImplementedException();
        }
        public void SalidadeProductos(List<Ingrediente> ingredientes, int cantidad)
        {
            for (int i = 0; i < ingredientes.LongCount(); i++)
            {
                DisminuirCantidadProductoCompuesto(ingredientes[i].Nombre, ingredientes[i].Cantidad, cantidad);

            }
        }
        public bool ValidarExistencia(ProductoCompuesto producto, int cantidad)
        {
            int validador = 0;
            for (int i = 0; i < Inventario.productos.Count(); i++)
            {
                for (int j = 0; j < producto.Ingredientes.Count(); j++)
                {
                    if (Inventario.productos[i].Nombre.Equals(producto.Ingredientes[j].Nombre))
                    {
                        if (Inventario.productos[i].Cantidad >= producto.Ingredientes[j].Cantidad * cantidad)
                        {
                            validador++;
                        }
                    }

                }
            }
            if (validador == producto.Ingredientes.Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Ingrediente
    {
        public string Nombre { get; private set; }
        public int Cantidad { get; private set; }
        public Ingrediente(string nombre, int cantidad)
        {
            Nombre = nombre;
            Cantidad = cantidad;
        }
    }
}
