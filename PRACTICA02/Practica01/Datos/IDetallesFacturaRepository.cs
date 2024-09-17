using Practica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Datos
{
    public interface IDetallesFacturaRepository
    {
        List<DetallesFactura> ConsolidarDetalles(List<DetallesFactura> det);
        public bool Delete(int id);
        List<DetallesFactura> GetAll();
        public DetallesFactura GetById(int id);
        public bool Save(DetallesFactura factura);
    }
}
