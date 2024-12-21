using AutoMapper;
using WebApiTask.Models;
using WebApiTask.Models.Dto;

namespace WebApiTask.Maping
{
    public class AutoMapperProfieles : Profile
    {

        public AutoMapperProfieles()
        {
            CreateMap<TaskUser, TaskUserDto>().ReverseMap();
            CreateMap<AddTaskUserRequestDto, TaskUser>().ReverseMap();
            CreateMap<UpdataTaskRequestDto, TaskUser>().ReverseMap();
        }
    }
}
