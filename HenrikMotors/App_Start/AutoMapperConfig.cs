using AutoMapper;
using HenrikMotors.Models;

namespace HenrikMotors.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Voertuig, VoertuigDTO>();
                cfg.CreateMap<VoertuigDTO, Voertuig>();
                cfg.CreateMap<Voertuig, VoertuigDetailDTO>();
                cfg.CreateMap<VoertuigDetailDTO, Voertuig>();
                cfg.CreateMap<Merk, MerkDTO>().ReverseMap();
                cfg.CreateMap<Carrosserietype, CarrosserietypeDTO>();
                cfg.CreateMap<CarrosserietypeDTO, Carrosserietype>();
                cfg.CreateMap<Uitrusting, UitrustingDTO>().ReverseMap();
                cfg.CreateMap<VoertuigUitrustingDTO, VoertuigUitrusting>().ReverseMap();
            });
        }
    }
}