using Practica01.Datos;
using Practica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Servicios
{
    public class FacturaService
    {
        private IFacturaRepository _repository;

        public FacturaService()
        {
            _repository = new FacturaRepositoryADO();
             
        }
        public List<Facturas> GetFacturas()
        {
            return _repository.GetAll();
        }
     

        public bool Save(Facturas fp)
        {
            return _repository.Save(fp);
        }

        public Facturas getByID(int id)
        {
            return _repository.GetById(id);
        }

        public bool delete(int id)
        {
            return _repository.Delete(id);
        }
        
    }
}
