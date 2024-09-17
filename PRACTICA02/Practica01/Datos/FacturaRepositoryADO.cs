using Practica01.Dominio;
using Practica01.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Datos
{
    public class FacturaRepositoryADO : IFacturaRepository
    {
        
        private SqlConnection _conn;
        private SqlTransaction t = null;
        public FacturaRepositoryADO()
        {
            _conn = new SqlConnection(Properties.Resources.StrCnn);
        }
        public bool Delete(int id)
        {
            var parametros= new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nro_factura", id));
            int rows = DataHelper.GetInstance().ExecuteSPDML("sp_Delete_Factura", parametros);
            return rows == 1; 
        }

        public List<Facturas> GetAll()
        {
            List<Facturas> lst = new List<Facturas>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("sp_Consult_Facturas");
            foreach (DataRow item in dt.Rows) 
            { 
                Facturas f = new Facturas();
                f.NroFactura = (int)item["nro_factura"];
                f.Fecha = (DateTime)item["fecha"];
                f.FormaPago = (int)item["forma_pago"];
                f.ClienteId = (int)item["cliente"];
                lst.Add(f);


            }
            return lst;


        }

        public Facturas GetById(int id)
        {
           var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nro_factura", id));
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("sp_Consultar_FacturaID" , parametros);
            if (dt != null && dt.Rows.Count ==1)
            {
                DataRow r = dt.Rows[0];
                int nro_factura = Convert.ToInt32(r["nro_factura"]);
                DateTime fecha = (DateTime)r["fecha"];
                int forma_pago = (int)r["forma_pago"];
                int cliente = (int)r["cliente"];



                Facturas ofactura = new Facturas()
                {
                    NroFactura = nro_factura,
                    Fecha=fecha,
                    FormaPago=forma_pago,
                    ClienteId = cliente
                };
                return ofactura;
             }

            return null;
        }

        public bool Save(Facturas ofacturas)
        {
            bool result = true;
            string query = "sp_Insert_Factura";

            try
            {
                if (ofacturas != null)
                {
                    _conn.Open();
                    t = _conn.BeginTransaction();
                    var cmd = new SqlCommand(query, _conn,t);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nroFactura", ofacturas.NroFactura);
                    cmd.Parameters.AddWithValue("@idFormaPago", ofacturas.FormaPago);
                    cmd.Parameters.AddWithValue("@idCliente", ofacturas.ClienteId);
                    result = cmd.ExecuteNonQuery() == 1; 
                }
                t.Commit();
            }
            catch (SqlException sqlEx)
            {
                if(t != null)
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
    }
}
