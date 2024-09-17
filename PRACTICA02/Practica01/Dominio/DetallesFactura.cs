using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Dominio
{
    public class DetallesFactura
    {
        public Articulos Articulo { get; set; }
        public int cantidad { get; set; }
        public Facturas Factura { get; set; }
        public double precio_venta {get;set;}
        public double importe { get; set; }
    }
}
