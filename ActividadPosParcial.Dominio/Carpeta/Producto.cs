﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPosParcial.Dominio.Carpeta
{
    public class Producto
    {
        public string Nombre { get; private set; }
        public decimal Costo { get; private set; }
        public bool VentaDirecta { get; private set; }
        public int Cantidad { get; private set; }

        protected List<VentaHuespede> _ventaHuespede;


        public Producto(string nombre, decimal costo, bool ventaDirecta)
        {
            Nombre = nombre;
            Costo = costo;
            VentaDirecta = ventaDirecta;
            _ventaHuespede = new List<VentaHuespede>();

        }

        public virtual string EntradaProductos( int cantidad)
        {

            if (cantidad >= 0)
            {
                Cantidad += cantidad;
                Inventario.productos.Add(this);
                return $"Su Nueva cantidad de {Nombre} es de {Cantidad}";
            }

            return "La cantidad debe ser mayor a 0";

        }

        public virtual void AumentarCantidadProducto(int cantidad)
        {
            Cantidad += cantidad;
        }

        public virtual void DisminuirCantidadProducto(string nombre, int cantidad)
        {
            if (cantidad > 0)
            {
                foreach (Producto producto in Inventario.productos)
                {
                    if (producto.Nombre.Equals(nombre))
                    {
                        if (producto.Cantidad >= cantidad)
                        {
                            producto.Cantidad -= cantidad;
                        }

                    }
                }
            }


        }
        
    }
}
