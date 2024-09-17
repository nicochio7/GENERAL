using Practica01.Dominio;
using Practica01.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Practica01.Datos
{
    public class ArticulosRepositoryADO : IArticulosRepository
    {
        
        private SqlConnection _conn;
        private SqlTransaction t = null;
        public bool Delete(int id)
        {
            var parametros = new List<ParametrosSQL>();
            parametros.Add(new ParametrosSQL("@cod_art", id));
            int rows = DataHelper.GetInstance().ExecuteSPDML("sp_delete_articulos", parametros);
            return rows == 1;
        }

        public List<Articulos> GetAll()
        {
            List<Articulos> lst = new List<Articulos>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("sp_consult_art");
            foreach (DataRow r in dt.Rows)
            {
                Articulos a = new Articulos();
                a.id = (int)r["cod_art"];
                a.Nombre = (string)r["NOMBRE"];
                a.Marca = (int)r["MARCA"];
                a.TipoArticulo = (int)r["TIPO"];
                a.PrecioUnitario = (double)r["PRECIO"];
                lst.Add(a);
            }
            return lst;
        }


        public Articulos GetById(int id)
        {
            var parametros = new List<ParametrosSQL>();
            parametros.Add(new ParametrosSQL("@cod_art", id));
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("sp_consult_artID",parametros);
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow r = dt.Rows[0];
                string nombre = (string)r["NOMBRE"];
                int marca = (int)r["MARCA"];
                int tipo_articulo = (int)r["TIPO"];
                double pre_unitario = (double)r["PRECIO"];

                Articulos oarticulo = new Articulos()
                {
                    Nombre = nombre,
                    Marca = marca,
                    TipoArticulo = tipo_articulo,
                    PrecioUnitario = pre_unitario,
                   
                };
                return oarticulo;
            }

            return null;
        }

        public bool Save(Articulos oarticulo)
        {
            bool result = true;
            string query = "sp_insert_articulos";

            try
            {
                if (oarticulo != null)
                {
                    _conn.Open();
                    t=_conn.BeginTransaction();
                    var cmd = new SqlCommand(query, _conn,t);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", oarticulo.Nombre);
                    cmd.Parameters.AddWithValue("@tipo_art",oarticulo.TipoArticulo);
                    cmd.Parameters.AddWithValue("@marca", oarticulo.Marca);
                    cmd.Parameters.AddWithValue("@pre_unitario", oarticulo.PrecioUnitario);
                    SqlParameter param = new SqlParameter("@cod_articulo", SqlDbType.Int);
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
