using Practica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Datos
{
    public interface IFormasPagoRepository
    {
        public bool Delete(int id);
        List<FormasPago> GetAll();
        public FormasPago GetById(int id);
        public bool Save(FormasPago factura);
    }
}
