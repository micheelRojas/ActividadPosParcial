using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPosParcial.Dominio.Carpeta
{
    
    public class Inventario
    {
        private static Inventario inventario;
        public List<Producto> productos { get; set; }

        private Inventario()
        {
            productos = new List<Producto>();
        }

        public static Inventario getInventario()
        {
            if(inventario == null)
            {
                inventario = new Inventario();
            }
            return inventario;
        }

    }
}
