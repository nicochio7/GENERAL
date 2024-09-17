using Practica01.Dominio;
using Practica01.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Datos
{
    public class DetallesFacturaRepositoryADO : IDetallesFacturaRepository
    {
        private SqlConnection _conn;
        private SqlTransaction t;
        public bool Delete(int id)
        {
            
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nro_factura", id));
            int rows = DataHelper.GetInstance().ExecuteSPDML("sp_Delete_DetalleFacturas", parametros);
            return rows == 1;
        }

        public List<DetallesFactura> GetAll()
        {
            List<DetallesFactura> lst = new List<DetallesFactura>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("sp_Consult_DetalleFacturas");
            foreach (DataRow r in dt.Rows) 
            {
                DetallesFactura d = new DetallesFactura();
                d.Factura.NroFactura = (int)r["nro_factura"];
                d.Articulo.id = (int)r["articulo"];
                d.cantidad = (int)r["cantidad"];
                d.precio_venta = (double)r["pre_venta"];
                d.importe = (double)r["importe"];
                lst.Add(d);
            }
            return lst;
        }

       
        public DetallesFactura GetById(int id)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_detalle", id));
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("sp_Consult_DetalleFacturasID" ,parametros);
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow r = dt.Rows[0];
                Facturas nro_factura = (Facturas)r["nro_factura"];
                Articulos articulo = (Articulos)r["articulo"];
                int cantidad = (int)r["cantidad"];
                double pre_venta = (int)r["pre_venta"];
                double importe = (double)r["importe"];

                 DetallesFactura odetalle = new DetallesFactura()
                {
                     Factura = nro_factura,
                     Articulo= articulo,
                     cantidad = cantidad,
                     importe = importe,
                     precio_venta = pre_venta
                 };
                return odetalle;
            }

            return null;
        }

        public bool Save(DetallesFactura odetalle)
        {
            bool result = true;
            string query = "sp_InsertDetalles";

            try
            {
                if (odetalle != null)
                {
                    _conn.Open();
                    t=_conn.BeginTransaction();
                    var cmd = new SqlCommand(query, _conn,t);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nrofactura", odetalle.Factura.NroFactura);
                    cmd.Parameters.AddWithValue("@articulo", odetalle.Articulo.id);
                    cmd.Parameters.AddWithValue("@cantidad", odetalle.cantidad);
                    cmd.Parameters.AddWithValue("@pre_venta", odetalle.precio_venta);
                    SqlParameter param = new SqlParameter("@id_detalle" , SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    result = cmd.ExecuteNonQuery() == 1; 
                }
                t.Commit();
            }
            catch (SqlException sqlEx)
            {
                if (t != null)
                {
                    t.Rollback();
                    result = false;
                }
            }
            finally
            {
                if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return result;
        }
        public List<DetallesFactura> ConsolidarDetalles(List<DetallesFactura> det)
        {
            Dictionary<int,DetallesFactura> detallesconsolidados = new Dictionary<int, DetallesFactura> ();

            foreach (var item in det)
            {
                if (detallesconsolidados.ContainsKey(item.Articulo.id))
                {
                    detallesconsolidados[item.Articulo.id].cantidad += item.cantidad;
                }
                else
                {
                    detallesconsolidados.Add(item.Articulo.id, item);
                }
            }
            return detallesconsolidados.Values.ToList();
        }
        
    }
}
