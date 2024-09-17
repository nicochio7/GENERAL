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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

namespace Practica01.Datos
{
    public class ArticulosRepositoryADO : IArticulosRepository
    {
        
        private SqlConnection _conn;
        private SqlTransaction t = null;
        public bool Delete(int id)
        {
            bool aux = false;
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@cod_art", id));
            
            return aux= DataHelper.GetInstance().ExecuteSPDML("sp_delete_articulos", parametros) == 1;
        }

        public List<Articulos> GetAll()
        {
            List<Articulos> lst = new List<Articulos>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("sp_consult_art");
            foreach (DataRow r in dt.Rows)
            {
                Articulos a = new Articulos();
                a.id = (int)r["COD ARTÍCULO"];
                a.Nombre = (string)r["NOMBRE"];
                a.Marca = (int)r["MARCA"];
                a.TipoArticulo = (int)r["TIPO"];
                a.PrecioUnitario = (decimal)r["PRECIO"];
                lst.Add(a);
            }
            return lst;
        }


        public Articulos GetById(int id)  
        {
            Articulos art = null;
            var parametros = new List<SqlParameter>() {  new SqlParameter("@idArt", id) };
          

            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("sp_consult_artID",parametros);
          if (dt != null && dt.Rows.Count == 1)
           {
                DataRow r = dt.Rows[0];


                art = new Articulos()
                {
                    Nombre = Convert.ToString(r["NOMBRE"]),
                    Marca = Convert.ToInt32(r["MARCA"]),
                    TipoArticulo = Convert.ToInt32(r["TIPO"]),
                    PrecioUnitario = Convert.ToDecimal(r["PRECIO"])

                };
                return art;
           }

            return null;
        }


        public bool Save(Articulos oarticulo)
        {
            bool result = false;
            string query = "sp_insert_articulos";
            List<SqlParameter> p = new List<SqlParameter>()
            {
                new SqlParameter("@nombre" , oarticulo.Nombre),
                new SqlParameter("@tipo_art" , oarticulo.TipoArticulo),
                new SqlParameter("@marca" , oarticulo.Marca),
                new SqlParameter("@pre_unitario" , oarticulo.PrecioUnitario)
            };
            return result = DataHelper.GetInstance().ExecuteSPDML(query, p)==1;
          
        }
    }
}
