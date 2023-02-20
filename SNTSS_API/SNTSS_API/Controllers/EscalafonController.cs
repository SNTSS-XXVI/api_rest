using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SNTSS_API.DTO;
using SNTSS_API.Models;
using SNTSS_API.Utilitys;
using SpreadsheetLight;
using System.Data;
using System.Security.Claims;
using System.Net.Mail;


namespace SNTSS_API.Controllers
{
    [ApiController]
    [Route("api/scalafon")]
    public class EscalafonController : Controller
    {
        private readonly SNTSS26Context _context;
        private readonly IMapper _mapper;
        private readonly string path = "Multimedia/Escalafon/";
        private readonly string _connection;
        public EscalafonController(SNTSS26Context context, IMapper mapper, IConfiguration config)
        {
            this._context = context;
            this._mapper = mapper;
            this._connection = config["ConnectionStrings:SqlServer"];
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ListEscalafon(int id) 
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.ValidarToken(identity!);

            if (!rToken.success) return NotFound(rToken);

            int idT = (int)rToken.result;

            var userData = this._context.Users.FirstOrDefault(x => x.IdUsers == idT);

            var rolName = this._context.Rols.Where(r => r.IdRol == userData!.RolUsers).FirstOrDefault();

            if (rolName!.NameRol == "ADMINISTRADOR")
            {
                try
                {
                    var esc = await this._context.Escalafons.Where(e => e.Matricula == id)
                                .Where(e => e.StatusEscalafon == "A").Select(x => new
                                {
                                    x.NumberEscalafon,
                                    x.DateEscalafon,
                                    DateUpdateEscalafon = x.DateUpdateEscalafon.ToShortDateString(),
                                    x.StatusEscalafon,
                                    x.CategoryEscalafon,
                                    x.GrupEscalafon,
                                    x.QualificationsEscalafon,  
                                    x.TypeHiringEscalafon
                                }).ToListAsync();

                    return Ok(
                        new
                        {
                            success = true,
                            message = "Lista de Escalafons",
                            result = esc
                        });
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Error al prcesar tu solicitud",
                        result = ex.Message
                    });
                }
            }
            else if(rolName!.NameRol == "AFILIADO")
            {
                try
                {
                    var esc = await this._context.Escalafons.Where(e => e.Matricula == id)
                                .Where(e => e.StatusEscalafon == "A").Select(x => new
                                {
                                    x.NumberEscalafon,
                                    x.DateEscalafon,
                                    DateUpdateEscalafon = x.DateUpdateEscalafon.ToShortDateString(),
                                    x.StatusEscalafon,
                                    x.CategoryEscalafon,
                                    x.GrupEscalafon,
                                    x.QualificationsEscalafon,
                                    x.TypeHiringEscalafon
                                }).ToListAsync();

                    return Ok(
                        new
                        {
                            success = true,
                            message = "Lista de Escalafons",
                            result = esc
                        });
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Error al prcesar tu solicitud",
                        result = ex.Message
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    message = "No tienes permisos para realizar esta operacion",
                    result = ""
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostEscalafon([FromForm] IFormFile file)
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
                    var tab = new escalafon_update();
                    var carga = new Upload();
                    string dir = Directory.GetCurrentDirectory() + '/';
                    var pathCombine = Path.Combine(dir, this.path, file.Name + ".xlsx");

                    string namearch = carga.UploadPictureUsers(file, file.Name + ".xlsx", this.path).ToString()!;
                    var table = tab.LoadFromExcelFile(file.Name + ".xlsx",this._connection);
                    System.IO.File.Delete(pathCombine);

                    return Ok(
                            new
                            {
                                success = true,
                                message = "Escalafon Agregado",
                                result = ""
                            }
                        );
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        succes = false,
                        message = "error al procesar la informacion",
                        result = ex.Message
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    message = "No tienes permisos para realizar esta operacion",
                    result = ""
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteEscalafon(int id)
        {
            var escalafon = await this._context.Escalafons.FirstOrDefaultAsync(x => x.IdEscalafon == id);
            this._context.Remove(escalafon!);
            await this._context.SaveChangesAsync();

            return Ok(
                    new
                    {
                        success = true,
                        message = "Escalafon eliminado",
                        result = ""
                    }
                );
        }
    }
}
