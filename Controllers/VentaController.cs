using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi2.Controllers.DTO;
using MiPrimeraApi2.Repository;

namespace MiPrimeraApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public String CargarVenta([FromBody] List<PostProductoVendido> listaProductosVendido)
        {
            int maxIdVenta = 0;

            String resultado = "";

            List<PostProductoVendido> lista = new List<PostProductoVendido>();

            Producto productoId             = new Producto();

            bool resultadoRestoStockProducto   = false;
            bool resultadoNuevaVenta           = false;
            bool getProductos                  = false;
            bool resultadoCrearProductoVendido = false;

            lista = listaProductosVendido;

            //0 - Verifico si los productos Vendidos de la lista enviada por el front existen en la tabla de productos. 
            foreach (PostProductoVendido productoVendido in lista)
            {
                getProductos = ProductoHandler.ExisteProducto(productoVendido.IdProducto);

                if (getProductos == false)
                {
                    return resultado = "El producto " + productoVendido.IdProducto + " no existe en la tabla Productos.";
                }
            }
            //0 - FIN

            //0.1 - Verifico si los productos vendidos de la lista enviada por el front tienen stock disponible.
            foreach (PostProductoVendido productoVendido in lista)
            {
                productoId = ProductoHandler.GetProductoPorId(productoVendido.IdProducto);            

                if (productoVendido.Stock > productoId.Stock)
                {                
                    return resultado = "La cantidad de unidades solicitadas " + productoVendido.Stock + " supera al stock disponible de " + productoId.Stock + " unidades del producto " + productoVendido.IdProducto;
                }
            }
            //0.1 - Fin

            //1 - Cargo en la tabla venta una nueva venta. 
            resultadoNuevaVenta = VentaHandler.NuevaVenta(new Venta
            {
                Comentarios = "Nueva venta " + DateTime.Now, 
            }) ;
            //1 - fin

            //2 - Busco el ultimo ID de venta asigando en el paso 1 para usarlo en la carga de productoVendido y mantener la relacion de las tablas.
            maxIdVenta = VentaHandler.VentaMaxId();
            //2 - fin

            //3 - Cargo en la tabla ProductoVendido la lista de productos vendidos. 
            foreach (PostProductoVendido productoVendido in lista)
            {
                //3.1 - Cargo cada producto vendido enviado por el front en la tabla productoVendido
                resultadoCrearProductoVendido = ProductoVendidoHandler.CargoProductoVendido(new ProductoVendido
                {
                    Stock      = productoVendido.Stock,
                    IdProducto = productoVendido.IdProducto,
                    //Este es el ultimo IdVenta generado
                    IdVenta    = maxIdVenta
                });
                //3.1 - fin

                //3.2 - Busco el stock en la tabla de productos con el id del productoVendido para restar el stock en la tabla Producto                
                productoId = ProductoHandler.GetProductoPorId(productoVendido.IdProducto);
                //3.2 - fin

                //3.3 - Modifico / resto el stock del producto vendido menos el stock del producto. Asumo que el stock en producto vendido
                //refiere a la cantidad de productos vendidos para esa venta. 
                resultadoRestoStockProducto = ProductoHandler.ModificarProductoStock(new Producto
                {
                    Id    = productoVendido.IdProducto,
                    Stock = productoId.Stock - productoVendido.Stock
                });
                //3.3 - fin
            }
            //3 - FIN

            //4 - Verifico si todos los metodos terminaron ok.
            if (resultadoNuevaVenta == true && resultadoCrearProductoVendido == true && resultadoRestoStockProducto == true)
            {
                resultado = "Operacion satisfactoria, Se ha cargado la venta";
            }
            else
            {
                resultado = "Operacion erronea";
            }
            //4 - fin

            return resultado;
        }

        [HttpGet]
        public List<Venta> GetVentas()
        {
            return VentaHandler.GetVentas();
        }

        [HttpDelete]
        public string DeleteVenta([FromBody] int idVenta)
        {
            bool resultadoModificarProductoStock           = false;
            bool resultadoEliminoProductoVendidoPorIdVenta = false;
            bool resultadoDeleteVenta                      = false;

            List<ProductoVendido> listaProductoVendido = new List<ProductoVendido>();

            //0 - Busco productos vendidos por Idventa
            listaProductoVendido = ProductoVendidoHandler.GetProductoVendidoPorIdVenta(idVenta);
            //0 - fin

            //1 - Recorro la lista de productos vendidos por Idventa. 
            foreach(ProductoVendido productoVendido in listaProductoVendido)
            {
                //1.1 - Otengo el stock de la tabla producto.
                Producto producto = ProductoHandler.GetProductoPorId(productoVendido.IdProducto);
                //1.1 - fin

                //1.2 - Actualizo la tabla de Producto sumado el stock vendido al stock de producto
                resultadoModificarProductoStock = ProductoHandler.ModificarProductoStock(new Producto
                {
                    Id    = productoVendido.IdProducto,
                    Stock = producto.Stock + productoVendido.Stock
                });
                //1.2 - fin
            }
            //1 - fin
            
            //2 - Elimino las Ventas de ProductoVendido
            resultadoEliminoProductoVendidoPorIdVenta = ProductoVendidoHandler.EliminoProductoVendidoPorIdVenta(idVenta);
            //2 - fin

            //3 - Elimino la venta de la tabla Venta.            
            resultadoDeleteVenta = VentaHandler.DeleteVenta(idVenta);
            //3 - fin

            //4 - Verifico lso resultados.
            if (resultadoDeleteVenta == true && resultadoEliminoProductoVendidoPorIdVenta == true && resultadoModificarProductoStock == true)
            {
                return "Venta Id: " + idVenta + " Eliminada.";
            }
            else if(resultadoDeleteVenta == false)
            {
                return "No se pudo eliminar la venta Id: " + idVenta;
            }
            else if(resultadoModificarProductoStock == false)
            {
                return "No se pudo modificar el stock: " + idVenta;
            }
            else if(resultadoEliminoProductoVendidoPorIdVenta == false)
            {
                return "No se pudo eliminar productos vendidos: " + idVenta;
            }
            else
            {
                return "Error en delete venta";
            }
            //4 - fin
        }
    }
}
