using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Dominio;

namespace Practica01.Utilidades
{
    public class DataHelper
    {
        private static DataHelper? _instance;
        private readonly SqlConnection _strCnn;

        public DataHelper()
        {
           _strCnn = new SqlConnection(Properties.Resources.StrCnn);
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public DataTable ExecuteSPQuery(string sp)
        {
            DataTable dt = new DataTable();

            try
            {
                _strCnn.Open();
                var cmd = new SqlCommand(sp, _strCnn);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (SqlException ex)
            {

                throw new Exception("Error al ejecutar el sp: " + ex.Message);

            }
            finally { _strCnn.Close(); }
  
        }
        public DataTable ExecuteSPQuery(string sp, List<SqlParameter>? p)
        {
            DataTable dt = new DataTable();

            try
            {
                _strCnn.Open();
                var cmd = new SqlCommand(sp, _strCnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (p != null)
                {
                    foreach (var param in p)
                        cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                 }   
                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (SqlException ex)
            {
                dt = null;
                throw new Exception("Error al ejecutar el sp: " + ex.Message);

            }
            finally { _strCnn.Close(); }

        }
        

        public int ExecuteSPDML(string sp, List<SqlParameter>? parametros)
        {
            int rows;
            try
            {
                _strCnn.Open();
                var cmd = new SqlCommand(sp, _strCnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                }

                rows = cmd.ExecuteNonQuery();
                _strCnn.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }

            return rows;
        }

        public SqlConnection GetConnection()
        {
            return _strCnn;
        }
    }
}
