using Microsoft.AspNetCore.Mvc;
using Practica01.Servicios;
using Practica01.Dominio;
using System.Security.Cryptography.X509Certificates;


namespace Practica02.Controllers
{   [Route("api/[controller]")]
        [ApiController]

    public class ArticulosController : Controller
    {
        private ArticuloService servicio;

        public ArticulosController()
        {
            servicio = new ArticuloService();

        }
        [HttpPost("Delete Articulo")]
        public IActionResult DeleteArticulo(int art)
        {
            try
            {
                if (art == null)
                {
                    return BadRequest("NO SE PUDO ELIMINAR EL ARTICULO ");
                }
                return Ok(servicio.delete(art));
            }
            catch (Exception)
            {
                
                throw;
            } 
            
        }
        [HttpGet("GetAll Articulos")]
        public IActionResult GetArticulo()
        {
            try
            {
                if (servicio.GetArticulos().Count == 0)
                {
                    return BadRequest("No hay articulos ingrwesados ");
                }
                return Ok(servicio.GetArticulos());
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpPost("Save Articulo")]
        public IActionResult PutArticulo(Articulos articulo)
        {
            try
            {
                if (articulo == null)
                {
                    return BadRequest("No se ha podido guardar el articulo");

                }
                return Ok(servicio.Save(articulo));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("GetById Articulos")]
        public IActionResult GetByIDArticulos(int id)
        {
            try
            {
                if( id<0)
                {
                    return BadRequest("id no aceptado");
                }
                return Ok(servicio.getByID(id));
            }
            catch (Exception)
            {

                throw;
            }
        }











    }
}
