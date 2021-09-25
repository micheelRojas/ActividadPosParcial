﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtividadPosParcial.Test.Productos
{
    class ProductosTest
    {
        /*
         HU1. ENTRADA DE PRODUCTO (1.5)
        COMO USUARIO QUIERO REGISTRAR LA ENTRADA PRODUCTOS
        CRITERIOS DE ACEPTACIÓN
         1. La cantidad de la entrada de debe ser mayor a 0
         2. La cantidad de la entrada se le aumentará a la cantidad existente del producto
         */
        [Test]
        public void PuedoRegistrarProductosdeEntrada()
        {

            #region DADO EL RESTAURANTE TIENE PRODUCTO DE GASEODA DE LITRO CON UN PRECIO DE 5000 Y UN COSTO 2000 Y NO NECESITA PREPARACION
            var producto = new ProductoSimple(nombre: "Gaseosa", costo: 2000, precio: 5000);
            #endregion
            #region CUANDO registre 3 gaseosa
            int cantidad = 3;
            string respuesta = producto.EntradaProductos(producto: producto, cantidad: cantidad);
            #endregion
            #region ENTONCES  el sistema registrara el producto en el inventario y adicionara la cantidad del mismo 
            Assert.AreEqual("Su Nueva cantidad de Gaseosa es de 3", respuesta);
            #endregion

        }
        /*
         * HU1. SALIDA DE PRODUCTO (3.5)
        COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
        CRITERIOS DE ACEPTACIÓN
        1. La cantidad de la salida de debe ser mayor a 0
        2. En caso de un producto sencillo la cantidad de la salida se le disminuirá a la cantidad
        existente del producto.
        3. En caso de un producto compuesto la cantidad de la salida se le disminuirá a la cantidad
        existente de cada uno de su ingrediente
        4. Cada salida debe registrar el costo del producto y el precio de la venta
        5. El costo de los productos compuestos corresponden al costo de sus ingredientes por la
        cantidad de estos
         */
        [Test]
        public void PuedoRegistrarProductosdeSalidadSimple()
        {

            #region DADO EL RESTAURANTE TIENE VENTA DE  PRODUCTOS DE VENTA DIRECTA,COMO SE TIENEN REGISTRADO 3 GASEOSAS 
            var producto = new ProductoSimple(nombre: "Gaseosa", costo: 2000, precio: 5000);
            var yogur = new ProductoSimple(nombre: "Yogur", costo: 1000, precio: 3000);
            var agua = new ProductoSimple(nombre: "Agua", costo: 1000, precio: 2000);
            int cantidadEntrada = 3;
            producto.EntradaProductos(producto: producto, cantidad: cantidadEntrada);
            yogur.EntradaProductos(producto: yogur, cantidad: cantidadEntrada);
            agua.EntradaProductos(producto: agua, cantidad: cantidadEntrada);
            #endregion
            #region CUANDO se solicited la venta de 2 gaseosa por parte de un huespede
            var huespede = 1055;
            int cantidadSalida = 2;
            string respuesta = producto.SalidadeProductosSimple(producto: producto, cantidad: cantidadSalida, huespede: huespede);
            #endregion
            #region ENTONCES  el sistema registrara la salida del producto en el inventario y disminuira la cantidad del mismo y mostrar un mensaje "Su Nueva cantidad de Gaseosa es de 1"

            Assert.AreEqual("Su Nueva cantidad de Gaseosa es de 1", respuesta);
            #endregion

        }
        /*
         * HU1. SALIDA DE PRODUCTO (3.5)
        COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
        CRITERIOS DE ACEPTACIÓN
        1. La cantidad de la salida de debe ser mayor a 0
        3. En caso de un producto compuesto la cantidad de la salida se le disminuirá a la cantidad
        existente de cada uno de su ingrediente
        4. Cada salida debe registrar el costo del producto y el precio de la venta
        5. El costo de los productos compuestos corresponden al costo de sus ingredientes por la
        cantidad de estos
         */
        [Test]
        public void PuedoRegistrarProductosdeSalidadCompuesta()
        {
            /*
                         * un perro sencillo (ingredientes: un pan para perros, una salchicha, una lámina de queso)
            precio: 5.000. costo: calculado: 3.000, utilidad: precio - costo
             */

            #region DADO EL RESTAURANTE TIENE VENTA DE  PRODUCTOS DE VENTA INDIRECTA Que nesecitan transfotmacion 
            var panPerro = new Producto(nombre: "Salchica", costo: 1000, ventaDirecta: false);
            var salchicha = new Producto(nombre: "PanPerro", costo: 1000, ventaDirecta: false);
            var laminadequeso = new Producto(nombre: "LaminaQueso", costo: 1000, ventaDirecta: false);
            int cantidadEntrada = 3;
            panPerro.EntradaProductos(producto: laminadequeso, cantidad: cantidadEntrada);
            salchicha.EntradaProductos(producto: panPerro, cantidad: cantidadEntrada);
            laminadequeso.EntradaProductos(producto: salchicha, cantidad: cantidadEntrada);
            List<Ingrediente> ingredientesPerro = new List<Ingrediente>();
            ingredientesPerro.Add(new Ingrediente(panPerro.Nombre, 1));
            ingredientesPerro.Add(new Ingrediente(salchicha.Nombre, 1));
            ingredientesPerro.Add(new Ingrediente(laminadequeso.Nombre, 1));
            #endregion
            #region CUANDO se solicited la venta de tres perro Sencillos
            var huespede = 1055;
            var perroSencillo = new ProductoCompuesto(nombre: "PerroSencillo", precio: 5000, ingredientes: ingredientesPerro);

            int cantidadSalida = 3;
            string respuesta = perroSencillo.SalidadeProductosCompuesto(producto: perroSencillo, cantidad: cantidadSalida, huespede: huespede);
            #endregion
            #region ENTONCES la cantidad de la salida se le disminuirá a la cantidad existente de cada uno de su ingrediente y se mostrara el mensaje utilidad  La utilidad de PerroSencillo es de: $ 6.000,00
            Assert.AreEqual($"La utilidad de PerroSencillo es de: $ 6.000,00", respuesta);
            #endregion

        }
        [Test]
        public void PuedoRegistrarProductosdeSalidadCompuestaIncorrecta()
        {
            /*
                         * un perro sencillo (ingredientes: un pan para perros, una salchicha, una lámina de queso)
            precio: 5.000. costo: calculado: 3.000, utilidad: precio - costo
             */

            #region DADO EL RESTAURANTE TIENE VENTA DE  PRODUCTOS DE VENTA INDIRECTA Que nesecitan transfotmacion si solo se tiene uno de cada uno de productos
            var panPerro = new Producto(nombre: "Salchica", costo: 1000, ventaDirecta: false);
            var salchicha = new Producto(nombre: "PanPerro", costo: 1000, ventaDirecta: false);
            var laminadequeso = new Producto(nombre: "LaminaQueso", costo: 1000, ventaDirecta: false);
            int cantidadEntrada = 1;
            panPerro.EntradaProductos(producto: laminadequeso, cantidad: cantidadEntrada);
            salchicha.EntradaProductos(producto: panPerro, cantidad: cantidadEntrada);
            laminadequeso.EntradaProductos(producto: salchicha, cantidad: cantidadEntrada);
            List<Ingrediente> ingredientesPerro = new List<Ingrediente>();
            ingredientesPerro.Add(new Ingrediente(panPerro.Nombre, 1));
            ingredientesPerro.Add(new Ingrediente(salchicha.Nombre, 1));
            ingredientesPerro.Add(new Ingrediente(laminadequeso.Nombre, 1));
            #endregion
            #region CUANDO se solicited la venta de tres perro Sencillos
            var huespede = 1055;
            var perroSencillo = new ProductoCompuesto(nombre: "PerroSencillo", precio: 5000, ingredientes: ingredientesPerro);

            int cantidadSalida = 3;
            string respuesta = perroSencillo.SalidadeProductosCompuesto(producto: perroSencillo, cantidad: cantidadSalida, huespede: huespede);
            #endregion
            #region ENTONCES la cantidad de la salida se le disminuirá a la cantidad existente de cada uno de su ingrediente y se mostrara el mensaje utilidad  "No existe la Cantidad de productos suficientes para la venta"
            Assert.AreEqual($"No existe la Cantidad de productos suficientes para la venta", respuesta);
            #endregion

        }
    }
    internal class VentaHuespede
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

    internal class Producto
    {
        public string Nombre { get; private set; }
        public decimal Costo { get; private set; }
        public bool VentaDirecta { get; private set; }
        public int Cantidad { get; set; }
        private static List<Producto> _productos { get; set; }

        protected List<VentaHuespede> _ventaHuespede;


        public Producto(string nombre, decimal costo, bool ventaDirecta)
        {
            Nombre = nombre;
            Costo = costo;
            VentaDirecta = ventaDirecta;
            _ventaHuespede = new List<VentaHuespede>();
            _productos = new List<Producto>();

        }

        public IReadOnlyCollection<VentaHuespede> VentaHuespedes => _ventaHuespede.AsReadOnly();
        public IReadOnlyCollection<Producto> Productos => _productos.AsReadOnly();
        internal virtual string EntradaProductos(Producto producto, int cantidad)
        {

            if (cantidad >= 0)
            {
                producto.Cantidad += cantidad;
                _productos.Add(producto);
                _productos.ToList();
                return $"Su Nueva cantidad de {Nombre} es de {producto.Cantidad}";
            }
            throw new NotImplementedException();
        }
        public virtual void DisminuirCantidadProducto(string nombre, int cantidad)
        {
            if (cantidad > 0)
            {
                foreach (Producto producto in _productos)
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
        public virtual void DisminuirCantidadProductoCompuesto(string nombre, int cantidad, int cantidadPedido)
        {

            if (cantidad > 0 && cantidadPedido > 0)
            {
                foreach (Producto producto in _productos.ToList())
                {
                    if (producto.Nombre.Equals(nombre))
                    {
                        if (producto.Cantidad >= (cantidad * cantidadPedido))
                        {
                            producto.Cantidad -= (cantidad * cantidadPedido);

                        }
                    }
                }
            }

        }
    }
    internal class ProductoSimple : Producto
    {

        public decimal Precio { get; private set; }
        public decimal Utilidad { get => Cantidad * (Precio - Costo); }


        public ProductoSimple(string nombre, decimal costo, decimal precio) : base(nombre, costo, true)
        {

            Precio = precio;
        }

        internal string SalidadeProductosSimple(ProductoSimple producto, int cantidad, int huespede)
        {
            if (cantidad >= 0)
            {

                DisminuirCantidadProducto(producto.Nombre, cantidad);
                _ventaHuespede.Add(new VentaHuespede(producto: this, huespede: huespede, venta: producto.Precio * Cantidad));
                return $"Su Nueva cantidad de {Nombre} es de {Cantidad}";



            }
            throw new NotImplementedException();
        }

    }
    internal class ProductoCompuesto : Producto
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
            for (int i = 0; i < Productos.LongCount(); i++)
            {
                for (int j = 0; j < ingredientes.LongCount(); j++)
                {
                    if (Productos.ToList()[i].Nombre.Equals(ingredientes[j].Nombre))
                    {
                        sumaCostos = sumaCostos + Productos.ToList()[i].Costo;
                    }
                }

            }
            return sumaCostos;
        }



        internal string SalidadeProductosCompuesto(ProductoCompuesto producto, int cantidad, int huespede)
        {
            if (cantidad >= 0)
            {

                if (ValidarExistencia(producto))
                {
                    _ventaHuespede.Add(new VentaHuespede(producto: this, huespede: huespede, venta: producto.Precio * cantidad));
                    Cantidad = cantidad;
                    return $"La utilidad de {Nombre} es de: {Utilidad:c2}";
                }
                if (!ValidarExistencia(producto))
                {
                    return "No existe la Cantidad de productos suficientes para la venta";
                }

            }
            throw new NotImplementedException();
        }
        internal void SalidadeProductos(List<Ingrediente> ingredientes, int cantidad)
        {

            for (int i = 0; i < ingredientes.LongCount(); i++)
            {
                DisminuirCantidadProductoCompuesto(ingredientes[i].Nombre, ingredientes[i].Cantidad, cantidad);

            }


        }
        internal bool ValidarExistencia(ProductoCompuesto producto)
        {
            int validador = 0;
            for (int i = 0; i < Productos.Count(); i++)
            {
                for (int j = 0; j < producto.Ingredientes.Count(); j++)
                {
                    if (Productos.ToList()[i].Nombre.Equals(producto.Ingredientes[j].Nombre))
                    {
                        if (Productos.ToList()[i].Cantidad > producto.Ingredientes[j].Cantidad)
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
    internal class Ingrediente
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
