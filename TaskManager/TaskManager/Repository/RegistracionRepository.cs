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
        
        /// <summary>
        /// Validacion del Recaptcha
        /// </summary>
        /// <param name="responseCaptcha"></param>
        /// <returns>Boolean</returns>
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
    }
}