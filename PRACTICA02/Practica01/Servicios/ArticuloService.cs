using Practica01.Datos;
using Practica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Servicios
{
    public class ArticuloService
    {
        private IArticulosRepository _repository;
        public ArticuloService()
        {
            _repository = new ArticulosRepositoryADO();
        }

        public List<Articulos> GetArticulos()
        {
            return _repository.GetAll();
        }
        public bool Save(Articulos a)
        {
            return _repository.Save(a);
        }

        public Articulos getByID(int id)
        {
            return _repository.GetById(id);
        }

        public bool delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
