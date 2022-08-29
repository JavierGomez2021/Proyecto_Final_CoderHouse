using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi2.Controllers.DTO;

namespace MiPrimeraApi2.Controllers
{
    [ApiController] 
    [Route("[controller]")]
    public class UsuarioController : ControllerBase 
    {
        [HttpGet(Name = "GetUsuario")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios();         
        }

        [HttpGet("{nombreUsuario}")]
        public Usuario GetUsuarioPorNombreUsuario(string nombreUsuario)
        {
            return UsuarioHandler.GetUsuarioPorNombreUsuario(nombreUsuario);
        }

        [HttpDelete]
        public bool EliminarUsuario([FromBody] int id)
        {
            return UsuarioHandler.EliminarUsuario(id);
        }

        [HttpPut]
        public string ModificarUsuario([FromBody] PutUsuario usuario)
        {
            bool datosUsuarioValidos = UsuarioHandler.ValidarDatosUsuario(new Usuario
            {
                Nombre        = usuario.Nombre,
                Apellido      = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña    = usuario.Contraseña,
                Mail          = usuario.Mail
            });

            bool nombreUsuarioExistente = UsuarioHandler.ValidarNombreUsuarioExistente(usuario.NombreUsuario);

            if (datosUsuarioValidos == true && nombreUsuarioExistente == false)
            {
                bool resultadoModificarUsuario = false;

                resultadoModificarUsuario = UsuarioHandler.ModificarUsuario(new Usuario
                {
                    Id            = usuario.Id,
                    Nombre        = usuario.Nombre,
                    Apellido      = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña    = usuario.Contraseña,
                    Mail          = usuario.Mail
                });

                if (resultadoModificarUsuario == true)
                {
                    return "Usuario Actualizado.";
                }
                else if (resultadoModificarUsuario == false)
                {
                    return "Error en la modificacion";
                }
                else
                {
                    return "Error";
                }
            }
            else if(datosUsuarioValidos == false)
            {
                return "Alguno de los datos de usuario no es valido o esta vacio.";
            }
            else if(nombreUsuarioExistente == true)
            {
                return "El nombre de usuario ya existe, use otro nombre de usuario";
            }
            else
            {
                return "Error en la modificacion del usuario";
            }
        }

        [HttpPost]
        public string CrearUsuario([FromBody] PostUsuario usuario)
        {
            bool datosUsuarioValidos = UsuarioHandler.ValidarDatosUsuario(new Usuario
            {
                Nombre        = usuario.Nombre,
                Apellido      = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña    = usuario.Contraseña,
                Mail          = usuario.Mail
            });

            bool nombreUsuarioExistente = UsuarioHandler.ValidarNombreUsuarioExistente(usuario.NombreUsuario);

            if (datosUsuarioValidos == true && nombreUsuarioExistente == false)
            {
                bool crearUsuario = false;

                crearUsuario = UsuarioHandler.CrearUsuario(new Usuario
                {
                    Nombre        = usuario.Nombre,
                    Apellido      = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña    = usuario.Contraseña,
                    Mail          = usuario.Mail

                });

                if (crearUsuario == true)
                {
                    return "El usuario " + usuario.Nombre + ", " + usuario.Apellido + " ha sido dado de alta.";
                }
                else
                {                    
                    return "Error en alta de usuario";
                }
            }
            else if(datosUsuarioValidos == false)
            {
                return "Error en alta de Usuario. Corroborar que los datos no esten vacios";
            }
            else if(nombreUsuarioExistente == true)
            {
                return "El nombre de usuario " + usuario.NombreUsuario + " ya existe.";
            }
            else
            {
                return "Error en alta de usuario";
            }
        }
       
    }
}
