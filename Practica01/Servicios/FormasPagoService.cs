using Practica01.Datos;
using Practica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Servicios
{
    public class FormasPagoService
    {
        IFormasPagoRepository _repository;
        public FormasPagoService()
        {
            _repository = new FormasPagoRepositoryADO();

        }

        public List<FormasPago> GetFormasPagos()
        {

        return _repository.GetAll(); 
        }
        
        public bool Save(FormasPago fp)
        {
            return _repository.Save(fp);
        }

        public FormasPago getByID(int id)
        {
            return _repository.GetById(id);
        }

        public bool delete(int id)
        {
            return _repository.Delete(id);
        }

    }
}

