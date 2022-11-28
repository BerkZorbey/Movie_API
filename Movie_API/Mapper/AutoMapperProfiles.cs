using AutoMapper;
using Movie_API.Models;
using Movie_API.Models.DTOs;
using Movie_API.Models.Value_Object;

namespace Movie_API.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MovieDetailDTO, Movie>();
            CreateMap<MovieDurationDTO, Movie>();
            CreateMap<RegisterDTO, User>();
            CreateMap<LoginDTO, User>();
        }
    }
}
