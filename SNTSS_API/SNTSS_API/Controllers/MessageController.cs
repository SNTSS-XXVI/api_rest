using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SNTSS_API.DTO;
using SNTSS_API.Models;
using SNTSS_API.Utilitys;
using System.Data;
using System.Security.Claims;

namespace SNTSS_API.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class MessageController : Controller
    {
        private readonly SNTSS26Context _context;
        private readonly IMapper _mapper;
        private readonly string _connection;
        public MessageController(SNTSS26Context context, IMapper mapper, IConfiguration config)
        {
            this._context = context;
            this._mapper = mapper;
            this._connection = config["ConnectionStrings:SqlServer"];
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<dynamic>> GetMessage(int id)
        {
            bool exists = await this._context.Users.AnyAsync(x => x.IdUsers == id);

            if (exists)
            {
                try
                {
                    var usermessagesAdmin = await this._context.MessageHasUsers.Where(m => m.UserMessageHasUser == 22).ToListAsync();
                    var usermessagesUser = await this._context.MessageHasUsers.Where(m => m.UserMessageHasUser == id).ToListAsync();
                    var usermessages = usermessagesUser.Union(usermessagesAdmin);

                    var messages = await this._context.MessageTs.ToListAsync();

                    var messageRelationship = (from m in messages
                                               join userM in usermessages on
                                               m.IdMessageT equals userM.MessageMessageHasUser
                                               select
                                                    new
                                                    {
                                                        m.TitleMessageT,
                                                        m.DateMessageT,
                                                        m.ContenidoMessageT
                                                    }).ToList();
                    return Ok(messageRelationship);
                }
                catch (Exception ex)
                {
                    return BadRequest(
                            new
                            {
                                success = false,
                                message = "error:" + ex.Message,
                                result = ""
                            }
                        );
                }
            }
            else
            {
                return NotFound(new
                {
                    success = false,
                    message = "El usuario no existe",
                    result = ""
                });
            }
        }

        [HttpGet("allusers")]
        public async Task<ActionResult<dynamic>> GetMessage()
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

            using (SqlConnection sql = new SqlConnection(this._connection))
            {

                List<MessageAllDTO> listObj = new List<MessageAllDTO>();
                var imgD = new Download();

                string query = @"
                            select * from (select ROW_NUMBER() OVER (
	                        partition by s.matricula_users
	                        ORDER BY m.date_messageT desc
                           ) rowN, s.matricula_users,m.title_messageT,m.date_messageT,m.contenido_messageT,s.rol_users, s.name_users,s.id_users
	                        from messageT m join message_has_user r on m.id_messageT = r.message_message_has_user join users s 
                            on r.user_message_has_user = s.id_users) as o where o.rowN <= 1
                ";
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(query, sql))
                {
                    try
                    {
                        SqlDataReader readerQuery = cmd.ExecuteReader();
                        while (readerQuery.Read())
                        {
                            Console.WriteLine(readerQuery.GetValue(2).ToString());
                            var obj = new MessageAllDTO()
                            {
                                Matricula = readerQuery.GetValue(1).ToString()!,
                                Message = readerQuery.GetValue(4).ToString()!,
                                Title = readerQuery.GetValue(2).ToString()!,
                                Fecha = readerQuery.GetValue(3).ToString()!,
                                nombre = readerQuery.GetValue(6).ToString()!,
                                rol = (int)readerQuery.GetValue(5),
                                id_user = (int)readerQuery.GetValue(7),
                            };
                            listObj.Add(obj);
                        }
                        return new
                        {
                            success = true,
                            message = "Lista de mensajes",
                            result = listObj
                        };
                    }
                    catch (Exception e)
                    {
                        return new
                        {
                            success = false,
                            message = "Error de servidor",
                            result = e.Message
                        };
                    }
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> PostMessage([FromBody] PostMessage message)
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

            if (message.title != "")
            {
                try
                {
                    MessageT m = new MessageT
                    {
                        TitleMessageT = message.title,
                        ContenidoMessageT = message.menssage,
                        DateMessageT = DateTime.Now,
                    };

                    this._context.MessageTs.Add(m);
                    await this._context.SaveChangesAsync();

                    MessageHasUser mr = new MessageHasUser
                    {
                        MessageMessageHasUser = m.IdMessageT,
                        UserMessageHasUser = message.iduser,
                    };

                    this._context.MessageHasUsers.Add(mr);
                    await this._context.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(
                      new
                      {
                          success = false,
                          message = "no tienes permisos de administrador",
                          result = ""
                      }
                  );
                }
            }
            else
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
        }

        [HttpPost("{id2:int}")]
        public async Task<ActionResult<dynamic>> PostPrueba(int id2, [FromBody] PostMessage body)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.ValidarToken(identity!);

            if (!rToken.success) return NotFound(rToken);

            int id = (int)rToken.result;

            var user = this._context.Users.FirstOrDefault(x => x.IdUsers == id).RolUsers;
            var rolName = this._context.Rols.Where(r => r.IdRol == user).FirstOrDefault();

            if (rolName.NameRol != "ADMINISTRADOR")
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

            return Ok(
                 new
                 {
                     success = true,
                     message = "exito",
                     result = body
                 });
        }
    }
}
