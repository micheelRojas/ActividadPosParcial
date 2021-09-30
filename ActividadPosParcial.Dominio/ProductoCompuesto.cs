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
                    if (Inventario.productos.ToList()[i].Nombre.Equals(ingredientes[j].Producto.Nombre))
                    {
                        sumaCostos = sumaCostos + Inventario.productos.ToList()[i].Costo;
                    }
                }

            }
            return sumaCostos;
        }



        public override string SalidadeProductos( int cantidad, int huespede)
        {
            if (cantidad >= 0)
            {
                var validacion = ValidarExistencia(this, cantidad);
                if (validacion)
                {
                    _ventaHuespede.Add(new VentaHuespede(producto: this, huespede: huespede, venta: Precio * cantidad));
                    AumentarCantidadProducto(cantidad);
                    SalidadeProductosdelProductoCompuesto(Ingredientes,cantidad);
                    return $"La utilidad de {Nombre} es de: {Utilidad}";
                }
                if (!validacion)
                {
                    return "No existe la Cantidad de productos suficientes para la venta";
                }

            }
            throw new NotImplementedException();
        }
        public void SalidadeProductosdelProductoCompuesto(List<Ingrediente> ingredientes, int cantidad)
        {
            for (int i = 0; i < Inventario.productos.Count; i++)
            {
                for (int j = 0; j < ingredientes.Count(); j++)
                {

                    if (Inventario.productos[i].Nombre.Equals(ingredientes[j].Producto.Nombre))
                    {
                        Inventario.productos[i].DisminuirCantidadProducto(ingredientes[j].Cantidad * cantidad);

                    }

                }
            }
            
        }
        public bool ValidarExistencia(ProductoCompuesto producto, int cantidad)
        {
            int validador = 0;
            for (int i = 0; i < Inventario.productos.Count(); i++)
            {
                for (int j = 0; j < producto.Ingredientes.Count(); j++)
                {
                    if (Inventario.productos[i].Nombre.Equals(producto.Ingredientes[j].Producto.Nombre))
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
        public Producto Producto { get; private set; }
        public int Cantidad { get; private set; }
        public Ingrediente(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }
    }
}
