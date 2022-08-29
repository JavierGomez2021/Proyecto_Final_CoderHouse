using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi2.Repository;

namespace MiPrimeraApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("{IdUsuario}")]
        public List<ProductoVendido> GetProductosVendidos(int IdUsuario)
        {
            int idUsuarioF = IdUsuario;

            return ProductoVendidoHandler.GetProductosVendidos(idUsuarioF);
        }
    }
}
