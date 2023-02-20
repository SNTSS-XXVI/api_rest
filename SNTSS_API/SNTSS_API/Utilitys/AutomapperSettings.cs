using AutoMapper;
using SNTSS_API.DTO;
using SNTSS_API.Models;

namespace SNTSS_API.Utilitys
{
    public class AutomapperSettings: Profile
    {
        public AutomapperSettings()
        {
            CreateMap<RolDTO, Rol>().ReverseMap();
            CreateMap<UsersDTO, User>().ReverseMap();
            CreateMap<EscalafonDTO, Escalafon>().ReverseMap();
            //CreateMap<PostEscalafon, Escalafon>().ReverseMap();
            CreateMap<DashboardDTO, Dashboard>().ReverseMap();
            CreateMap<CallsDTO, Call>().ReverseMap();
            CreateMap<ConventionsDTO, Convention>().ReverseMap();
            CreateMap<LogsDTO, Log>().ReverseMap();
        }
    }
}
