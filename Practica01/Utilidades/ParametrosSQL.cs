using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Utilidades
{
    public class ParametrosSQL
    {
        public string nombre { get; set; }
        public object valor {  get; set; }

        public ParametrosSQL(string Nombre, object Valor)
        {
            Nombre = nombre;
            Valor = valor;
                
        }
    }
}
