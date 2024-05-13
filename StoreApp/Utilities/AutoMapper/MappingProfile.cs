using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;

namespace StoreApp.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
