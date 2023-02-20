using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace SNTSS_API.Utilitys
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Token no valido",
                        result = ""
                    };
                }

                var id = int.Parse(identity.Claims.FirstOrDefault(x => x.Type == "id")!.Value);

                return new
                {
                    success = true,
                    message = "exito",
                    result = id,
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Credenciales incorrectas" + ex.Message,
                    result = ""
                };
            }
        }
    }
}
