using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NPOI.SS.Util;
using SNTSS_API.DTO;
using SNTSS_API.Models;

namespace SNTSS_API.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : Controller
    {
        private readonly SNTSS26Context _context;
        private readonly IMapper _mapper;
        private readonly string _con;
        public LogsController(SNTSS26Context contex, IMapper mapper, IConfiguration conf)
        {
            this._context = contex;
            this._mapper = mapper;
            this._con = conf["ConnectionStrings:SqlServer"];
        }

        [HttpGet]
        public async Task<ActionResult> GetLogs()
        {
            var date = new DateTime();
            var listLogs = await this._context.Logs.ToListAsync();
            return Ok(new
            {
                success = true,
                message = "Consulta de Logs",
                result = listLogs
            });
        }

        [HttpPost]
        public async Task<ActionResult> PostLogs([FromBody] LogsDTO log)
        {
            try
            {
                string time = DateTime.UtcNow.ToString("yyyy/MM/dd HH:MM:ss");
                using (SqlConnection sql = new SqlConnection(this._con))
                {
                    string commando = @"insert into logs (fecha_logs,type_logs,user_logs,descripcion) values ('" + time +"','"+log.TypeLogs+"','"+log.UserLogs+"','"+log.Descripcion+"');";

                    sql.Open();

                    using (SqlCommand cmd = new SqlCommand(commando, sql))
                    {
                         cmd.ExecuteNonQuery();
                    }
                }

                return Ok(new
                {
                    success = true,
                    message = "Logs creado con exito",
                    result = ""
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "error de log",
                    result = ""
                });
            }

        }

    }
}
