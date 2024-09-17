using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Dominio
{
    public class Articulos
    { 
        public int id {  get; set; }
        public  string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Marca { get; set; }
        public int TipoArticulo { get; set; }
    }
}
