using Practica01.Datos;
using Practica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Servicios
{
    public class DetalleFacturasService
    {
        private IDetallesFacturaRepository _repository;
        public DetalleFacturasService()
        {
            _repository = new DetallesFacturaRepositoryADO();
        }

        public List<DetallesFactura> GetDetalle()
        {
            return _repository.GetAll();
        }
        public bool Save(DetallesFactura df)
        {
            return _repository.Save(df);
        }

        public DetallesFactura getByID(int id)
        {
            return _repository.GetById(id);
        }

        public bool delete(int id)
        {
            return _repository.Delete(id);
        }

        public List<DetallesFactura> detalle(List<DetallesFactura> det)
        {

            return _repository.ConsolidarDetalles(det);
        }


    }
}
