using Practica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Datos
{
    public interface IFacturaRepository
    {
       
        public bool Delete(int id);
        List<Facturas> GetAll();
        Facturas GetById(int id);
        public bool Save(Facturas factura);
    }
}
