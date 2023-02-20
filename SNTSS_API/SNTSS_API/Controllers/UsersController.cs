using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SNTSS_API.DTO;
using SNTSS_API.Models;
using SNTSS_API.Utilitys;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.IO;
using Microsoft.Data.SqlClient;

namespace SNTSS_API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        public readonly SNTSS26Context _context;
        public readonly IMapper _mapper;
        public readonly IConfiguration _configuration;
        public readonly string _conexion;

        public UsersController(SNTSS26Context context, IMapper mapper, IConfiguration configuration)
        {
            this._context = context;
            this._mapper = mapper;
            this._configuration = configuration;
            this._conexion = configuration["ConnectionStrings:SqlServer"];
        }

        [HttpGet]
        public async Task<ActionResult<List<UsersDTO>>> GetUsers()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.ValidarToken(identity!);

            if (!rToken.success) return NotFound(rToken);

            int id = (int)rToken.result;

            var user = this._context.Users.FirstOrDefault(x => x.IdUsers == id)!.RolUsers;

            var rolName = this._context.Rols.Where(r => r.IdRol == user).FirstOrDefault();

            if (rolName!.NameRol == "ADMINISTRADOR")
            {
                var users = await this._context.Users.Where(x => x.IdUsers != 1).ToListAsync();

                return this._mapper.Map<List<UsersDTO>>(users);
            }
            else
            {
                return NotFound(
                        new
                        {
                            succes = false,
                            message = "No tienes permisos para emplear esta accion",
                            result = ""
                        }
                    );
            }
        }

        [HttpGet("category")]
        public async Task<ActionResult> GetCategory()
        {
            var category = await this._context.Categoria.ToListAsync();

            return Ok(category);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<dynamic>> GetEdit(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.ValidarToken(identity!);

            if (!rToken.success) return NotFound(rToken);

            int idT = (int)rToken.result;

            var user = this._context.Users.FirstOrDefault(x => x.IdUsers == idT)!.RolUsers;

            var rolName = this._context.Rols.Where(r => r.IdRol == user).FirstOrDefault();

            if (rolName!.NameRol == "ADMINISTRADOR")
            {
                var users = await this._context.Users.Where(x => x.IdUsers == id).ToListAsync();

                return Ok(
                    new
                    {
                        success = true,
                        message = "",
                        result = users
                    });
            }
            else if (rolName!.NameRol == "AFILIADO")
            {
                var users = await this._context.Users.Where(x => x.IdUsers == id).ToListAsync();

                return Ok(
                    new
                    {
                        success = true,
                        message = "",
                        result = users
                    });
            }
            else
            {
                return BadRequest(
                    new
                    {
                        succes = false,
                        message = "No tienes permisos para ejecutar esta acción",
                        result = ""
                    });
            }
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> PostUsers([FromBody] object user)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.ValidarToken(identity!);

            if (!rToken.success) return NotFound(rToken);

            int idT = (int)rToken.result;

            var userData = this._context.Users.FirstOrDefault(x => x.IdUsers == idT)!.RolUsers;

            var rolName = this._context.Rols.Where(r => r.IdRol == userData).FirstOrDefault();

            if (rolName!.NameRol == "ADMINISTRADOR")
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(user.ToString()!);

                    var User = new UsersDTO()
                    {
                        MatriculaUsers = data.matriculaUsers,
                        NameUsers = data.nameUsers,
                        PasswordUsers = data.passwordUsers,
                        RolUsers = data.rolUsers,
                        DayJobUsers = data.dayJobUsers,
                        CategoryUsers = data.categoryUsers,
                        CveAdscripcionUsers = data.cveAdscripcionUsers,
                        AdscripcionUsers = data.adscripcionUsers,
                        DirectionUsers = data.direccion,
                        PhoneUsers = data.telefono,
                        StatusUsers = data.statusUsers,
                        ShiftUsers = data.shiftUsers,
                        WorkerContractUsers = data.workerContractUsers,
                        ObservationsUsers = data.observationsUsers
                    };

                    var newUser = this._mapper.Map<User>(User);
                    this._context.Users.Add(newUser);
                    await this._context.SaveChangesAsync();

                    return Ok(newUser);
                }
                catch (Exception e)
                {
                    return new
                    {
                        success = false,
                        message = "Error:" + e.Message,
                        result = ""
                    };
                }
            }
            else
            {
                return BadRequest(
                    new
                    {
                        succes = false,
                        message = "No tienes permisos para ejecutar esta acción",
                        result = ""
                    });
            }

        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchUsers(int id, [FromBody] string query)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(this._conexion))
                {
                    string commando = @"
                               Update users Set password_users ="+ query +"where id_users ="+id;

                    sql.Open();

                    using (SqlCommand cmd = new SqlCommand(commando, sql))
                    {
                        SqlDataReader readerQuery = cmd.ExecuteReader();
                    }
                }
                return Ok(new
                {
                    success = true,
                    message = "Actualizado con exito.",
                    result = ""
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success = false,
                    message = e.Message,
                    result = ""
                });
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<dynamic>> EditUsers(int id, [FromBody] UsersDTO user)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.ValidarToken(identity!);

            if (!rToken.success) return NotFound(rToken);

            int idT = (int)rToken.result;

            var userData = this._context.Users.FirstOrDefault(x => x.IdUsers == idT)!.RolUsers;

            var rolName = this._context.Rols.Where(r => r.IdRol == userData).FirstOrDefault();

            if (rolName!.NameRol == "ADMINISTRADOR")
            {
                try
                {
                    var userUpdate = await this._context.Users.FirstOrDefaultAsync(x => x.IdUsers == id);
                    //string ruta = "C:/Users/kevin/source/repos/SNTSS/SNTSS_API/SNTSS_API/Multimedia/PictureUser/" + userUpdate!.PictureUsers;
                    //System.IO.File.Delete(ruta);

                    userUpdate = this._mapper.Map(user, userUpdate);

                    await this._context.SaveChangesAsync();

                    return Ok(
                            new
                            {
                                success = true,
                                message = "Usuario editado!",
                                result = ""
                            }
                        );
                }
                catch (Exception e)
                {
                    return BadRequest(
                            new
                            {
                                success = false,
                                message = "Error al editar usuario",
                                result = e.Message
                            }
                        );
                }
            }
            else
            {
                return BadRequest(
                   new
                   {
                       succes = false,
                       message = "No tienes permisos para ejecutar esta acción",
                       result = ""
                   });
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] object DataUsers)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(DataUsers.ToString()!);

            string user = data.user.ToString();
            string password = data.password.ToString();

            var dataUser = await this._context.Users.Where(x => x.MatriculaUsers.ToString() == user && x.PasswordUsers == password).FirstOrDefaultAsync();

            if (dataUser == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales incorrectas",
                    result = ""
                };
            }

            var rol = await this._context.Rols.Where(x => x.IdRol == dataUser.RolUsers).FirstOrDefaultAsync();

            var jwt = this._configuration.GetSection("JWT").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", dataUser.IdUsers.ToString()),
                new Claim("matricula", dataUser.MatriculaUsers.ToString()),
                new Claim("rol", rol!.NameRol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: singIn
                );

            return Ok(new
            {
                success = true,
                message = "exito",
                result = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiracion = DateTime.UtcNow.AddDays(1)
                }
            }); ;
        }

        [HttpPost("pictureuser")]
        public async Task<ActionResult<dynamic>> UploadPicture([FromForm] IFormFile picture)
        {
            var upload = new Upload();
            try
            {
                var succesPicture = upload.UploadPictureUsers(picture, picture.FileName, "Multimedia/PictureUser/");
                if (succesPicture.ToString() == "")
                {
                    return new
                    {
                        success = false,
                        message = "Error de carga de imagen",
                        result = ""
                    };
                }
                return new
                {
                    success = true,
                    message = "Imagen carga",
                    result = succesPicture,
                };
            }
            catch (Exception e)
            {
                return new
                {
                    success = false,
                    message = "Error de carga de imagen",
                    result = ""
                };
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<dynamic>> DeleteUser(int id)
        {
            try
            {
                var user = await this._context.Users.FirstOrDefaultAsync(x => x.IdUsers == id);
                //string ruta = "C:/Users/kevin/source/repos/SNTSS/SNTSS_API/SNTSS_API/Multimedia/PictureUser/" + user!.PictureUsers;
                //System.IO.File.Delete(ruta);

                this._context.Remove(user);
                await this._context.SaveChangesAsync();

                return Ok(
                        new
                        {
                            success = true,
                            message = "usuario eliminado",
                            result = ""
                        }
                    );
            }
            catch (Exception e)
            {
                return Ok(
                        new
                        {
                            success = false,
                            message = "error al eliminar imagen de usuario",
                            result = e.Message.ToString()
                        }
                    );
            }
        }
    }
}
//workstation id=SNTSS26.mssql.somee.com;packet size=4096;user id=kevin_nick_SQLLogin_1;pwd=8bnrj5cpa6;data source=SNTSS26.mssql.somee.com;persist security info=False;initial catalog=SNTSS26