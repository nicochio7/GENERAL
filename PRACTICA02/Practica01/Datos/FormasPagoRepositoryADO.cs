using Practica01.Dominio;
using Practica01.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Datos
{
    public class FormasPagoRepositoryADO : IFormasPagoRepository
    {
        private SqlConnection _conn;
        private SqlTransaction t;
        public bool Delete(int id)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@id_forma_pago", id));
            int rows = DataHelper.GetInstance().ExecuteSPDML("sp_delete_articulos", parametros);
            return rows == 1;
        }

        public List<FormasPago> GetAll()
        {
            List<FormasPago> f = new List<FormasPago>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("sp_Consult_formaPago");
            foreach (DataRow row in dt.Rows)
            {
                FormasPago f2 = new FormasPago();
                f2.id = (int)row["id_forma_pago"];
                f2.nombre = (string)row["forma_pago"];
                f.Add(f2);
            }
            return f;
        }

        public FormasPago GetById(int id)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@cod_art", id));
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("sp_Consult_formaPagoID", parametros);
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow r = dt.Rows[0];
                string forma = (string)r["Forma_pago"];


                FormasPago oForma = new FormasPago()
                {
                    nombre = forma,


                };
                return oForma;
            }

            return null;
        }

        public bool Save(FormasPago oForma)
        {
            bool result = true;
            string query = "sp_insert_formaPago";

            try
            {
                if (oForma != null)
                {
                    _conn.Open();
                    t = _conn.BeginTransaction();
                    var cmd = new SqlCommand(query, _conn,t);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@forma_pago", oForma.nombre);
                    SqlParameter param = new SqlParameter("@id_forma_pago",SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

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


    }
}