using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SNTSS_API.DTO;
using SNTSS_API.Models;
using SNTSS_API.Utilitys;
using System.Runtime.Serialization;

namespace SNTSS_API.Controllers
{
    [ApiController]
    [Route("api/media")]
    public class MediaController : Controller
    {
        private readonly SNTSS26Context _context;
        private readonly IMapper _mapper;
        public MediaController(SNTSS26Context context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }


        [HttpGet("guard")]
        public async Task<ActionResult> GetGuard()
        {
            var guard = this._context.Guards.ToList();
            return Ok(guard);
        }
        [HttpGet("dashboard")]
        public async Task<ActionResult> GetDashboard()
        {
            var imgD = new Download();
            var data = this._context.Dashboards.ToList();

            foreach (var item in data)
            {
                item.NameDashboard = "data:image/png;base64," + imgD.DownloadImg(item.NameDashboard, "Multimedia/PictureDashboard/");
            }

            return Ok(new
            {
                success = true,
                message = "Lista de Imagenes",
                result = data
            });
        }
        [HttpGet("conventions")]
        public async Task<ActionResult> GetConventions()
        {
            var imgD = new Download();
            var data = this._context.Conventions.ToList();

            foreach (var item in data)
            {
                item.PictureConventions = "data:image/png;base64," + imgD.DownloadImg(item.PictureConventions, "Multimedia/PictureConventions/");
            }

            return Ok(new
            {
                success = true,
                message = "Lista de Imagenes",
                result = data
            });
        }
        [HttpGet("convocatorias")]
        public async Task<ActionResult> GetConvocations()
        {
            var imgD = new Download();
            var data = this._context.Calls.ToList();

            foreach (var item in data)
            {
                item.PdfCalls = "data:application/pdf;base64," + imgD.DownloadImg(item.PdfCalls, "Multimedia/PdfCalls/");
            }

            return Ok(new
            {
                success = true,
                message = "Lista de Imagenes",
                result = data.OrderByDescending(x => x.IdCalls) 
            });
        }

        [HttpGet("conventions/{type}")]
        public async Task<ActionResult> GetConventionSelect(string type)
        {
            var imgD = new Download();
            var data = this._context.Conventions.Where(x => x.TypeConventions == type).OrderByDescending(x => x.IdConventions).ToList();

            foreach (var item in data)
            {
                item.PictureConventions = "data:image/png;base64," + imgD.DownloadImg(item.PictureConventions, "Multimedia/PictureConventions/");
            }

            return Ok(new
            {
                success = true,
                message = "Lista de Imagenes",
                result = data
            });
        }
        //---------------------------------------------- POST MEDIA ---------------------------------------------------//

        [HttpPost("dashboard")]
        public async Task<ActionResult> PostDashboard([FromBody] DashboardDTO Dash)
        {
            var dataDash = this._mapper.Map<Dashboard>(Dash);
            this._context.Dashboards.Add(dataDash);
            await this._context.SaveChangesAsync();
            return Ok(
                new
                {
                    success = true,
                    message = "Imagen agregada con exito",
                    result = ""
                });
        }

        [HttpPost("dashboard/img")]
        public async Task<ActionResult> PostImgDashboard([FromForm] IFormFile picture)
        {
            var upload = new Upload();

            try
            {
                var succesPicture = upload.UploadPictureUsers(picture, picture.FileName, "Multimedia/PictureDashboard/");
                if (succesPicture.ToString() == "")
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Error de carga de imagen",
                        result = succesPicture.ToString()
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Imagen carga",
                    result = succesPicture.ToString(),
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al cargar imagen",
                    result = ex.Message
                });
            }
        }

        [HttpPost("conventions")]
            public async Task<ActionResult> PostConventions([FromBody] ConventionsDTO conv)
        {
            try
            {
                var dataConv = this._mapper.Map<Convention>(conv);

                this._context.Conventions.Add(dataConv);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al cargar convenio",
                    result = ex.Message
                });
            }


            return Ok(new
            {
                success = true,
                message = "convenio agregado",
                result = ""
            });
        }
        [HttpPost("conventions/img")]
        public async Task<ActionResult> PostImgConventions([FromForm] IFormFile picture)
        {
            var upload = new Upload();
            try
            {
                var succesPicture = upload.UploadPictureUsers(picture, picture.FileName, "Multimedia/PictureConventions/");
                if (succesPicture.ToString() == "")
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Error de carga de imagen",
                        result = ""
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Imagen carga",
                    result = succesPicture.ToString(),
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al cargar imagen",
                    result = ex.Message
                });
            }
        }

        [HttpPost("convocatorias")]
        public async Task<ActionResult> PostConvocatorias([FromBody] CallsDTO conv)
        {
            try
            {
                var dataConv = this._mapper.Map<Call>(conv);

                this._context.Calls.Add(dataConv);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al cargar convenio",
                    result = ex.Message
                });
            }


            return Ok(new
            {
                success = true,
                message = "convocatoria agregado",
                result = ""
            });
        }

        [HttpPost("convocatorias/picture")]
        public async Task<ActionResult> PostConvocatoriasPdf([FromForm] IFormFile pdf)
        {
            var upload = new Upload();
            try
            {
                var succesPicture = upload.UploadPictureUsers(pdf, pdf.FileName, "Multimedia/PdfCalls/");
                if (succesPicture.ToString() == "")
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Error de carga de imagen",
                        result = ""
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Imagen carga",
                    result = succesPicture,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al cargar imagen",
                    result = ex.Message
                });
            }
        }

        //---------------------------------------------- DELETE MEDIA --------------------------------------------------//

        [HttpDelete("dashboard/{id:int}")]
        public async Task<ActionResult> DeleteDashboard(int id)
        {
            try
            {
                var Dash = await this._context.Dashboards.FirstOrDefaultAsync(x => x.IdDashboard == id);
                string ruta = "C:/Users/kevin/source/repos/SNTSS/SNTSS_API/SNTSS_API/Multimedia/PictureUser/" + Dash!.NameDashboard;
                System.IO.File.Delete(ruta);

                this._context.Remove(Dash);
                await this._context.SaveChangesAsync();

                return Ok(
                        new
                        {
                            success = true,
                            message = "Imagen Eliminada",
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
                            message = "error al eliminar imagen",
                            result = e.Message.ToString()
                        }
                    );
            }
        }

        [HttpDelete("conventions/{id:int}")]
        public async Task<ActionResult> DeleteConventions(int id)
        {
            try
            {
                var conv = await this._context.Conventions.FirstOrDefaultAsync(x => x.IdConventions == id);
                this._context.Remove(conv);
                await this._context.SaveChangesAsync();
                string ruta = "C:/Users/kevin/source/repos/SNTSS/SNTSS_API/SNTSS_API/Multimedia/PictureConventions/" + conv!.PictureConventions;
                System.IO.File.Delete(ruta);
    
                return Ok(
                        new
                        {
                            success = true,
                            message = "Convenio Eliminado",
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
                            message = "error al eliminar convenio",
                            result = e.Message.ToString()
                        }
                    );
            }
        }
        [HttpDelete("convocatorias/{id:int}")]
        public async Task<ActionResult> DeleteConvocatoria(int id)
        {
            var conv = await this._context.Calls.FirstOrDefaultAsync(x => x.IdCalls == id);
            string ruta = "C:/Users/kevin/source/repos/SNTSS/SNTSS_API/SNTSS_API/Multimedia/PdfCalls/" + conv.PdfCalls;
            System.IO.File.Delete(ruta);
            this._context.Remove(conv);
            await this._context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Convocatoria Eliminada con exito",
                result = ""
            });
        }
    }
}