using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SNTSS_API.DTO;
using SNTSS_API.Models;
using SNTSS_API.Utilitys;
using System.Security.Claims;

namespace SNTSS_API.Controllers
{
    [ApiController]
    [Route("api/rol")]
    public class RolController : Controller
    {
        private readonly SNTSS26Context _context;
        private readonly IMapper _Mapper;
        public RolController(SNTSS26Context context, IMapper mapper)
        {
            this._context = context;
            this._Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> GetRol()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.ValidarToken(identity!);

            if (!rToken.success) return NotFound(rToken);

            int id = (int)rToken.result;

            var user = this._context.Users.FirstOrDefault(x => x.IdUsers == id)!.RolUsers;
            var rolName = this._context.Rols.Where(r => r.IdRol == user).FirstOrDefault();

            if (rolName!.NameRol != "ADMINISTRADOR")
            {
                return NotFound(
                        new
                        {
                            success = false,
                            message = "no tienes permisos de administrador",
                            result = ""
                        }
                    );
            }

            var Rol = await this._context.Rols.ToListAsync();
            // '''Se dice que Fedor Mijailovich Dostoyevsky dijo o escribió: La tolerancia llegará a tal nivel que a las personas inteligentes se les prohibirá pensar para no ofender a los idiotas2'''
            return this._Mapper.Map<List<RolDTO>>(Rol);
        }
    }
}
