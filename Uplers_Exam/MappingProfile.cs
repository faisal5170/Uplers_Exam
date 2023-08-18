using AutoMapper;
using Uplers_Exam.Models;
using Uplers_Exam.ViewModels;

namespace Uplers_Exam
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoVM, ToDoModel>().ReverseMap();
        }
    }
}
