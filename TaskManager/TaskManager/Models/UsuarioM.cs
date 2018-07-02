using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using TaskManager.Repository;
using System.Web.SessionState;

namespace TaskManager.Models
{
    public class UsuarioM
    {
        UsuarioRepository UsuarioRepository = new UsuarioRepository();
        LoginRepository LoginRepository = new LoginRepository();

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo {0} tiene de maximo 50 caracteres.")]
        [Display(Name = "Nombre *")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo {0} tiene de maximo 50 caracteres.")]
        [Display(Name = "Apellido *")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(200, ErrorMessage = "El campo {0} tiene de maximo 200 caracteres.")]
        [EmailAddress(ErrorMessage = "Ingresar un Email Valido")]
        [Display(Name = "Email *")]
        public string Email { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{0,20}$", ErrorMessage = "El campo {0} debe contener como maximo 20 caracteres, almenos una Mayuscula, una Minuscula y un Numero")]
        [Display(Name = "Contraseña *")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Compare(otherProperty: "Contrasena", ErrorMessage = "Las Contraseñas no coinciden")]
        public string Contrasena2 { get; set; }

        [Display(Name = "Activo")]
        public int Activo { get; set; }

        [Display(Name = "Fecha de registracion")]
        public DateTime FechaRegistracion { get; set; }

        [Display(Name = "Fecha de activacion")]
        public DateTime FechaActivacion { get; set; }

        [Display(Name = "Codigo de activacion")]
        public string CodigoActivacion { get; set; }

        public List<CarpetaM> Carpetas { get; set; }

        public List<Tarea> Tarea { get; set; }

        public string ErrorRegistro;


        /// <summary>
        /// Verifica el login
        /// </summary>
        /// <returns></returns>
        public String VerificarLogin()
        {
            Usuario userInBase = UsuarioRepository.BuscarUsuarioPorEmailYPass(this);
            if(userInBase != null)
            {
                this.IdUsuario = userInBase.IdUsuario;   
            }
            return LoginRepository.VerificarLogin(userInBase);
        }

        /// <summary>
        /// Convierte el Modelo en un Usuario Entidad
        /// </summary>
        /// <returns></returns>
        public Usuario ConvertirModelo()
        {
            CarpetaM Carpetas = new CarpetaM();
            Usuario usuario = new Usuario();

            usuario.IdUsuario = this.IdUsuario;
            usuario.Activo = (Int16)this.Activo;
            usuario.Apellido = this.Apellido;
            usuario.Nombre = this.Nombre;
            usuario.Contrasenia = this.Contrasena;
            usuario.Email = this.Email;
            usuario.FechaActivacion = (DateTime?)this.FechaActivacion;
            usuario.FechaRegistracion = this.FechaRegistracion;
            usuario.CodigoActivacion = this.CodigoActivacion;

            foreach (CarpetaM carpetaModel in this.Carpetas)
            {
                usuario.Carpeta.Add(Carpetas.ConvertirModelo(carpetaModel));
            }
            return usuario;
        }


        /// <summary>
        /// Modela la Entidad usuario que recibe en un Modelo Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>UsuarioM</returns>
        public UsuarioM ModelarUsuario(Usuario usuario)
        {
            UsuarioM usuarioM = new UsuarioM();
            usuarioM.IdUsuario = usuario.IdUsuario;
            usuarioM.Nombre = usuario.Nombre;
            usuarioM.Apellido = usuario.Apellido;
            usuarioM.Email = usuario.Email;
            usuarioM.Contrasena = usuario.Contrasenia;
            usuarioM.Activo = usuario.Activo;
            if (usuario.FechaActivacion != null)
            {
                usuarioM.FechaActivacion = (DateTime)usuario.FechaActivacion;
            }
            else
            {
                DateTime myDate = DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                usuarioM.FechaActivacion = myDate;
            }

            usuarioM.FechaRegistracion = usuario.FechaRegistracion;
            usuarioM.CodigoActivacion = usuario.CodigoActivacion;

            return usuarioM;
        }


        /// <summary>
        /// Activa el usuario y genera la carpeta general en caso de que no la tenga
        /// </summary>
        public void ActivarUsuario()
        {
            DateTime LocalDate = DateTime.Now;
            string Nombre = "General";
            string Descripcion = "Carpeta por Defecto";

            this.Activo = 1;
            this.FechaActivacion = LocalDate;
            this.CodigoActivacion = GenerarCodigoActivacion();

            if (this.Carpetas != null)
            {
                if (this.Carpetas.Count != 0)
                {
                    foreach (CarpetaM carpeta in this.Carpetas)
                    {
                        if (carpeta.Nombre.Equals(Nombre))
                        {
                            return;
                        }
                    }
                }
            }
            this.Carpetas = new List<CarpetaM>();
            this.Carpetas.Add(new CarpetaM { Nombre = Nombre, Descripcion = Descripcion, FechaCreacion = LocalDate });
        }

        /// <summary>
        /// Genera un codigo de activacion Random con la forma : XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXXX
        /// </summary>
        /// <returns>Un String con el codigo de activacion</returns>
        public string GenerarCodigoActivacion()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Int32 longitud;

            longitud = 8;
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            res.Append('-');


            for (int i = 1; i <= 3; i++)
            {
                longitud = 4;
                while (0 < longitud--)
                {
                    res.Append(caracteres[rnd.Next(caracteres.Length)]);
                }
                res.Append('-');
            }

            longitud = 12;
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }

            if (usuarioRepository.codigoActivacionExiste(res.ToString()))
            {
                GenerarCodigoActivacion();
            }


            return res.ToString();


        }

        /// <summary>
        /// Guarda en variable de sesion el Usuario
        /// </summary>
        /// <param name="usuario"></param>
        public void guardarEnSesion(Usuario usuario)
        {
            UsuarioM user = ModelarUsuario(usuario);
            HttpContext.Current.Session["userLogged"] = user;
        }

        /// <summary>
        /// Cierra y vacia la Sesion actual
        /// </summary>
        public void Logout()
        {
            HttpContext.Current.Session.Abandon();
        }


        public void ModificarUsuarioEntidad(Usuario usr)
        {
            usr.Nombre = this.Nombre;
            usr.Apellido = this.Apellido;
            usr.Contrasenia = this.Contrasena;
            usr.Activo = (Int16)this.Activo;
            usr.CodigoActivacion = this.CodigoActivacion;
        }

        /// <summary>
        /// Metodo que retorna el Usuario Logueado Almacenado en la Sesion
        /// </summary>
        /// <returns>El Modelo del usuario en sesion</returns>
        public UsuarioM GetUser()
        {
            return (UsuarioM)HttpContext.Current.Session["userLogged"];
        }


        /// <summary>
        /// Metodo que Crea y setea un cookie en el sistema con el ID del usuario logueado
        /// El tiempo de expiracion de la cookie creada es 1 dia
        /// </summary>
        public void generarCookie()
        {
            HttpCookie cookie = new HttpCookie("User");

            cookie["ID"] = this.IdUsuario.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Lee la cookie creada con el ID del usuario 
        /// </summary>
        /// <returns>id del usuario</returns>
        public UsuarioM getCookie()
        {
            try
            {
                HttpCookie Cookie = HttpContext.Current.Request.Cookies.Get("User");
                var id = Cookie["ID"];

                if (id != null)
                {
                    if (id != String.Empty)
                    {
                        int idUsuario = int.Parse(id);

                        UsuarioM usuarioM = this.ModelarUsuario(UsuarioRepository.BuscarUsuarioPorId(idUsuario));

                        if (usuarioM != null) return usuarioM;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

    }
}