using ActividadPosParcial.Dominio;
using ActividadPosParcial.Dominio.Carpeta;
using NUnit.Framework;
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
            var producto = new ProductoSimple(nombre: "Gaseosa", costo: 2000, precio: 5000, ventaDirecta:true);
            #endregion
            #region CUANDO registre 3 gaseosa
            int cantidad = 3;
            string respuesta = producto.EntradaProductos( cantidad: cantidad);
            #endregion
            #region ENTONCES  el sistema registrara el producto en el inventario y adicionara la cantidad del mismo 
            Assert.AreEqual("Su Nueva cantidad de Gaseosa es de 3", respuesta);
            #endregion

        }

        /*
         HU1. ENTRADA DE PRODUCTO (1.5)
        COMO USUARIO QUIERO REGISTRAR LA ENTRADA PRODUCTOS
        CRITERIOS DE ACEPTACIÓN
         1. La cantidad de la entrada de debe ser mayor a 0
         */
        [Test]
        public void NoPuedoRegistrarProductosdeEntrada()
        {

            #region DADO EL RESTAURANTE TIENE PRODUCTO DE GASEODA DE LITRO CON UN PRECIO DE 5000 Y UN COSTO 2000 Y NO NECESITA PREPARACION
            var producto = new ProductoSimple(nombre: "Gaseosa", costo: 2000, precio: 5000,ventaDirecta:true);
            #endregion
            #region CUANDO intente registrar -1 gaseosa
            int cantidad = -1;
            string respuesta = producto.EntradaProductos( cantidad: cantidad);
            #endregion
            #region ENTONCES  el sistema no registrara el producto en el inventario y presentará el mensaje "La cantidad debe ser mayor a 0" 
            Assert.AreEqual("La cantidad debe ser mayor a 0", respuesta);
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
            var producto = new ProductoSimple(nombre: "Gaseosa", costo: 2000, precio: 5000,ventaDirecta:true);
            var yogur = new ProductoSimple(nombre: "Yogur", costo: 1000, precio: 3000, ventaDirecta:true);
            var agua = new ProductoSimple(nombre: "Agua", costo: 1000, precio: 2000,ventaDirecta:true);
            int cantidadEntrada = 3;
            producto.EntradaProductos( cantidad: cantidadEntrada);
            yogur.EntradaProductos(cantidad: cantidadEntrada);
            agua.EntradaProductos( cantidad: cantidadEntrada);
            #endregion
            #region CUANDO se solicited la venta de 2 gaseosa por parte de un huespede
            var huespede = 1055;
            int cantidadSalida = 2;
            string respuesta = producto.SalidadeProductos(cantidad: cantidadSalida, huespede: huespede);
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
         */
        [Test]
        public void NoPuedoRegistrarProductosdeSalidadSimple()
        {

            #region DADO EL RESTAURANTE TIENE VENTA DE  PRODUCTOS DE VENTA DIRECTA, COMO SE TIENEN REGISTRADO 3 GASEOSAS 
            var producto = new ProductoSimple(nombre: "Gaseosa", costo: 2000, precio: 5000,ventaDirecta:true);
            var yogur = new ProductoSimple(nombre: "Yogur", costo: 1000, precio: 3000,ventaDirecta:true);
            var agua = new ProductoSimple(nombre: "Agua", costo: 1000, precio: 2000,ventaDirecta:true);
            int cantidadEntrada = 3;
            producto.EntradaProductos( cantidad: cantidadEntrada);
            yogur.EntradaProductos( cantidad: cantidadEntrada);
            agua.EntradaProductos( cantidad: cantidadEntrada);
            #endregion
            #region CUANDO se solicite la venta de -1 gaseosa por parte de un huespede
            var huespede = 1055;
            int cantidadSalida = -1;
            string respuesta = producto.SalidadeProductos(cantidad: cantidadSalida, huespede: huespede);
            #endregion
            #region ENTONCES  el sistema no registrara la salida del producto en el inventario y mostrará un mensaje "La cantidad solicitada es incorrecta"

            Assert.AreEqual("La cantidad solicitada es incorrecta", respuesta);
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
            var panPerro = new ProductoSimple(nombre: "Salchica", costo: 1000, ventaDirecta: false, precio: 0);
            var salchicha = new ProductoSimple(nombre: "PanPerro", costo: 1000, ventaDirecta: false, precio:0);
            var laminadequeso = new ProductoSimple(nombre: "LaminaQueso", costo: 1000, ventaDirecta: false, precio: 0);
            int cantidadEntrada = 3;
            panPerro.EntradaProductos( cantidad: cantidadEntrada);
            salchicha.EntradaProductos(cantidad: cantidadEntrada);
            laminadequeso.EntradaProductos( cantidad: cantidadEntrada);
            List<Ingrediente> ingredientesPerro = new List<Ingrediente>();
            ingredientesPerro.Add(new Ingrediente(panPerro.Nombre, 1));
            ingredientesPerro.Add(new Ingrediente(salchicha.Nombre, 1));
            ingredientesPerro.Add(new Ingrediente(laminadequeso.Nombre, 1));
            #endregion
            #region CUANDO se solicited la venta de tres perro Sencillos
            var huespede = 1055;
            var perroSencillo = new ProductoCompuesto(nombre: "PerroSencillo", precio: 5000, ingredientes: ingredientesPerro);

            int cantidadSalida = 3;
            string respuesta = perroSencillo.SalidadeProductos( cantidad: cantidadSalida, huespede: huespede);
            #endregion
            #region ENTONCES la cantidad de la salida se le disminuirá a la cantidad existente de cada uno de su ingrediente y se mostrara el mensaje utilidad  La utilidad de PerroSencillo es de: $ 6.000,00
            Assert.AreEqual($"La utilidad de PerroSencillo es de: 6000", respuesta);
            #endregion

        }
        [Test]
        public void PuedoRegistrarProductosdeSalidadCompuestaIncorrecta()
        {
            /*
                         * un perro sencillo (ingredientes: un pan para perros, una salchicha, una lámina de queso)
            precio: 5.000. costo: calculado: 3.000, utilidad: precio - costo
             */

            #region DADO EL RESTAURANTE TIENE VENTA DE  PRODUCTOS DE VENTA INDIRECTA Que nesecitan transfotmacion si solo se tiene 4 de cada uno de productos

            var panPerro = new ProductoSimple (nombre: "Salchica", costo: 1000, ventaDirecta: false,precio:0);
            var salchicha = new ProductoSimple(nombre: "PanPerro", costo: 1000, ventaDirecta: false,precio:0);
            var laminadequeso = new ProductoSimple(nombre: "LaminaQueso", costo: 1000, ventaDirecta: false, precio: 0);
            int cantidadEntrada = 1;
            panPerro.EntradaProductos( cantidad: cantidadEntrada);
            salchicha.EntradaProductos( cantidad: cantidadEntrada);
            laminadequeso.EntradaProductos( cantidad: cantidadEntrada);
            List<Ingrediente> ingredientesPerro = new List<Ingrediente>();
            ingredientesPerro.Add(new Ingrediente(panPerro.Nombre, 1));
            ingredientesPerro.Add(new Ingrediente(salchicha.Nombre, 1));
            ingredientesPerro.Add(new Ingrediente(laminadequeso.Nombre, 1));
            #endregion
            #region CUANDO se solicited la venta de 5 perro Sencillos
            var huespede = 1055;
            var perroSencillo = new ProductoCompuesto(nombre: "PerroSencillo", precio: 5000, ingredientes: ingredientesPerro);

            int cantidadSalida = 5;
            string respuesta = perroSencillo.SalidadeProductos( cantidad: cantidadSalida, huespede: huespede);
            #endregion
            #region ENTONCES  se mostrara el mensaje   "No existe la Cantidad de productos suficientes para la venta"
            Assert.AreEqual($"No existe la Cantidad de productos suficientes para la venta", respuesta);
            #endregion

        }
    }
    

    
    
    
    

    
}
