// Crear una instancia del servicio que contiene los métodos para manejar facturas
using Practica01.Datos;
using Practica01.Dominio;
using Practica01.Servicios;

FacturaService facturaService = new FacturaService();


var oFP = new FormasPagoService();
var fp = oFP.GetFormasPagos();

var oA = new ArticuloService();
var a = oA.GetArticulos();


// Crear detalles de la factura
DetallesFactura detalle1 = new DetallesFactura { Articulo = a[0], cantidad = 2 };
DetallesFactura detalle2 = new DetallesFactura { Articulo = a[0], cantidad = 1 };
DetallesFactura detalle3 = new DetallesFactura { Articulo = a[0], cantidad = 3 };

List<DetallesFactura> detallesFactura = new List<DetallesFactura> { detalle1, detalle2, detalle3 };



// Crear una factura
Facturas factura = new Facturas
{
    NroFactura = 2,
    Fecha = DateTime.Now,
    ClienteId = 1,
    FormaPago = 1
    
};

// Agregar la factura a la base de datos
try
{
    facturaService.Save(factura);
    Console.WriteLine("Factura creada e insertada con éxito.");
}
catch (Exception ex)
{
    Console.WriteLine("Error al insertar la factura: " + ex.Message);
}

Console.WriteLine("\nPresiona cualquier tecla para salir...");
Console.ReadKey();