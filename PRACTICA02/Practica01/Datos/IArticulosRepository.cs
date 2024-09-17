using Practica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Datos
{
    public interface IArticulosRepository
    {
        public bool Delete(int id);
        List<Articulos> GetAll();
        public Articulos GetById(int id);
        public bool Save(Articulos factura);
    }
}
