using Microsoft.AspNetCore.Mvc;

namespace MiPrimeraApi2.Controllers
{
    [ApiController] 
    [Route("[controller]")]
    public class InicioSesionController : ControllerBase
    {
        [HttpGet("{nombreUsuario}/{contraseña}")]
        public String InicioSesion(String nombreUsuario, String contraseña)
        {            
            Usuario usuario = new Usuario();

            usuario = UsuarioHandler.InicioSesion(nombreUsuario, contraseña);

            if (usuario.NombreUsuario == null)
            {
                 return "Usuario o contraseña invalidos";                  
            }
            else
            {
                return "El Usuario " + usuario.NombreUsuario + " ha iniciado sesion.";
            }
        }
    }
}
