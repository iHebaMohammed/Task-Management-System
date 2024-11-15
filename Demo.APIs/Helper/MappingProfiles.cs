using AutoMapper;
using Demo.APIs.DTOs;
using Demo.DAL.Entities;

namespace Demo.APIs.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TaskEntity, TaskDto>().ReverseMap();
        }
    }
}
