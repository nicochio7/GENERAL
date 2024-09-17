using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Dominio
{
    public class Facturas
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int FormaPago { get; set; }
        public int ClienteId { get; set; }


    }
}
