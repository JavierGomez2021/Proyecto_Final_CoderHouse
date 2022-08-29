using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MiPrimeraApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraerNombreController : ControllerBase
    {
        [HttpGet]
        public string TraerNombre()
        {
            return "BACKEND - SISTEMA DE GESTION - PROYECTO FINAL CODERHOUSE";
        }
    }
}
