using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TaskManager.Models;


namespace TaskManager.Repository
{
    public class RegistracionRepository
    {

        public bool RegistrarNewUser(UsuarioM user)
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario userInBase = usuarioRepository.buscarUsuarioPorEmail(user.Email);

            if (userInBase != null)
            {
                if (userInBase.Activo.Equals(1))
                {
                    return false;
                }
                else
                {
                    ActivarUsuario(user);
                    usuarioRepository.modificarUsuario(user);
                    return true;
                }
            }
            else
            {
                ActivarUsuario(user);
                usuarioRepository.crearUsuario(user);
                return true;
            }
        }

        public void ActivarUsuario(UsuarioM user)
        {
            DateTime LocalDate = DateTime.Now;
            string Nombre = "General";
            string Descripcion = "Carpeta por Defecto";

            user.Activo = 1;
            user.FechaActivacion = LocalDate;
            user.CodigoActivacion = GenerarCodigoActivacion();

            if (user.Carpetas != null)
            {
                if (user.Carpetas.Count != 0)
                {
                    foreach (CarpetaM carpeta in user.Carpetas)
                    {
                        if (carpeta.Nombre.Equals(Nombre))
                        {
                            return;
                        }
                    }
                }
            }
            user.Carpetas = new List<CarpetaM>();
            user.Carpetas.Add(new CarpetaM { Nombre = Nombre, Descripcion = Descripcion, FechaCreacion = LocalDate });
        }

        public string GenerarCodigoActivacion()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            Int32 longitud;

            longitud = 8;
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

        public bool IsReCaptchValid(string responseCaptcha)
        {
            var result = false;
            var captchaResponse = responseCaptcha;
            var secretKey = "6LdRMGAUAAAAAOKDGziYAtrb0KVVS83Ns5eTaTZ3";
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }

        public bool verificarCampoVacio(UsuarioM user)
        {
            if (user.Apellido != null || user.Apellido != String.Empty)
            {
                if (user.Nombre != null || user.Nombre != String.Empty)
                {
                    if (user.Email != null || user.Email != String.Empty)
                    {
                        if (user.Contrasena != null || user.Contrasena != String.Empty)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }
}